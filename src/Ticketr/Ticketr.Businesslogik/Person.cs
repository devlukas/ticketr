using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Schnittstellen;

namespace Ticketr.Businesslogik
{
    public class Person
    {
        //-----------------Members-------------------

        #region Members
        /// <summary>
        /// Id der Person
        /// </summary>
        private int id;

        /// <summary>
        /// Name der Person
        /// </summary>
        private string name;

        /// <summary>
        /// Vorname der Person
        /// </summary>
        private string vorname;

        /// <summary>
        /// Email der Person
        /// </summary>
        private string eMail;

        /// <summary>
        /// Telefon der Person
        /// </summary>
        private string telefon;

        /// <summary>
        /// Erstelldatum der Person
        /// </summary>
        private DateTime erstellDatum;

        /// <summary>
        /// letztes Anederungsdatum der Person
        /// </summary>
        private DateTime aenderungsDatum;

        private byte[] profilePicture;

        #endregion

        //-----------------Properties----------------

        #region Properties

        /// <summary>
        /// Id der Person
        /// </summary>
        public int PersonId
        {
            get { return id; }
        }

        /// <summary>
        /// Name der Person
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Vorname der Person
        /// </summary>
        public string Vorname
        {
            get { return vorname; }
            set { vorname = value; }
        }

        /// <summary>
        /// Email der Person
        /// </summary>
        public string EMail
        {
            get { return eMail; }
            set { eMail = value; }
        }
        /// <summary>
        /// Telefon der Person
        /// </summary>
        public string Telefon
        {
            get { return telefon; }
            set { telefon = value; }
        }
        /// <summary>
        /// Erstelldatum der Person
        /// </summary>
        public DateTime PersonErstellDatum
        {
            get { return erstellDatum; }
        }

        /// <summary>
        /// letztes Anederungsdatum der Person
        /// </summary>
        public DateTime PersonAenderungsDatum
        {
            get { return aenderungsDatum; }
        }

        public string FullName
        {
            get { return String.Format("{0} {1}", Vorname, Name); }
        }


        #endregion


        public Person(Schnittstellen.Dto.Person person)
        {
            this.id = person.Id;
            this.name = person.Name;
            this.vorname = person.Vorname;
            this.eMail = person.EMail;
            this.aenderungsDatum = person.AenderungsDatum;
            this.erstellDatum = person.ErstellDatum;
            this.telefon = person.Telefon;

        }

        private Task<byte[]> pictureLoader;

        /// <summary>
        /// Gibt das Profilbild für die Person zurück
        /// </summary>
        public async Task<byte[]> GetProfilePicture()
        {
            if (pictureLoader == null && profilePicture == null)
            {
                pictureLoader = TicketSystem.Service.GetProfilePicture(id);
                profilePicture = await pictureLoader;
            }

            if (pictureLoader.Status == TaskStatus.WaitingForActivation)
            {
                profilePicture = await pictureLoader;
            }

            return profilePicture;
        }

        /// <summary>
        /// Lädt das angegebene Profilbild hoch
        /// </summary>
        /// <param name="pictureFile"></param>
        public void UploadProfilePicture(string fileName)
        {
            //TODO Der Serivce kann das bereits, es existiert aber noch keine Client-Methode

            throw new NotImplementedException();
        }

    }
}
