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
        private string name;

        private string iconName;

        /// <summary>
        /// Intiaisliert das MenuItemViewModel
        /// </summary>
        /// <param name="name">Der Anzeigename des Menupunkts</param>
        /// <param name="iconName">Das Icon, dass angezeigt werden sollte. Die Iconnamen findet man auf : https://materialdesignicons.com/</param>
        public MenuItemViewModel(string name, string iconName)
        {
            this.name = name;
            this.iconName = iconName;
        }
        /// <summary>
        /// Gibt den Anzeigenamen des Menu Items zurück
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Gibt den Icon Namen zurück. Die Iconnamen findet man auf : https://materialdesignicons.com/
        /// </summary>
        public string IconName { get
            {
                return iconName;
            }
        }
    }
}
