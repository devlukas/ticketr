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

        public Kommentar(Schnittstellen.Dto.Kommentar kommentar)
        {
            this.kommentar = kommentar;
        }
    }
}
