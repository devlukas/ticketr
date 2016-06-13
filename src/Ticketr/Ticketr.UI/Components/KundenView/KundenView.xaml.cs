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

namespace Ticketr.UI.Components
{
    /// <summary>
    /// Interaction logic for KundenView.xaml
    /// </summary>
    public partial class KundenView : UserControl
    {
        public KundenView()
        {
            InitializeComponent();
        }

        private void KundeButtonLöschen_Click(object sender, RoutedEventArgs e)
        {
            KundeViewModel kundeViewModel = (KundeViewModel) ((Button) sender).DataContext;
            kundeViewModel.Remove();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            KundenViewModel kundenViewModel = (KundenViewModel) ((Button) sender).DataContext;
            kundenViewModel.DashboardViewModel.OpenEditPersonView();
        }
    }
}
