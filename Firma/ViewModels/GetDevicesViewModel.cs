using System.Collections.Generic;
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
                List = new ObservableCollection<Devices>(List.OrderBy(item => item.Name));
        }

        public override void Find()
        {
            Load();
            if (FindField == "Nazwa")
                List = new ObservableCollection<Devices>(List.Where(item => item.Name != null && item.Name.StartsWith(FindText)));
        }
    }
}
