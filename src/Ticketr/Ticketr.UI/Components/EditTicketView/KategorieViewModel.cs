using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Businesslogik;

namespace Ticketr.UI.Components.EditTicketView
{
    /// <summary>
    /// Representiert eine Kategorie oder Subkategorie
    /// </summary>
    public class KategorieViewModel
    {
        private Kategorie kategorie;
        private bool isSubItem;

        /// <summary>
        /// Gibt das Kategorie Businessobjekt zurück.
        /// </summary>
        /// <remarks>
        /// NICHT FÜR BINDINGS VERWENDEN
        /// </remarks>
        public Kategorie Kategorie
        {
            get
            {
                return kategorie;
            }
        }
        /// <summary>
        /// Initialisitert das KategorieViewModel
        /// </summary>
        /// <param name="kategorie">Das Kategorie Businessobjekt</param>
        /// <param name="isSubItem">Ob es eine Unterkategorie ist</param>
        public KategorieViewModel(Kategorie kategorie, bool isSubItem)
        {
            this.kategorie = kategorie;
            this.isSubItem = isSubItem;
        }
        
        /// <summary>
        /// Gibt den Namen zurück der Kategorie.
        /// </summary>
        /// <remarks>
        /// Wenn es eine Subkategorie ist, hat es einen Tabulator vor dem Namen ("\t{Name}")
        /// </remarks>
        public string FormattedName
        {
            get
            {
                return isSubItem ? String.Format("    {0}", kategorie.Name) : kategorie.Name;
            }
        }
        
        /// <summary>
        /// Gibt den Namen zurück der Kategorie
        /// </summary>
        public string Name
        {
            get { return kategorie.Name; }
        }

        /// <summary>
        /// Gibt zurück ob es eine Unterkategorie ist.
        /// </summary>
        public bool IsSubItem
        {
            get
            {
                return isSubItem;
            }
        }

        /// <summary>
        /// Gibt die Id der Kategorie zurück
        /// </summary>
        public int Id
        {
            get { return kategorie.Id; }
        }

    }
}
