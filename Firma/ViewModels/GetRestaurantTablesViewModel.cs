using System.Collections.ObjectModel;
using System.Linq;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetRestaurantTablesViewModel : ViewModelBase<RestaurantTables>
    {
        public GetRestaurantTablesViewModel() : base("Stoliki w restauracji")
        {
        }

        public override void Load()
        {
            List = new ObservableCollection<RestaurantTables>(restaurantEntities.RestaurantTables.Select(x => x));
        }
    }
}
