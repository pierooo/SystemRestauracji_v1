using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetCompaniesViewModel : ViewModelBase<Companies>
    {
        public GetCompaniesViewModel() : base("Firmy")
        {
        }

        public override void Load()
        {
            List = new ObservableCollection<Companies>(restaurantEntities.Companies.Select(x => x));
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
                List = new ObservableCollection<Companies>(List.OrderBy(item => item.Name));
        }

        public override void Find()
        {
            Load();
            if (FindField == "Nazwa")
                List = new ObservableCollection<Companies>(List.Where(item => item.Name != null && item.Name.StartsWith(FindText)));
        }
    }
}
