using System.Collections.ObjectModel;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;
using System.Linq;

namespace SystemRestauracji.ViewModels
{
    public class WszyscyDostawcyViewModel : ViewModelBase<Companies>
    {
        #region Konstruktor
        public WszyscyDostawcyViewModel() : base("Typy kategorii")
        {
        }
        #endregion

        public override void Load()
        {
            List = new ObservableCollection<Companies>(restaurantEntities.Companies.Select(x => x));
        }
    }
}