using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.UI.Components.Dashboard;
using Ticketr.UI.Models;

namespace Ticketr.UI.Components
{
    public class KundenViewModel : ViewModel
    {
        /// <summary>
        /// Initialisiert das KundeViewModel
        /// </summary>
        /// <param name="dashboardViewModel"></param>
        public KundenViewModel(DashboardViewModel dashboardViewModel)
        {
            this.dashboardViewModel = dashboardViewModel;
        }

        private string searchField;
        /// <summary>
        /// Gibt den Sucherwert zurück und legt diesen fest und filtert die Kunden damit
        /// </summary>
        public string SearchField
        {
            get
            {
                return searchField;
            }
            set
            {
                searchField = value;
                RaisePropertyChanged("FilteredKunden");
            }
        }

        /// <summary>
        /// Ladet alle relevanten Datne
        /// </summary>
        /// <returns>Task</returns>
        public async Task LoadItems()
        {
            IsLoading = true;
            await App.TicketSystem.ReloadKunden();
            Kunden = new ObservableCollection<KundeViewModel>(App.TicketSystem.Kunden.Select(k => new KundeViewModel(k, this)));
            IsLoading = false;
        }

        private bool isLoading;
        /// <summary>
        /// Gibt zurück ob es am Daten laden ist.
        /// </summary>
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        private ObservableCollection<KundeViewModel> kunden;
        private DashboardViewModel dashboardViewModel;
        
        /// <summary>
        /// Gibt alle KundenViewModel zurück
        /// </summary>
        public ObservableCollection<KundeViewModel> Kunden
        {
            get { return kunden; }
            private set
            {
                kunden = value;
                RaisePropertyChanged("Kunden");
                RaisePropertyChanged("FilteredKunden");
            }
        }

        /// <summary>
        /// Gibt die Kunden mit dem Filter zurück
        /// </summary>
        public ObservableCollection<KundeViewModel> FilteredKunden
        {
            get
            {
                ObservableCollection<KundeViewModel> filteredKunden;
                if (!string.IsNullOrEmpty(SearchField))
                {
                    filteredKunden = new ObservableCollection<KundeViewModel>(kunden.Where(k => k.Name.IndexOf(searchField) == 0 || k.Vorname.IndexOf(searchField) == 0).ToList());
                }
                else
                {
                    filteredKunden = kunden;
                }
                return filteredKunden;
            }
        }

        /// <summary>
        /// Gibt das DashboardViewModel zurück
        /// </summary>
        public DashboardViewModel DashboardViewModel
        {
            get { return dashboardViewModel; }
        }
        /// <summary>
        /// Löscht den Kunde aus dem UI
        /// </summary>
        /// <param name="kundeViewModel">Das zu löschende KundeViewModel</param>
        public void RemoveKunde(KundeViewModel kundeViewModel)
        {
            Kunden.Remove(kundeViewModel);
            RaisePropertyChanged("FilteredKunden");
        }
    }
}
