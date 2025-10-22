using ProvinciesBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvinciesBL.Interfaces
{
    public interface IProvincieBestandslezer
    {
        List<string> GeefInhoudZip(string fileName);
        List<Provincie> LeesBestanden(string folder, List<string> bestandsNamen);
    }
}
