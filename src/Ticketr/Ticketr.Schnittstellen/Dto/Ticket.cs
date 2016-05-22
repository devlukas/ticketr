using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketr.Schnittstellen.Dto
{
    public class Ticket
    {
        public int Id { get; set; }

        public string Bezeichnung { get; set; }

        public string Beschreibung { get; set; }

        public int KommentarCount { get; set; }

        public DateTime ErstellDatum { get; set; }

        public DateTime AenderungsDatum { get; set; }

        public bool Abgeschlossen { get; set; }

        public Kategorie Kategorie { get; set; }

        public Kunde Kunde { get; set; }

        public Mitarbeiter Bearbeiter { get; set; }

        public Prioritaet Prioritaet { get; set; }

        public List<Kommentar> Kommentare { get; set; }

    }
}
