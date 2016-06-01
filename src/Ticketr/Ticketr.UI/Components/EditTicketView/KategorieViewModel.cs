using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Businesslogik;

namespace Ticketr.UI.Components.EditTicketView
{
    public class KategorieViewModel
    {
        private Kategorie kategorie;
        private bool isSubItem;
        public KategorieViewModel(Kategorie kategorie, bool isSubItem)
        {
            this.kategorie = kategorie;
            this.isSubItem = isSubItem;
        }

        public string Name
        {
            get
            {
                return isSubItem ? String.Format("\t{0}", kategorie.Name) : kategorie.Name;
            }
        }

        public bool IsSubItem
        {
            get
            {
                return isSubItem;
            }
        }



    }
}
