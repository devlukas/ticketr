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

        public bool HasProfilePicture
        {
            get { return ProfilePicture != null && ProfilePicture.Length != 0; }
        }

        public string PersonInitials
        {
            get { return String.Format("{0}{1}", Vorname.ToUpper()[0], Name.ToUpper()[0]); }
        }

        public int MitarbeiterId { get; set; }

        public int KundeId { get; set; }
    }
}
