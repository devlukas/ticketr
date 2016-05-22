using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketr.Schnittstellen.Dto
{
    public class Kategorie
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Beschreibung { get; set; }

        public List<Kategorie> SubKategorien { get; set; }
    }
}
