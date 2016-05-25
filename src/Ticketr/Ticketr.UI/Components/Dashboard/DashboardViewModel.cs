using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Ticketr.UI.Models;

namespace Ticketr.UI.Components.Dashboard
{
    public class DashboardViewModel : ViewModel
    {
        public void OpenTicketMenu()
        {
            TicketViewWidth = "1*";
            BenutzerViewWidth = "0";
        }
        public void OpenBenutzerMenu()
        {
            TicketViewWidth = "0";
            BenutzerViewWidth = "1*";

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

        private string benutzerViewWidth;
        /// <summary>
        /// Gibt die Breite der BenutzerColumn zurück und legt diese fest
        /// </summary>
        public string BenutzerViewWidth
        {
            get { return benutzerViewWidth; }
            private set
            {
                benutzerViewWidth = value;
                RaisePropertyChanged("BenutzerViewWidth");
            }
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
        /// <summary>
        /// Gibt das Benutzerbild zurück, in dem der Ticketr Service angesprochen wird.
        /// </summary>
        public byte[] UserImage
        {
            get { return App.TicketSystem.CurrentUser.GetProfilePicture(); }
        }

    }
}
