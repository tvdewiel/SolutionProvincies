using ProvinciesBL.Interfaces;
using ProvinciesBL.Model;
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

        public void ClearFolder(string folderName)
        {
            bestandslezer.CleanFolder(folderName);
        }

        public List<string> GeefInhoudZip(string fileName)
        {
            return bestandslezer.GeefInhoudZip(fileName);
        }

        public bool IsFolderEmpty(string folderName)
        {
            return bestandslezer.IsFolderEmpty(folderName);
        }

        public void UnZip(string zipFile, string outputFolder)
        {
           bestandslezer.Unzip(zipFile, outputFolder);
        }

        public Statistieken UploadNaarDatabank(string folder, List<string> bestandsnamen)
        {
            //stap 1 lezen bestanden
            var data=bestandslezer.LeesBestanden(folder, bestandsnamen);
            //stap 2 schrijven naar databank
            repo.UploadToDatabase(data);
            return new Statistieken(data);
        }
    }
}
