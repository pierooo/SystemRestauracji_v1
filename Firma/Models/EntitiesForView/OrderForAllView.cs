using System;

namespace SystemRestauracji.Models.EntitiesForView
{
    public class OrderForAllView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmployeeFullName  { get; set; }
        public string RestaurantTableName { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalPriceGross { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentName { get; set; }
        public string DocumentName { get; set; }
    }
}
