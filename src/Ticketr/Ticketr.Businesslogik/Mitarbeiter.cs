using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketr.Businesslogik
{
    public class Mitarbeiter : Person
    {
        private int id;

        private DateTime erstellDatum;

        private int assignedTicketsCount;

        public int Id
        {
            get { return id; }
        }

        public DateTime ErstellDatum
        {
            get { return erstellDatum; }
        }

        public int AssignedTicketsCount
        {
            get { return assignedTicketsCount; }
        }
        public Mitarbeiter(Schnittstellen.Dto.Mitarbeiter mitarbeiter) 
            : base(mitarbeiter.Person)
        {
            this.assignedTicketsCount = mitarbeiter.TicketsCount;
            this.erstellDatum = mitarbeiter.ErstellDatum;
            this.id = mitarbeiter.Id;
        }

        public Mitarbeiter()
        {
        }
    } 
}
