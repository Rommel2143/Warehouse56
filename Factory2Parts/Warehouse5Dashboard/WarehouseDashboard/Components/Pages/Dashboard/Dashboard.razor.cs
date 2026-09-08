using MySqlConnector;

namespace WarehouseDashboard.Components.Pages.Dashboard;

public partial class Dashboard : IAsyncDisposable
{
    private const int RefreshIntervalSeconds = 30;
    private const int RecentItemLimit = 20;

    private readonly IConfiguration _configuration;

    private PeriodicTimer? _refreshTimer;
    private CancellationTokenSource? _refreshCts;

    private bool IsLoading;
    private string? ErrorMessage;

    private int SecondsUntilRefresh = RefreshIntervalSeconds;

    private DashboardSummary Summary { get; set; } = new();

    private List<PartStock> PartStocks { get; set; } = [];
    private List<Item> RecentItems { get; set; } = [];

    private string StockSearchText { get; set; } = string.Empty;

    private IEnumerable<PartStock> FilteredPartStocks =>
        string.IsNullOrWhiteSpace(StockSearchText)
            ? PartStocks
            : PartStocks.Where(x =>
                ContainsIgnoreCase(x.PartCode, StockSearchText) ||
                ContainsIgnoreCase(x.PartName, StockSearchText));


    // =========================================================
    // CONSTRUCTOR
    // =========================================================

    public Dashboard(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    // =========================================================
    // INITIALIZATION
    // =========================================================

    protected override async Task OnInitializedAsync()
    {
        await LoadDashboardAsync();

        _refreshCts = new CancellationTokenSource();

        _ = StartAutoRefreshAsync(_refreshCts.Token);
    }


    // =========================================================
    // AUTO REFRESH
    // =========================================================

    private async Task StartAutoRefreshAsync(
        CancellationToken cancellationToken)
    {
        _refreshTimer = new PeriodicTimer(
            TimeSpan.FromSeconds(1));

        try
        {
            while (await _refreshTimer.WaitForNextTickAsync(
                cancellationToken))
            {
                if (IsLoading)
                    continue;

                SecondsUntilRefresh--;

                if (SecondsUntilRefresh > 0)
                {
                    await InvokeAsync(StateHasChanged);
                    continue;
                }

                await InvokeAsync(LoadDashboardAsync);
            }
        }
        catch (OperationCanceledException)
        {
            // Expected when component is disposed.
        }
    }


    // =========================================================
    // LOAD DASHBOARD
    // =========================================================

    private async Task LoadDashboardAsync()
    {
        if (IsLoading)
            return;

        IsLoading = true;
        ErrorMessage = null;

        try
        {
            var connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "DefaultConnection is not configured.");
            }

            await using var connection =
                new MySqlConnection(connectionString);

            await connection.OpenAsync();

            await LoadSummaryAsync(connection);
            await LoadPartStocksAsync(connection);
            await LoadRecentItemsAsync(connection);
        }
        catch (Exception ex)
        {
            ErrorMessage =
                $"Unable to load dashboard: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
            SecondsUntilRefresh = RefreshIntervalSeconds;

            await InvokeAsync(StateHasChanged);
        }
    }


    // =========================================================
    // SUMMARY
    // =========================================================

    private async Task LoadSummaryAsync(
        MySqlConnection connection)
    {
        const string sql = """
            SELECT
                COUNT(*) AS TotalRecords,

                COALESCE(
                    SUM(
                        CASE
                            WHEN  DATE(datestamp) = CURDATE()
                            THEN 1
                            ELSE 0
                        END
                    ),
                    0
                ) AS TotalIn,

                COALESCE(
                    SUM(
                        CASE
                            WHEN status = 'WIP'
                                 AND updatestamp IS NOT NULL
                                 AND DATE(updatestamp) = CURDATE()
                            THEN 1
                            ELSE 0
                        END
                    ),
                    0
                ) AS TotalOut,

                COUNT(
                    DISTINCT
                    CASE
                        WHEN status NOT IN ('IN', 'WIP', 'PAIR')
                             AND updatestamp IS NOT NULL
                             AND DATE(updatestamp) = CURDATE()
                             AND NULLIF(dn, '') IS NOT NULL
                        THEN dn
                    END
                ) AS TotalTrip,

                COALESCE(
                    SUM(
                        CASE
                            WHEN status IN ('IN', 'WIP')
                            THEN qty
                            ELSE 0
                        END
                    ),
                    0
                ) AS TotalQuantity,

                COALESCE(
                    SUM(
                        CASE
                            WHEN status IN ('IN', 'WIP')
                            THEN 1
                            ELSE 0
                        END
                    ),
                    0
                ) AS TotalBoxes

            FROM parts_scan;
            """;

        await using var command =
            new MySqlCommand(sql, connection);

        await using var reader =
            await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
            return;

        Summary = new DashboardSummary
        {
            TotalRecords = GetInt32(reader, "TotalRecords"),
            TotalIn = GetInt32(reader, "TotalIn"),
            TotalOut = GetInt32(reader, "TotalOut"),
            TotalTrip = GetInt32(reader, "TotalTrip"),
            TotalQuantity = GetInt32(reader, "TotalQuantity"),
            TotalBoxes = GetInt32(reader, "TotalBoxes")
        };
    }


    // =========================================================
    // RECENT ITEMS
    // =========================================================

    private async Task LoadRecentItemsAsync(
        MySqlConnection connection)
    {
        const string sql = """
            SELECT
                p.id,
                p.status,
                p.`QRcode`,
                p.rfidbarcode,
                p.partcode,
                m.partname,
                p.lotnumber,
                p.batchcode,
                p.dn,
                p.suppliercode,
                p.qty,
                p.remarks,
                p.warehouseId,
                p.rssi,
                p.antenna,
                p.datestamp,
                p.updatestamp

            FROM parts_scan p

            LEFT JOIN masterlist.parts_masterlist m
                ON m.partcode = p.partcode

            ORDER BY
                COALESCE(p.updatestamp, p.datestamp) DESC,
                p.id DESC

            LIMIT 20;
            """;

        await using var command =
            new MySqlCommand(sql, connection);

        await using var reader =
            await command.ExecuteReaderAsync();

        var items = new List<Item>(RecentItemLimit);

        while (await reader.ReadAsync())
        {
            items.Add(new Item
            {
                Id = GetInt32(reader, "id"),

                Status =
                    GetString(reader, "status") ?? "UNKNOWN",

                QrCode =
                    GetString(reader, "QRcode"),

                RfidBarcode =
                    GetString(reader, "rfidbarcode"),

                PartCode =
                    GetString(reader, "partcode"),

                PartName =
                    GetString(reader, "partname"),

                LotNumber =
                    GetString(reader, "lotnumber"),

                BatchCode =
                    GetString(reader, "batchcode"),

                DeliveryNote =
                    GetString(reader, "dn"),

                SupplierCode =
                    GetString(reader, "suppliercode"),

                Quantity =
                    GetDecimal(reader, "qty"),

                Remarks =
                    GetString(reader, "remarks"),

                WarehouseId =
                    GetString(reader, "warehouseId"),

                Rssi =
                    GetString(reader, "rssi"),

                Antenna =
                    GetString(reader, "antenna"),

                DateIn =
                    GetNullableDateTime(reader, "datestamp"),

                TimeIn =
                    GetTimeString(reader, "datestamp"),

                DateOut =
                    GetNullableDateTime(reader, "updatestamp"),

                TimeOut =
                    GetTimeString(reader, "updatestamp")
            });
        }

        RecentItems = items;
    }


    // =========================================================
    // PART STOCK
    // =========================================================

    private async Task LoadPartStocksAsync(
        MySqlConnection connection)
    {
        const string sql = """
            SELECT
                p.partcode,
                m.partname,

                COALESCE(
                    SUM(
                        CASE
                            WHEN p.status = 'IN'
                            THEN p.qty
                            ELSE 0
                        END
                    ),
                    0
                ) AS stock,

                COALESCE(
                    SUM(
                        CASE
                            WHEN p.status = 'IN'
                            THEN 1
                            ELSE 0
                        END
                    ),
                    0
                ) AS boxes,

                COALESCE(
                    SUM(
                        CASE
                            WHEN p.status = 'IN'
                                 AND DATE(p.datestamp) = CURDATE()
                            THEN p.qty
                            ELSE 0
                        END
                    ),
                    0
                ) AS stock_in,

                COALESCE(
                    SUM(
                        CASE
                            WHEN p.status = 'WIP'
                                 AND p.updatestamp IS NOT NULL
                                 AND DATE(p.updatestamp) = CURDATE()
                            THEN p.qty
                            ELSE 0
                        END
                    ),
                    0
                ) AS stock_out,

                COALESCE(MAX(m.TotalStock), 0) AS master_stock,

                COALESCE(MAX(m.ReorderStock), 0) AS reorder_stock,

                MAX(
                    COALESCE(p.updatestamp, p.datestamp)
                ) AS latest_activity

            FROM parts_scan p

            LEFT JOIN masterlist.parts_masterlist m
                ON m.partcode = p.partcode

            GROUP BY
                p.partcode,
                m.partname

            HAVING stock > 0

            ORDER BY
                latest_activity DESC,
                p.partcode ASC;
            """;

        await using var command =
            new MySqlCommand(sql, connection);

        await using var reader =
            await command.ExecuteReaderAsync();

        var stocks = new List<PartStock>();

        while (await reader.ReadAsync())
        {
            stocks.Add(new PartStock
            {
                PartCode =
                    GetString(reader, "partcode"),

                PartName =
                    GetString(reader, "partname"),

                Stock =
                    GetDecimal(reader, "stock"),

                Boxes =
                    GetInt32(reader, "boxes"),

                StockIn =
                    GetDecimal(reader, "stock_in"),

                StockOut =
                    GetDecimal(reader, "stock_out")
            });
        }

        PartStocks = stocks;
    }


    // =========================================================
    // HELPERS
    // =========================================================

    private static bool ContainsIgnoreCase(
        string? value,
        string search)
    {
        return value?.Contains(
            search,
            StringComparison.OrdinalIgnoreCase) == true;
    }


    private static string? GetString(
        MySqlDataReader reader,
        string column)
    {
        var ordinal = reader.GetOrdinal(column);

        return reader.IsDBNull(ordinal)
            ? null
            : Convert.ToString(reader.GetValue(ordinal));
    }


    private static int GetInt32(
        MySqlDataReader reader,
        string column)
    {
        var ordinal = reader.GetOrdinal(column);

        return reader.IsDBNull(ordinal)
            ? 0
            : Convert.ToInt32(reader.GetValue(ordinal));
    }


    private static decimal GetDecimal(
        MySqlDataReader reader,
        string column)
    {
        var ordinal = reader.GetOrdinal(column);

        return reader.IsDBNull(ordinal)
            ? 0m
            : Convert.ToDecimal(reader.GetValue(ordinal));
    }


    private static DateTime? GetNullableDateTime(
        MySqlDataReader reader,
        string column)
    {
        var ordinal = reader.GetOrdinal(column);

        return reader.IsDBNull(ordinal)
            ? null
            : Convert.ToDateTime(reader.GetValue(ordinal));
    }


    private static string? GetTimeString(
        MySqlDataReader reader,
        string column)
    {
        var ordinal = reader.GetOrdinal(column);

        if (reader.IsDBNull(ordinal))
            return null;

        return reader.GetValue(ordinal) switch
        {
            TimeSpan time =>
                time.ToString(@"hh\:mm\:ss"),

            DateTime dateTime =>
                dateTime.ToString("HH:mm:ss"),

            _ =>
                Convert.ToDateTime(
                    reader.GetValue(ordinal))
                    .ToString("HH:mm:ss")
        };
    }


    // =========================================================
    // SEARCH
    // =========================================================

    private void ClearStockSearch()
    {
        StockSearchText = string.Empty;
    }


    // =========================================================
    // DISPOSE
    // =========================================================

    public async ValueTask DisposeAsync()
    {
        if (_refreshCts is not null)
        {
            await _refreshCts.CancelAsync();
            _refreshCts.Dispose();
        }

        _refreshTimer?.Dispose();
    }
}