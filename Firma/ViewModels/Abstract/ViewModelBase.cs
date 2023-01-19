using System.Collections.ObjectModel;
using System.Windows.Input;
using SystemRestauracji.Helpers;
using SystemRestauracji.Models.Entities;

namespace SystemRestauracji.ViewModels.Abstract
{
    public abstract class ViewModelBase<T> : WorkspaceViewModel
    {
        #region Fields
        protected readonly RestaurantSystemEntities restaurantEntities;
        private BaseCommand loadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (loadCommand == null)
                {
                    loadCommand = new BaseCommand(() => Load());
                }
                return loadCommand;
            }
        }

        private ObservableCollection<T> list;
        public ObservableCollection<T> List
        {
            get
            {
                if (list == null)
                {
                    Load();
                }
                return list;
            }
            set
            {
                list = value;
                OnPropertyChanged(() => List);
            }
        }
        #endregion
        #region Konstruktor
        public ViewModelBase(string title)
        {
            base.DisplayName = title;
            this.restaurantEntities = new RestaurantSystemEntities();
        }
        #endregion

        #region Helpers
        public abstract void Load();
        #endregion
    }
}
