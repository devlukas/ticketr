using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Ticketr.UI.Components.Menu
{
    /// <summary>
    /// Representiert das View Model für das Menu User Control
    /// </summary>
    public class MenuViewModel
    {

        /// <summary>
        /// Gibt den Vornamen und Namen im Format "{Vorname} {Name}" zurück.
        /// </summary>
        public string FormattedUserName
        {
            get
            {
                return string.Format("{0} {1}", App.TicketSystem.CurrentUser.Vorname, App.TicketSystem.CurrentUser.Name);
            }
        }
        /// <summary>
        /// Gibt das Benutzerbild zurück, in dem der Ticketr Service angesprochen wird.
        /// </summary>
        public byte[] UserImage
        {
            get { return App.TicketSystem.CurrentUser.GetProfilePicture(); }
        } 
    }
}
