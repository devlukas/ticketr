using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Ticketr.UI.Components.EditPersonView;
using Ticketr.UI.Components.EditTicketView;
using Ticketr.UI.Components.TicketTable;
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
            EditPersonViewWidth = "0";
            MitarbeiterViewWidth = "0";
            TicketTableViewModel = new TicketTableViewModel();
            TicketTableViewModel.LoadItems();
        }
        /// <summary>
        /// Öffnet die Benutzerview
        /// </summary>
        public void OpenKundenMenu()
        {
            KundenViewModel = new KundenViewModel(this);
            KundenViewModel.LoadItems();
            TicketViewWidth = "0";
            EditTicketViewWidth = "0";
            KundenViewWidth = "*";
            MitarbeiterViewWidth = "0";
            EditPersonViewWidth = "0";

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
            EditPersonViewWidth = "0";

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
            EditPersonViewWidth = "0";
        }
        /// <summary>
        /// Öffnet die EditPerosn View
        /// </summary>
        public void OpenEditPersonView()
        {
            EditPersonViewModel = new EditPersonViewModel(this);
            EditPersonViewModel.LoadItems();
            EditPersonViewWidth = "*";
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

        private string editPersonViewWidth;
        /// <summary>
        /// Gibt die Breite der EditPersonView  zurück und legt diese fest
        /// </summary>
        public string EditPersonViewWidth
        {
            get { return editPersonViewWidth; }
            private set { editPersonViewWidth = value; RaisePropertyChanged("EditPersonViewWidth"); }
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
            set { editTicketViewModel = value; }
        }

        private EditPersonViewModel editPersonViewModel;
        /// <summary>
        /// Gibt das EditKundeViewModel zurück
        /// </summary>
        public EditPersonViewModel EditPersonViewModel
        {
            get { return editPersonViewModel; }
            set
            {
                editPersonViewModel = value;
                RaisePropertyChanged("EditPersonViewModel");
            }
        }

        private KundenViewModel kundenViewModel;
        /// <summary>
        /// Gibt das KundenViewModel zurück
        /// </summary>
        public KundenViewModel KundenViewModel
        {
            get { return kundenViewModel; }
            private set
            {
                kundenViewModel = value;
                RaisePropertyChanged("KundenViewModel");
            }
        }

        public TicketTableViewModel TicketTableViewModel
        {
            get { return ticketTableViewModel; }
            set
            {
                ticketTableViewModel = value;
                RaisePropertyChanged("TicketTableViewModel");
            }
        }

        private TicketTableViewModel ticketTableViewModel;
    }
}
