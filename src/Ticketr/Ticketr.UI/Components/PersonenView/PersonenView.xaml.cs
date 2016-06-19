using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ticketr.Businesslogik;

namespace Ticketr.UI.Components
{
    /// <summary>
    /// Interaction logic for KundenView.xaml
    /// </summary>
    public partial class PersonenView : UserControl
    {
        public PersonenView()
        {
            InitializeComponent();
        }

        private void PersonLöschenButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddPersonButton_Click(object sender, RoutedEventArgs e)
        {
            PersonenViewModel personenViewModel = (PersonenViewModel) ((Button) sender).DataContext;

            personenViewModel.DashboardViewModel.OpenEditPersonView();
            if (personenViewModel is KundenViewModel)
            {
                personenViewModel.DashboardViewModel.EditPersonViewModel.Person = new Kunde();
            }
            else if (personenViewModel is PersonenViewModel)
            {
                personenViewModel.DashboardViewModel.EditPersonViewModel.Person = new Mitarbeiter();
            }
        }

 
        private void EditPersonButton_Click(object sender, RoutedEventArgs e)
        {
            PersonViewModel personViewModel = (PersonViewModel)((ListBoxItem)sender).DataContext;

            personViewModel.PersonenViewModel.DashboardViewModel.OpenEditPersonView();
            if (personViewModel is KundeViewModel)
            {
                KundeViewModel kundeViewModel = (KundeViewModel) personViewModel;
                kundeViewModel.PersonenViewModel.DashboardViewModel.EditPersonViewModel.Person = kundeViewModel.Kunde;

            }
            else if(personViewModel is MitarbeiterViewModel)
            {
                MitarbeiterViewModel mitarbeiterViewModel = (MitarbeiterViewModel)personViewModel;
                mitarbeiterViewModel.PersonenViewModel.DashboardViewModel.EditPersonViewModel.Person = mitarbeiterViewModel.Mitarbeiter;
            }
            
        }
    }
}
