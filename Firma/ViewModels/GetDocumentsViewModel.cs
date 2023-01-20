using System.Collections.ObjectModel;
using System.Linq;
using SystemRestauracji.Models.EntitiesForView;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetDocumentsViewModel : ViewModelBase<DocumentForAllView>
    {
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
    }
}
