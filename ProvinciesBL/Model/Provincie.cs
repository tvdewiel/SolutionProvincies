using ProvinciesBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvinciesBL.Model
{
    public class Provincie
    {
        public Provincie(string naam,List<Gemeente> data)
        {
            Naam = naam;
            if (data==null || data.Count == 0) throw new ProvincieException("gemeentes niet ok");
            foreach(var g in data) VoegGemeenteToe(g);
        }

        int? Id { get; set; }
        public string Naam { get; set; }
        private List<Gemeente> gemeentes =new();
        public IReadOnlyList<Gemeente> Gemeentes => gemeentes;
        public void VoegGemeenteToe(Gemeente gemeente)
        {
            if (gemeente == null) throw new ProvincieException("gemeente is null");
            gemeentes.Add(gemeente);
        }
    }
}
