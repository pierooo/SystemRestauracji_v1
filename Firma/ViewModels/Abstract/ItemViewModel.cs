using System.Windows.Input;
using SystemRestauracji.Helpers;
using SystemRestauracji.Models.Entities;

namespace SystemRestauracji.ViewModels.Abstract
{
    public abstract class ItemViewModel<T> : WorkspaceViewModel
    {
        #region Fields
        public RestaurantSystemEntities Database { get; set; }

        public T Item { get; set; }

        #endregion
        #region Konstruktor
        public ItemViewModel(string displayName)
        {
            base.DisplayName = displayName;
            Database = new RestaurantSystemEntities();
        }
        #endregion    

        #region Command
        private BaseCommand saveAndCloseCommand;
        public ICommand SaveAndCloseCommand
        {
            get
            {
                if (saveAndCloseCommand is null)
                {
                    saveAndCloseCommand = new BaseCommand(() => SaveAndClose());
                }
                return saveAndCloseCommand;
            }
        }
        #endregion

        #region Save
        public abstract void Save();

        private void SaveAndClose()
        {
            Save();
            base.OnRequestClose();
        }
        #endregion

    }
}
