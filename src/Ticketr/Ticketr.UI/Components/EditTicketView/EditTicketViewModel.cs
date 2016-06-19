using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Businesslogik;
using Ticketr.UI.Components.Dashboard;
using Ticketr.UI.Components.TicketTable;
using Ticketr.UI.Models;

namespace Ticketr.UI.Components.EditTicketView
{
    /// <summary>
    /// Representiert das ViewModel für die EditTicketUserControl.xaml View
    /// </summary>
    public class EditTicketViewModel : ViewModel
    {
        private Ticket ticket = new Ticket();

        private List<Task> loaderTasks = new List<Task>();

        /// <summary>
        /// Initialsiert das EditTicketViewModel
        /// </summary>
        public EditTicketViewModel()
        {
            Loading = true;
            
            loaderTasks.Add(LoadKunden());
            loaderTasks.Add(LoadMitarbeiter());
            loaderTasks.Add(LoadCategories());

            Task complete = Task.WhenAll(loaderTasks);

            //Wenn alle Tasks geladen sind, wird der View Signalisiert, das sie jetzt alles anzeigen kann
            complete.ContinueWith(a => Loading = false);
        }

        public EditTicketViewModel(int ticketId)
        {
            Loading = true;

            loaderTasks.Add(LoadKunden());
            loaderTasks.Add(LoadMitarbeiter());
            loaderTasks.Add(LoadCategories());
            loaderTasks.Add(LoadTicket(ticketId));

            Task complete = Task.WhenAll(loaderTasks);

            //Wenn alle Tasks geladen sind, wird der View Signalisiert, das sie jetzt alles anzeigen kann
            complete.ContinueWith(a =>
            {
                Loading = false;
                LoadMitarbeiterPicturesAsync();
                RaisePropertyChanged("Kategorien");
            });

            
        }

        private string fehler;
        public string Fehler
        {
            get { return fehler; }
            set
            {
                fehler = value;
                RaisePropertyChanged("Fehler");
            }
        }

        public async Task LoadTicket(int ticketId)
        {
            this.ticket = await App.TicketSystem.GetTicketDetail(ticketId);
            RaisePropertyChanged("");
        }


        public async Task LoadCategories()
        {
            await App.TicketSystem.ReloadKategorien();
            foreach (Kategorie kategorie in App.TicketSystem.Kategorien)
            {
                kategorien.Add(new KategorieViewModel(kategorie, false));
                foreach (Kategorie subKategorie in kategorie.SubKategorien)
                {
                    kategorien.Add(new KategorieViewModel(subKategorie, true));
                }
                
            RaisePropertyChanged("Kategorien");
            RaisePropertyChanged("SelectedKategorie");

            }

        }

        public async Task LoadMitarbeiter()
        {
            await App.TicketSystem.ReloadMitarbeiter();
            this.mitarbeiter = App.TicketSystem.Mitarbeiter.Select(m => new PersonDropdownItemViewModel
            {
                Id = m.PersonId,
                MitarbeiterId = m.Id,
                Name = m.Name,
                Vorname = m.Vorname
            }).ToList();

            RaisePropertyChanged("Mitarbeiter");
            RaisePropertyChanged("SelectedMitarbeiter");

            //Wenn neues Ticket
            if (Id == 0)
            {
                //Automatisch aktuellen Benutzer setzen
                SetCurrentUserAsBearbeiter();
            }

            //Ladet die Profilbilder der Mitarbeiter im Hintergrund asynchron ins Dropdown
            LoadMitarbeiterPicturesAsync();
        }

        public void SetCurrentUserAsBearbeiter()
        {
            SelectedMitarbeiter = mitarbeiter.FirstOrDefault(m => m.MitarbeiterId == App.TicketSystem.CurrentUser.Id);
            RaisePropertyChanged("SelectedMitarbeiter");
        }

        public async Task LoadKunden()
        {
            await App.TicketSystem.ReloadKunden();

            this.kunden = App.TicketSystem.Kunden.Select(k => new PersonDropdownItemViewModel
            {
                Id = k.PersonId,
                Name = k.Name,
                Vorname = k.Vorname,
                KundeId = k.Id
            }).ToList();

            RaisePropertyChanged("Kunden");

            RaisePropertyChanged("SelectedKunde");
        }

        private bool loading;

        public bool Loading
        {
            get { return loading; }
            set
            {
                loading = value;
                RaisePropertyChanged("Loading");
            }
        }

        public bool StatusEnabled
        {
            get { return Id > 0; }
        }

        /// <summary>
        /// Gibt alle Prioritäten zurück
        /// </summary>
        public IEnumerable<Prioritaet> Prioritäten
        {
            get
            {
                return Enum.GetValues(typeof(Prioritaet)).Cast<Prioritaet>();
            }
        }

        private List<KategorieViewModel> kategorien = new List<KategorieViewModel>();

        /// <summary>
        /// Gibt alle Kategorien und Subkategorien zurück
        /// </summary>
        /// <remarks>
        /// Es folgt die Kategorie, danach folgen alle Subkategorien.
        /// </remarks>
        public List<KategorieViewModel> Kategorien
        {
            get
            {
                return kategorien;
            }
        }

        public List<string> AllStatus
        {
            get { return new List<string> { "Abgeschlossen", "Offen"}; }
        }

        public string SelectedStatus
        {
            get { return this.ticket.Abgeschlossen ? "Abgeschlossen" : "Offen"; }
            set { this.ticket.Abgeschlossen = (value == "Abgeschlossen"); }
        }

        /// <summary>
        /// Gibt die selektierte Priorität, des Users, zurück und legt diese fest.
        /// </summary>
        public Prioritaet SelectedPriority
        {
            get { return this.ticket.Prioritaet; }
            set { this.ticket.Prioritaet = value; }
        }
        /// <summary>
        /// Gibt alle Mitarbeiter zurück
        /// </summary>
        public List<PersonDropdownItemViewModel> Mitarbeiter
        {
            get
            {
                return mitarbeiter;
            }
        }

        private List<PersonDropdownItemViewModel> mitarbeiter;


        /// <summary>
        /// Ladet die Profilbilder der Mitarbeiter im Hintergrund asynchron ins Dropdown
        /// </summary>
        public async void LoadMitarbeiterPicturesAsync()
        {
            if (mitarbeiter != null)
            {
                foreach (PersonDropdownItemViewModel person in mitarbeiter)
                {
                    try
                    {
                        person.ProfilePicture =
                        await App.TicketSystem.Mitarbeiter.FirstOrDefault(mt => mt.PersonId == person.Id).GetProfilePicture();
                        person.RaisePropertyChanged("ProfilePicture");

                    }
                    catch (Exception) { }

                }
            }

        }

        /// <summary>
        /// Gibt alle Kunden zurück
        /// </summary>
        public List<PersonDropdownItemViewModel> Kunden
        {
            get { return kunden; }
        }

        private List<PersonDropdownItemViewModel> kunden;


        public string SiteTitle
        {
            get { return Id > 0 ? "Ticket Bearbeiten" : "Ticket Erstellen"; }
        }


        public int Id
        {
            get { return this.ticket.Id; }
        }


        /// <summary>
        /// Gibt die Selektierte Kategorie zurück und legt diese fest.
        /// </summary>
        public KategorieViewModel SelectedKategorie {
            get
            {
                if (this.kategorien != null && this.ticket.Kategorie != null)
                {
                    return this.kategorien.FirstOrDefault(k => k.Id == this.ticket.Kategorie.Id);
                }

                return null;
            }
            set
            {
                if (value != null)
                {
                    this.ticket.Kategorie = App.TicketSystem.Kategorien.SelectMany(k => k.SubKategorien).FirstOrDefault(k => k.Id == value.Id);
                }

            }
        }

        /// <summary>
        /// Gibt den Titel zurück und legt diesen fest.
        /// </summary>
        public string Titel
        {
            get { return this.ticket.Bezeichnung; }
            set { this.ticket.Bezeichnung = value; }
        }

        /// <summary>
        /// Gibt die Beschreibung zurück und legt diese fest.
        /// </summary>
        public string Beschreibung
        {
            get { return this.ticket.Beschreibung; }
            set { this.ticket.Beschreibung = value; }
        }

        /// <summary>
        /// Gibt die Loesung zurück oder legt dieses fest
        /// </summary>
        public string Loesung
        {
            get { return this.ticket.Loesung; }
            set { this.ticket.Loesung = value; }
        }

        public string Erstelldatum
        {
            get { return this.ticket.ErstellDatum.ToString("dd.MM.yyyy HH:mm:ss"); }
        }

        public string Aenderungsdatum
        {
            get { return this.ticket.AenderungsDatum.ToString("dd.MM.yyyy HH:mm:ss"); }
        }

        public List<KommentarViewModel> Kommentare
        {
            get
            {
                if (this.ticket.Kommentare != null)
                {
                    List<KommentarViewModel> comments = this.ticket.Kommentare.Select(k => new KommentarViewModel(k)).ToList();
                    return comments;
                }
                return null;
            }
        }

        public List<HistoryViewModel> History
        {
            get
            {
                if (this.ticket.Histories != null)
                {
                    List<HistoryViewModel> histories = this.ticket.Histories.OrderByDescending(h => h.Datum).Select(h => new HistoryViewModel(h)).ToList();
                    return histories;
                }
                return null;
            }
        }


        /// <summary>
        /// Gibt den Selektierten Mitarbeiter zurück und legt diesen fest.
        /// </summary>
        public PersonDropdownItemViewModel SelectedMitarbeiter {
            get
            {
                if (this.mitarbeiter != null && this.ticket.Bearbeiter != null)
                {
                    return this.mitarbeiter.FirstOrDefault(k => k.MitarbeiterId == this.ticket.Bearbeiter.Id);
                }

                return null;
            }
            set { this.ticket.Bearbeiter = App.TicketSystem.Mitarbeiter.FirstOrDefault(k => k.Id == value.MitarbeiterId); }
        }

        /// <summary>
        /// Gibt den selektieren Kunde zurück und legt diesen fest.
        /// </summary>
        public PersonDropdownItemViewModel SelectedKunde
        {
            get
            {
                if (this.kunden != null && this.ticket.Kunde != null)
                {
                    return this.kunden.FirstOrDefault(k => k.KundeId == this.ticket.Kunde.Id);
                }

                return null;
            }
            set { this.ticket.Kunde = App.TicketSystem.Kunden.FirstOrDefault(k => k.Id == value.KundeId); }
        }

        public async Task SaveTicket()
        {
            string fehler = "";
            fehler += SelectedKunde == null ? "Kunde " : "";
            fehler += SelectedKategorie == null ? "Kategorie " : "";
            fehler += SelectedMitarbeiter == null ? "Mitarbeiter " : "";
            fehler += String.IsNullOrEmpty(Titel) ? "Titel " : "";

            if (fehler == "")
            {
                this.Loading = true;
                int ticketId = await App.TicketSystem.SaveTicket(ticket);

                DashboardViewModel dashboardViewModel = App.MainWindowViewModel.SelectedViewModel as DashboardViewModel;
                dashboardViewModel.EditTicketViewModel = new EditTicketViewModel(ticketId);
            }
            else
            {
                Fehler = "Fehlende Angaben: " + fehler;
            }



            this.Loading = false;
        }

        public async Task DeleteTicket()
        {
            this.Loading = true;
            await App.TicketSystem.RemoveTicket(Id);
            this.Loading = false;

            DashboardViewModel dashboardViewModel = App.MainWindowViewModel.SelectedViewModel as DashboardViewModel;
            dashboardViewModel.OpenTicketMenu();

        }

        public string Kommentar { get; set; }



        public async Task AddComment()
        {
            this.Loading = true;
            await ticket.AddKommentar(new Kommentar
            {
                Text = Kommentar
            });
            this.Loading = false;

            DashboardViewModel dashboardViewModel = App.MainWindowViewModel.SelectedViewModel as DashboardViewModel;
            //Reload Ticket Page
            if (dashboardViewModel != null)
                dashboardViewModel.EditTicketViewModel = new EditTicketViewModel(Id);


        }

        public async Task RemoveComment(int id)
        {
            this.Loading = true;
            await ticket.DeleteKommentar(id);
            this.Loading = false;
            DashboardViewModel dashboardViewModel = App.MainWindowViewModel.SelectedViewModel as DashboardViewModel;

            //Reload Ticket Page
            if (dashboardViewModel != null)
                dashboardViewModel.EditTicketViewModel = new EditTicketViewModel(Id);
        }


    }
}
