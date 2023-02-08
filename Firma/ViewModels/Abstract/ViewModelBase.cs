using System;
using System.Collections.Generic;
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
        private BaseCommand sortCommand;
        private BaseCommand filtrCommand;        

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

        public string SortField { get; set; }
        public List<string> SortComboboxItems
        {
            get
            {
                return GetComboboxSortList();
            }
        }

        public ICommand SortCommand
        {
            get
            {
                if(sortCommand == null)
                {
                    sortCommand = new BaseCommand(() => Sort());
                }
                return sortCommand;
            }
        }

        public string FindField { get; set; }
        public List<string> FiltrComboboxItems
        {
            get
            {
                return GetComboboxFindList();
            }
        }

        public ICommand FiltrCommand
        {
            get
            {
                if (filtrCommand == null)
                {
                    filtrCommand = new BaseCommand(() => Find());
                }
                return filtrCommand;
            }
        }

        public string FindText { get; set; } = string.Empty;
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

        public abstract void Sort();
        public abstract List<string> GetComboboxSortList();
        public abstract void Find();
        public abstract List<string> GetComboboxFindList();

        #endregion
    }
}
