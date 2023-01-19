using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRestauracji.ViewModels
{
    public class WszystkieTowaryViewModel: WorkspaceViewModel //bo wszystkie zakładki dziedzicza po WorkspaceViewModel
    {
        #region Konstruktor
        public WszystkieTowaryViewModel()
        {
            base.DisplayName = "Towary";//tu ustawiamy nazwę zakładki
        }
        #endregion
    }
}
