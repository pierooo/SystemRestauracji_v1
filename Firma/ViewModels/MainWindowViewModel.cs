using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using SystemRestauracji.Helpers;
using SystemRestauracji.Models.BusinessLogic;
using SystemRestauracji.Models.Correspondences;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.Models.EntitiesForView;

namespace SystemRestauracji.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region left side flyout menu
        private int _SzerokoscKolumnyMenuBocznego = 150;
        public int SzerokoscKolumnyMenuBocznego
        {
            get
            {
                return _SzerokoscKolumnyMenuBocznego;
            }
            set
            {
                if (_SzerokoscKolumnyMenuBocznego != value)
                {
                    _SzerokoscKolumnyMenuBocznego = value;
                    OnPropertyChanged(() => SzerokoscKolumnyMenuBocznego);
                }
            }
        }
        #endregion
        #region get commands
        public ICommand GetProductsCommand
        {
            get
            {
                return new BaseCommand(GetProducts);
            }
        }

        public ICommand GetCompaniesCommand
        {
            get
            {
                return new BaseCommand(GetCompanies);
            }
        }

        public ICommand GetCategoriesCommand
        {
            get
            {
                return new BaseCommand(GetCategories);
            }
        }

        public ICommand GetRestaurantTablesCommand
        {
            get
            {
                return new BaseCommand(GetRestaurantTables);
            }
        }

        public ICommand GetReservationsCommand
        {
            get
            {
                return new BaseCommand(GetReservations);
            }
        }

        public ICommand GetWorkstationsCommand
        {
            get
            {
                return new BaseCommand(GetWorkstations);
            }
        }

        public ICommand GetWorkstationDeviceLinksCommand
        {
            get
            {
                return new BaseCommand(GetWorkstationDeviceLinks);
            }
        }

        public ICommand GetDocumentsCommand
        {
            get
            {
                return new BaseCommand(GetDocuments);
            }
        }

        public ICommand GetDevicesCommand
        {
            get
            {
                return new BaseCommand(GetDevices);
            }
        }

        public ICommand GetOpenedOrdersCommand
        {
            get
            {
                return new BaseCommand(GetOpenedOrders);
            }
        }

        public ICommand GetClosedOrdersCommand
        {
            get
            {
                return new BaseCommand(GetClosedOrders);
            }
        }

        public ICommand GetOrdersCommand
        {
            get
            {
                return new BaseCommand(GetOrders);
            }
        }

        public ICommand GetPaymentsCommand
        {
            get
            {
                return new BaseCommand(GetPayments);
            }
        }

        public ICommand GetOpenPaymentsCommand
        {
            get
            {
                return new BaseCommand(GetOpenPayments);
            }
        }

        public ICommand GetClosedPaymentsCommand
        {
            get
            {
                return new BaseCommand(GetClosedPayments);
            }
        }

        public ICommand GetFeePaymentsCommand
        {
            get
            {
                return new BaseCommand(GetFeePayments);
            }
        }
        public ICommand GetUsersCommand
        {
            get
            {
                return new BaseCommand(GetUsers);
            }
        }

        public ICommand GetEmployeesCommand
        {
            get
            {
                return new BaseCommand(GetEmployees);
            }
        }

        public ICommand GetInvoicesCommand
        {
            get
            {
                return new BaseCommand(GetInvoices);
            }
        }

        #endregion
        #region add commands
        public ICommand AddCategoryCommand
        {
            get
            {
                return new BaseCommand(() => createView(new AddCategoryViewModel()));
            }
        }

        public ICommand AddProductCommand
        {
            get
            {
                return new BaseCommand(() => createView(new AddProductViewModel()));
            }
        }

        public ICommand AddDeviceCommand
        {
            get
            {
                return new BaseCommand(() => createView(new AddDeviceViewModel()));
            }
        }

        public ICommand AddEmployeeCommand
        {
            get
            {
                return new BaseCommand(() => createView(new AddEmployeeViewModel()));
            }
        }

        public ICommand AddClientCommand
        {
            get
            {
                return new BaseCommand(() => createView(new AddClientViewModel()));
            }
        }

        public ICommand AddWorkstationCommand
        {
            get
            {
                return new BaseCommand(() => createView(new AddWorkstationViewModel()));
            }
        }

        public ICommand AddOrderCommand
        {
            get
            {
                return new BaseCommand(() => createView(new AddOrderViewModel()));
            }
        }

        public ICommand AddWorkstationDeviceLinkCommand
        {
            get
            {
                return new BaseCommand(AddWorkstationDeviceLink);
            }
        }

        public ICommand AddRestaurantTableCommand
        {
            get
            {
                return new BaseCommand(() => createView(new AddRestaurantTableViewModel()));
            }
        }

        public ICommand AddCompanyCommand
        {
            get
            {
                return new BaseCommand(() => createView(new AddCompanyViewModel()));
            }
        }

        public ICommand AddReservationCommand
        {
            get
            {
                return new BaseCommand(() => createView(new AddReservationViewModel()));
            }
        }

        #endregion

        #region other Command
        public ICommand CloseTabsCommand
        {
            get
            {
                return new BaseCommand(closeAllTabs);
            }
        }
        #endregion
        #region left side menu buttons
        private ReadOnlyCollection<CommandViewModel> _Commands;//to jest kolekcja komend wlewym menu
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_Commands == null)//sprawdzam czy przyciski z lewej strony menu nie zostały zainicjalizowane
                {
                    List<CommandViewModel> cmds = this.CreateCommands();//tworzę listę przyciskow za pomocą funkcji CreateCommands
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);//tę listę przypisuje do ReadOnlyCollection (bo readOnlyCollection można tylko tworzyć, nie można do niej dodawać)
                }
                return _Commands;
            }
        }
        private List<CommandViewModel> CreateCommands()//tu decydujemy jakie przyciski są w lewym menu
        {
            Messenger.Default.Register<OrderForOrderDetailsView>(this, OpenGetOrdersDetails);
            Messenger.Default.Register<AddProductToOrder>(this, OpenViewForAddingProductToOrderDetails);
            Messenger.Default.Register<Categories>(this, OpenProducts);
            Messenger.Default.Register<string>(this, Open);

            return new List<CommandViewModel>
            {
                new CommandViewModel("Produkty",new BaseCommand(GetProducts)),
                new CommandViewModel("Kategorie",new BaseCommand(GetCategories)),
                new CommandViewModel("Stoliki",new BaseCommand(GetRestaurantTables)),
                new CommandViewModel("Rezerwacje",new BaseCommand(GetReservations))
            };
        }
        #endregion
        #region tabs
        private ObservableCollection<WorkspaceViewModel> _Workspaces;
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.onWorkspacesChanged;
                }
                return _Workspaces;
            }
        }
        private void onWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.onWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.onWorkspaceRequestClose;
        }
        private void onWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            this.Workspaces.Remove(workspace);
        }
        #endregion
        #region additional functionality

        private async Task PokazUkryjmenuBoczneAsync()
        {
            if (SzerokoscKolumnyMenuBocznego > 0)
            {
                while (SzerokoscKolumnyMenuBocznego > 0)
                {
                    SzerokoscKolumnyMenuBocznego -= 10;
                    await Task.Delay(1);
                }
            }
            else
            {
                while (SzerokoscKolumnyMenuBocznego < 150)
                {
                    SzerokoscKolumnyMenuBocznego += 10;
                    await Task.Delay(1);
                }
            }
        }

        private void createView(WorkspaceViewModel workspace)
        {
            this.Workspaces.Add(workspace);
            this.setActiveWorkspace(workspace);
        }

        private void setActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        private void closeAllTabs()
        {
            Workspaces.Clear();
        }

        #endregion
        #region methods to run views GET

        private void GetProducts()
        {
            GetProductsViewModel workspaceProducts = this.Workspaces.FirstOrDefault(vm => vm is GetProductsViewModel) as GetProductsViewModel;
            GetCategoriesViewModel workspaceCategories = this.Workspaces.FirstOrDefault(vm => vm is GetCategoriesViewModel) as GetCategoriesViewModel;

            if (workspaceProducts == null && workspaceCategories == null)
            {
                workspaceProducts = new GetProductsViewModel();
                this.Workspaces.Add(workspaceProducts);
            }
            else if (workspaceCategories != null)
            {
                workspaceProducts = new GetProductsViewModel();
                Workspaces[Workspaces.IndexOf(workspaceCategories)] = workspaceProducts;
            }
            else if (workspaceProducts != null)
            {
                var newWorkspaceProducts = new GetProductsViewModel();
                Workspaces[Workspaces.IndexOf(workspaceProducts)] = newWorkspaceProducts;
                workspaceProducts = newWorkspaceProducts;
            }
            this.setActiveWorkspace(workspaceProducts);
        }

        private void GetCompanies()
        {
            GetCompaniesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetCompaniesViewModel) as GetCompaniesViewModel;
            if (workspace == null)
            {
                workspace = new GetCompaniesViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void GetCategories()
        {
            GetProductsViewModel workspaceProducts = this.Workspaces.FirstOrDefault(vm => vm is GetProductsViewModel) as GetProductsViewModel;
            GetCategoriesViewModel workspaceCategories = this.Workspaces.FirstOrDefault(vm => vm is GetCategoriesViewModel) as GetCategoriesViewModel;

            if (workspaceProducts == null && workspaceCategories == null)
            {
                workspaceCategories = new GetCategoriesViewModel();
                this.Workspaces.Add(workspaceCategories);
            }
            else if (workspaceProducts != null)
            {
                workspaceCategories = new GetCategoriesViewModel();
                Workspaces[Workspaces.IndexOf(workspaceProducts)] = workspaceCategories;
            }
            this.setActiveWorkspace(workspaceCategories);
        }

        private void GetRestaurantTables()
        {
            GetRestaurantTablesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetRestaurantTablesViewModel) as GetRestaurantTablesViewModel;
            if (workspace == null)
            {
                workspace = new GetRestaurantTablesViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void GetReservations()
        {
            GetReservationsViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetReservationsViewModel) as GetReservationsViewModel;
            if (workspace == null)
            {
                workspace = new GetReservationsViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void GetWorkstations()
        {
            GetWorkstationsViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetWorkstationsViewModel) as GetWorkstationsViewModel;
            if (workspace == null)
            {
                workspace = new GetWorkstationsViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void GetWorkstationDeviceLinks()
        {
            GetWorkstationDeviceLinksViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetWorkstationDeviceLinksViewModel) as GetWorkstationDeviceLinksViewModel;
            if (workspace == null)
            {
                workspace = new GetWorkstationDeviceLinksViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void GetDocuments()
        {
            GetDocumentsViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetDocumentsViewModel) as GetDocumentsViewModel;
            if (workspace == null)
            {
                workspace = new GetDocumentsViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void GetDevices()
        {
            GetDevicesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetDevicesViewModel) as GetDevicesViewModel;
            if (workspace == null)
            {
                workspace = new GetDevicesViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void GetOpenedOrders()
        {
            GetOrdersViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetOrdersViewModel) as GetOrdersViewModel;
            var newWorkspace = new GetOrdersViewModel(new[] { Status.Added, Status.InProgress, Status.Paid }, "Otwarte zamówienia");
            if (workspace != null)
            {
                Workspaces[Workspaces.IndexOf(workspace)] = newWorkspace;
            }
            else
            {
                this.Workspaces.Add(newWorkspace);
            }

            this.setActiveWorkspace(newWorkspace);
        }

        private void GetClosedOrders()
        {
            GetOrdersViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetOrdersViewModel) as GetOrdersViewModel;
            var newWorkspace = new GetOrdersViewModel(new[] { Status.Done, Status.Cancelled }, "Zamknięte zamówienia");
            if (workspace != null)
            {
                Workspaces[Workspaces.IndexOf(workspace)] = newWorkspace;
            }
            else
            {
                this.Workspaces.Add(newWorkspace);
            }

            this.setActiveWorkspace(newWorkspace);
        }

        private void GetOrders()
        {
            GetOrdersViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetOrdersViewModel) as GetOrdersViewModel;
            var newWorkspace = new GetOrdersViewModel(new[] { Status.Added, Status.InProgress, Status.Paid, Status.Done, Status.Cancelled }, "Wszystkie zamówienia");
            if (workspace != null)
            {
                Workspaces[Workspaces.IndexOf(workspace)] = newWorkspace;
            }
            else
            {
                this.Workspaces.Add(newWorkspace);
            }

            this.setActiveWorkspace(newWorkspace);
        }

        private void GetOpenPayments()
        {
            GetPaymentsViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetPaymentsViewModel) as GetPaymentsViewModel;
            var newWorkspace = new GetPaymentsViewModel(new[] { Status.Added, Status.InProgress, Status.Open }, "Otwarte płatności");
            if (workspace != null)
            {
                Workspaces[Workspaces.IndexOf(workspace)] = newWorkspace;
            }
            else
            {
                this.Workspaces.Add(newWorkspace);
            }

            this.setActiveWorkspace(newWorkspace);
        }

        private void GetClosedPayments()
        {
            GetPaymentsViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetPaymentsViewModel) as GetPaymentsViewModel;
            var newWorkspace = new GetPaymentsViewModel(new[] { Status.Done, Status.Cancelled, Status.Paid }, "Zamknięte płatności");
            if (workspace != null)
            {
                Workspaces[Workspaces.IndexOf(workspace)] = newWorkspace;
            }
            else
            {
                this.Workspaces.Add(newWorkspace);
            }

            this.setActiveWorkspace(newWorkspace);
        }

        private void GetPayments()
        {
            GetPaymentsViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetPaymentsViewModel) as GetPaymentsViewModel;
            var newWorkspace = new GetPaymentsViewModel(new[] { Status.Added, Status.InProgress, Status.Paid, Status.Done, Status.Cancelled, Status.Fee }, "Wszystkie płatności");
            if (workspace != null)
            {
                Workspaces[Workspaces.IndexOf(workspace)] = newWorkspace;
            }
            else
            {
                this.Workspaces.Add(newWorkspace);
            }

            this.setActiveWorkspace(newWorkspace);
        }

        private void GetFeePayments()
        {
            GetPaymentsViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetPaymentsViewModel) as GetPaymentsViewModel;
            var newWorkspace = new GetPaymentsViewModel(new[] { Status.Fee }, "Wszystkie wydatki");
            if (workspace != null)
            {
                Workspaces[Workspaces.IndexOf(workspace)] = newWorkspace;
            }
            else
            {
                this.Workspaces.Add(newWorkspace);
            }

            this.setActiveWorkspace(newWorkspace);
        }

        private void GetInvoices()
        {
            GetInvoicesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetInvoicesViewModel) as GetInvoicesViewModel;
            if (workspace == null)
            {
                workspace = new GetInvoicesViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void GetUsers()
        {
            GetUsersViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetUsersViewModel) as GetUsersViewModel;
            var newWorkspace = new GetUsersViewModel((int)Role.Client, "Klienci");
            if (workspace != null)
            {
                Workspaces[Workspaces.IndexOf(workspace)] = newWorkspace;
            }
            else
            {
                this.Workspaces.Add(newWorkspace);
            }
            this.setActiveWorkspace(newWorkspace);
        }

        private void GetEmployees()
        {
            GetUsersViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetUsersViewModel) as GetUsersViewModel;
            var newWorkspace = new GetUsersViewModel((int)Role.Employee, "Pracownicy");
            if (workspace != null)
            {
                Workspaces[Workspaces.IndexOf(workspace)] = newWorkspace;
            }
            else
            {
                this.Workspaces.Add(newWorkspace);
            }
            this.setActiveWorkspace(newWorkspace);
        }

        #endregion
        #region methods to run views ADD
        private void AddWorkstationDeviceLink()
        {
            AddWorkstationDeviceLinkViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is AddWorkstationDeviceLinkViewModel) as AddWorkstationDeviceLinkViewModel;
            if (workspace == null)
            {
                workspace = new AddWorkstationDeviceLinkViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        #endregion
        #region messenger Usings
        private void OpenGetOrdersDetails(OrderForOrderDetailsView order)
        {
            GetOrderDetailsViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetOrderDetailsViewModel) as GetOrderDetailsViewModel;
            var newWorkspace = new GetOrderDetailsViewModel(order, order.Id.ToString() + " " + order.Name);
            if (workspace != null)
            {
                Workspaces[Workspaces.IndexOf(workspace)] = newWorkspace;
            }
            else
            {
                this.Workspaces.Add(newWorkspace);
            }
            this.setActiveWorkspace(newWorkspace);
        }

        private void OpenProducts(Categories category)
        {
            GetCategoriesViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GetCategoriesViewModel) as GetCategoriesViewModel;
            var newWorkspace = new GetProductsViewModel(category);
            Workspaces[Workspaces.IndexOf(workspace)] = newWorkspace;
            this.setActiveWorkspace(newWorkspace);
        }

        private void OpenViewForAddingProductToOrderDetails(AddProductToOrder addProductToOrder)
        {
            GetCategoriesViewModel workspaceCategories = this.Workspaces.FirstOrDefault(vm => vm is GetCategoriesViewModel) as GetCategoriesViewModel;
            GetProductsViewModel workspaceProducts = this.Workspaces.FirstOrDefault(vm => vm is GetProductsViewModel) as GetProductsViewModel;

            var newWorkspaceForCategories = new GetCategoriesViewModel(addProductToOrder);
            var newWorkspaceForProducts = new GetProductsViewModel(addProductToOrder);

            if (addProductToOrder.BackToCategories || addProductToOrder.CategoryId == null && addProductToOrder.Product == null)
            {
                if (workspaceCategories != null)
                {
                    Workspaces[Workspaces.IndexOf(workspaceCategories)] = newWorkspaceForCategories;
                }
                else
                {
                    this.Workspaces.Add(newWorkspaceForCategories);
                }

                this.setActiveWorkspace(newWorkspaceForCategories);
            }
            else if (addProductToOrder.CategoryId != null && addProductToOrder.Product == null)
            {
                if (workspaceCategories != null)
                {
                    Workspaces[Workspaces.IndexOf(workspaceCategories)] = newWorkspaceForProducts;
                }
                else if (workspaceProducts != null)
                {
                    this.Workspaces.Remove(workspaceProducts);
                }
                this.setActiveWorkspace(newWorkspaceForProducts);
            }
            else if (addProductToOrder.CategoryId != null && addProductToOrder.Product != null && !addProductToOrder.Added)
            {
                var newWorkspaceForProductsDetails = new AddOrderDetailsProductDetailsViewModel(addProductToOrder);
                this.Workspaces.Add(newWorkspaceForProductsDetails);
                this.setActiveWorkspace(newWorkspaceForProductsDetails);
            }
            else if (addProductToOrder.Added)
            {
                OpenGetOrdersDetails(new OrderForOrderDetailsView(addProductToOrder.OrderId, addProductToOrder.OrderName, Status.Open));
            }
        }

        private void Open(string view)
        {
            switch (view)
            {
                case "Orders": GetOpenedOrders(); break;
                case "SystemRestauracji.Models.EntitiesForView.OrderForAllView": createView(new AddOrderViewModel()); break;
                case "SystemRestauracji.Models.Entities.Products": createView(new AddProductViewModel()); break;
                case "SystemRestauracji.Models.Entities.Categories": createView(new AddCategoryViewModel()); break;
                case "SystemRestauracji.Models.Entities.RestaurantTables": createView(new AddRestaurantTableViewModel()); break;
                case "SystemRestauracji.Models.EntitiesForView.ReservationForAllView": createView(new AddReservationViewModel()); break;
                case "SystemRestauracji.Models.Entities.Invoices": createView(new AddOrderViewModel()); break;
                case "SystemRestauracji.Models.EntitiesForView.DocumentForAllView": createView(new AddOrderViewModel()); break;
                case "SystemRestauracji.Models.EntitiesForView.PaymentsForAllView": createView(new AddOrderViewModel()); break;
                case "SystemRestauracji.Models.Entities.Companies": createView(new AddCompanyViewModel()); break;
                case "PracownicyAdd": createView(new AddEmployeeViewModel()); break;
                case "KlienciAdd": createView(new AddClientViewModel()); break;
                case "SystemRestauracji.Models.Entities.Devices": createView(new AddDeviceViewModel()); break;
                case "SystemRestauracji.Models.Entities.Workstations": createView(new AddWorkstationViewModel()); break;
                case "SystemRestauracji.Models.EntitiesForView.WorkstationDeviceLinksForAllView": createView(new AddWorkstationDeviceLinkViewModel()); break;
                default: break;
            }
        }

        #endregion
    }
}
