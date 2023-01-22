namespace SystemRestauracji.Models.EntitiesForView
{
    public class OrderDetailsForAllView
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPriceNetto { get; set; }
        public decimal? VAT { get; set; }
        public decimal? UnitPriceGross { get; set; }
    }
}
