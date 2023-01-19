using System.Collections.ObjectModel;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;
using System.Linq;

namespace SystemRestauracji.ViewModels
{
    public class WszystkieTypyKategoriViewModel : ViewModelBase<Workstations>
    {
        public WszystkieTypyKategoriViewModel() : base("Workstations")
        {
        }

        public override void Load()
        {
            List = new ObservableCollection<Workstations>(restaurantEntities.Workstations.Select(x => x));
        }
    }
}