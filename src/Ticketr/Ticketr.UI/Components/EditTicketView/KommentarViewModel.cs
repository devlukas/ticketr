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
            LoadProfilePicture();
        }

        public async void LoadProfilePicture()
        {
            profilePicture = await this.kommentar.Verfasser.GetProfilePicture();
            RaisePropertyChanged("ProfilePicture");
        }

        public string Text
        {
            get { return kommentar.Text; }
            set { kommentar.Text = value; }
        }

        public DateTime Erstelldatum
        {
            get { return this.kommentar.Datum; }
        }

        public string Verfasser
        {
            get { return this.kommentar.Verfasser.FullName; }
        }

        private byte[] profilePicture;

        public byte[] ProfilePicture
        {
            get { return profilePicture; }
        }

        public Kommentar Kommentar
        {
            get { return kommentar; }
        }
    }
}
