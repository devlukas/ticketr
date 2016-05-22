using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketr.UI.Models
{
    /// <summary>
    /// Representiert ein ViewModel
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Aktualisiert das Property für die View
        /// </summary>
        /// <param name="change">Der Name des Property</param>
        public void RaisePropertyChanged(string change)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(change));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
