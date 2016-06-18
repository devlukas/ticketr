using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

        public string Titel
        {
            get
            {
                string titel = person is Mitarbeiter ? "Mitarbeiter " : "Kunde ";
                titel += person.PersonId == 0 ? "Erstellen" : "Bearbeiten";
                return titel;
            }
        }

        public string Name
        {
            get { return person.Name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Name muss angegebene werden");
                }
                person.Name = value;
            }
        }

        public string Vorname
        {
            get { return person.Vorname; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Vorname muss angegebenen werden");
                }
                person.Vorname = value;
            }
        }

        public string EMail
        {
            get { return person.EMail; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Email muss angegeben werden");
                }
                try
                {
                    MailAddress m = new MailAddress(value);
                }
                catch (FormatException)
                {
                    throw new ApplicationException("Ungültige Mailadresse");
                }
                person.EMail = value;
            }
        }

        public string Telefon
        {
            get { return person.Telefon; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Telefon muss angegebene werden");
                }
                person.Telefon = value;
            }
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
                RaisePropertyChanged("");
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
