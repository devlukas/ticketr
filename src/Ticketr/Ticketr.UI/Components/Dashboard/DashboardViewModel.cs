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

        private void OnViewChange()
        {
            TicketViewWidth = "0";
            KundenViewWidth = "0";
            EditTicketViewWidth = "0";
            EditPersonViewWidth = "0";
            MitarbeiterViewWidth = "0";
            IsKundenViewOpen = false;
            IsMitarbeiterViewOpen = false;
        }
        /// <summary>
        /// Öffnet die TickketView
        /// </summary>
        public void OpenTicketMenu()
        {
            OnViewChange();
            TicketViewWidth = "*";
            TicketTableViewModel = new TicketTableViewModel();
            TicketTableViewModel.LoadItems();
        }
        /// <summary>
        /// Öffnet die Benutzerview
        /// </summary>
        public void OpenKundenMenu()
        {
            OnViewChange();
            PersonenViewModel = new KundenViewModel(this);
            PersonenViewModel.LoadItems();
            KundenViewWidth = "*";
            IsKundenViewOpen = true;

        }
        /// <summary>
        /// Öffnet die EditTicketView
        /// </summary>
        public void OpenEditTicketView()
        {
            OnViewChange();
            EditTicketViewWidth = "*";

        }

        
        /// <summary>
        /// Öffnet die Mitarbeiter View
        /// </summary>
        public void OpenMitarbeiterView()
        {
            OnViewChange();
            IsMitarbeiterViewOpen = true;
            PersonenViewModel = new MitarbeitersViewModel(this);
            PersonenViewModel.LoadItems();
            KundenViewWidth = "*";
        }
        /// <summary>
        /// Öffnet die EditPerosn View
        /// </summary>
        public void OpenEditPersonView()
        {
            OnViewChange();
            EditPersonViewModel = new EditPersonViewModel(this);
            EditPersonViewModel.LoadItems();
            EditPersonViewWidth = "*";
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
            set 
            { 
                editTicketViewModel = value;
                RaisePropertyChanged("EditTicketViewModel");
            }
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

        private PersonenViewModel personenViewModel;
        /// <summary>
        /// Gibt das KundenViewModel zurück
        /// </summary>
        public PersonenViewModel PersonenViewModel
        {
            get { return personenViewModel; }
            private set
            {
                personenViewModel = value;
                RaisePropertyChanged("PersonenViewModel");
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
        private bool isMitarbeiterViewOpen;
        /// <summary>
        /// Gibt zurück ob die MitarbeiterView offen ist oder nicht
        /// </summary>
        public bool IsMitarbeiterViewOpen
        {
            get
            {
                return isMitarbeiterViewOpen;
            }

            private set
            {
                isMitarbeiterViewOpen = value;
                RaisePropertyChanged("IsMitarbeiterViewOpen");
            }
        }
        /// <summary>
        /// Gibt zurück ob die Kunden View offen ist oder nicht
        /// </summary>
        public bool IsKundenViewOpen
        {
            get
            {
                return isKundenViewOpen;
            }

            private set
            {
                isKundenViewOpen = value;
                RaisePropertyChanged("IsKundenViewOpen");
            }
        }

        private bool isKundenViewOpen;


        private TicketTableViewModel ticketTableViewModel;
    }
}
