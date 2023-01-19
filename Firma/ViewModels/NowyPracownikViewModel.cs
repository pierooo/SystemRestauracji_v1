using System;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    internal class NowyPracownikViewModel : ItemViewModel<Users>
    {
        #region Konstruktor
        public NowyPracownikViewModel() : base("Nowa pracownik")
        {
            base.Item = new Users();
        }
        #endregion

        #region Properties
        public string LastName
        {
            get => Item.LastName;
            set
            {
                if (value != Item.LastName)
                {
                    Item.LastName = value;
                    this.OnPropertyChanged(() => LastName);
                }
            }
        }

        public string FirstName
        {
            get => Item.FirstName;
            set
            {
                if (value != Item.FirstName)
                {
                    Item.FirstName = value;
                    this.OnPropertyChanged(() => FirstName);
                }
            }
        }
        public int Role
        {
            get => Item.Role;
            set
            {
                if (value != Item.Role)
                {
                    Item.Role = value;
                    this.OnPropertyChanged(() => Role);
                }
            }
        }
        public string Address
        {
            get => Item.Address;
            set
            {
                if (value != Item.Address)
                {
                    Item.Address = value;
                    this.OnPropertyChanged(() => Address);
                }
            }
        }
        public string City
        {
            get => Item.City;
            set
            {
                if (value != Item.City)
                {
                    Item.City = value;
                    this.OnPropertyChanged(() => City);
                }
            }
        }
        public string PhoneNumber
        {
            get => Item.PhoneNumber;
            set
            {
                if (value != Item.PhoneNumber)
                {
                    Item.PhoneNumber = value;
                    this.OnPropertyChanged(() => PhoneNumber);
                }
            }
        }
        public DateTime? BirthDate
        {
            get => Item.BirthDate;
            set
            {
                if (value != Item.BirthDate)
                {
                    Item.BirthDate = value;
                    this.OnPropertyChanged(() => BirthDate);
                }
            }
        }
        public string PostalCode
        {
            get => Item.PostalCode;
            set
            {
                if (value != Item.PostalCode)
                {
                    Item.PostalCode = value;
                    this.OnPropertyChanged(() => PostalCode);
                }
            }
        }
        public string UserName
        {
            get => Item.UserName;
            set
            {
                if (value != Item.UserName)
                {
                    Item.UserName = value;
                    this.OnPropertyChanged(() => UserName);
                }
            }
        }

        #endregion

        #region Save
        public override void Save()
        {
            Database.Users.AddObject(Item);
            Database.SaveChanges();
        }
        #endregion
    }
}