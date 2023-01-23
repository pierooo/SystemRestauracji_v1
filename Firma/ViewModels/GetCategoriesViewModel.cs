using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using SystemRestauracji.Models.Correspondences;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetCategoriesViewModel : ViewModelBase<Categories>
    {
        private AddProductToOrder addProductToOrder;
        private Categories selectedCategory;

        public Categories SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                if (selectedCategory != value)
                {
                    selectedCategory = value;
                    if(addProductToOrder != null)
                    {
                        addProductToOrder.CategoryId = selectedCategory.Id;
                        addProductToOrder.CategoryName = selectedCategory.Name;
                        Messenger.Default.Send(addProductToOrder);
                    }
                    else
                    {
                        Messenger.Default.Send(selectedCategory);
                    }
                }
            }
        }

        public GetCategoriesViewModel() : base("Kategorie")
        {
        }

        public GetCategoriesViewModel(AddProductToOrder addProductToOrder) : base("Dodaj do " + addProductToOrder.OrderFullName)
        {
            this.addProductToOrder = addProductToOrder;
        }

        public override void Load()
        {
            List = new ObservableCollection<Categories>(restaurantEntities.Categories.Select(x => x));
        }
    }
}
