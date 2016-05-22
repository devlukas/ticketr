using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketr.Schnittstellen.Dto
{
    public class Kunde
    {
        public int Id { get; set; }

        public DateTime ErstellDatum { get; set; }

        public Person Person { get; set; }
    }
}
