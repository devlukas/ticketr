using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketr.Businesslogik
{
    public class Position
    {
        
        public Position(Schnittstellen.Dto.Position position)
        {
            this.id = id;
            this.name = name;
        }

        private int id;
        public int Id { 
            get { return id; } 
        }

        private string name;
        public string Name { 
            get { return name; } 
        }
    }
}
