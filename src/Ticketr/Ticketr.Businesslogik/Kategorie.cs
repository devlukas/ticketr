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
        /// Die Über-Kategorie, falss SubKAtegorie
        /// </summary>
        private Kategorie parent;

        public Kategorie(Schnittstellen.Dto.Kategorie kategorie)
        {
            this.name = kategorie.Name;
            this.beschreibung = kategorie.Beschreibung;
            this.id = kategorie.Id;

            if (kategorie.SubKategorien != null)
            {
                this.subKategorien = kategorie.SubKategorien.Select(k => new Kategorie(k, this)).ToList();
            }
        }

        public Kategorie(Schnittstellen.Dto.Kategorie kategorie, Kategorie parent) : this(kategorie)
        {
            this.parent = parent;
        }

        public int Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Beschreibung
        {
            get { return beschreibung; }
            set { beschreibung = value; }
        }

        public List<Kategorie> SubKategorien
        {
            get { return subKategorien; }
        }

        public Kategorie Parent
        {
            get { return parent; }
        }
    }
}
