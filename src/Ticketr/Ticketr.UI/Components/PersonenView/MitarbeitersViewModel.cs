using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.UI.Components.Dashboard;

namespace Ticketr.UI.Components
{
    public class MitarbeitersViewModel : PersonenViewModel
    {
        public MitarbeitersViewModel(DashboardViewModel dashboardViewModel) : base(dashboardViewModel)
        {
        }

        public override async Task LoadItems()
        {
            IsLoading = true;
            await App.TicketSystem.ReloadMitarbeiter();
            MitarbeiterViewModels = App.TicketSystem.Mitarbeiter.Select(m => new MitarbeiterViewModel(m, this)).ToList();
            IsLoading = false;
            RaisePropertyChanged("FilteredPersonen");
        }

        protected override List<PersonViewModel> GetPersonenViewModels()
        {
            List<PersonViewModel> personen = new List<PersonViewModel>();
            foreach (MitarbeiterViewModel mitarbeiterViewModel in MitarbeiterViewModels)
            {
                personen.Add(mitarbeiterViewModel);
            }
            return personen;
        }

        /// <summary>
        /// Löscht den angegebenen Mitarbeiter aus dem UI
        /// </summary>
        /// <param name="mitarbeiterViewModel">Der Mitarbeiter</param>
        public void RemoveMitarbeiter(MitarbeiterViewModel mitarbeiterViewModel)
        {
            MitarbeiterViewModels.Remove(mitarbeiterViewModel);
            RaisePropertyChanged("FilteredPersonen");
        }

        public List<MitarbeiterViewModel> mitarbeiterViewModels = new List<MitarbeiterViewModel>();

        public List<MitarbeiterViewModel> MitarbeiterViewModels
        {
            get { return mitarbeiterViewModels; }
            set
            {
                mitarbeiterViewModels = value;
                RaisePropertyChanged("FilteredPersonen");
            }
        }
    }
}
