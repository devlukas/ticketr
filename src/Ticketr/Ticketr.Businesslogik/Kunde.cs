using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketr.Businesslogik
{
    public class Kunde : Person
    {

        //Id des Kunden
        private int id;

        //erstellDatum des Kunden
        private DateTime erstellDatum;

        private Position position;

        public int Id
        {
            get { return id; }
        }

        public DateTime ErstellDatum
        {
            get { return erstellDatum; }
        }

        public Position Position
        {
            get { return position; }
        }

        public Kunde(Schnittstellen.Dto.Kunde kunde) 
            : base(kunde.Person)
        {
            this.id = kunde.Id;
            this.erstellDatum = kunde.ErstellDatum;
            this.position = new Position(kunde.Position);
        }

    }
}
