using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Schnittstellen.Dto;

namespace Ticketr.Businesslogik
{
    public class Kommentar
    {
        private Schnittstellen.Dto.Kommentar kommentar;

        private Mitarbeiter verfasser;

        private string text;

        private DateTime datum;


        public Kommentar(Schnittstellen.Dto.Kommentar kommentar)
        {
            this.kommentar = kommentar;
            this.verfasser = new Mitarbeiter(kommentar.Mitarbeiter);
            this.text = kommentar.Text;
            this.datum = kommentar.ErstellDatum;
        }


        public Mitarbeiter Verfasser
        {
            get { return verfasser; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public DateTime Datum
        {
            get { return datum; }
        }
    }
}
