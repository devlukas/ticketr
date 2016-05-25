using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.UI.Components.TicketTableItem;

namespace Ticketr.UI.Components.TicketTable
{
    public class TicketTableViewModel
    {
        public TicketTableViewModel()
        {
            Tickets = new List<TicketTableItemViewModel>();
        }

        public void LoadItems()
        {
            Tickets = App.TicketSystem.Tickets.Select(t => new TicketTableItemViewModel(t)).ToList();
        }

        public List<TicketTableItemViewModel> Tickets { get; private set; }
    }
}
