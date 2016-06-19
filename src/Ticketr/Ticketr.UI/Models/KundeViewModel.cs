using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Businesslogik;
using Ticketr.UI.Models;

namespace Ticketr.UI.Components
{
    public class KundeViewModel : PersonViewModel
    {
        private Kunde kunde;
        private KundenViewModel kundenViewModel;
        /// <summary>
        /// Initialisiert das ViewModel
        /// </summary>
        /// <param name="kunde">Der Kunde</param>
        /// <param name="kundenViewModel">Das KundenViewModel</param>
        public KundeViewModel(Kunde kunde, KundenViewModel kundenViewModel)
            :base(kunde, kundenViewModel)
        {
            this.kunde = kunde;
            this.kundenViewModel = kundenViewModel;
            GetProfilePicture();
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
        public override void Remove()
        {
            App.TicketSystem.RemoveKunde(kunde.Id);
            kundenViewModel.RemoveKunde(this);
        }

        /// <summary>
        /// Gibt den Kunde zurück. NICHT FÜR BINDING VERWENDEN
        /// </summary>
        public Kunde Kunde
        {
            get
            {
                return kunde;
            }
        }

        private byte[] userImage;

        private async Task GetProfilePicture()
        {
            userImage = await App.TicketSystem.CurrentUser.GetProfilePicture();
            RaisePropertyChanged("ProfilePicture");
            RaisePropertyChanged("HasProfilePicture");
        }

        public override byte[] ProfilePicture
        {
            get { return userImage; }
        }
    }
}
