using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetInvoicesViewModel: ViewModelBase<Invoices>
    {
        public GetInvoicesViewModel():base("Faktury")
        {
        }

        public override void Load()
        {
            List = new ObservableCollection<Invoices>(restaurantEntities.Invoices.Select(x => x));
        }
    }
}
