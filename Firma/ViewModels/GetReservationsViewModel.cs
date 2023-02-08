using System.Collections.Generic;
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
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "Nazwa" };
        }
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "Nazwa" };
        }

        public override void Sort()
        {
            if (SortField == "Nazwa")
                List = new ObservableCollection<ReservationForAllView>(List.OrderBy(item => item.Name));
        }

        public override void Find()
        {
            Load();
            if (FindField == "Nazwa")
                List = new ObservableCollection<ReservationForAllView>(List.Where(item => item.Name != null && item.Name.StartsWith(FindText)));
        }
    }
}
