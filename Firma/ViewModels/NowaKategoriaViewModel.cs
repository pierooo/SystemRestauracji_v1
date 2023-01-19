using System.Windows.Input;
using SystemRestauracji.Helpers;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class NowaKategoriaViewModel : ItemViewModel<Categories>
    {
        #region Konstruktor
        public NowaKategoriaViewModel()
            :base("Nowa kategoria")
        {
            base.Item = new Categories();
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
        
        public string CategoryTypeId
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
            Database.Categories.AddObject(Item);
            Database.SaveChanges();
        }
        #endregion
    }
}