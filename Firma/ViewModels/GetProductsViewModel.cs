using System.Collections.ObjectModel;
using System.Linq;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetProductsViewModel : ViewModelBase<Products>
    {
        public GetProductsViewModel() : base("Produkty")
        {
        }

        public override void Load()
        {
            List = new ObservableCollection<Products>(restaurantEntities.Products.Select(x => x));
        }
    }
}
