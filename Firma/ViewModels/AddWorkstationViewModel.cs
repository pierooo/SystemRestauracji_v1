using System;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class AddWorkstationViewModel : ItemViewModel<Workstations>
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

        public AddWorkstationViewModel() : base("Nowa Stacja Robocza")
        {
            base.Item = new Workstations();
        }

        public override void Save()
        {
            Item.PhotoUrl = ImageUrl ?? ImageUrl;
            Item.IsActive = true;
            Item.LastModified = DateTime.Now;
            Database.Workstations.AddObject(Item);
            Database.SaveChanges();
        }
    }
}
