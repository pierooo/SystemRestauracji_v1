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
    }
}
