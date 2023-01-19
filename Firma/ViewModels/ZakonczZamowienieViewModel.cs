using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRestauracji.ViewModels
{
    public class ZakonczZamowienieViewModel:WorkspaceViewModel //bo wszystkie zakładki dziedzicza po workspaceVM
    {
        #region Konstruktor
        public ZakonczZamowienieViewModel()
        {
            base.DisplayName = "Zakończ zamówienie";
        }
        #endregion
    }
}
