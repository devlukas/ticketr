using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Ticketr.UI.Models;

namespace Ticketr.UI.Components.Login
{
    /// <summary>
    /// Representiert das ViewModel für die Login View
    /// </summary>
    public class LoginViewModel : ViewModel
    {
        private string errorMessage;

        private string email;

        /// <summary>
        /// Gibt die Email zurück und legt diese fest
        /// </summary>
        public string Email
        {
            get { return email; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Email muss angegeben werden");
                }
                email = value;
            }
        }


        private bool loginInProcess;

        public Visibility LoginProcessVisibility
        {
            get { return loginInProcess ? Visibility.Visible : Visibility.Collapsed; }
        }

        public bool LoginButtonEnabled
        {
            get { return !loginInProcess; }
        }

        /// <summary>
        /// Loggt den User ein mit dem angegeben Passwort und dem Email Property des LoginViewModels ein.
        /// <see cref="Email"/>
        /// </summary>
        /// <param name="password">Das Passwort des Users</param>
        /// <returns>
        /// Ob das Login funktioniert hat</returns>
        public async Task<bool> Login(string password)
        {
            LoginInProcess = true;
            return await App.TicketSystem.Login(Email, password);
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

        public bool LoginInProcess
        {
            get { return loginInProcess; }
            set
            {
                loginInProcess = value;
                RaisePropertyChanged("LoginInProcess");
                RaisePropertyChanged("LoginButtonEnabled");
                RaisePropertyChanged("LoginProcessVisibility");
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
