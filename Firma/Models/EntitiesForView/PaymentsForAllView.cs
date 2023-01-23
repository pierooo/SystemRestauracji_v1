using System;

namespace SystemRestauracji.Models.EntitiesForView
{
    public class PaymentsForAllView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmployeeFullName { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentDateLimit { get; set; }
        public decimal? TotalAmountGross { get; set; }
        public string DeviceName { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentType { get; set; }
        public DateTime? LastModified{ get; set; }
    }
}

