using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRestauracji.ViewModels
{
    public class NowyTowarViewModel:WorkspaceViewModel
    {
        #region Konstruktor
        public NowyTowarViewModel()
        {
            base.DisplayName = "Towar";//tu ustawiamy nazwę zakładki
        }
        #endregion
    }
}
