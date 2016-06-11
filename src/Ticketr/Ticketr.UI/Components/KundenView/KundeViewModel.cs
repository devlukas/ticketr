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
        private KundenViewModel kundenViewModel;
        /// <summary>
        /// Initialisiert das ViewModel
        /// </summary>
        /// <param name="kunde">Der Kunde</param>
        /// <param name="kundenViewModel">Das KundenViewModel</param>
        public KundeViewModel(Kunde kunde, KundenViewModel kundenViewModel)
        {
            this.kunde = kunde;
            this.kundenViewModel = kundenViewModel;
        }
        /// <summary>
        /// Gibt den Formatierten Namen im Format {Vorname} {Name} zurück
        /// </summary>
        public string FormattedName
        {
            get { return string.Format("{0} {1}", kunde.Vorname, kunde.Name); }
        }
        /// <summary>
        /// Gibt die Bezeichnung der Position zurück
        /// </summary>
        public string PositionsBezeichnung
        {
            get { return kunde.Position.Name; }
        }
        /// <summary>
        /// Löscht diesen Kunde von der Datenbank und vom UI
        /// </summary>
        public void Remove()
        {
            App.TicketSystem.RemoveKunde(kunde.Id);
            kundenViewModel.Kunden.Remove(this);
        }
    }
}
