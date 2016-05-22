using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ticketr.Schnittstellen.Dto;

namespace Ticketr.Schnittstellen
{
    public class TicketrService
    {
        private const string apiUrl = "http://ticketr.weblu.ch/service/";

        private Uri BaseUrl
        {
            get
            {
                return new Uri(apiUrl);
            }
        }

        private WebClient webClient = null;


        public bool Authorized
        {
            get { return Authorize(); }
        }


        public TicketrService()
        {
            webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
        }


        /// <summary>
        /// Logt einen Benutzer anhand dem Angegebenen Passwort und Benutzernamen ein
        /// </summary>
        /// <param name="eMail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(string eMail, string password)
        {
            webClient.Credentials = new NetworkCredential(eMail, password);
            return Authorize();
        }

        /// <summary>
        /// Gibt alle vorhandenen Mitarbeiter zurück
        /// </summary>
        /// <returns></returns>
        public List<Mitarbeiter> GetAllMitarbeiter()
        {
            string response = webClient.DownloadString(new Uri(BaseUrl, "getAllMitarbeiter"));
            return JsonConvert.DeserializeObject<List<Mitarbeiter>>(response);
        }

        /// <summary>
        /// Gibt alle vorhandenen Kunden zurück
        /// </summary>
        /// <returns></returns>
        public List<Kunde> GetAllKunden()
        {
            string response = webClient.DownloadString(new Uri(BaseUrl, "getAllKunden"));
            return JsonConvert.DeserializeObject<List<Kunde>>(response);
        }

        /// <summary>
        /// Gibt (momentan noch :D ) alle Tickets zurück
        /// </summary>
        /// <returns></returns>
        public List<Ticket> GetTickets()
        {
            string response = webClient.DownloadString(new Uri(BaseUrl, "getTickets"));
            return JsonConvert.DeserializeObject<List<Ticket>>(response);
        }

        /// <summary>
        /// Gibt die DetailInfomationen eines Tickets zurück
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Ticket GetTicketDetail(int id)
        {
            string response = webClient.DownloadString(new Uri(BaseUrl, String.Format("getTicketDetail?id={0}", id)));
            return JsonConvert.DeserializeObject<Ticket>(response);
        }


        /// <summary>
        /// Gibt die Detailinformationen eines Mitarbeiters zurück
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Mitarbeiter GetMitarbeiterDetail(int id)
        {
            string response = webClient.DownloadString(new Uri(BaseUrl, String.Format("getMitarbeiterDetail?id={0}", id)));
            return JsonConvert.DeserializeObject<Mitarbeiter>(response);
        }

        /// <summary>
        /// Fügt einen neuen Mitarbeiter hinzu
        /// </summary>
        /// <param name="mitarbeiter"></param>
        public int AddMitarbeiter(Mitarbeiter mitarbeiter)
        {
            //sets all property names to lowercase (otherwise PHP dont understand)
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new LowercaseContractResolver();

            string data = JsonConvert.SerializeObject(mitarbeiter, settings);
            string response = webClient.UploadString(new Uri(BaseUrl, "addMitarbeiter"), data);

            //Gets the Id of the created Ticket
            dynamic added = JsonConvert.DeserializeObject<dynamic>(response);
            return added.mitarbeiterId;
        }


        /// <summary>
        ///Fügt einen neuen Kudnen hinzu
        /// </summary>
        /// <param name="kunde"></param>
        public int AddKunde(Kunde kunde)
        {
            //sets all property names to lowercase (otherwise PHP dont understand)
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new LowercaseContractResolver();

            string data = JsonConvert.SerializeObject(kunde, settings);
            string response = webClient.UploadString(new Uri(BaseUrl, "addKunde"), data);

            //Gets the Id of the created Ticket
            dynamic added = JsonConvert.DeserializeObject<dynamic>(response);
            return added.kundeId;
        }


        /// <summary>
        /// Gibt die Detailinformationen des angemeldeten Mitarbeiters zurück
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Mitarbeiter GetCurrentMitarbeiterDetail()
        {
            string response = webClient.DownloadString(new Uri(BaseUrl, "getCurrentMitarbeiterDetail"));
            return JsonConvert.DeserializeObject<Mitarbeiter>(response);
        }


        /// <summary>
        /// Gibt alle möglichen Kategorien zurück
        /// </summary>
        /// <returns></returns>
        public List<Kategorie> GetKategorien()
        {
            string response = webClient.DownloadString(new Uri(BaseUrl, "getKategorien"));
            return JsonConvert.DeserializeObject<List<Kategorie>>(response);
        }


        /// <summary>
        /// Fügt ein Ticket im System hinzu und gibt die Id des erstellten Tickets zurück
        /// </summary>
        public int AddTicket(Ticket ticket)
        {
            //sets all property names to lowercase (otherwise PHP dont understand)
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new LowercaseContractResolver();

            string data = JsonConvert.SerializeObject(ticket, settings);
            string response = webClient.UploadString(new Uri(BaseUrl, "createTicket"), data);

            //Gets the Id of the created Ticket
            dynamic added = JsonConvert.DeserializeObject<dynamic>(response);
            return added.ticketId;
        }

        /// <summary>
        /// Updatet das angegebene Ticket
        /// </summary>
        /// <param name="ticket"></param>
        public void UpdateTicket(Ticket ticket)
        {
            //sets all property names to lowercase (otherwise PHP dont understand)
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new LowercaseContractResolver();

            string data = JsonConvert.SerializeObject(ticket, settings);
            webClient.UploadString(new Uri(BaseUrl, "updateTicket"), data);
        }


        /// <summary>
        /// Gibt die Service Infos (in HTML) zurück
        /// </summary>
        /// <returns></returns>
        public string GetServiceInformations()
        {
            return webClient.DownloadString(new Uri(BaseUrl, "info"));
        }

        /// <summary>
        /// Autorisiert den Benutzer, sofern die Credentails bereits gesetzt sind
        /// </summary>
        /// <returns></returns>
        private bool Authorize()
        {
            try
            {
                webClient.DownloadString(new Uri(BaseUrl, "info"));
            }
            catch (WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Unauthorized)
                {
                    return false;
                }
                else
                {
                    throw new Exception("Unbekannter Fehler bei der Anfrage");
                }
            }

            return true;
        }

    }
}
