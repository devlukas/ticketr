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
using Ticketr.UI.Components.EditPersonView;
using Ticketr.UI.Components.EditTicketView;
using Ticketr.UI.Components.TicketTableItem;

namespace Ticketr.UI.Components
{
    /// <summary>
    /// Interaction logic for EditKundeView.xaml
    /// </summary>
    public partial class EditPerson : UserControl
    {
        public EditPerson()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            EditPersonViewModel editPersonViewModel = (EditPersonViewModel)((Button)sender).DataContext;
            if (editPersonViewModel.IsKunde)
            {
                App.TicketSystem.SaveKunde(editPersonViewModel.Kunde);
                editPersonViewModel.DashboardViewModel.OpenKundenMenu();
            }
            else
            {
                if (PasswordBox.Password == PasswordBoxRepeat.Password)
                {
                    App.TicketSystem.SaveMitarbeiter(editPersonViewModel.Mitarbeiter, PasswordBox.Password);
                    editPersonViewModel.DashboardViewModel.OpenMitarbeiterView();
                }
                else
                {
                    throw new Exception("Passwort");
                }

            }

        }

        private void TicketListBoxItemClick(object sender, RoutedEventArgs e)
        {
            TicketTableItemViewModel ticket = (TicketTableItemViewModel)((ListBoxItem)sender).DataContext;
            DashboardViewModel dashboard = App.MainWindowViewModel.SelectedViewModel as DashboardViewModel;

            dashboard.EditTicketViewModel = new EditTicketViewModel(ticket.Id);
            dashboard.OpenEditTicketView();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            EditPersonViewModel editPersonViewModel = (EditPersonViewModel)((Button)sender).DataContext;
            editPersonViewModel.RemoveCurrentPerson();
            if (editPersonViewModel.IsKunde)
            {
                editPersonViewModel.DashboardViewModel.OpenKundenMenu();
            }
            else
            {
                editPersonViewModel.DashboardViewModel.OpenMitarbeiterView();
            }
        }
    }
}
