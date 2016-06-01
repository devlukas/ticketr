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
using Ticketr.UI.Components.EditTicketView;

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
            if (editTicketViewModel.SelectedKategorie != null)
            {
                Ticket ticket = new Ticket()
                {
                    Prioritaet = editTicketViewModel.SelectedPriority,
                    Kategorie = editTicketViewModel.SelectedKategorie.Kategorie,
                    Bearbeiter = editTicketViewModel.SelectedMitarbeiter,
                    Abgeschlossen = false,
                    Beschreibung = editTicketViewModel.Beschreibung,
                    Bezeichnung = editTicketViewModel.Titel,
                    Kunde = editTicketViewModel.SelectedKunde
                };
                App.TicketSystem.SaveTicket(ticket);
                editTicketViewModel.DashboardViewModel.OpenTicketMenu();
            }

        }
    }
}
