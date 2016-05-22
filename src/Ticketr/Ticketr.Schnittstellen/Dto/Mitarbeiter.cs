using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketr.Schnittstellen.Dto
{
    public class Mitarbeiter
    {
        public int Id { get; set; }

        public DateTime ErstellDatum { get; set; }

        public string Passwort { get; set; }

        public Person Person { get; set; }

        public int TicketsCount { get; set; }
    }
}
