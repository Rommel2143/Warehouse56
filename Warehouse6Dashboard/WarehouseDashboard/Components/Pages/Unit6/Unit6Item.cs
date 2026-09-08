namespace WarehouseDashboard.Components.Pages.Unit6
{
    public sealed class Unit6Item
    {
        public int Id { get; set; }

        public string? Status { get; set; }

        public bool IsReturn { get; set; }

        public string? QrCode { get; set; }

        public string? PartCode { get; set; }

        public string? PartName { get; set; }

        public string? LotNumber { get; set; }

        public DateTime? ProductionDate { get; set; }

        public string? Supplier { get; set; }

        public decimal Quantity { get; set; }

        public string? BoxNo { get; set; }

        public string? UserIn { get; set; }
        public string BatchIN { get; set; } = string.Empty;
        public string BatchOUT { get; set; } = string.Empty;

        public string? TimeIn { get; set; }

        public DateTime? DateIn { get; set; }

        public string? UserOut { get; set; }

        public DateTime? DateOut { get; set; }

        public string? TimeOut { get; set; }
    }
}
