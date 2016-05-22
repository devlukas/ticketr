using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Schnittstellen;
using Ticketr.UI.Models;

namespace Ticketr.UI
{
    /// <summary>
    /// Representiert das MainWindowViewModel.
    /// </summary>
    public class MainWindowViewModel : ViewModel
    {
        private readonly TicketrService ticketrService = new TicketrService();

        public bool Login(string email, string password)
        {
            return ticketrService.Login(email, password);
        }
    }
}
