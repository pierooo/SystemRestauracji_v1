using System.Collections.ObjectModel;
using System.Linq;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetUsersViewModel : ViewModelBase<Users>
    {
        public GetUsersViewModel():base("Użytkownik")
        {
        }

        public override void Load()
        {
            List = new ObservableCollection<Users>(restaurantEntities.Users.Select(x => x));
        }
    }
}
