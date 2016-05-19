using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.UI.Components.MenuItem;

namespace Ticketr.UI.Components.Menu
{
    /// <summary>
    /// Representiert das View Model für das Menu User Control
    /// </summary>
    public class MenuViewModel
    {
        /// <summary>
        /// Initialisiert das MenuViewModel und die Listen des Objekts
        /// </summary>
        public MenuViewModel()
        {
            MenuItems = new List<MenuItemViewModel>();
        }

        /// <summary>
        /// Gibt die Menupunkte zurück
        /// </summary>
        public List<MenuItemViewModel> MenuItems { get; }
    }
}
