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
using Ticketr.UI.Components.Dashboard;
using Ticketr.UI.Components.TicketTable;

namespace Ticketr.UI.Components
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();

        }

        private void KundenButton_Click(object sender, RoutedEventArgs e)
        {
            DashboardViewModel dashboardViewModel = (DashboardViewModel)((Button)sender).DataContext;
            dashboardViewModel.OpenKundenMenu();

        }

        private void TicketsButton_Click(object sender, RoutedEventArgs e)
        {
            DashboardViewModel dashboardViewModel = (DashboardViewModel)((Button)sender).DataContext;
            dashboardViewModel.OpenTicketMenu();
        }

        private void MitarbeiterButton_Click(object sender, RoutedEventArgs e)
        {
            DashboardViewModel dashboardViewModel = (DashboardViewModel)((Button)sender).DataContext;
            dashboardViewModel.OpenMitarbeiterView();
        }
    }
}
