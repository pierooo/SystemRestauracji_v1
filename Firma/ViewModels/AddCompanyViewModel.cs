using System;
using System.ComponentModel;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.Models.Validators;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class AddCompanyViewModel : ItemViewModel<Companies>, IDataErrorInfo
    {
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

        public string Country
        {
            get => Item.Country;
            set
            {
                if (value != Item.Country)
                {
                    Item.Country = value;
                    this.OnPropertyChanged(() => Country);
                }
            }
        }

        public string NIP
        {
            get => Item.NIP;
            set
            {
                if (value != Item.NIP)
                {
                    Item.NIP = value;
                    this.OnPropertyChanged(() => NIP);
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

        public string Mail
        {
            get => Item.Mail;
            set
            {
                if (value != Item.Mail)
                {
                    Item.Mail = value;
                    this.OnPropertyChanged(() => Mail);
                }
            }
        }
        #endregion
        public AddCompanyViewModel() : base("Nowa Firma")
        {
            Item = new Companies();
        }

        public override void Save()
        {
            Item.PhotoUrl = ImageUrl ?? ImageUrl;
            Item.IsActive = true;
            Item.LastModified = DateTime.Now;
            Database.Companies.AddObject(Item);
            Database.SaveChanges();
        }

        #region Validatory
        public string Error
        {
            get
            {
                return null;

            }
        }
        public string this[string name]
        {
            get
            {
                string message = null;
                if (name == "Name")
                {
                    message = StringValidator.CheckIfStartsWithUpper(this.Name);
                }

                if (name == "City")
                {
                    message = StringValidator.CheckIfStartsWithUpper(this.City);
                }

                if (name == "Country")
                {
                    message = StringValidator.CheckIfStartsWithUpper(this.Country);
                }
                return message;

            }
        }
        public override bool IsValid()
        {
            if (this["Name"] == null && this["City"] == null && this["Country"] == null)
                return true;
            else
                return false;
        }

        #endregion
    }
}
