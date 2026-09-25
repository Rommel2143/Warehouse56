using MySqlConnector;

namespace WarehouseDashboard.Components.Pages.Unit6
{
    public partial class Unit6Dashboard : IAsyncDisposable
    {
        private readonly IConfiguration _configuration;

        private const int RefreshIntervalSeconds = 30;
        public int Stock { get; set; }
        private bool IsLoading;
        private string? ErrorMessage;
        private List<PartStock> PartStocks { get; set; } = [];
        private int SecondsUntilRefresh = RefreshIntervalSeconds;

        private PeriodicTimer? _refreshTimer;
        private CancellationTokenSource? _refreshCts;

        private DashboardSummary Summary { get; set; } = new();

        private List<Unit6Item> RecentItems { get; set; } = [];

        public Unit6Dashboard(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadDashboardAsync();

            _refreshCts = new CancellationTokenSource();

            _ = StartAutoRefreshAsync(_refreshCts.Token);
        }

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

                    if (SecondsUntilRefresh <= 0)
                    {
                        await InvokeAsync(async () =>
                        {
                            await LoadDashboardAsync();

                            SecondsUntilRefresh =
                                RefreshIntervalSeconds;

                            StateHasChanged();
                        });
                    }
                    else
                    {
                        await InvokeAsync(StateHasChanged);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Expected when the component is disposed.
            }
        }

        private async Task LoadDashboardAsync()
        {
            if (IsLoading)
                return;

            IsLoading = true;
            ErrorMessage = null;

            try
            {
                string? connectionString =
                    _configuration.GetConnectionString(
                        "DefaultConnection");

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
                    $"Unable to load Unit 6 dashboard: {ex.Message}";
            }
            finally
            {
                IsLoading = false;

                SecondsUntilRefresh =
                    RefreshIntervalSeconds;

                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task LoadSummaryAsync(
            MySqlConnection connection)
        {
            const string sql = """
                SELECT
                    COUNT(*) AS TotalRecords,

                    COALESCE(SUM(
                        CASE
                            WHEN status = 1 AND DATE(datein) = CURDATE() THEN 1
                            ELSE 0
                        END
                    ), 0) AS TotalIn,

                    COALESCE(SUM(
                        CASE
                            WHEN status = 0 AND DATE(dateout) = CURDATE() THEN 1
                            ELSE 0
                        END
                    ), 0) AS TotalOut,

                    COALESCE(SUM(
                        CASE
                            WHEN isreturn = 1 THEN 1
                            ELSE 0
                        END
                    ), 0) AS TotalReturns,
                COUNT(DISTINCT
                    CASE
                        WHEN status = 0
                             AND DATE(dateout) = CURDATE()
                             AND NULLIF(batchout, '') IS NOT NULL
                        THEN batchout
                    END
                ) AS TotalTrip,
                    COALESCE(SUM(  CASE
                               WHEN status = 1 THEN qty
                               ELSE 0
                           END), 0) AS TotalQuantity,

                 COALESCE(SUM(
                           CASE
                               WHEN status = 1 THEN 1
                               ELSE 0
                           END
                       ), 0) AS TotalBoxes

                FROM logistics_unit56;
                """;

            await using var command =
                new MySqlCommand(sql, connection);

            await using var reader =
                await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                Summary = new DashboardSummary
                {
                    TotalRecords = reader.GetInt32(
                        reader.GetOrdinal("TotalRecords")),

                    TotalIn = reader.GetInt32(
                        reader.GetOrdinal("TotalIn")),

                    TotalOut = reader.GetInt32(
                        reader.GetOrdinal("TotalOut")),

                    TotalReturns = reader.GetInt32(
                        reader.GetOrdinal("TotalReturns")),

                    TotalQuantity = reader.GetInt32(
                        reader.GetOrdinal("TotalQuantity")),
                    TotalBoxes = reader.GetInt32(
                        reader.GetOrdinal("TotalBoxes")),
                    TotalTrip = reader.GetInt32(
                        reader.GetOrdinal("TotalTrip"))

                };
            }
        }

        private async Task LoadRecentItemsAsync(
            MySqlConnection connection)
        {
            const string sql = """
                SELECT
                    u.id,
                    u.status,
                    u.isreturn,
                    u.qrcode,
                    u.partcode,
                    m.partname,
                    u.lotnumber,
                    u.prod_date,
                    u.supplier,
                    u.qty,
                    u.boxno,
                    u.userin,
                    u.timeIN,
                    u.datein,
                    u.batchin,
                    u.userout,
                    u.dateout,
                    u.batchout,
                    u.timeOUT

                FROM logistics_unit56 u

                LEFT JOIN logistics_masterlist m
                    ON m.partcode = u.partcode

                ORDER BY UpdatedAt DESC

                LIMIT 20;
                """;

            await using var command =
                new MySqlCommand(sql, connection);

            await using var reader =
                await command.ExecuteReaderAsync();

            RecentItems = [];

            while (await reader.ReadAsync())
            {
                RecentItems.Add(new Unit6Item
                {
                    Id = reader.GetInt32(
                        reader.GetOrdinal("id")),

                    Status = GetBoolean(reader, "status")
                        ? "IN"
                        : "OUT",

                    IsReturn = GetBoolean(
                        reader,
                        "isreturn"),

                    QrCode = GetString(
                        reader,
                        "qrcode"),

                    PartCode = GetString(
                        reader,
                        "partcode"),

                    PartName = GetString(
                        reader,
                        "partname"),

                    LotNumber = GetString(
                        reader,
                        "lotnumber"),

                    ProductionDate =
                        GetNullableDateTime(
                            reader,
                            "prod_date"),

                    Supplier = GetString(
                        reader,
                        "supplier"),

                    Quantity = GetDecimal(
                        reader,
                        "qty"),

                    BoxNo = GetString(
                        reader,
                        "boxno"),

                    UserIn = GetString(
                        reader,
                        "userin"),

                    DateIn = GetNullableDateTime(
                        reader,
                        "datein"),

                    TimeIn = GetTimeString(
                        reader,
                        "timeIN"),

                    UserOut = GetString(
                        reader,
                        "userout"),

                    DateOut = GetNullableDateTime(
                        reader,
                        "dateout"),

                    TimeOut = GetTimeString(
                        reader,
                        "timeOUT"),
                    BatchIN=GetString(
                        reader,
                        "batchin"),
                    BatchOUT= GetString(
                        reader,
                        "batchout")

                });
            }
        }
        private async Task LoadPartStocksAsync(
    MySqlConnection connection)
        {
            const string sql = """
        SELECT
            u.partcode,
            m.partname,
            SUM(qty) AS stock,
            COUNT(*) AS Boxes,
            SUM(
                CASE
                    WHEN status = 1 AND DATE(datein) = CURDATE() THEN qty
                    ELSE 0
                END
            ) AS stock_in,
            SUM(
                CASE
                    WHEN status = 0 AND DATE(dateout) = CURDATE() THEN qty
                    ELSE 0
                END
            ) AS stock_out

        FROM logistics_unit56 u

        LEFT JOIN logistics_masterlist m
            ON m.partcode = u.partcode

        WHERE u.status = 1

        GROUP BY
            u.partcode,
            m.partname

        ORDER BY UpdatedAt DESC;
        """;

            await using var command =
                new MySqlCommand(sql, connection);

            await using var reader =
                await command.ExecuteReaderAsync();

            PartStocks = [];

            while (await reader.ReadAsync())
            {
                PartStocks.Add(new PartStock
                {
                    PartCode = GetString(
                        reader,
                        "partcode"),

                    PartName = GetString(
                        reader,
                        "partname"),

                    Stock = reader.GetInt32(
                        reader.GetOrdinal("stock")),
                    Boxes = reader.GetInt32(
                        reader.GetOrdinal("Boxes")),
                    StockIn = reader.GetInt32(
                        reader.GetOrdinal("stock_in")),
                    StockOut = reader.GetInt32(
                        reader.GetOrdinal("stock_out"))
                });
            }
        }
        private static string? GetTimeString(
            MySqlDataReader reader,
            string column)
        {
            int ordinal = reader.GetOrdinal(column);

            if (reader.IsDBNull(ordinal))
                return null;

            object value = reader.GetValue(ordinal);

            return value switch
            {
                TimeSpan time =>
                    time.ToString(@"hh\:mm\:ss"),

                DateTime dateTime =>
                    dateTime.ToString("HH:mm:ss"),

                _ =>
                    Convert.ToString(value)
            };
        }

        private static string? GetString(
            MySqlDataReader reader,
            string column)
        {
            int ordinal = reader.GetOrdinal(column);

            if (reader.IsDBNull(ordinal))
                return null;

            return Convert.ToString(
                reader.GetValue(ordinal));
        }

        private static bool GetBoolean(
            MySqlDataReader reader,
            string column)
        {
            int ordinal = reader.GetOrdinal(column);

            if (reader.IsDBNull(ordinal))
                return false;

            return Convert.ToBoolean(
                reader.GetValue(ordinal));
        }

        private static decimal GetDecimal(
            MySqlDataReader reader,
            string column)
        {
            int ordinal = reader.GetOrdinal(column);

            if (reader.IsDBNull(ordinal))
                return 0;

            return Convert.ToDecimal(
                reader.GetValue(ordinal));
        }

        private static DateTime? GetNullableDateTime(
            MySqlDataReader reader,
            string column)
        {
            int ordinal = reader.GetOrdinal(column);

            if (reader.IsDBNull(ordinal))
                return null;

            return Convert.ToDateTime(
                reader.GetValue(ordinal));
        }

        public async ValueTask DisposeAsync()
        {
            if (_refreshCts is not null)
            {
                await _refreshCts.CancelAsync();

                _refreshCts.Dispose();
            }

            _refreshTimer?.Dispose();
        }

        private string StockSearchText { get; set; } = string.Empty;

        private IEnumerable<PartStock> FilteredPartStocks =>
            string.IsNullOrWhiteSpace(StockSearchText)
                ? PartStocks
                : PartStocks.Where(x =>
                    (x.PartCode?.Contains(
                        StockSearchText,
                        StringComparison.OrdinalIgnoreCase) ?? false)
                    ||
                    (x.PartName?.Contains(
                        StockSearchText,
                        StringComparison.OrdinalIgnoreCase) ?? false));

        private void ClearStockSearch()
        {
            StockSearchText = string.Empty;
        }
    }
}