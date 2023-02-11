using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using SystemRestauracji.Models.Correspondences;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetDocumentsViewModel : ViewModelBase<DocumentForAllView>
    {

        private DocumentForAllView selectedDocument;

        public DocumentForAllView SelectedDocument
        {
            get
            {
                return selectedDocument;
            }
            set
            {
                if (selectedDocument != value)
                {
                    selectedDocument = value;

                    Messenger.Default.Send(new DocumentForDocumentPositionsDetails(selectedDocument.Name, selectedDocument.Id));
                    base.OnRequestClose();
                }
            }
        }

        public GetDocumentsViewModel() : base("Dokumenty")
        {
        }

        public override void Load()
        {
            List = new ObservableCollection<DocumentForAllView>(restaurantEntities.Documents
                .Select(x =>
                new DocumentForAllView()
                {
                    Id = x.Id,
                    Name = x.Name,
                    InvoiveName = x.Invoices.Title ?? "Nie wystawiono",
                    AuthorFullName = x.AuthorFullName,
                    AuthorNIP = x.AuthorNIP,
                    RecipientFullName = x.RecipientFullName,
                    RecipientNIP = x.RecipientNIP,
                    PaymentDateLimit = x.PaymentDateLimit,
                    TotalAmountGross = x.TotalAmountGross,
                    DocumentStatus = x.DocumentStatus,
                    DocumentDate = x.DocumentDate,
                    EmployeeFullName = x.EmployeeFullName
                }));
        }



        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "Nazwa" };
        }

        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "Nazwa" };
        }

        public override void Sort()
        {
            if (SortField == "Nazwa")
                List = new ObservableCollection<DocumentForAllView>(List.OrderBy(item => item.Name));
        }

        public override void Find()
        {
            Load();
            if (FindField == "Nazwa")
                List = new ObservableCollection<DocumentForAllView>(List.Where(item => item.Name != null && item.Name.StartsWith(FindText)));
        }
    }
}
