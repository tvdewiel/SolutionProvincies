using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvinciesBL.Model
{
    public class Straat
    {
        public Straat(string naam)
        {
            Naam = naam;
        }

        int? Id { get; set; }
        public string Naam { get; set; }
    }
}
