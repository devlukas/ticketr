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
using Ticketr.UI.Components.EditTicketView;

namespace Ticketr.UI.Components
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class TicketView : UserControl
    {
        public TicketView()
        {
            InitializeComponent();
        }

        private void TicketErstellenButtonClick(object sender, RoutedEventArgs e)
        {
            DashboardViewModel dashboardViewModel = App.MainWindowViewModel.SelectedViewModel as DashboardViewModel;
            dashboardViewModel.OpenEditTicketView();
        }
    }
}
