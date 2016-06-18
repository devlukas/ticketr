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
    public abstract class PersonenViewModel : ViewModel
    {
        private readonly DashboardViewModel dashboardViewModel;
        /// <summary>
        /// Initialisiert das KundeViewModel
        /// </summary>
        /// <param name="dashboardViewModel"></param>
        public PersonenViewModel(DashboardViewModel dashboardViewModel)
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
                RaisePropertyChanged("FilteredPersonen");
            }
        }

        /// <summary>
        /// Ladet alle relevanten Datne
        /// </summary>
        /// <returns>Task</returns>
        public abstract Task LoadItems();
        

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





        protected abstract List<PersonViewModel> GetPersonenViewModels();

        /// <summary>
        /// Gibt alle PersonViewModels zurück
        /// </summary>
        public ObservableCollection<PersonViewModel> PersonenViewModels
        {
            get { return new ObservableCollection<PersonViewModel>(GetPersonenViewModels()); }
        }

        /// <summary>
        /// Gibt die Kunden mit dem Filter zurück
        /// </summary>
        public ObservableCollection<PersonViewModel> FilteredPersonen
        {
            get
            {
                List<PersonViewModel> personen = GetPersonenViewModels();
                personen = FilterPersonen(personen);
                return new ObservableCollection<PersonViewModel>(personen);
            }
        }

        /// <summary>
        /// Gibt das DashboardViewModel zurück
        /// </summary>
        public DashboardViewModel DashboardViewModel
        {
            get { return dashboardViewModel; }
        }

        

        private List<PersonViewModel> FilterPersonen(List<PersonViewModel> personen)
        {
            List<PersonViewModel> filteredPersonViewModels = new List<PersonViewModel>();
            if (personen != null)
            {
                if (!string.IsNullOrEmpty(SearchField))
                {
                    filteredPersonViewModels =
                        personen.Where(p => p.Name.ToLower().IndexOf(searchField.ToLower()) == 0 || p.Vorname.ToLower().IndexOf(searchField.ToLower()) == 0)
                            .ToList();
                }
                else
                {
                    filteredPersonViewModels = personen;
                }
            }

            return filteredPersonViewModels;
        }
    }
}
