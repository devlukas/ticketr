using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketr.UI.Components.MenuItem
{
    /// <summary>
    /// Representiert ein Menupunkt
    /// </summary>
    public class MenuItemViewModel
    {
        /// <summary>
        /// Intiaisliert das MenuItemViewModel
        /// </summary>
        /// <param name="name">Der Anzeigename des Menupunkts</param>
        /// <param name="iconName">Das Icon, dass angezeigt werden sollte. Die Iconnamen findet man auf : https://materialdesignicons.com/</param>
        public MenuItemViewModel(string name, string iconName)
        {
            this.Name = name;
            this.IconName = iconName;
        }
        /// <summary>
        /// Gibt den Anzeigenamen des Menu Items zurück
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gibt den Icon Namen zurück. Die Iconnamen findet man auf : https://materialdesignicons.com/
        /// </summary>
        public string IconName { get; }
    }
}
