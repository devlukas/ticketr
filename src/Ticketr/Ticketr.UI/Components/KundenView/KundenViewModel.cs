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
        private string searchField;

        public string SearchField
        {
            get
            {
                return searchField;
            }
            set
            { 
                searchField = value; 
            }
        }
        public async Task LoadItems()
        {
            IsLoading = true;
            await App.TicketSystem.ReloadKunden();
            Kunden = new ObservableCollection<KundeViewModel>(App.TicketSystem.Kunden.Select(k => new KundeViewModel(k, this)));
            IsLoading = false;
        }

        private bool isLoading;
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

        public KundenViewModel(DashboardViewModel dashboardViewModel)
        {
            this.dashboardViewModel = dashboardViewModel;
        }

        public ObservableCollection<KundeViewModel> Kunden
        {
            get { return kunden; }
            private set
            {
                kunden = value;
                RaisePropertyChanged("Kunden");
            }
        }

        /// <summary>
        /// Gibt das DashboardViewModel zurück
        /// </summary>
        public DashboardViewModel DashboardViewModel
        {
            get { return dashboardViewModel; }
        }
    }
}
