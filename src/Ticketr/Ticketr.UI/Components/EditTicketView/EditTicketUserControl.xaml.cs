using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ticketr.Businesslogik;
using Ticketr.UI.Components.Dashboard;
using Ticketr.UI.Components.EditTicketView;
using Ticketr.UI.Components.TicketTable;

namespace Ticketr.UI.Components
{
    /// <summary>
    /// Interaction logic for EditTicketUserControl.xaml
    /// </summary>
    public partial class EditTicketUserControl : UserControl
    {
        public EditTicketUserControl()
        {
            InitializeComponent();
        }

        private void SpeichernButton_Click(object sender, RoutedEventArgs e)
        {
            EditTicketViewModel editTicketViewModel = (EditTicketViewModel)((Button) sender).DataContext;
            editTicketViewModel.SaveTicket();

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            EditTicketViewModel editTicketViewModel = (EditTicketViewModel)((Button)sender).DataContext;
            editTicketViewModel.DeleteTicket();
        }

        private void PostComment_Click(object sender, RoutedEventArgs e)
        {
            EditTicketViewModel editTicketViewModel = (EditTicketViewModel)((Button)sender).DataContext;
            editTicketViewModel.AddComment();
        }

        private void DeleteComment_Click(object sender, RoutedEventArgs e)
        {
            DashboardViewModel dashboardViewModel = App.MainWindowViewModel.SelectedViewModel as DashboardViewModel;
            dashboardViewModel.EditTicketViewModel.RemoveComment((int) ((Button) sender).Tag);
        }
    }
}
