using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using SystemRestauracji.Helpers;
using SystemRestauracji.Models.BusinessLogic;
using SystemRestauracji.Models.BusinessLogic.Calculations;
using SystemRestauracji.Models.Correspondences;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class AddPaymentViewModel : ItemViewModel<Payments>
    {
        // What with total amout gross ?
        private List<Users> users;
        private List<Devices> devices;
        //private PaymentType paymentType;
        //private PaymentType paymentType;
        #region prop
        public string BLIK { get; set; }
        public string Cash { get; set; }
        public string Card { get; set; }

        public enum EnumPaymentMethod
        {
            CREDITCARD = 1,
            PAYPAL = 2,
            BANKDEPOSIT = 3,
            CASHONDELIVERY = 4,
            CHEQUE = 5,
            PICKUP = 6,
            PHONE = 7
        }

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

        public DateTime? PaymentDate
        {
            get => Item.PaymentDate;
            set
            {
                if (value != Item.PaymentDate)
                {
                    Item.PaymentDate = value;
                    this.OnPropertyChanged(() => PaymentDate);
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

        public DateTime? LastModified
        {
            get => Item.LastModified;
            set
            {
                if (value != Item.LastModified)
                {
                    Item.LastModified = value;
                    this.OnPropertyChanged(() => LastModified);
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

        public List<KeyValuePair<string, int>> GetEnumList<T>()
        {
            var list = new List<KeyValuePair<string, int>>();
            foreach (var e in Enum.GetValues(typeof(T)))
            {
                list.Add(new KeyValuePair<string, int>(e.ToString(), (int)e));
            }
            return list;
        }

        //public string PaymentType
        //{
        //    get => Item.PaymentType;
        //    set
        //    {
        //        if (value != Item.PaymentType)
        //        {
        //            Item.PaymentType = value;
        //            this.OnPropertyChanged(() => PaymentType);
        //        }
        //    }
        //}

        public string PhotoUrl
        {
            get => Item.PhotoUrl;
            set
            {
                if (value != Item.PhotoUrl)
                {
                    Item.PhotoUrl = value;
                    this.OnPropertyChanged(() => PhotoUrl);
                }
            }
        }

        //public List<KeyAndValue> GetPaymenttype()
        //{
        //    var list = new List<KeyAndValue>();
        //    foreach (var e in Object.GetValues(typeof(T)))
        //    {
        //        list.Add(new KeyValuePair<string, int>(e.ToString(), (int)e));
        //    }
        //    return list;
        //}
        //public IEnumerable<PaymentType> PaymentTypes
        //{
        //    get => GetType.;
        //    set
        //    {
        //        if (value != PaymentType)
        //        {
        //            PaymentType = value;
        //            this.OnPropertyChanged(() => PaymentType);
        //        }
        //    }
        //}


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

        //public IEnumerable<List<PaymentType>> PaymentTypes
        //{
        //    get
        //    {
        //        return GetPaymentTypes();
        //    }
        //}
        //public IQueryable<KeyAndValue> PaymentType
        //{
        //    get
        //    {
        //        paymentType.Card = V
        //    }
        //}
        //public IQueryable<KeyAndValue> PaymentType
        //{

        //    get
        //    {
        //        return GetPaymentType();
        //    }
        //}

        //public IQueryable<PaymentType> PaymentTypes
        //{
        //    //var card = paymentType.Card;
        //    //var cash = paymentType.Cash;
        //    //var blik = paymentType.BLIK;
        //    get
        //    {
        //        return GetPaymentTypes();
        //    }

        //}

        public List<string> rodzaje()
        {
            return new List<string> { "Nazwa", "Kod", "Nip" };
        }
        #endregion

        private BaseCommand saveAndOpenPaymentsCommand;
        public ICommand SaveAndOpenPaymentsCommand
        {
            get
            {
                if (saveAndOpenPaymentsCommand is null)
                {
                    saveAndOpenPaymentsCommand = new BaseCommand(() => SaveAndOpenPayments());
                    this.OnPropertyChanged(() => Image);
                }
                return saveAndOpenPaymentsCommand;
            }
        }

        public AddPaymentViewModel() : base("Dodaj Płatność")
        {
            base.Item = new Payments();
            Item.EmployeeId = Database.Users.Where(x => x.Role == (int)Role.Employee).Select(x => x.Id).First();
            List<KeyValuePair<string, int>> list = GetEnumList<EnumPaymentMethod>();

        }

        public override void Save()
        {
            TotalAmountGross = TotalAmountGross;
            Item.Description = Description;
            Item.PhotoUrl = ImageUrl ?? ImageUrl;
            Item.IsActive = true;
            Item.LastModified = DateTime.Now;
            Database.Payments.AddObject(Item);
            Database.SaveChanges();
        }
        // What in situation, if the user is not Employee ? 

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

        //private IEnumerable<List<PaymentType>> GetPaymentTypes()
        //{
        //    paymentType = new PaymentType();
        //    paymentType.Card = "cipa";
        //    paymentType.Cash = "ciurek";
        //    paymentType.BLIK = "nie ma juz sil";
        //    paymentType.AddRange();
        //    return GetPaymentTypes();
            

        //}



        //private IQueryable<KeyAndValue> GetPaymentType()
        //{
        //    this.paymentType = PaymentType.AsQueryable().ToList();
        //    return PaymentType.ToList().AsQueryable();
        //}

        //private void GetPaymentTypes()
        //{
        //    var card = paymentType.Card;
        //    var cash = paymentType.Cash;
        //    var blik = paymentType.BLIK;
        //}

        private void SaveAndOpenPayments()
        {
            Save();
            Messenger.Default.Send("Payments");
            base.OnRequestClose();
        }
    }
}
