using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketr.UI.Components.TicketTable
{
    public class TicketTableViewModel
    {
        public TicketTableViewModel()
        {
            TicketTableViewModels = new List<TicketTableViewModel>();
        }
        public List<TicketTableViewModel> TicketTableViewModels { get; private set; }
    }
}
