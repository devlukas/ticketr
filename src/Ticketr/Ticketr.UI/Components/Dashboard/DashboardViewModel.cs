using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Ticketr.UI.Components.EditKundeView;
using Ticketr.UI.Components.EditTicketView;
using Ticketr.UI.Models;

namespace Ticketr.UI.Components.Dashboard
{
    /// <summary>
    /// Representiert das Viewmodel für DashboardViewUserControl.xaml
    /// </summary>
    public class DashboardViewModel : ViewModel
    {
        /// <summary>
        /// Initialisiert das DashboardViewModel
        /// </summary>
        public DashboardViewModel()
        {
            kundenViewModel = new KundenViewModel(this);
            kundenViewModel.LoadItems();
            GetProfilePicture();
        }
        /// <summary>
        /// Öffnet die TickketView
        /// </summary>
        public void OpenTicketMenu()
        {
            TicketViewWidth = "*";
            KundenViewWidth = "0";
            EditTicketViewWidth = "0";
            EditKundeViewWidth = "0";
            MitarbeiterViewWidth = "0";
        }
        /// <summary>
        /// Öffnet die Benutzerview
        /// </summary>
        public void OpenKundenMenu()
        {
            TicketViewWidth = "0";
            EditTicketViewWidth = "0";
            KundenViewWidth = "*";
            MitarbeiterViewWidth = "0";
            EditKundeViewWidth = "0";

        }
        /// <summary>
        /// Öffnet die EditTicketView
        /// </summary>
        public void OpenEditTicketView()
        {
            TicketViewWidth = "0";
            KundenViewWidth = "0";
            EditTicketViewWidth = "*";
            MitarbeiterViewWidth = "0";
            EditKundeViewWidth = "0";

        }
        /// <summary>
        /// Öffnet die Mitarbeiter View
        /// </summary>
        public void OpenMitarbeiterView()
        {
            TicketViewWidth = "0";
            KundenViewWidth = "0";
            EditTicketViewWidth = "0";
            MitarbeiterViewWidth = "*";
            EditKundeViewWidth = "0";
        }
        /// <summary>
        /// Öffnet die EditKunde View
        /// </summary>
        public void OpenEditKundeView()
        {
            EditKundeViewWidth = "*";
            TicketViewWidth = "0";
            KundenViewWidth = "0";
            EditTicketViewWidth = "0";
            MitarbeiterViewWidth = "0";
        }
        private string ticketViewWidth;
        /// <summary>
        /// Gibt die Breite der TicketView zurück und legt diese fest
        /// </summary>
        public string TicketViewWidth
        {
            get { return ticketViewWidth; }
            private set
            {
                ticketViewWidth = value;
                RaisePropertyChanged("TicketViewWidth");
            }
        }

        private string kundenViewWidth;
        /// <summary>
        /// Gibt die Breite der KundenColumn zurück und legt diese fest
        /// </summary>
        public string KundenViewWidth
        {
            get { return kundenViewWidth; }
            private set
            {
                kundenViewWidth = value;
                RaisePropertyChanged("KundenViewWidth");
            }
        }

        private string mitarbeiterViewWidth;
        /// <summary>
        /// Gibt die Breite der MitarbeiterColumn zurück und legt diese fest
        /// </summary>
        public string MitarbeiterViewWidth
        {
            get { return mitarbeiterViewWidth; }
            private set
            {
                mitarbeiterViewWidth = value;
                RaisePropertyChanged("MitarbeiterViewWidth");
            }
        }
        private string editTicketViewWidth;
        /// <summary>
        /// Gibt die Breite der EditTicketColumn zurück und legt diese fest
        /// </summary>
        public string EditTicketViewWidth
        {
            get { return editTicketViewWidth; }
            private set
            {
                editTicketViewWidth = value;
                RaisePropertyChanged("EditTicketViewWidth");
            }
        }

        private string editKundeViewWidth;
        /// <summary>
        /// Gibt die Breite der EditKundeColumn zurück und legt diese fest
        /// </summary>
        public string EditKundeViewWidth
        {
            get { return editKundeViewWidth; }
            private set { editKundeViewWidth = value; }
        }

        /// <summary>
        /// Gibt den Vornamen und Namen im Format "{Vorname} {Name}" zurück.
        /// </summary>
        public string FormattedUserName
        {
            get
            {
                return string.Format("{0} {1}", App.TicketSystem.CurrentUser.Vorname, App.TicketSystem.CurrentUser.Name);
            }
        }


        private byte[] userImage;


        /// <summary>
        /// Gibt das Benutzerbild zurück, in dem der Ticketr Service angesprochen wird.
        /// </summary>
        public byte[] UserImage
        {
            get
            {
                return userImage;
            }
        }


        public async Task GetProfilePicture()
        {
            userImage = await App.TicketSystem.CurrentUser.GetProfilePicture();
            RaisePropertyChanged("UserImage");
        }

        private EditTicketViewModel editTicketViewModel;
        
        /// <summary>
        /// Gibt das EditTicketViewModel zurück
        /// </summary>
        public EditTicketViewModel EditTicketViewModel
        {
            get { return editTicketViewModel; }
        }

        private EditKundeViewModel editKundeViewModel;
        /// <summary>
        /// Gibt das EditKundeViewModel zurück
        /// </summary>
        public EditKundeViewModel EditKundeViewModel
        {
            get { return editKundeViewModel; }
        }

        private KundenViewModel kundenViewModel;
        /// <summary>
        /// Gibt das KundenViewModel zurück
        /// </summary>
        public KundenViewModel KundenViewModel
        {
            get { return kundenViewModel; }
        }
    }
}
