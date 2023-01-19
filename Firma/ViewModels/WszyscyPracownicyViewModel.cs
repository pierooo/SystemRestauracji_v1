using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;
using System.Linq;
using System.Collections.ObjectModel;

namespace SystemRestauracji.ViewModels
{
    internal class WszyscyPracownicyViewModel : ViewModelBase<Users>
    {
        #region Konstruktor
        public WszyscyPracownicyViewModel() : base("Pracownicy")
        {
        }
        #endregion

        public override void Load()
        {
            List = new ObservableCollection<Users>(restaurantEntities.Users.Select(x => x));
        }
    }
}