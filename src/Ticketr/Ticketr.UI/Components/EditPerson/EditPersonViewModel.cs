using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Businesslogik;
using Ticketr.UI.Components.Dashboard;
using Ticketr.UI.Models;

namespace Ticketr.UI.Components.EditPersonView
{
    public class EditPersonViewModel : ViewModel
    {
        private Person person = new Person();
        private DashboardViewModel dashboardViewModel;

        public EditPersonViewModel(DashboardViewModel dashboardViewModel)
        {
            this.dashboardViewModel = dashboardViewModel;
        }

        public DashboardViewModel DashboardViewModel
        {
            get
            {
                return dashboardViewModel;
            }
        }
        public async Task LoadItems()
        {
            await App.TicketSystem.ReloadPositionen();
            Positionen = App.TicketSystem.Positionen;
            RaisePropertyChanged("Position");
        }
        public string Name
        {
            get { return person.Name; }
            set { person.Name = value; }
        }

        public string Vorname
        {
            get { return person.Vorname; }
            set { person.Vorname = value; }
        }

        public string EMail
        {
            get { return person.EMail; }
            set { person.EMail = value; }
        }

        public string Telefon
        {
            get { return person.Telefon; }
            set { person.Telefon = value; }
        }

        public Position Position
        {
            get
            {
                if (IsKunde)
                {
                    return Kunde.Position;
                }
                return null;
            }
            set
            {
                if (IsKunde)
                {
                    Kunde.Position = value;
                }
            }
        }

        public Kunde Kunde
        {
            get { return (Kunde)person; }
        }

        public Mitarbeiter Mitarbeiter
        {
            get { return (Mitarbeiter) person; }
        }
        /// <summary>
        /// Gibt zurück ob die Person ein Kunde ist, oder nicht
        /// </summary>
        public bool IsKunde
        {
            get { return person is Kunde; }
        }

        public Person Person
        {
            get { return person; }
            set
            {
                person = value;
                RaisePropertyChanged("IsKunde");
                RaisePropertyChanged("Person");
                RaisePropertyChanged("Telefon");
                RaisePropertyChanged("EMail");
                RaisePropertyChanged("Position");
                RaisePropertyChanged("Name");
                RaisePropertyChanged("Vorname");
            }
        }

        private List<Position> positionen = new List<Position>();
        public List<Position> Positionen
        {
            get { return positionen; }
            set
            {
                positionen = value;
                RaisePropertyChanged("Positionen");
            }
        }
    }
}
