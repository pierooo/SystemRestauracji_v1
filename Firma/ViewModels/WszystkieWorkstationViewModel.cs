using System.Collections.ObjectModel;
using System.Linq;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class WszystkieWorkstationViewModel : ViewModelBase<WorkstationForAllView>
    {
        public WszystkieWorkstationViewModel() : base("Komputery")
        {
        }

        public override void Load()
        {
            List = new ObservableCollection<WorkstationForAllView>(restaurantEntities.Workstations
                .Select(x => 
                new WorkstationForAllView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                }));
        }
    }
}
