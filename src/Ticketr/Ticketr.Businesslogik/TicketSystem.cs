using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Schnittstellen;

namespace Ticketr.Businesslogik
{
    public class TicketSystem
    {
        /// <summary>
        /// Die Referenz auf den Ticketr-Service
        /// </summary>
        private static TicketrService service;

        /// <summary>
        /// Alle Mitarbeiter des TicketSystems
        /// </summary>
        private List<Mitarbeiter> mitarbeiter;

        /// <summary>
        /// Alle Kunden des TicketSystems
        /// </summary>
        private List<Kunde> kunden;

        /// <summary>
        /// Alle Tickets, die das System zurzeit hat
        /// </summary>
        private List<Ticket> tickets;


        /// <summary>
        /// Der aktuell eingeloggte Benutzer
        /// </summary>
        private Mitarbeiter currentUser;


        public static TicketrService Service
        {
            get
            {
                if (service == null)
                {
                    service = new TicketrService();
                }
                return service;
            }
        }

        /// <summary>
        /// Der aktuell eingeloggte Benutzer
        /// </summary>
        public Mitarbeiter CurrentUser
        {
            get { return currentUser; }
        }


        public TicketSystem()
        {
            service = new TicketrService();
            mitarbeiter = new List<Mitarbeiter>();
            kunden = new List<Kunde>();
            tickets = new List<Ticket>();
        }

        /// <summary>
        /// Loggt einen Benutzer im System ein
        /// </summary>
        /// <returns></returns>
        public bool Login(string eMail, string password)
        {
            bool authenticated = service.Login(eMail, password);
            currentUser = new Mitarbeiter(service.GetCurrentMitarbeiterDetail());
            return authenticated;
        }



    }
}
