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
        private TicketrService service;

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




        public TicketSystem()
        {
            service = new TicketrService();
            mitarbeiter = new List<Mitarbeiter>();
        }

        
    }
}
