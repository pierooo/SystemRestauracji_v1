using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using SystemRestauracji.Helpers;
using SystemRestauracji.Models.Entities;

namespace SystemRestauracji.ViewModels.Abstract
{
    public abstract class ViewModelBase<T> : WorkspaceViewModel
    {
        #region Fields
        protected readonly RestaurantSystemEntities restaurantEntities;
        private BaseCommand loadCommand;
        private BaseCommand addCommand;
        private T triggerType;

        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new BaseCommand(() => OpenAddView());
                }
                return addCommand;
            }
        }

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
            this.triggerType = (T)Activator.CreateInstance(typeof(T));
        }
        #endregion

        #region Helpers
        public abstract void Load();

        private void OpenAddView()
        {
            var triggerTypeString = triggerType.ToString();
            if(triggerTypeString == "SystemRestauracji.Models.Entities.Users")
            {
                triggerTypeString = DisplayName + "Add";
            }
            Messenger.Default.Send(triggerTypeString);
        }
        #endregion
    }
}
