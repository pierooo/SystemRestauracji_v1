using System.Collections.ObjectModel;
using System.Linq;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetCategoriesViewModel : ViewModelBase<Categories>
    {
        public GetCategoriesViewModel() : base("Kategorie")
        {
        }

        public override void Load()
        {
            List = new ObservableCollection<Categories>(restaurantEntities.Categories.Select(x => x));
        }
    }
}
