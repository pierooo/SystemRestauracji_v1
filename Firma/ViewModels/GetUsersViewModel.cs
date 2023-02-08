using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetUsersViewModel : ViewModelBase<Users>
    {
        private readonly int role;
        public GetUsersViewModel(int role, string viewName):base(viewName)
        {
            this.role = role;
        }

        public override void Load()
        {
            List = new ObservableCollection<Users>(restaurantEntities.Users.Where(x => x.Role == role).Select(x => x));
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
                List = new ObservableCollection<Users>(List.OrderBy(item => item.FirstName));
        }

        public override void Find()
        {
            Load();
            if (FindField == "Nazwa")
                List = new ObservableCollection<Users>(List.Where(item => item.FirstName != null && item.FirstName.StartsWith(FindField)));
        }
    }
}
