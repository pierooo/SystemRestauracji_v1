using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SystemRestauracji.Models.Correspondences;
using SystemRestauracji.Models.Entities;
using SystemRestauracji.ViewModels.Abstract;

namespace SystemRestauracji.ViewModels
{
    public class GetDocumentPositionsDetailsViewModel : ViewModelBase<DocumentPositionsDetails>
    {
        public string DocumentFullName { get; set; }
        private int documentId;

        public GetDocumentPositionsDetailsViewModel(DocumentForDocumentPositionsDetails document) : base("Szczegóły rachunku - " + document.DocumentFullNane)
        {
            this.DocumentFullName = document.DocumentFullNane;
            this.documentId = document.DocumentId;
        }

        public override void Load()
        {
            if (restaurantEntities.DocumentPositionsDetails.Any(x => x.DucumentId == documentId))
            {
                List = new ObservableCollection<DocumentPositionsDetails>(restaurantEntities.DocumentPositionsDetails.Where(x => x.DucumentId == documentId).AsQueryable());
            }
        }

        public override void Sort()
        {
            if (SortField == "Nazwa")
                List = new ObservableCollection<DocumentPositionsDetails>(List.OrderBy(item => item.PositionName));
        }

        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "Nazwa" };
        }

        public override void Find()
        {
            Load();
            if (FindField == "Nazwa")
                List = new ObservableCollection<DocumentPositionsDetails>(List.Where(item => item.PositionName != null && item.PositionName.StartsWith(FindText)));
        }

        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "Nazwa" };
        }
    }
}
