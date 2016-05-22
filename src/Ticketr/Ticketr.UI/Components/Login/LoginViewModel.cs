using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Ticketr.UI.Models;

namespace Ticketr.UI.Components.Login
{
    /// <summary>
    /// Representiert das ViewModel für die Login View
    /// </summary>
    public class LoginViewModel : ViewModel
    {
        private string errorMessage;


        /// <summary>
        /// Gibt die Email zurück und legt diese fest
        /// </summary>

        public string Email { get; set; }

        /// <summary>
        /// Loggt den User ein mit dem angegeben Passwort und dem Email Property des LoginViewModels ein.
        /// <see cref="Email"/>
        /// </summary>
        /// <param name="password">Das Passwort des Users</param>
        /// <returns>
        /// Ob das Login funktioniert hat</returns>
        public bool Login(string password)
        {
            return App.MainWindowViewModel.Login(Email, password);
        }

        /// <summary>
        /// Gibt die Fehlermeldung zurück und legt diese fest
        /// </summary>
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                RaisePropertyChanged("ErrorMessage");
            }
        }

        /// <summary>
        /// Gibt zurück und der Benutzer eine Internetverbindung hat, indem man google.com anpingt.
        /// </summary>
        /// <returns>Ob der Benutzer eine Internetverbindug hat</returns>
        public bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
