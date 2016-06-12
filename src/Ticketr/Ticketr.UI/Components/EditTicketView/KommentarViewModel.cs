using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Businesslogik;
using Ticketr.UI.Models;

namespace Ticketr.UI.Components.EditTicketView
{
    public class KommentarViewModel : ViewModel
    {
        private Kommentar kommentar;

        public KommentarViewModel(Kommentar kommentar)
        {
            this.kommentar = kommentar;
            LoadPic();
        }


        public string Text
        {
            get { return kommentar.Text; }
            set { kommentar.Text = value; }
        }

        public string Erstelldatum
        {
            get { return this.kommentar.Datum.ToString("dd.MM.yyyy HH:mm:ss"); }
        }

        public string Verfasser
        {
            get { return this.kommentar.Verfasser.FullName; }
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

        public Mitarbeiter Mitarbeiter
        {
            get { return this.kommentar.Verfasser; }
        }

        public Kommentar Kommentar
        {
            get { return kommentar; }
        }

        public int PersonId
        {
            get { return this.Kommentar.Verfasser.PersonId; }
        }
    }
}
