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
                List = new ObservableCollection<Invoices>(List.OrderBy(item => item.Name));
        }

        public override void Find()
        {
            Load();
            if (FindField == "Nazwa")
                List = new ObservableCollection<Invoices>(List.Where(item => item.Name != null && item.Name.StartsWith(FindText)));
        }
    }
}
