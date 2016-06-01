using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Businesslogik;
using Ticketr.UI.Models;

namespace Ticketr.UI.Components.EditTicketView
{
    /// <summary>
    /// Representiert das ViewModel für die EditTicketUserControl.xaml View
    /// </summary>
    public class EditTicketViewModel : ViewModel
    {

        public EditTicketViewModel()
        {
            mitarbeiter = App.TicketSystem.Mitarbeiter;
            App.TicketSystem.ReloadKategorien();
            foreach (Kategorie kategorie in App.TicketSystem.Kategorien)
            {
                kategorien.Add(new KategorieViewModel(kategorie, false));
                foreach (Kategorie subKategorie in kategorie.SubKategorien)
                {
                    kategorien.Add(new KategorieViewModel(subKategorie, true));
                }
            }
            
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

        private List<Mitarbeiter> mitarbeiter;
    }
}
