using ProvinciesBL.Interfaces;
using ProvinciesDL_File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProvinciesUtil
{
    public static class ProvincieBestandslezerFactory
    {
        public static IProvincieBestandslezer GeefBestandslezer()
        {
            return new ProvincieBestandslezer();
        }
    }
}
