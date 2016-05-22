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
                ReloadMitarbeiter();
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
                ReloadKunden();
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
                ReloadTickets();
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

#endregion


        public TicketSystem()
        {
            service = new TicketrService();
            mitarbeiter = new List<Mitarbeiter>();
            kunden = new List<Kunde>();
            tickets = new List<Ticket>();
        }


        /// <summary>
        /// Loggt einen Benutzer im System ein
        /// </summary>
        /// <returns></returns>
        public bool Login(string eMail, string password)
        {
            currentUser = new Mitarbeiter(service.GetCurrentMitarbeiterDetail());
            return service.Login(eMail, password);
        }

        /// <summary>
        /// Lädt alle Tickets neu
        /// </summary>
        public void ReloadTickets()
        {
            this.tickets = service.GetTickets().Select(t => new Ticket(t)).ToList();
        }

        /// <summary>
        /// Lädt alle Mitarbeiter neu
        /// </summary>
        public void ReloadMitarbeiter()
        {
            this.mitarbeiter = service.GetAllMitarbeiter().Select(m => new Mitarbeiter(m)).ToList();
        }

        /// <summary>
        /// Lädt alle Kunden neu
        /// </summary>
        public void ReloadKunden()
        {
            this.kunden = service.GetAllKunden().Select(k => new Kunde(k)).ToList();
        }

        public void ReloadKategorien()
        {
            this.kategorien = service.GetKategorien().Select(k => new Kategorie(k)).ToList();
        }

        /// <summary>
        /// Gibt ein Ticket für die Detailansicht zurück
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Ticket GetTicketDetail(int id)
        {
            Schnittstellen.Dto.Ticket ticket = service.GetTicketDetail(id);
            return new Ticket(ticket);
        }


        /// <summary>
        /// Speichert ein Ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public int SaveTicket(Ticket ticket)
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
                }
            };

            int id = ticket.Id;

            //Wenn neues Ticket -> neues hinzufügen
            if (ticket.Id == 0)
            {
                //Neue Id zurückgeben
                id = service.AddTicket(ticketDto);
            }
            else //Sonst -> update
            {
                service.UpdateTicket(ticketDto);
            }

            return id;
        }


        
    }
}
