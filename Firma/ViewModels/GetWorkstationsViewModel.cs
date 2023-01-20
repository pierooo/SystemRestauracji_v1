using System.Collections.ObjectModel;
using System.Linq;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetWorkstationsViewModel : ViewModelBase<Workstations>
    {
        public GetWorkstationsViewModel() : base("Stacje robocze")
        {
        }

        public override void Load()
        {
            List = new ObservableCollection<Workstations>(restaurantEntities.Workstations.Select(x => x));
        }
    }
}