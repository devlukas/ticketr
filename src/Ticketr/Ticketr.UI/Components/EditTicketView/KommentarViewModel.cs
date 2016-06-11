using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Businesslogik;

namespace Ticketr.UI.Components.EditTicketView
{
    public class KommentarViewModel
    {
        private Kommentar kommentar;

        public KommentarViewModel(Kommentar kommentar)
        {
            this.kommentar = kommentar;
        }

        public string Text
        {
            get { return kommentar.Text; }
            set { kommentar.Text = value; }
        }

        public DateTime Erstelldatum
        {
            get { return this.kommentar.Datum; }
        }

        public string Verfasser
        {
            get { return this.kommentar.Verfasser.FullName; }
        }
    }
}
