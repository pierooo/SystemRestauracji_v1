using System;

namespace SystemRestauracji.Models.EntitiesForView
{
    public class ReservationForAllView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmployeeFullName { get; set; }
        public DateTime? ReservationDate { get; set; }
        public decimal? OrderAmountLimit { get; set; }
        public string RestaurantTableName { get; set; }
        public int? NumberOfPeople { get; set; }
        public string ReservationStatus { get; set; }
    }
}
