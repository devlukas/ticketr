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
        /// Erstelldatum der Person
        /// </summary>
        private DateTime erstellDatum;

        /// <summary>
        /// letztes Anederungsdatum der Person
        /// </summary>
        private DateTime aenderungsDatum;

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

        #endregion


        public Person(Schnittstellen.Dto.Person person)
        {
            this.id = person.Id;
            this.name = person.Name;
            this.vorname = person.Vorname;
            this.eMail = person.EMail;
            this.aenderungsDatum = person.AenderungsDatum;
            this.erstellDatum = person.ErstellDatum;
        }

        /// <summary>
        /// Gibt das Profilbild für die Person zurück
        /// </summary>
        public byte[] ProfilePicture
        {
            get
            {
                return TicketSystem.Service.GetProfilePicture(id);
            }
        }

        /// <summary>
        /// Lädt das angegebene Profilbild hoch
        /// </summary>
        /// <param name="pictureFile"></param>
        public void UploadProfilePicture(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
