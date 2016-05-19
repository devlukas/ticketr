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
using Ticketr.UI.Components.Menu;
using Ticketr.UI.Components.MenuItem;

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

            MenuItemViewModel tickets = new MenuItemViewModel("Tickets", "Home");

            MenuViewModel menuViewModel = new MenuViewModel();
            menuViewModel.MenuItems.Add(tickets);
            DataContext = menuViewModel;
        }
    }
}
