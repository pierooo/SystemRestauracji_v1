using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using SystemRestauracji.Helpers;
using SystemRestauracji.Models.Entities;

namespace SystemRestauracji.ViewModels.Abstract
{
    public abstract class ItemViewModel<T> : WorkspaceViewModel
    {
        #region Fields
        public RestaurantSystemEntities Database { get; set; }

        public T Item { get; set; }

        private BitmapImage image;

        public BitmapImage Image
        {
            get
            {
                return this.image;
            }
            set
            {
                if (value != image)
                {
                    image = value;
                    base.OnPropertyChanged(() => image);
                }
            }
        }

        private string imageUrl;

        public string ImageUrl
        {
            get
            {
                return this.imageUrl;
            }
            set
            {
                if (value != imageUrl)
                {
                    imageUrl = value;
                    base.OnPropertyChanged(() => imageUrl);
                }
            }
        }

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

        private BaseCommand loadImageCommand;
        public ICommand LoadImageCommand
        {
            get
            {
                if(loadImageCommand is null)
                {
                    loadImageCommand = new BaseCommand(() => LoadImage());
                    this.OnPropertyChanged(() => Image);
                }
                return loadImageCommand;
            }
        }

        #endregion

        #region Validators

        public virtual bool IsValid()
        {
            return true;
        }

        #endregion
        #region Methods
        public abstract void Save();

        private void SaveAndClose()
        {
            if (IsValid())
            {
                Save();
                base.OnRequestClose();
            }
            else
            {
                ShowMessageBox("Popraw Błędy");
            }
        }

        public void LoadImage()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Wybierz zdjęcie";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                string selectedFileName = op.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                Image = bitmap;
                ImageUrl = op.FileName;
            }
        }
        #endregion
    }
}
