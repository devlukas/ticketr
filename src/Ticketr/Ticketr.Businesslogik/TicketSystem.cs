using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Schnittstellen;

namespace Ticketr.Businesslogik
{
    public class TicketSystem
    {
        /// <summary>
        /// Die Referenz auf den Ticketr-Service
        /// </summary>
        private static TicketrService service;

        /// <summary>
        /// Alle Mitarbeiter des TicketSystems
        /// </summary>
        private List<Mitarbeiter> mitarbeiter;

        /// <summary>
        /// Alle Kunden des TicketSystems
        /// </summary>
        private List<Kunde> kunden;

        /// <summary>
        /// Alle Tickets, die das System zurzeit hat
        /// </summary>
        private List<Ticket> tickets;

        /// <summary>
        /// Alle Kategorien, welche möglich sind
        /// </summary>
        private List<Kategorie> kategorien;

        /// <summary>
        /// Alle Positionen
        /// </summary>
        private List<Position> positionen;

        /// <summary>
        /// Der aktuell eingeloggte Benutzer
        /// </summary>
        private Mitarbeiter currentUser;


        /// <summary>
        /// Der Service des Ticket-Systems
        /// </summary>
        public static TicketrService Service
        {
            get
            {
                if (service == null)
                {
                    service = new TicketrService();
                }
                return service;
            }
        }

        /// <summary>
        /// Der aktuell eingeloggte Benutzer
        /// </summary>
        public Mitarbeiter CurrentUser
        {
            get { return currentUser; }
        }

        #region Properties

        /// <summary>
        /// Alle Mitarbeiter des TicketSystems
        /// </summary>
        public List<Mitarbeiter> Mitarbeiter
        {
            get
            {
                return mitarbeiter;
            }
        }

        /// <summary>
        /// Alle Kunden des TicketSystems
        /// </summary>
        public List<Kunde> Kunden
        {
            get
            {
                return kunden;
            }
        }

        /// <summary>
        /// Alle Tickets, die das System zurzeit hat
        /// </summary>
        public List<Ticket> Tickets
        {
            get
            {
                return tickets;
            }
        }

        /// <summary>
        /// Alle Kategorien, welche möglich sind
        /// </summary>
        public List<Kategorie> Kategorien
        {
            get { return kategorien; }
        }

        public List<Position> Positionen
        {
            get
            {
                return positionen;
            }
        }
#endregion 


        public TicketSystem()
        {
            service = new TicketrService();
            mitarbeiter = new List<Mitarbeiter>();
            kunden = new List<Kunde>();
            tickets = new List<Ticket>();
            positionen = new List<Position>();
        }


        /// <summary>
        /// Loggt einen Benutzer im System ein
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Login(string eMail, string password)
        {
            bool authenticated = await service.Login(eMail, password);

            if (authenticated)
            {
                currentUser = new Mitarbeiter(await service.GetCurrentMitarbeiterDetail());
            }

            return authenticated;
        }

        /// <summary>
        /// Lädt alle Tickets neu
        /// </summary>
        public async Task ReloadTickets()
        {
            this.tickets = (await service.GetTickets()).Select(t => new Ticket(t)).ToList();
        }

        /// <summary>
        /// Lädt alle Mitarbeiter neu
        /// </summary>
        public async Task ReloadMitarbeiter()
        {
            List<Schnittstellen.Dto.Mitarbeiter> loadedMitarbeiter = await service.GetAllMitarbeiter();
            this.mitarbeiter = loadedMitarbeiter.Select(m => new Mitarbeiter(m)).ToList();
        }

        /// <summary>
        /// Lädt alle Kunden neu
        /// </summary>
        public async Task ReloadKunden()
        {
            this.kunden = (await service.GetAllKunden()).Select(k => new Kunde(k)).ToList();
        }

        public async Task ReloadKategorien()
        {
            this.kategorien = (await service.GetKategorien()).Select(k => new Kategorie(k)).ToList();
        }

        public async Task ReloadPositionen()
        {
            this.positionen = (await service.GetPositionen()).Select(p => new Position(p)).ToList();
        }

        /// <summary>
        /// Gibt ein Ticket für die Detailansicht zurück
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Ticket> GetTicketDetail(int id)
        {
            Schnittstellen.Dto.Ticket ticket = await service.GetTicketDetail(id);
            return new Ticket(ticket);
        }


        /// <summary>
        /// Speichert ein Ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public async Task<int> SaveTicket(Ticket ticket)
        {
            var ticketDto = new Schnittstellen.Dto.Ticket
            {
                Id = ticket.Id,
                Abgeschlossen = ticket.Abgeschlossen,
                Bearbeiter = new Schnittstellen.Dto.Mitarbeiter
                {
                    Id = ticket.Bearbeiter.Id
                },
                Bezeichnung = ticket.Bezeichnung,
                Beschreibung = ticket.Beschreibung,
                Kategorie = new Schnittstellen.Dto.Kategorie
                {
                    Id = ticket.Kategorie.Id
                },
                Kunde =  new Schnittstellen.Dto.Kunde()
                {
                    Id =  ticket.Kunde.Id
                },
                Prioritaet = (Schnittstellen.Dto.Prioritaet)ticket.Prioritaet

            };

            int id = ticket.Id;

            //Wenn neues Ticket -> neues hinzufügen
            if (ticket.Id == 0)
            {
                //Neue Id zurückgeben
                id = await service.AddTicket(ticketDto);
            }
            else //Sonst -> update
            {
                await service.UpdateTicket(ticketDto);
            }

            return id;
        }


        /// <summary>
        /// Löscht das angegebene Ticket
        /// </summary>
        /// <param name="id"></param>
        public void RemoveTicket(int id)
        {
            service.DeleteTicket(id);
        }

        /// <summary>
        /// Löscht den Kunde mit der angegebenen Id
        /// </summary>
        /// <param name="kundeId">Die Id des Kunden</param>
        public void RemoveKunde(int kundeId)
        {
            service.DeleteKunde(kundeId);
        }

        /// <summary>
        /// Löscht den Kommentar mit der angegebenen Id
        /// </summary>
        /// <param name="kommentarId">Die Id des Kommentares</param>
        public void RemoveKommentar(int kommentarId)
        {
            service.DeleteKommentar(kommentarId);
        }

        /// <summary>
        /// Löscht den Mitarbeiter mit der angegebenen Id
        /// </summary>
        /// <param name="mitarbeiterId">Die Id des Mitarbeiters</param>
        public void RemoveMitarbeiter(int mitarbeiterId)
        {
            service.DeleteMitarbeiter(mitarbeiterId);
        }

        /// <summary>
        /// Fügt den angegebene Kunde dem System hinzu oder aktualisiert ihn.
        /// </summary>
        /// <param name="kunde">Kunde, der hinzugefügt werden sollte</param>
        /// <returns>Gibt die neu erstellte Id des Kunden zurück</returns>
        public int SaveKunde(Kunde kunde)
        {
            Ticketr.Schnittstellen.Dto.Kunde kundeDto = new Ticketr.Schnittstellen.Dto.Kunde()
            { 
                ErstellDatum = DateTime.Now, 
                Person = new Ticketr.Schnittstellen.Dto.Person(){
                    EMail = kunde.EMail,
                    ErstellDatum = DateTime.Now,
                    Name = kunde.Name,
                    Telefon = kunde.Telefon
                },
                Position = new Ticketr.Schnittstellen.Dto.Position()
                {
                    Name = kunde.Position.Name
                }
            };

            int id = kunde.Id;

            if (id == 0)
            {
                id = service.AddKunde(kundeDto);
            }
            else
            {
                // Todo : Update Kunde
            }

            return id;
        }
    }
}
