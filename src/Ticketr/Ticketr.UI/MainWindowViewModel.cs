using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Businesslogik;
using Ticketr.UI.Components.Login;
using Ticketr.UI.Components.TicketTable;
using Ticketr.UI.Models;

namespace Ticketr.UI
{
    /// <summary>
    /// Representiert das MainWindowViewModel.
    /// </summary>
    public class MainWindowViewModel : ViewModel
    {
        public TicketTableViewModel TicketTableViewModel { get; set; }

        
    }
}
