using ProvinciesBL.Interfaces;
using ProvinciesDL_SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvinciesUtil
{
    public static class ProvincieRepositoryFactory
    {
        public static IProvincieRepository GeefRepository(string connectionString)
        {
            return new ProvincieRepository(connectionString);
        }
    }
}
