namespace WarehouseDashboard.Components.Pages.Unit6
{
   public sealed class DashboardSummary
    {
        public int TotalRecords { get; set; }

        public int TotalIn { get; set; }

        public int TotalOut { get; set; }

        public int TotalReturns { get; set; }

        public int TotalQuantity { get; set; }

        public int TotalBoxes { get; set; }
        public int TotalTrip { get; internal set; }
    }
}
