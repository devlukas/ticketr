using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Ticketr.UI.Components.TicketTableItem;
using Ticketr.UI.Models;

namespace Ticketr.UI.Components.TicketTable
{
    public class TicketTableViewModel : ViewModel
    {
        public TicketTableViewModel()
        {
            Tickets = new List<TicketTableItemViewModel>();
        }

        private bool isLoading;

        public async Task LoadItems()
        {
            IsLoading = true;
            await App.TicketSystem.ReloadTickets();
            Tickets = App.TicketSystem.Tickets.Select(t => new TicketTableItemViewModel(t))
                .OrderBy(t => t.Ticket.Abgeschlossen)
                .ThenBy(t => t.Ticket.Prioritaet)
                .ToList();
            IsLoading = false;
            RaisePropertyChanged("Tickets");
        }

        public List<TicketTableItemViewModel> Tickets { get; private set; }


        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                RaisePropertyChanged("IsLoading");
                RaisePropertyChanged("LoadProcessVisibility");
            }
        }

        public Visibility LoadProcessVisibility
        {
            get { return isLoading ? Visibility.Visible : Visibility.Hidden; }
        }

        public async void ChangeView(string view)
        {
            await LoadItems();

            switch (view)
            {
                case "Offen":
                    Tickets = Tickets.Where(t => !t.Ticket.Abgeschlossen).ToList();
                    break;
                case "Meine":
                    Tickets = Tickets.Where(t => t.Ticket.Bearbeiter.Id == App.TicketSystem.CurrentUser.Id).ToList();
                    break;
                case "Abgeschlossen":
                    Tickets = Tickets.Where(t => t.Ticket.Abgeschlossen).ToList();
                    break;
            }

            RaisePropertyChanged("Tickets");
        }
    }
}
