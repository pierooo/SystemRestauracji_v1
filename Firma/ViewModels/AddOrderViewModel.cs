using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using SystemRestauracji.Helpers;
using SystemRestauracji.Models.BusinessLogic;
using SystemRestauracji.Models.BusinessLogic.Calculations;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class AddOrderViewModel : ItemViewModel<Orders>
    {
        private List<Users> users;
        private List<RestaurantTables> restaurantTables;

        #region prop
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

        public int EmployeeId
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

        public IQueryable<KeyAndValue> Users
        {
            get
            {
                return GetUsers();
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

        private BaseCommand saveAndOpenOrdersCommand;
        public ICommand SaveAndOpenOrdersCommand
        {
            get
            {
                if (saveAndOpenOrdersCommand is null)
                {
                    saveAndOpenOrdersCommand = new BaseCommand(() => SaveAndOpenOrders());
                    this.OnPropertyChanged(() => Image);
                }
                return saveAndOpenOrdersCommand;
            }
        }

        public AddOrderViewModel() : base("Nowe zamówienie")
        {
            base.Item = new Orders();
        }

        public override void Save()
        {
            Item.TotalPriceGross = 0;
            Item.TotalPriceNet = 0;
            Item.OrderStatus = StatusMapper.MapToDbStatus(Status.Added);
            Item.IsActive = true;
            Item.LastModified = DateTime.Now;
            Item.OrderDate = DateTime.Now;
            Database.Orders.AddObject(Item);
            Database.SaveChanges();
        }

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

        private void SaveAndOpenOrders()
        {
            Save();
            Messenger.Default.Send("Orders");
            base.OnRequestClose();
        }
    }
}
