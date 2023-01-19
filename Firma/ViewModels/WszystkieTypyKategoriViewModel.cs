using System.Collections.ObjectModel;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;
using System.Linq;

namespace SystemRestauracji.ViewModels
{
    public class WszystkieKategorieViewModel : ViewModelBase<Categories>
    {
        public WszystkieKategorieViewModel() : base("Kategorie")
        {
        }

        public override void Load()
        {
            List = new ObservableCollection<Categories>(restaurantEntities.Categories.Select(x => x));
        }
    }
}