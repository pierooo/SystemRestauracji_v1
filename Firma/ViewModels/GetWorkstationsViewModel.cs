using System.Collections.Generic;
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

        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "Nazwa" };
        }

        public override void Sort()
        {
            if (SortField == "Nazwa")
                List = new ObservableCollection<Workstations>(List.OrderBy(item => item.Name));
        }

        public override void Find()
        {
            Load();
            if (FindField == "Nazwa")
                List = new ObservableCollection<Workstations>(List.Where(item => item.Name != null && item.Name.StartsWith(FindText)));
        }

        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "Nazwa" };
        }

        public override void Load()
        {
            List = new ObservableCollection<Workstations>(restaurantEntities.Workstations.Select(x => x));
        }        
    }
}