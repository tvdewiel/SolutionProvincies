using ProvinciesBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvinciesDL_SQL
{
    public class ProvincieRepository : IProvincieRepository
    {
        private string connectionString;

        public ProvincieRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
