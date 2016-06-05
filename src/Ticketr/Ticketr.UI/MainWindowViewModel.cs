using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
        private ViewModel selectedViewModel;
        /// <summary>
        /// Setzt die angegebene View
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="usercontrol"></param>
        public void SetSelectedView(ViewModel viewModel, UserControl usercontrol)
        {
            selectedViewModel = viewModel;
            App.MainWindow.Content = usercontrol;
        }
        /// <summary>
        /// Gibt das selektiere ViewModel, dass gerade aktiv ist, zurück
        /// </summary>
        public ViewModel SelectedViewModel
        {
            get
            {
                return selectedViewModel;
            }
        }

       
    }
}
