using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using SystemRestauracji.Helpers;
using SystemRestauracji.Models.BusinessLogic;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class AddPaymentViewModel : ItemViewModel<Payments>
    {
        private List<Users> users;
        private List<Devices> devices;
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

        public int? DeviceId
        {
            get
            {
                return this.Item.DeviceId;
            }
            set
            {
                if (value != Item.DeviceId)
                {
                    Item.DeviceId = value;
                    base.OnPropertyChanged(() => DeviceId);
                }
            }
        }


        public DateTime? PaymentDateLimit
        {
            get => Item.PaymentDateLimit;
            set
            {
                if (value != Item.PaymentDateLimit)
                {
                    Item.PaymentDateLimit = value;
                    this.OnPropertyChanged(() => PaymentDateLimit);
                }
            }
        }

        public decimal? TotalAmountGross
        {
            get => Item.TotalAmountGross;
            set
            {
                if (value != Item.TotalAmountGross)
                {
                    Item.TotalAmountGross = value;
                    this.OnPropertyChanged(() => TotalAmountGross);
                }
            }
        }

        public string SelectedPaymentType
        {
            get => Item.PaymentType;
            set
            {
                if (value != Item.PaymentType)
                {
                    Item.PaymentType = value;
                    this.OnPropertyChanged(() => SelectedPaymentType);
                }
            }
        }

        public IQueryable<KeyAndValue> Users
        {
            get
            {
                return GetEmployees();
            }
        }

        public IQueryable<KeyAndValue> Devices
        {
            get
            {
                return GetDevices();
            }
        }

        public IQueryable<string> PaymentTypes
        {
            get
            {
                return GetPaymentTypes();
            }
        }
        #endregion

        public ICommand SaveAndOpenPaymentsCommand
        {
            get
            {
                return new BaseCommand(SaveAndOpenPayments);
            }
        }

        public AddPaymentViewModel() : base("Dodaj Płatność")
        {
            base.Item = new Payments();
            Item.EmployeeId = Database.Users.Where(x => x.Role == (int)Role.Employee).Select(x => x.Id).First();
        }

        public override void Save()
        {
            Item.IsActive = true;
            Item.PaymentStatus = StatusMapper.MapToDbStatus(Status.Fee);
            Item.LastModified = DateTime.Now;
            Item.PaymentDate = DateTime.Now;
            Database.Payments.AddObject(Item);
            Database.SaveChanges();
        }

        private IQueryable<KeyAndValue> GetEmployees()
        {
            this.users = Database.Users.ToList();
            return users.Select(x => new KeyAndValue { Key = x.Id, Value = x.FirstName }).ToList().AsQueryable();
        }

        private IQueryable<KeyAndValue> GetDevices()
        {
            this.devices = Database.Devices.ToList();
            return devices.Select(x => new KeyAndValue { Key = x.Id, Value = x.Name }).ToList().AsQueryable();
        }

        private IQueryable<string> GetPaymentTypes()
        {
            return PaymentType.GetPaymentTypes().AsQueryable();
        }

        private void SaveAndOpenPayments()
        {
            Save();
            Messenger.Default.Send("Payments");
            base.OnRequestClose();
        }
    }
}
