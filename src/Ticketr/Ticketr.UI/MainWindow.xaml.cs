using MahApps.Metro.Controls;
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
using Ticketr.Schnittstellen;
using Ticketr.Schnittstellen.Dto;

namespace Ticketr.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            TicketrService service = new TicketrService();

            bool test = service.Login("test@test.ch", "test");

            List<Mitarbeiter> mitarbeiters = service.GetAllMitarbeiter();
            List<Kunde> kundenList = service.GetAllKunden();
            //List<Ticket> ticketList = service.GetTickets();
            //Ticket ticket = service.GetTicketDetail(ticketList.First().Id);
            Mitarbeiter m1 = service.GetCurrentMitarbeiterDetail();
            Mitarbeiter m2 = service.GetMitarbeiterDetail(mitarbeiters.First().Id);
            //List<Kategorie> kategories = service.GetKategorien();

            //int id = service.AddTicket(ticketList.First());
            //ticketList.First().Bezeichnung = "HAllo123";
            //service.UpdateTicket(ticketList.First());

            byte[] pb = service.GetProfilePicture(1);
            service.SetProfilePicture(pb, 1);
            
            bool auth = service.Authorized;
        }
    }
}
