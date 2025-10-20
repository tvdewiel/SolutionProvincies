using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvinciesDL_File.Model
{
    internal class ProvincieDL
    {
        public ProvincieDL(string naam)
        {
            Naam = naam;
        }

        public string Naam {  get; set; }
        public Dictionary<int,GemeenteDL> Gemeentes { get; set; } = new();
    }
}
