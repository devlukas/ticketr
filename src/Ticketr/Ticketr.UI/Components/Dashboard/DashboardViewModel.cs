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

        public string TicketViewWidth
        {
            get { return ticketViewWidth; }
            set
            {
                ticketViewWidth = value;
                RaisePropertyChanged("TicketViewWidth");
            }
        }

        private string benutzerViewWidth;

        public string BenutzerViewWidth
        {
            get { return benutzerViewWidth; }
            set
            {
                benutzerViewWidth = value;
                RaisePropertyChanged("BenutzerViewWidth");
            }
        }

    }
}
