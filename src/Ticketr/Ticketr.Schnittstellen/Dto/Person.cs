using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketr.Schnittstellen.Dto
{
    public class Person
    {
        public int Id { get; set; }

        public string Vorname { get; set; }

        public string Name { get; set; }

        public string EMail { get; set; }

        public string Telefon { get; set; }

        public DateTime ErstellDatum { get; set; }

        public DateTime AenderungsDatum { get; set; }
    }
}
