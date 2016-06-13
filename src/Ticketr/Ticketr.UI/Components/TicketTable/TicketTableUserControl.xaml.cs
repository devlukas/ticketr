using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ticketr.UI.Components.Dashboard;
using Ticketr.UI.Components.EditTicketView;
using Ticketr.UI.Components.TicketTable;
using Ticketr.UI.Components.TicketTableItem;

namespace Ticketr.UI.Components
{
    /// <summary>
    /// Interaction logic for TicketTableUserControl.xaml
    /// </summary>
    public partial class TicketTableUserControl : UserControl
    {
        public TicketTableUserControl()
        {
            InitializeComponent();
        }

        private void TicketTableClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject) e.OriginalSource;

            while ((dep != null) &&
            !(dep is DataGridCell))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            //Die Row des Cells holen
            while ((dep != null) && !(dep is DataGridRow))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            DataGridRow row = dep as DataGridRow;

            if (row != null)
            {
                TicketTableItemViewModel viewModel = row.Item as TicketTableItemViewModel;
                DashboardViewModel dashboard = App.MainWindowViewModel.SelectedViewModel as DashboardViewModel;


                dashboard.OpenEditTicketView();

            }

        }
    }
}
