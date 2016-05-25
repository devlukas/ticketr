using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
        private ViewModel selectedViewModel;

        public bool Login(string username, string password)
        {
            return App.TicketSystem.Login(username, password);
        }
        /// <summary>
        /// Setzt die View vom UI her und setzt das Properties <see cref="SelectedViewModel"/>
        /// </summary>
        /// <param name="viewModel">Das ViewModel der View</param>
        /// <param name="userControl">Das UserControl der View</param>
        public void SetSelectedView(ViewModel viewModel, UserControl userControl)
        {
            userControl.DataContext = viewModel;
            App.MainWindow.Content = userControl;
            this.selectedViewModel = viewModel;
        }

        /// <summary>
        /// Gibt das Selektierte View Model zurück.
        /// </summary>
        public ViewModel SelectedViewModel
        {
            get { return this.selectedViewModel; }
        }


    }
}
