using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ticketr.Businesslogik
{
    public class Ticket
    {

        //--------------Members------------

        #region Members

        private int id;

        private string bezeichnung;

        private string beschreibung;

        private string loesung;

        private DateTime erstellDatum;

        private DateTime aenderungsDatum;

        private bool abgeschlossen;

        private Kategorie kategorie;

        private Kunde kunde;

        private Mitarbeiter bearbeiter;

        private Prioritaet prioritaet;

        private List<Kommentar> kommentare;

        #endregion

        //--------------Properties------------

        #region Properties

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string Bezeichnung
        {
            get
            {
                return bezeichnung;
            }

            set
            {
                bezeichnung = value;
            }
        }

        public string Beschreibung
        {
            get
            {
                return beschreibung;
            }

            set
            {
                beschreibung = value;
            }
        }

        public string Loesung
        {
            get
            {
                return loesung;
            }

            set
            {
                loesung = value;
            }
        }

        public DateTime ErstellDatum
        {
            get
            {
                return erstellDatum;
            }

        }

        public DateTime AenderungsDatum
        {
            get
            {
                return aenderungsDatum;
            }
        }

        public bool Abgeschlossen
        {
            get
            {
                return abgeschlossen;
            }

            set
            {
                abgeschlossen = value;
            }
        }

        public Kategorie Kategorie
        {
            get
            {
                return kategorie;
            }

            set
            {
                kategorie = value;
            }
        }

        public Kunde Kunde
        {
            get
            {
                return kunde;
            }

            set
            {
                kunde = value;
            }
        }

        public Mitarbeiter Bearbeiter
        {
            get
            {
                return bearbeiter;
            }

            set
            {
                bearbeiter = value;
            }
        }

        public Prioritaet Prioritaet
        {
            get
            {
                return prioritaet;
            }

            set
            {
                prioritaet = value;
            }
        }

        public List<Kommentar> Kommentare
        {
            get
            {
                return kommentare;
            }
        }

#endregion

        public Ticket()
        {
            
        }

        public Ticket(Schnittstellen.Dto.Ticket ticket)
        {
            prioritaet = (Prioritaet)ticket.Prioritaet;
            abgeschlossen = ticket.Abgeschlossen;
            aenderungsDatum = ticket.AenderungsDatum;
            bearbeiter = new Mitarbeiter(ticket.Bearbeiter);

            if(ticket.Kommentare != null)
            {
                kommentare = ticket.Kommentare.Select(k => new Kommentar(k)).ToList();
            }
 
            kunde = new Kunde(ticket.Kunde);
            kategorie = new Kategorie(ticket.Kategorie);
            erstellDatum = ticket.ErstellDatum;
            bezeichnung = ticket.Bezeichnung;
            beschreibung = ticket.Beschreibung;
            loesung = ticket.Loesung;
            id = ticket.Id;
        }
    }
}
