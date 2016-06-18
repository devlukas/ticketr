using MaterialDesignThemes.Wpf;
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
        private string[] monate = new string[12] {"Jan","Feb","März","April","Mai","Juni","Juli","Aug","Okt","Sep","Nov","Dez"};
        public TicketTableItemViewModel(Ticket ticket)
        {
            this.ticket = ticket;
        }

        public PackIconKind StatusIcon
        {
            get
            {
                return this.ticket.Abgeschlossen ? PackIconKind.Check : PackIconKind.AlertOutline;
            }
        }

        public int Id
        {
            get { return this.ticket.Id; }
        }

        public string Bezeichnung
        {
            get { return this.ticket.Bezeichnung; }
        }

        public string Beschreibung
        {
            get { return this.ticket.Beschreibung; }
        }

        public string KurzBeschreibung
        {
            get { return ticket.Beschreibung.Length <= 100 ? ticket.Beschreibung : ticket.Beschreibung.Substring(0, 120) + " .."; }
        }

        public string KundeInitials
        {
            get { return String.Format("{0}{1}", ticket.Kunde.Vorname.ToUpper()[0], ticket.Kunde.Name.ToUpper()[0]); }
        }
        public string Erfassung
        {
            get
            {
                return this.ticket.ErstellDatum.ToString("dd.MM.yyyy HH:mm:ss");
            }
        }
        public string ErfassungFormatiert
        {
            get
            {
                return string.Format("{0} {1}", monate[ticket.ErstellDatum.Month].ToUpper(), this.ticket.ErstellDatum.ToString("dd"));
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

        public string ParentKategorie
        {
            get { return this.ticket.Kategorie.Parent != null ? this.ticket.Kategorie.Parent.Name : ""; }
        }

    }
}
