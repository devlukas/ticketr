using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketr.Businesslogik
{
    public class TicketHistory
    {
        private DateTime datum;

        private Mitarbeiter mitarbeiter;

        public TicketHistory(Schnittstellen.Dto.History history)
        {
            datum = history.Datum;
            mitarbeiter = new Mitarbeiter(history.Mitarbeiter);
        }

        public Mitarbeiter Mitarbeiter
        {
            get { return mitarbeiter; }
        }

        public DateTime Datum
        {
            get { return datum; }
        }
    }
}
