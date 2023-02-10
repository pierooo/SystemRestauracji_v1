namespace SystemRestauracji.Models.EntitiesForView
{
    public class DocumentPositionForDocumentView
    {
        public string ProductName { get; set; }

        public decimal? UnitPriceNet { get; set; }

        public decimal? VAT { get; set; }

        public decimal? UnitPriceGross { get; set; }

        public int? Quantity { get; set; }
    }
}
