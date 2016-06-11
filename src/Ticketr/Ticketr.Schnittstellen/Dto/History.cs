using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketr.Schnittstellen.Dto
{
    public class History
    {
        public DateTime Datum { get; set; }

        public Mitarbeiter Mitarbeiter { get; set; }
    }
}
