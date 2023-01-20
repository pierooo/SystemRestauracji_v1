using System.Collections.ObjectModel;
using System.Linq;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetReservationsViewModel : ViewModelBase<ReservationForAllView>
    {
        public GetReservationsViewModel() : base("Rezerwacje")
        {
        }

        public override void Load()
        {
            List = new ObservableCollection<ReservationForAllView>(restaurantEntities.Reservations
                .Select(x =>
                new ReservationForAllView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    EmployeeFullName = x.Users.FirstName + " " + x.Users.LastName,
                    ReservationDate = x.ReservationDate,
                    OrderAmountLimit = x.OrderAmountLimit,
                    RestaurantTableName = x.RestaurantTables.Name,
                    NumberOfPeople = x.NumberOfPeople,
                    ReservationStatus = x.ReservationStatus
                }));
        }
    }
}
