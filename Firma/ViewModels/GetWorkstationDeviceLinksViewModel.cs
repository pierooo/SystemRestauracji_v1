using System.Collections.ObjectModel;
using System.Linq;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetWorkstationDeviceLinksViewModel : ViewModelBase<WorkstationDeviceLinksForAllView>
    {
        public GetWorkstationDeviceLinksViewModel() : base("Połączone stacje robocze i urządzenia")
        {
        }

        public override void Load()
        {
            List = new ObservableCollection<WorkstationDeviceLinksForAllView>(restaurantEntities.WorkstationDeviceLinks
                .Select(x =>
                new WorkstationDeviceLinksForAllView()
                {
                    Id = x.Id,
                    WorkstationName = x.Workstations.Name,
                    WorkstationDescription = x.Workstations.Description,
                    DeviceName = x.Devices.Name,
                    DeviceDescription = x.Devices.Description
                }));
        }
    }
}
