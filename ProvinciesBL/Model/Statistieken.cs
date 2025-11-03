using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvinciesBL.Model
{
    public class Statistieken
    {
        public Statistieken(List<Provincie> provincies)
        {
            ProvinciesAantalGemeenten=provincies.ToDictionary(x=>x.Naam,x=>x.Gemeentes.Count());
            GemeentenAantalStraten=provincies
                .SelectMany(x=>x.Gemeentes,(p,g)=>new {naam=$"{p.Naam},{g.Naam}",aantal=g.Straten.Count()})
                .ToDictionary(x=>x.naam,x=>x.aantal);
        }

        public IReadOnlyDictionary<string,int> ProvinciesAantalGemeenten {  get;private set; }
        public IReadOnlyDictionary<string,int> GemeentenAantalStraten { get;private set; }
    }
}
