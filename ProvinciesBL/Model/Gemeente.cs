using ProvinciesBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProvinciesBL.Model
{
    public class Gemeente
    {
        public Gemeente(string naam,List<Straat> data)
        {
            Naam = naam;
            if (data == null || data.Count == 0) throw new ProvincieException("straten niet ok");
            foreach (var g in data) VoegStraatToe(g);
        }

        int? Id { get; set; }
        public string Naam { get; set; }
        private List<Straat> straten = new();
        public IReadOnlyList<Straat> Straten => straten;
        public void VoegStraatToe(Straat straat)
        {
            if (straat == null) throw new ProvincieException("straat is null");
            straten.Add(straat);
        }
    }
}
