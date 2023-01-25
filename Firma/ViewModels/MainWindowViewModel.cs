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
        #region PolaiWlasciwosci
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
        #region Get commands
        //after refactoring
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

        public ICommand GetInvoicesCommand
        {
            get
            {
                return new BaseCommand(GetInvoices);
            }
        }

        #endregion

        #region Add commands
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

        #endregion

        #region stare
        public ICommand PokazUkryjMenuBoczneAsyncCommand { get { return new BaseCommand(async () => await PokazUkryjmenuBoczneAsync()); } }

        public ICommand NowyTowarCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyTowarViewModel()));
            }
        }
        public ICommand KategorieCommand
        {
            get
            {
                return new BaseCommand(ShowAllKategorie);
            }
        }
        public ICommand NowaKategoriaCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowaKategoriaViewModel()));
            }
        }
        public ICommand DodajProduktDoKategoriCommand
        {
            get
            {
                return new BaseCommand(() => createView(new DodajProduktDoKategoriiViewModel()));
            }
        }
        public ICommand DodajTypKategoriCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyTypKategoriiViewModel()));
            }
        }
        public ICommand StolikiCommand
        {
            get
            {
                return new BaseCommand(ShowAllStoliki);
            }
        }
        public ICommand ZamowieniaCommand
        {
            get
            {
                return new BaseCommand(ShowAllZamowienia);
            }
        }
        public ICommand DodajStolikCommand
        {
            get
            {
                return new BaseCommand(() => createView(new DodajStolikViewModel()));
            }
        }
        public ICommand NoweZamowienieCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NoweZamowienieViewModel()));
            }
        }
        public ICommand TaZmianaZamowieniaCommand
        {
            get
            {
                return new BaseCommand(ShowTaZmianaZamowienia);
            }
        }
        public ICommand MagazynProduktyCommand
        {
            get
            {
                return new BaseCommand(ShowMagazynProdukow);
            }
        }
        public ICommand ZakonczZamowienieCommand
        {
            get
            {
                return new BaseCommand(() => createView(new ZakonczZamowienieViewModel()));
            }
        }
        public ICommand MojeZamknieteZamowieniaCommand
        {
            get
            {
                return new BaseCommand(ShowMojeZamknieteZamowienia);
            }
        }
        public ICommand WszystkieTypyKategoriiCommand
        {
            get
            {
                return new BaseCommand(ShowTypyKategorii);
            }
        }
        public ICommand MojeOtwarteZamowieniaCommand
        {
            get
            {
                return new BaseCommand(ShowMojeOtwarteZamowienia);
            }
        }

        public ICommand TowaryCommand
        {
            get
            {
                return new BaseCommand(showAllTowar);
            }
        }
        public ICommand NowaFakturaCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowaFakturaViewModel()));
            }
        }
        public ICommand DodajPracownikaCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyPracownikViewModel()));
            }
        }
        public ICommand ZamknijZmianeCommand
        {
            get
            {
                return new BaseCommand(ZamknijZmiane);
            }
        }
        public ICommand DrukarkiCommand
        {
            get
            {
                return new BaseCommand(ShowDrukarki);
            }
        }

        public ICommand DodajDostawceCommand
        {
            get
            {
                return new BaseCommand(() => createView(new NowyDostawcaViewModel()));
            }
        }
        public ICommand DostawcyCommand
        {
            get
            {
                return new BaseCommand(showAllDostawcy);
            }
        }
        public ICommand FakturyCommand
        {
            get
            {
                return new BaseCommand(showAllFaktury);
            }
        }
        public ICommand WynagrodzeniaCommand
        {
            get
            {
                return new BaseCommand(showAllWynagrodzenia);
            }
        }
        public ICommand RestauracjaCommand
        {
            get
            {
                return new BaseCommand(showDaneRestauracji);
            }
        }
        public ICommand GrafikCommand
        {
            get
            {
                return new BaseCommand(showGrafik);
            }
        }
        public ICommand PracownicyCommand
        {
            get
            {
                return new BaseCommand(showPracownicy);
            }
        }
        #endregion


        #region Przyciski w menu z lewej strony
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

            return new List<CommandViewModel>
            {
                new CommandViewModel("Moja zmiana",new BaseCommand(ShowMojaZmiana)),
                new CommandViewModel("Produkty",new BaseCommand(GetProducts)),
                new CommandViewModel("Kategorie",new BaseCommand(GetCategories)),
                new CommandViewModel("Stoliki",new BaseCommand(GetRestaurantTables)),
                new CommandViewModel("Rezerwacje",new BaseCommand(GetReservations)),
                new CommandViewModel("Dane firmy",new BaseCommand(showDaneRestauracji)),
            };
        }
        #endregion

        #region Zakładki
        private ObservableCollection<WorkspaceViewModel> _Workspaces; //to jest kolekcja zakładek
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
        #region Funkcje pomocnicze

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
            if(workspace == null)
            {
                workspace = new GetUsersViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        #endregion
        #region MessengerUsings
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
                else if(workspaceProducts != null)
                {
                    this.Workspaces.Remove(workspaceProducts);
                }
                this.setActiveWorkspace(newWorkspaceForProducts);
            }
        }

        #endregion



        #region stare
        private void ShowAllKategorie()
        {
            WszystkieKategorieViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is WszystkieKategorieViewModel) as WszystkieKategorieViewModel;
            if (workspace == null)
            {
                workspace = new WszystkieKategorieViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void ShowMojaZmiana()
        {
            MojaZmianaViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is MojaZmianaViewModel) as MojaZmianaViewModel;
            if (workspace == null)
            {
                workspace = new MojaZmianaViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllRezerwacje()
        {
            WszystkieRezerwacjeViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is WszystkieRezerwacjeViewModel) as WszystkieRezerwacjeViewModel;
            if (workspace == null)
            {
                workspace = new WszystkieRezerwacjeViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void ShowDrukarki()
        {
            WszystkieDrukarkiViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is WszystkieDrukarkiViewModel) as WszystkieDrukarkiViewModel;
            if (workspace == null)
            {
                workspace = new WszystkieDrukarkiViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void showPracownicy()
        {
            WszyscyPracownicyViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is WszyscyPracownicyViewModel) as WszyscyPracownicyViewModel;
            if (workspace == null)
            {
                workspace = new WszyscyPracownicyViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showGrafik()
        {
            GrafikViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is GrafikViewModel) as GrafikViewModel;
            if (workspace == null)
            {
                workspace = new GrafikViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showDaneRestauracji()
        {
            DaneRestauracjiViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is DaneRestauracjiViewModel) as DaneRestauracjiViewModel;
            if (workspace == null)
            {
                workspace = new DaneRestauracjiViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllWynagrodzenia()
        {
            WszystkieWynagrodzeniaViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is WszystkieWynagrodzeniaViewModel) as WszystkieWynagrodzeniaViewModel;
            if (workspace == null)
            {
                workspace = new WszystkieWynagrodzeniaViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllDostawcy()
        {
            WszyscyDostawcyViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is WszyscyDostawcyViewModel) as WszyscyDostawcyViewModel;
            if (workspace == null)
            {
                workspace = new WszyscyDostawcyViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void ShowMagazynProdukow()
        {
            MagazynProduktowViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is MagazynProduktowViewModel) as MagazynProduktowViewModel;
            if (workspace == null)
            {
                workspace = new MagazynProduktowViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void ShowTaZmianaZamowienia()
        {
            TaZmianaZamowieniaViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is TaZmianaZamowieniaViewModel) as TaZmianaZamowieniaViewModel;
            if (workspace == null)
            {
                workspace = new TaZmianaZamowieniaViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void ShowAllZamowienia()
        {
            WszystkieZamowieniaViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is WszystkieZamowieniaViewModel) as WszystkieZamowieniaViewModel;
            if (workspace == null)
            {
                workspace = new WszystkieZamowieniaViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void ShowAllStoliki()
        {
            WszystkieStolikiViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is WszystkieStolikiViewModel) as WszystkieStolikiViewModel;
            if (workspace == null)
            {
                workspace = new WszystkieStolikiViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void showAllFaktury()
        {
            WszystkieFakturyViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is WszystkieFakturyViewModel) as WszystkieFakturyViewModel;
            if (workspace == null)
            {
                workspace = new WszystkieFakturyViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void showAllTowar()
        {
            WszystkieTowaryViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is WszystkieTowaryViewModel) as WszystkieTowaryViewModel;
            if(workspace == null)
            {
                workspace=new WszystkieTowaryViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void ShowMojeOtwarteZamowienia()
        {
            MojeOtwarteZamowieniaViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is MojeOtwarteZamowieniaViewModel) as MojeOtwarteZamowieniaViewModel;
            if (workspace == null)
            {
                workspace = new MojeOtwarteZamowieniaViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void ShowMojeZamknieteZamowienia()
        {
            MojeZamknieteZamowieniaViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is MojeZamknieteZamowieniaViewModel) as MojeZamknieteZamowieniaViewModel;
            if (workspace == null)
            {
                workspace = new MojeZamknieteZamowieniaViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        private void ShowTypyKategorii()
        {
            WszystkieTypyKategoriViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is WszystkieTypyKategoriViewModel) as WszystkieTypyKategoriViewModel;
            if (workspace == null)
            {
                workspace = new WszystkieTypyKategoriViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }
        
        private void ZamknijZmiane()
        {
            ZamknijZmianeViewModel workspace = this.Workspaces.FirstOrDefault(vm => vm is ZamknijZmianeViewModel) as ZamknijZmianeViewModel;
            if (workspace == null)
            {
                workspace = new ZamknijZmianeViewModel();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }



        #endregion

        private void setActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }
    }
}
