using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketr.Schnittstellen.Dto;

namespace Ticketr.Businesslogik
{
    /// <summary>
    /// Representiert eine Kategorie
    /// </summary>
    public class Kategorie
    {
        private int id;
        private string name;
        private string beschreibung;
        private List<Kategorie> subKategorien;
        /// <summary>
        /// Initialisiert die Kategorie und kopiert die Daten des DTO-Objekts auf das neu erstelle Kategorie Objekt.
        /// </summary>
        /// <param name="kategorie">Das Kategorie DTO Objekt</param>
        public Kategorie(Schnittstellen.Dto.Kategorie kategorie)
        {
            id = kategorie.Id;
            name = kategorie.Name;
            beschreibung = kategorie.Beschreibung;
            subKategorien = kategorie.SubKategorien.Select(sk => new Kategorie(sk)).ToList();
        }
    }
}
