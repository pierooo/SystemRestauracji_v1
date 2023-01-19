using System.Windows.Input;
using SystemRestauracji.Helpers;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class NowyTypKategoriiViewModel : ItemViewModel<Workstations>
    {
        #region Konstruktor
        public NowyTypKategoriiViewModel()
            :base("Nowa workstation")
        {
            base.Item = new Workstations();
        }
        #endregion

        #region Properties
        public string Name 
        { 
            get => Item.Name;
            set
            {
                if(value != Item.Name)
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
            Database.Workstations.AddObject(Item);
            Database.SaveChanges();
        }
        #endregion
    }
}