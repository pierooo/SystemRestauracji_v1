using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                List = new ObservableCollection<WorkstationDeviceLinksForAllView>(List.OrderBy(item => item.WorkstationName));
        }

        public override void Find()
        {
            Load();
            if (FindField == "Nazwa")
                List = new ObservableCollection<WorkstationDeviceLinksForAllView>(List.Where(item => item.WorkstationName != null && item.WorkstationName.StartsWith(FindText)));
        }
    }
}
