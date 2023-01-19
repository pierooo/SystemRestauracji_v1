using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class NowyDostawcaViewModel : ItemViewModel<Companies>
    {
        #region Konstruktor
        public NowyDostawcaViewModel()
            : base("Nowa firma")
        {
            base.Item = new Companies();
        }
        #endregion

        #region Properties
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
        public string VATEU
        {
            get => Item.NIP;
            set
            {
                if (value != Item.NIP)
                {
                    Item.NIP = value;
                    this.OnPropertyChanged(() => VATEU);
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


        #endregion

        #region Save
        public override void Save()
        {
            Database.Companies.AddObject(Item);
            Database.SaveChanges();
        }
        #endregion
    }
}