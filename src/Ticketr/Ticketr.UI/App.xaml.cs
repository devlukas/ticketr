using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;

namespace Ticketr.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static MainWindowViewModel mainWindowViewModel;

        public App()
        {
            mainWindowViewModel = new MainWindowViewModel();
        }

        /// <summary>
        /// Gibt das MainWindowViewModel zurück und legt dieses fest.
        /// </summary>
        public static MainWindowViewModel MainWindowViewModel
        {
            get { return mainWindowViewModel; }
        }
        
        public static MainWindow MainWindow { get; set; }
    }
}
