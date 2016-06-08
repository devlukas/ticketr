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


        private NetworkCredential credentials;


        public bool Authorized
        {
            get { return Authorize(); }
        }


        /// <summary>
        /// Logt einen Benutzer anhand dem Angegebenen Passwort und Benutzernamen async ein
        /// </summary>
        /// <param name="eMail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<bool> Login(string eMail, string password)
        {
            credentials = new NetworkCredential(eMail, password);
            return AuthorizeAsync();
            
        }

        /// <summary>
        /// Gibt alle vorhandenen Mitarbeiter zurück
        /// </summary>
        /// <returns></returns>
        public async Task<List<Mitarbeiter>> GetAllMitarbeiter()
        {
            using (WebClient webClient = NewWebClient())
            {
                string response = await webClient.DownloadStringTaskAsync(new Uri(BaseUrl, "getAllMitarbeiter"));
                return JsonConvert.DeserializeObject<List<Mitarbeiter>>(response);
            }
        }

        /// <summary>
        /// Gibt alle vorhandenen Kunden zurück
        /// </summary>
        /// <returns></returns>
        public async Task<List<Kunde>> GetAllKunden()
        {
            using (WebClient webClient = NewWebClient())
            {
                string response = await webClient.DownloadStringTaskAsync(new Uri(BaseUrl, "getAllKunden"));
                return JsonConvert.DeserializeObject<List<Kunde>>(response);
            }
        }

        /// <summary>
        /// Gibt alle vorhandenen Positionen zurück
        /// </summary>
        /// <returns></returns>
        public async Task<List<Position>> GetPositionen()
        {
            using (WebClient webClient = NewWebClient())
            {
                string response = await webClient.DownloadStringTaskAsync(new Uri(BaseUrl, "getPositionen"));
                return JsonConvert.DeserializeObject<List<Position>>(response);
            }
        }

        /// <summary>
        /// Gibt (momentan noch :D ) alle Tickets zurück
        /// </summary>
        /// <returns></returns>
        public async Task<List<Ticket>> GetTickets()
        {
            using (WebClient webClient = NewWebClient())
            {
                string response = await webClient.DownloadStringTaskAsync(new Uri(BaseUrl, "getTickets"));
                return JsonConvert.DeserializeObject<List<Ticket>>(response);
            }
        }

        /// <summary>
        /// Gibt die DetailInfomationen eines Tickets zurück
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Ticket> GetTicketDetail(int id)
        {
            using (WebClient webClient = NewWebClient())
            {
                string response = await webClient.DownloadStringTaskAsync(new Uri(BaseUrl, String.Format("getTicketDetail?id={0}", id)));
                return JsonConvert.DeserializeObject<Ticket>(response);
            }
        }


        /// <summary>
        /// Gibt die Detailinformationen eines Mitarbeiters zurück
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Mitarbeiter> GetMitarbeiterDetail(int id)
        {
            using (WebClient webClient = NewWebClient())
            {
                string response = await webClient.DownloadStringTaskAsync(new Uri(BaseUrl, String.Format("getMitarbeiterDetail?id={0}", id)));
                return JsonConvert.DeserializeObject<Mitarbeiter>(response);
            }
        }

        /// <summary>
        /// Fügt einen neuen Mitarbeiter hinzu
        /// </summary>
        /// <param name="mitarbeiter"></param>
        public int AddMitarbeiter(Mitarbeiter mitarbeiter)
        {
            using (WebClient webClient = NewWebClient())
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
        }


        /// <summary>
        ///Fügt einen neuen Kudnen hinzu
        /// </summary>
        /// <param name="kunde"></param>
        public int AddKunde(Kunde kunde)
        {
            using (WebClient webClient = NewWebClient())
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
        }


        /// <summary>
        /// Gibt die Detailinformationen des angemeldeten Mitarbeiters zurück
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Mitarbeiter> GetCurrentMitarbeiterDetail()
        {
            using (WebClient webClient = NewWebClient())
            {
                string response = await webClient.DownloadStringTaskAsync(new Uri(BaseUrl, "getCurrentMitarbeiterDetail"));
                return JsonConvert.DeserializeObject<Mitarbeiter>(response);
            }
        }


        /// <summary>
        /// Gibt alle möglichen Kategorien zurück
        /// </summary>
        /// <returns></returns>
        public async Task<List<Kategorie>> GetKategorien()
        {
            using (WebClient webClient = NewWebClient())
            {
                string response = await webClient.DownloadStringTaskAsync(new Uri(BaseUrl, "getKategorien"));
                return JsonConvert.DeserializeObject<List<Kategorie>>(response);
            }
        }


        /// <summary>
        /// Fügt ein Ticket im System hinzu und gibt die Id des erstellten Tickets zurück
        /// </summary>
        public int AddTicket(Ticket ticket)
        {
            using (WebClient webClient = NewWebClient())
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
        }

        /// <summary>
        /// Updatet das angegebene Ticket
        /// </summary>
        /// <param name="ticket"></param>
        public void UpdateTicket(Ticket ticket)
        {
            using (WebClient webClient = NewWebClient())
            {
                //sets all property names to lowercase (otherwise PHP dont understand)
                var settings = new JsonSerializerSettings();
                settings.ContractResolver = new LowercaseContractResolver();

                string data = JsonConvert.SerializeObject(ticket, settings);
                webClient.UploadString(new Uri(BaseUrl, "updateTicket"), data);
            }
        }

        /// <summary>
        /// Gibt das Bild einer Person zurück
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public async Task<byte[]> GetProfilePicture(int personId)
        {
            using (WebClient webClient = NewWebClient())
            {
                return
                    await
                        webClient.DownloadDataTaskAsync(new Uri(BaseUrl,
                            String.Format("getPersonPicture?id={0}", personId)));
            }
        }


        /// <summary>
        /// Ändert das Bild einer Person
        /// </summary>
        /// <param name="profilePicture"></param>
        /// <param name="personId"></param>
        public void SetProfilePicture(byte[] profilePicture, int personId)
        {
            using (WebClient webClient = NewWebClient())
            {
                webClient.UploadData(new Uri(BaseUrl, String.Format("setPersonPicture?personId={0}", personId)),
                    profilePicture);
            }
        }

        /// <summary>
        /// Ändert die Angaben einer Person
        /// </summary>
        public void UpdatePerson(Person person)
        {
            using (WebClient webClient = NewWebClient())
            {
                //sets all property names to lowercase (otherwise PHP dont understand)
                var settings = new JsonSerializerSettings();
                settings.ContractResolver = new LowercaseContractResolver();

                string data = JsonConvert.SerializeObject(person, settings);
                webClient.UploadString(new Uri(BaseUrl, "updatePerson"), data);
            }
        }

        /// <summary>
        /// Löscht den angegebenen Kunden
        /// </summary>
        /// <param name="id"></param>
        public void DeleteKunde(int id)
        {
            using (WebClient webClient = NewWebClient())
            {
                var reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("id", id.ToString());

                webClient.UploadValues(new Uri(BaseUrl, "deleteKunde"), "POST", reqparm);
            }
        }


        /// <summary>
        /// Löscht den angegebenen Mitarbeiter
        /// </summary>
        /// <param name="id"></param>
        public void DeleteMitarbeiter(int id)
        {
            using (WebClient webClient = NewWebClient())
            {
                var reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("id", id.ToString());

                webClient.UploadValues(new Uri(BaseUrl, "deleteMitarbeiter"), "POST", reqparm);
            }
        }

        /// <summary>
        /// Löscht ein Ticket und dessen angehängte Kommentare
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTicket(int id)
        {
            using (WebClient webClient = NewWebClient())
            {
                var reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("id", id.ToString());

                webClient.UploadValues(new Uri(BaseUrl, "deleteTicket"), "POST", reqparm);
            }
        }


        /// <summary>
        /// Fügt einem Ticket ein Kommentar hinzu
        /// </summary>
        /// <param name="kommentar"></param>
        public void AddKommentar(Kommentar kommentar)
        {
            using (WebClient webClient = NewWebClient())
            {
                //sets all property names to lowercase (otherwise PHP dont understand)
                var settings = new JsonSerializerSettings();
                settings.ContractResolver = new LowercaseContractResolver();

                string data = JsonConvert.SerializeObject(kommentar, settings);
                webClient.UploadString(new Uri(BaseUrl, "addKommentar"), data);
            }
        }

        /// <summary>
        /// Löscht einen Kommentar
        /// </summary>
        /// <param name="id"></param>
        public void DeleteKommentar(int id)
        {
            using (WebClient webClient = NewWebClient())
            {
                var reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("id", id.ToString());

                webClient.UploadValues(new Uri(BaseUrl, "deleteKommentar"), "POST", reqparm);
            }
        }


        /// <summary>
        /// Gibt die Service Infos (in HTML) zurück
        /// </summary>
        /// <returns></returns>
        public string GetServiceInformations()
        {
            using (WebClient webClient = NewWebClient())
            {
                return webClient.DownloadString(new Uri(BaseUrl, "info"));
            }
        }

        /// <summary>
        /// Autorisiert den Benutzer, sofern die Credentails bereits gesetzt sind
        /// </summary>
        /// <returns></returns>
        private bool Authorize()
        {
            try
            {
                using (WebClient webClient = NewWebClient())
                {
                    webClient.DownloadString(new Uri(BaseUrl, "info"));
                }
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

        /// <summary>
        /// Autorisiert den Benutzer, sofern die Credentails bereits gesetzt sind
        /// </summary>
        /// <returns></returns>
        private async Task<bool> AuthorizeAsync()
        {
            try
            {
                using (WebClient webClient = NewWebClient())
                {
                    await webClient.DownloadStringTaskAsync(new Uri(BaseUrl, "info"));
                }
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

        private WebClient NewWebClient()
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Credentials = this.credentials;

            return client;
        }

    }
}
