using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.UI.Models;

namespace Ticketr.UI.Components.EditTicketView
{
    public class PersonDropdownItemViewModel : ViewModel
    {
        public int Id { get; set; }

        public byte[] ProfilePicture { get; set; }

        public string Vorname { get; set; }

        public string Name { get; set; }

        public string FullName
        {
            get { return String.Format("{0} {1}", Vorname, Name); }
        }

        public int MitarbeiterId { get; set; }

        public int KundeId { get; set; }
    }
}
