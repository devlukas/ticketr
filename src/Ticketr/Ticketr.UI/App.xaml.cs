﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using Ticketr.Businesslogik;

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
            //Create new TicketSystem Obejct
            TicketSystem = new TicketSystem();
            mainWindowViewModel = new MainWindowViewModel();
        }

        /// <summary>
        /// Gibt das MainWindowViewModel zurück und legt dieses fest.
        /// </summary>
        public static MainWindowViewModel MainWindowViewModel
        {
            get { return mainWindowViewModel; }
        }
        
        /// <summary>
        /// Gibt das MainWindow zurück
        /// </summary>
        public static MainWindow MainWindow { get; set; }
        /// <summary>
        /// Gibt das TicketSystem zurück
        /// </summary>
        public static TicketSystem TicketSystem { get; set; }
    }
}
