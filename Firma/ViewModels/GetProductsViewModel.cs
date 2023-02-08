using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using SystemRestauracji.Models.Correspondences;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetProductsViewModel : ViewModelBase<Products>
    {
        private Categories category;
        private AddProductToOrder addProductToOrder;
        private Products selectedProduct;

        public Products SelectedProduct
        {
            get
            {
                return selectedProduct;
            }
            set
            {
                if (selectedProduct != value)
                {
                    selectedProduct = value;
                    if (addProductToOrder != null)
                    {
                        addProductToOrder.Product = selectedProduct;
                        MoveToAddProductDetails();
                        base.OnRequestClose();
                    }
                }
            }
        }

        public GetProductsViewModel() : base("Produkty")
        {
        }

        public GetProductsViewModel(AddProductToOrder addProductToOrder) : base("Dodaj do " + addProductToOrder.OrderFullName)
        {
            this.addProductToOrder = addProductToOrder;
        }

        public GetProductsViewModel(Categories category) : base("Kategoria " + category.Name)
        {
            this.category = category;
        }

        public override void Load()
        {
            if (addProductToOrder?.CategoryId == null && category == null)
            {
                List = new ObservableCollection<Products>(restaurantEntities.Products.Select(x => x));
            }
            else if (addProductToOrder?.CategoryId != null)
            {
                List = new ObservableCollection<Products>(restaurantEntities.Products.Where(x => x.CategoryId == addProductToOrder.CategoryId).Select(x => x));
            }
            else if (category != null)
            {
                List = new ObservableCollection<Products>(restaurantEntities.Products.Where(x => x.CategoryId == category.Id).Select(x => x));
            }
        }

        private void MoveToAddProductDetails()
        {
            Messenger.Default.Send(addProductToOrder);
        }
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "Nazwa" };
        }
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "Nazwa" };
        }

        public override void Sort()
        {
            if (SortField == "Nazwa")
                List = new ObservableCollection<Products>(List.OrderBy(item => item.Name));
        }

        public override void Find()
        {
            Load();
            if (FindField == "Nazwa")
                List = new ObservableCollection<Products>(List.Where(item => item.Name != null && item.Name.StartsWith(FindText)));
        }
    }
}
