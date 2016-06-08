using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Businesslogik;
using Ticketr.UI.Models;

namespace Ticketr.UI.Components
{
    public class KundeViewModel : ViewModel
    {
        private Kunde kunde;

        public KundeViewModel(Kunde kunde)
        {
            this.kunde = kunde;
        }

        public string FormattedName
        {
            get { return string.Format("{0} {1}", kunde.Vorname, kunde.Name); }
        }

        public string PositionsBezeichnung
        {
            get { return kunde.Position.Name; }
        }
    }
}
