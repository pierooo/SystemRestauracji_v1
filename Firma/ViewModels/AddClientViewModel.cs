﻿using System;
using SystemRestauracji.Models.BusinessLogic;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    internal class AddClientViewModel : ItemViewModel<Users>
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

        #endregion

        public AddClientViewModel() : base("Nowy Kontrahent")
        {
            base.Item = new Users();
        }

        public override void Save()
        {
            Item.Role = ((int)Role.Employee);
            Item.PhotoUrl = ImageUrl ?? ImageUrl;
            Item.IsActive = true;
            Item.LastModified = DateTime.Now;
            Database.Users.AddObject(Item);
            Database.SaveChanges();
        }
    }
}
