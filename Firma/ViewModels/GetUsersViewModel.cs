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
    }
}
