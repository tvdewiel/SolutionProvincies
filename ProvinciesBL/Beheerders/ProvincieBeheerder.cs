using ProvinciesBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvinciesBL.Beheerders
{
    public class ProvincieBeheerder
    {
        private IProvincieRepository repo;
        private IProvincieBestandslezer bestandslezer;

        public ProvincieBeheerder(IProvincieRepository repo, IProvincieBestandslezer bestandslezer)
        {
            this.repo = repo;
            this.bestandslezer = bestandslezer;
        }

        public void UploadNaarDatabank(string folder, List<string> bestandsnamen)
        {
            //stap 1 lezen bestanden
            var data=bestandslezer.LeesBestanden(folder, bestandsnamen);
            //stap 2 schrijven naar databank

        }
    }
}
