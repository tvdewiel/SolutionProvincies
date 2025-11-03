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
        void CleanFolder(string folderName);
        List<string> GeefInhoudZip(string fileName);
        bool IsFolderEmpty(string folderName);
        List<Provincie> LeesBestanden(string folder, List<string> bestandsNamen);
        void Unzip(string zipFile, string outputFolder);
    }
}
