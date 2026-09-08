namespace WarehouseDashboard.Components.Pages.Unit6
{
    public sealed class PartStock
    {
        public string? PartCode { get; set; }

        public string? PartName { get; set; }

        public int Stock { get; set; }
        public int Boxes { get; set; }
        public int StockIn { get; internal set; }
        public int StockOut { get; internal set; }
    }
}
