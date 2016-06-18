using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Businesslogik;
using Ticketr.UI.Models;

namespace Ticketr.UI.Components
{
    public class MitarbeiterViewModel : PersonViewModel
    {
        private Mitarbeiter mitarbeiter;
        private MitarbeitersViewModel mitarbeitersViewModel;

        public MitarbeiterViewModel(Mitarbeiter mitarbeiter, MitarbeitersViewModel mitarbeitersViewModel)
            :base(mitarbeiter, mitarbeitersViewModel)
        {
            this.mitarbeiter = mitarbeiter;
            this.mitarbeitersViewModel = mitarbeitersViewModel;
        }

        /// <summary>
        /// Gibt den Mitarbeiter zurück. NICHT FÜR BINDING VERWENDEN
        /// </summary>
        public Mitarbeiter Mitarbeiter
        {
            get
            {
                return mitarbeiter;
            }
        }

        /// <summary>
        /// Löscht den Mitarbeiter aus der Datenbank und aus dem UI
        /// </summary>
        public override void Remove()
        {
            App.TicketSystem.RemoveMitarbeiter(Mitarbeiter.Id);
            mitarbeitersViewModel.RemoveMitarbeiter(this);
        }
    }
}
