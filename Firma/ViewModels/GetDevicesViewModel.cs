using System.Collections.ObjectModel;
using System.Linq;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetDevicesViewModel : ViewModelBase<Devices>
    {
        public GetDevicesViewModel() : base("Urządzenia")
        {
        }

        public override void Load()
        {
            List = new ObservableCollection<Devices>(restaurantEntities.Devices.Select(x => x));
        }
    }
}
