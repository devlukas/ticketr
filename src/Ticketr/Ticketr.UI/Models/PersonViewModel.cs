using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Ticketr.Businesslogik;
using Ticketr.UI.Models;

namespace Ticketr.UI.Components
{
    public abstract class PersonViewModel : ViewModel
    {
        private Person person;
        private PersonenViewModel personenViewModel;

        public PersonViewModel(Person person, PersonenViewModel personenViewModel)
        {
            this.person = person;
            this.personenViewModel = personenViewModel;
        }

        /// <summary>
        /// Gibt den Formatierten Namen im Format {Vorname} {Name} zurück
        /// </summary>
        public string FormattedName
        {
            get { return string.Format("{0} {1}", person.Vorname, person.Name); }
        }

        /// <summary>
        /// Gibt den Namen des Kunde zurück
        /// </summary>
        public string Name
        {
            get
            {
                return person.Name;
            }
        }
        /// <summary>
        /// Gibt den Vorname des Kunde zurück
        /// </summary>
        public string Vorname
        {
            get
            {
                return person.Vorname;
            }
        }



        /// <summary>
        /// Gibt das PersonenViewModel zurück
        /// </summary>
        public PersonenViewModel PersonenViewModel
        {
            get
            {
                return personenViewModel;
            }
        }

        public abstract void Remove();
    }
}
