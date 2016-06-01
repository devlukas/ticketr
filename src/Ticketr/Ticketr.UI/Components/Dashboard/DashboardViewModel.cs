﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Ticketr.UI.Components.EditTicketView;
using Ticketr.UI.Models;

namespace Ticketr.UI.Components.Dashboard
{
    /// <summary>
    /// Representiert das Viewmodel für DashboardViewUserControl.xaml
    /// </summary>
    public class DashboardViewModel : ViewModel
    {
        /// <summary>
        /// Initialisiert das DashboardViewModel
        /// </summary>
        public DashboardViewModel()
        {
            editTicketViewModel = new EditTicketViewModel(this);
        }
        /// <summary>
        /// Öffnet die TickketView
        /// </summary>
        public void OpenTicketMenu()
        {
            TicketViewWidth = "*";
            BenutzerViewWidth = "0";
            EditTicketViewWidth = "0";
        }
        /// <summary>
        /// Öffnet die Benutzerview
        /// </summary>
        public void OpenBenutzerMenu()
        {
            TicketViewWidth = "0";
            EditTicketViewWidth = "0";
            BenutzerViewWidth = "*";
        }
        /// <summary>
        /// Öffnet die EditTicketView
        /// </summary>
        public void OpenEditTicketView()
        {
            TicketViewWidth = "0";
            BenutzerViewWidth = "0";
            EditTicketViewWidth = "*";
        }
        private string ticketViewWidth;
        /// <summary>
        /// Gibt die Breite der TicketView zurück und legt diese fest
        /// </summary>
        public string TicketViewWidth
        {
            get { return ticketViewWidth; }
            private set
            {
                ticketViewWidth = value;
                RaisePropertyChanged("TicketViewWidth");
            }
        }

        private string benutzerViewWidth;
        /// <summary>
        /// Gibt die Breite der BenutzerColumn zurück und legt diese fest
        /// </summary>
        public string BenutzerViewWidth
        {
            get { return benutzerViewWidth; }
            private set
            {
                benutzerViewWidth = value;
                RaisePropertyChanged("BenutzerViewWidth");
            }
        }
        private string editTicketViewWidth;
        /// <summary>
        /// Gibt die Breite der EditTicketColumn zurück und legt diese fest
        /// </summary>
        public string EditTicketViewWidth
        {
            get { return editTicketViewWidth; }
            private set
            {
                editTicketViewWidth = value;
                RaisePropertyChanged("EditTicketViewWidth");
            }
        }
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
            get { return App.TicketSystem.CurrentUser.ProfilePicture; }
        }

        private EditTicketViewModel editTicketViewModel;
        
        /// <summary>
        /// Gibt das EditTicketViewModel zurück
        /// </summary>
        public EditTicketViewModel EditTicketViewModel
        {
            get { return editTicketViewModel; }
        }
    }
}
