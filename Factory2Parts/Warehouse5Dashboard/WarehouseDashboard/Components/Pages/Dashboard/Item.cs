namespace WarehouseDashboard.Components.Pages.Dashboard
{
    public sealed class Item
    {
        public int Id { get; set; }

        public string? Status { get; set; }

        public string? QrCode { get; set; }

        public string? RfidBarcode { get; set; }

        public string? PartCode { get; set; }

        public string? PartName { get; set; }

        public string? LotNumber { get; set; }

        public string? BatchCode { get; set; }

        public string? DeliveryNote { get; set; }

        public string? SupplierCode { get; set; }

        public decimal Quantity { get; set; }

        public string? Remarks { get; set; }

        public string? WarehouseId { get; set; }

        public string? Rssi { get; set; }

        public string? Antenna { get; set; }

        public DateTime? DateIn { get; set; }

        public string? TimeIn { get; set; }

        public DateTime? DateOut { get; set; }

        public string? TimeOut { get; set; }
    }
}
