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

        public string Bezeichnung
        {
            get { return this.ticket.Bezeichnung; }
        }

        public string Beschreibung
        {
            get { return this.ticket.Beschreibung; }
        }

        public string KategorieName
        {
            get { return this.ticket.Kategorie.Name; }
        }
    }
}
