using System;
using System.ComponentModel;
using SystemRestauracji.Models.BusinessLogic;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.Models.Validators;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class AddEmployeeViewModel : ItemViewModel<Users>, IDataErrorInfo
    {
        #region prop
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

        public AddEmployeeViewModel() : base("Nowy pracownik")
        {
            base.Item = new Users();
        }

        public override void Save()
        {
            Item.Password = "1234";
            Item.Role = ((int)Role.Employee);
            Item.PhotoUrl = ImageUrl ?? ImageUrl;
            Item.IsActive = true;
            Item.LastModified = DateTime.Now;
            Database.Users.AddObject(Item);
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
                if (name == "FirstName")
                {
                    message = StringValidator.CheckIfStartsWithUpper(this.FirstName);
                }
                
                if (name == "LastName")
                {
                    message = StringValidator.CheckIfStartsWithUpper(this.LastName);
                }
                return message;
            }
        }
        public override bool IsValid()
        {
            //return base.IsValid();
            if (this["FirstName"] == null && this["LastName"] == null)
                return true;
            else
                return false;
        }

        #endregion
    }
}
