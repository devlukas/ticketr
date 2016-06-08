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
    }
}
