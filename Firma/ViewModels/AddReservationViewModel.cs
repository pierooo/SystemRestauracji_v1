using System;
using System.Collections.Generic;
using System.Linq;
using SystemRestauracji.Models.BusinessLogic;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class AddReservationViewModel : ItemViewModel<Reservations>
    {
        private List<Users> users;
        private List<RestaurantTables> restaurantTables;

        #region prop
        public string Error { get; set; }
        public string Name
        {
            get => Item.Name;
            set
            {
                if (value != Item.Name)
                {
                    Item.Name = value;
                    this.OnPropertyChanged(() => Name);
                }
            }
        }

        public string Description
        {
            get => Item.Description;
            set
            {
                if (value != Item.Description)
                {
                    Item.Description = value;
                    this.OnPropertyChanged(() => Description);
                }
            }
        }

        public int? NumberOfPeople
        {
            get => Item.NumberOfPeople;
            set
            {
                if (value != Item.NumberOfPeople)
                {
                    Item.NumberOfPeople = value;
                    this.OnPropertyChanged(() => NumberOfPeople);
                }
            }
        }

        public int? ReservationHour { get; set; }

        public int? ReservationMinute { get; set; }

        public DateTime? ReservationDate
        {
            get => Item.ReservationDate;
            set
            {
                if (value != Item.ReservationDate)
                {
                    Item.ReservationDate = value;
                    this.OnPropertyChanged(() => ReservationDate);
                }
            }
        }

        public decimal? OrderAmountLimit
        {
            get => Item.OrderAmountLimit;
            set
            {
                if (value != Item.OrderAmountLimit)
                {
                    Item.OrderAmountLimit = value;
                    this.OnPropertyChanged(() => OrderAmountLimit);
                }
            }
        }

        public IQueryable<KeyAndValue> Users
        {
            get
            {
                return GetUsers();
            }
        }

        public int? EmployeeId
        {
            get
            {
                return this.Item.EmployeeId;
            }
            set
            {
                if (value != Item.EmployeeId)
                {
                    Item.EmployeeId = value;
                    base.OnPropertyChanged(() => EmployeeId);
                }
            }
        }

        public int? RestaurantTableId
        {
            get
            {
                return this.Item.RestaurantTableId;
            }
            set
            {
                if (value != Item.RestaurantTableId)
                {
                    Item.RestaurantTableId = value;
                    base.OnPropertyChanged(() => RestaurantTableId);
                }
            }
        }

        public IQueryable<KeyAndValue> RestaurantTables
        {
            get
            {
                return GetRestaurantTables();
            }
        }

        #endregion
        public AddReservationViewModel() : base("Nowa rezerwacja")
        {
            base.Item = new Reservations();
        }

        public override void Save()
        {
            if (ReservationHour != null && ReservationMinute != null)
            {

                Item.ReservationDate = new DateTime(ReservationDate.Value.Year, ReservationDate.Value.Month, ReservationDate.Value.Day, (int)ReservationHour, (int)ReservationMinute, 0);
            }

            Item.IsActive = true;
            Item.LastModified = DateTime.Now;
            Database.Reservations.AddObject(Item);
            Database.SaveChanges();
        }

        #region helpers
        private IQueryable<KeyAndValue> GetUsers()
        {
            this.users = Database.Users.Where(x => x.Role == (int)Role.Employee).ToList();
            return users.Select(x => new KeyAndValue { Key = x.Id, Value = x.FirstName + " " + x.LastName }).ToList().AsQueryable();
        }
        private IQueryable<KeyAndValue> GetRestaurantTables()
        {
            this.restaurantTables = Database.RestaurantTables.ToList();
            return restaurantTables.Select(x => new KeyAndValue { Key = x.Id, Value = x.Name }).ToList().AsQueryable();
        }
        #endregion
    }
}
