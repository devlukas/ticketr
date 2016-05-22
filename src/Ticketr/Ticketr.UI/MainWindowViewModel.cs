using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Businesslogik;
using Ticketr.UI.Components.Login;
using Ticketr.UI.Models;

namespace Ticketr.UI
{
    /// <summary>
    /// Representiert das MainWindowViewModel.
    /// </summary>
    public class MainWindowViewModel : ViewModel
    {
        public bool Login(string username, string password)
        {
            return App.TicketSystem.Login(username, password);
        }

       
    }
}
