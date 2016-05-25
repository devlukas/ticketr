using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Businesslogik;

namespace Ticketr.UI.Components.TicketTableItem
{
    /// <summary>
    /// Representiert ein Ticket für die Tabelle
    /// </summary>
    public class TicketTableItemViewModel
    {
        private Ticket ticket;

        public TicketTableItemViewModel(Ticket ticket)
        {
            this.ticket = ticket;
        }

        public int Id
        {
            get { return this.ticket.Id; }
        }

        public string Bezeichnung
        {
            get { return this.ticket.Bezeichnung; }
        }


        public DateTime Erfassung
        {
            get
            {
                return this.ticket.ErstellDatum;
            }
        }

        public string Kunde
        {
            get
            {
                return String.Format("{0} {1}", ticket.Kunde.Name, ticket.Kunde.Vorname);
            }
        }

        public string Bearbeiter
        {
            get
            {
                return String.Format("{0} {1}", ticket.Bearbeiter.Name, ticket.Bearbeiter.Vorname);
            }
        }

        public string Kategorie
        {
            get { return this.ticket.Kategorie.Name; }
        }
    }
}
