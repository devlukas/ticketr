using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Businesslogik;
using Ticketr.UI.Components.Dashboard;
using Ticketr.UI.Models;

namespace Ticketr.UI.Components.EditTicketView
{
    /// <summary>
    /// Representiert das ViewModel für die EditTicketUserControl.xaml View
    /// </summary>
    public class EditTicketViewModel : ViewModel
    {
        private readonly DashboardViewModel dashboardViewModel;

        /// <summary>
        /// Initialsiert das EditTicketViewModel
        /// </summary>
        public EditTicketViewModel(DashboardViewModel dashboardViewModel)
        {
            this.loading = true;
            this.dashboardViewModel = dashboardViewModel;
            
            LoadKunden();
            LoadMitarbeiter();
            LoadCategories();
        }

        public EditTicketViewModel(DashboardViewModel dashboardViewModel, Ticket ticket) : this(dashboardViewModel)
        {
            this.Id = ticket.Id;
            this.Loesung = ticket.Loesung;
            this.Beschreibung = ticket.Beschreibung;
            this.Titel = ticket.Bezeichnung;
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
  



        public async void LoadCategories()
        {
            await App.TicketSystem.ReloadKategorien();
            foreach (Kategorie kategorie in App.TicketSystem.Kategorien)
            {
                kategorien.Add(new KategorieViewModel(kategorie, false));
                foreach (Kategorie subKategorie in kategorie.SubKategorien)
                {
                    kategorien.Add(new KategorieViewModel(subKategorie, true));
                }
            }

            RaisePropertyChanged("Kategorien");
        }

        public async void LoadMitarbeiter()
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

            SetCurrentUserAsBearbeiter();

            //Ladet die Profilbilder der Mitarbeiter im Hintergrund asynchron ins Dropdown
            LoadMitarbeiterPicturesAsync();
        }

        public void SetCurrentUserAsBearbeiter()
        {
            SelectedMitarbeiter = mitarbeiter.FirstOrDefault(m => m.MitarbeiterId == App.TicketSystem.CurrentUser.Id);
            RaisePropertyChanged("SelectedMitarbeiter");
        }

        public async void LoadKunden()
        {
            await App.TicketSystem.ReloadKunden();
            RaisePropertyChanged("Kunden");
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

        /// <summary>
        /// Gibt die selektierte Priorität, des Users, zurück und legt diese fest.
        /// </summary>
        public Prioritaet SelectedPriority
        {
            get; set;
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
            get
            {
                return App.TicketSystem.Kunden.Select(k => new PersonDropdownItemViewModel
                {
                    Id = k.PersonId,
                    Name = k.Name,
                    Vorname = k.Vorname,
                    KundeId = k.Id
                }).ToList();
            }
        }

        private readonly List<PersonDropdownItemViewModel> kunden;


        public string SiteTitle
        {
            get { return Id > 0 ? "Ticket Bearbeiten" : "Ticket Erstellen"; }
        }


        public int Id { get; set; }

        /// <summary>
        /// Gibt die Selektierte Kategorie zurück und legt diese fest.
        /// </summary>
        public KategorieViewModel SelectedKategorie { get; set; }

        /// <summary>
        /// Gibt den Titel zurück und legt diesen fest.
        /// </summary>
        public string Titel { get; set; }

        /// <summary>
        /// Gibt die Beschreibung zurück und legt diese fest.
        /// </summary>
        public string Beschreibung { get; set; }

        /// <summary>
        /// Gibt die Loesung zurück oder legt dieses fest
        /// </summary>
        public string Loesung { get; set; }

        /// <summary>
        /// Gibt den Selektierten Mitarbeiter zurück und legt diesen fest.
        /// </summary>
        public PersonDropdownItemViewModel SelectedMitarbeiter { get; set; }

        /// <summary>
        /// Gibt den selektieren Kunde zurück und legt diesen fest.
        /// </summary>
        public PersonDropdownItemViewModel SelectedKunde { get; set; }

        public DashboardViewModel DashboardViewModel
        {
            get { return dashboardViewModel; }
        }

    }
}
