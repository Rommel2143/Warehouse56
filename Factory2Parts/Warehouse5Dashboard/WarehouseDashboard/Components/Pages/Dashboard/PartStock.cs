namespace WarehouseDashboard.Components.Pages.Dashboard
{
    public sealed class PartStock
    {
        public string? PartCode { get; set; }

        public string? PartName { get; set; }

        public decimal Stock { get; set; }

        public int Boxes { get; set; }

        public decimal StockIn { get; set; }

        public decimal StockOut { get; set; }
    }
}
