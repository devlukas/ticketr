using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.UI.Components.Dashboard;

namespace Ticketr.UI.Components
{
    public class KundenViewModel : PersonenViewModel
    {
        private List<KundeViewModel> kunden = new List<KundeViewModel>();

        public KundenViewModel(DashboardViewModel dashboardViewModel)
            : base(dashboardViewModel)
        {
        }

        /// <summary>
        /// Gibt alle KundenViewModel zurück
        /// </summary>
        public List<KundeViewModel> Kunden
        {
            get { return kunden; }
            private set
            {
                kunden = value;
                RaisePropertyChanged("Kunden");
                RaisePropertyChanged("FilteredKunden");
            }
        }


        public override async Task LoadItems()
        {
            IsLoading = true;
            await App.TicketSystem.ReloadKunden();
            Kunden = App.TicketSystem.Kunden.Select(k => new KundeViewModel(k, this)).ToList();
            IsLoading = false;
            RaisePropertyChanged("FilteredPersonen");
        }

        /// <summary>
        /// Löscht den Kunde aus dem UI
        /// </summary>
        /// <param name="kundeViewModel">Das zu löschende KundeViewModel</param>
        public void RemoveKunde(KundeViewModel kundeViewModel)
        {
            Kunden.Remove(kundeViewModel);
            RaisePropertyChanged("FilteredPersonen");
        }

        protected override List<PersonViewModel> GetPersonenViewModels()
        {
            List<PersonViewModel> personen = new List<PersonViewModel>();
            foreach (KundeViewModel kunde in Kunden)
            {
                personen.Add(kunde);
            }
            return personen;
        }
    }
}
