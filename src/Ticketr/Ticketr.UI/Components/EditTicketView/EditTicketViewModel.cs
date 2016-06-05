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

            LoadCategories();

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
            }

            RaisePropertyChanged("Kategorien");
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
        public List<Mitarbeiter> Mitarbeiter
        {
            get
            {
                return mitarbeiter;
            }
        }

        private readonly List<Mitarbeiter> mitarbeiter;

        /// <summary>
        /// Gibt alle Kunden zurück
        /// </summary>
        public List<Kunde> Kunden
        {
            get
            {
                return kunden;
            }
        }

        private readonly List<Kunde> kunden;
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
        /// Gibt den Selektierten Mitarbeiter zurück und legt diesen fest.
        /// </summary>
        public Mitarbeiter SelectedMitarbeiter { get; set; }

        /// <summary>
        /// Gibt den selektieren Kunde zurück und legt diesen fest.
        /// </summary>
        public Kunde SelectedKunde { get; set; }

        public DashboardViewModel DashboardViewModel
        {
            get { return dashboardViewModel; }
        }

    }
}
