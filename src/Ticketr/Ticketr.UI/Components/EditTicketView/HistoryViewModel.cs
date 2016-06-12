using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Businesslogik;
using Ticketr.UI.Models;

namespace Ticketr.UI.Components.EditTicketView
{
    public class HistoryViewModel : ViewModel
    {
        private TicketHistory history;

        public HistoryViewModel(TicketHistory history)
        {
            this.history = history;
            LoadPic();
        }

        public async void LoadProfilePicture()
        {
            profilePicture = await this.history.Mitarbeiter.GetProfilePicture();
            RaisePropertyChanged("ProfilePicture");
        }

        public string Text
        {
            get { return String.Format("{0}: hat bearbeitet.", Erstelldatum); }
        }

        public string Erstelldatum
        {
            get { return this.history.Datum.ToString("dd.MM.yyyy HH:mm:ss"); }
        }

        public string Verfasser
        {
            get { return this.history.Mitarbeiter.FullName; }
        }

        public Mitarbeiter Mitarbeiter
        {
            get { return this.history.Mitarbeiter; }
        }

        public int PersonId
        {
            get { return this.history.Mitarbeiter.PersonId; }
        }

        private byte[] profilePicture;

        public byte[] ProfilePicture
        {
            get { return profilePicture; }
            set
            {
                profilePicture = value;
                RaisePropertyChanged("ProfilePicture");
            }
        }

        public async void LoadPic()
        {
            Mitarbeiter mitarbeiter = App.TicketSystem.Mitarbeiter.FirstOrDefault(m => m.PersonId == PersonId);
            if (mitarbeiter != null)
                ProfilePicture =
                    await mitarbeiter.GetProfilePicture();
        }

    }
}
