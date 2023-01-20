using System;

namespace SystemRestauracji.Models.EntitiesForView
{
    public class DocumentForAllView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InvoiveName { get; set; }
        public string AuthorFullName { get; set; }
        public string AuthorNIP { get; set; }
        public string RecipientFullName { get; set; }
        public string RecipientNIP { get; set; }
        public DateTime? PaymentDateLimit { get; set; }
        public decimal? TotalAmountGross { get; set; }
        public string DocumentStatus { get; set; }
        public string EmployeeFullName { get; set; }
        public DateTime? DocumentDate { get; set; }
    }
}
