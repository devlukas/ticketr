using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketr.Schnittstellen.Dto
{
    public class Kommentar
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime ErstellDatum { get; set; }

        public Mitarbeiter Mitarbeiter { get; set; }
    }
}
