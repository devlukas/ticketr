using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Kunden = App.TicketSystem.Kunden.Select(k => new KundeViewModel(k)).ToList();
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

        public List<KundeViewModel> Kunden { get; private set; }
    }
}
