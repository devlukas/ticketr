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


        public int Id
        {
            get { return id; }
        }

        public DateTime ErstellDatum
        {
            get { return erstellDatum; }
        }


        public Kunde(Schnittstellen.Dto.Kunde kunde) 
            : base(kunde.Person)
        {
            this.id = kunde.Id;
            this.erstellDatum = kunde.ErstellDatum;
        }

    }
}
