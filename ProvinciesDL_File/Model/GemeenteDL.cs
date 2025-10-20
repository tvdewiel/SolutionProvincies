using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvinciesDL_File.Model
{
    internal class GemeenteDL
    {
        public GemeenteDL(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public List<string> Straten { get; set; } = new();
    }
}
