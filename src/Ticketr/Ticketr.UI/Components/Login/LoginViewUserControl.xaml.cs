using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Ticketr.UI.Components.Dashboard;
using Ticketr.UI.Components.Login;

namespace Ticketr.UI.Components
{
    /// <summary>
    /// Interaction logic for LoginViewUserControl.xaml
    /// </summary>
    public partial class LoginViewUserControl : UserControl
    {
        public LoginViewUserControl()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            Login(sender);
        }


        private async void Login(object sender)
        {
            LoginViewModel loginViewModel = (LoginViewModel)((Button)sender).DataContext;

            string userPassword = PasswordTextBox.Password;
            try
            {
                bool login = await loginViewModel.Login(userPassword);
                if (login)
                {
                    DashboardViewModel dash = new DashboardViewModel();
                    App.MainWindowViewModel.SetSelectedView(dash, new DashboardViewUserControl());
                    dash.OpenTicketMenu();
                }
                else
                {
                    loginViewModel.ErrorMessage =
                        "Das Passwort und die E-Mail-Adresse, die Sie eingegeben haben, stimmen nicht überein.";
                }

            }
            catch (Exception)
            {
                if (loginViewModel.CheckForInternetConnection())
                {
                    loginViewModel.ErrorMessage =
                        "Der Ticketr Service ist nicht erreichbar. Versuchen Sie es später wieder.";
                }
                else
                {
                    loginViewModel.ErrorMessage = "Sie haben keine Interneverbindung. Sie können sich nur einloggen, wenn Sie eine Internetverbindung haben";
                }
            }
        }
    }
}
