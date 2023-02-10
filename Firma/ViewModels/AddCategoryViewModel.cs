using System;
using System.ComponentModel;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.Models.Validators;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class AddCategoryViewModel : ItemViewModel<Categories>, IDataErrorInfo
    {
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

        public AddCategoryViewModel() : base("Nowa kategoria")
        {
            base.Item = new Categories();
        }

        public override void Save()
        {
            Item.PhotoUrl = ImageUrl ?? ImageUrl;
            Item.IsActive = true;
            Item.LastModified = DateTime.Now;
            Database.Categories.AddObject(Item);
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
                return message;
            }
        }
        public override bool IsValid()
        {
            if (this["Name"] == null)
                return true;
            else
                return false;
        }

        #endregion
    }
}
