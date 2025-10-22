using ProvinciesBL.Interfaces;
using ProvinciesBL.Model;
using ProvinciesDL_File.Model;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvinciesDL_File
{
    public class ProvincieBestandslezer : IProvincieBestandslezer
    {
        private List<Provincie> Converteer(List<ProvincieDL> data)
        {
            List<Provincie> provincies = new();
            foreach (ProvincieDL provincie in data)
            {
                List<Gemeente> gemeentes = new();
                foreach(GemeenteDL gemeente in provincie.Gemeentes.Values)
                {
                    Gemeente g = new(gemeente.Naam,gemeente.Straten.Select(x=>new Straat(x)).ToList());
                    gemeentes.Add(g);
                }
                provincies.Add(new Provincie(provincie.Naam, gemeentes));
            }
            return provincies;
        }
        public List<Provincie> LeesBestanden(string folder,List<string> bestandsNamen)
        {
            HashSet<int> provincieIds = new();
            Dictionary<int, ProvincieDL> provincies = new();
            Dictionary<int, GemeenteDL> gemeentes = new();
            Dictionary<int, string> straten = new();
            using (StreamReader sr = new StreamReader(Path.Combine(folder, bestandsNamen[0])))
            {
                string[] line = sr.ReadLine().Split(',');
                foreach (string s in line) { provincieIds.Add(int.Parse(s)); }
            }
            using (StreamReader sr = new StreamReader(Path.Combine(folder, bestandsNamen[1])))
            {
                sr.ReadLine();
                string line;
                while((line=sr.ReadLine()) != null)
                {
                    string[] ss= line.Split(";");
                    int gemeenteId=int.Parse(ss[0]);
                    int provincieId=int.Parse(ss[1]);
                    string taalCode=ss[2];
                    string provincienaam=ss[3];
                    if (taalCode != "nl" || !provincieIds.Contains(provincieId)) continue;
                    if (!provincies.ContainsKey(provincieId))
                    {
                        provincies.Add(provincieId, new ProvincieDL(provincienaam));
                    }
                    //zit gemeente al in provincie ?
                    if (!provincies[provincieId].Gemeentes.ContainsKey(gemeenteId))
                    {
                        provincies[provincieId].Gemeentes.Add(gemeenteId,new GemeenteDL(gemeenteId));
                        gemeentes.Add(gemeenteId, provincies[provincieId].Gemeentes[gemeenteId]);
                    }
                }
            }
            using (StreamReader sr = new StreamReader(Path.Combine(folder, bestandsNamen[2])))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] ss = line.Split(';');
                    int gemeenteid = int.Parse(ss[1]);
                    string taalcode = ss[2];
                    string gemeentenaam = ss[3];
                    if (taalcode == "nl" && gemeentes.ContainsKey(gemeenteid))
                    {
                        gemeentes[gemeenteid].Naam = gemeentenaam;
                    }
                }
            }
            using (StreamReader sr = new StreamReader(Path.Combine(folder, bestandsNamen[4])))
            {
                sr.ReadLine();
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] ss = line.Split(";");
                    int id=int.Parse(ss[0]);
                    string naam=ss[1];
                    straten.Add(id, naam);
                }
            }
            using (StreamReader sr = new StreamReader(Path.Combine(folder, bestandsNamen[3])))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] ss = line.Split(";");
                    int straatid = int.Parse(ss[0]);
                    int gemeenteid=int.Parse(ss[1]);
                    if (gemeentes.ContainsKey(gemeenteid) && straten.ContainsKey(straatid))
                    {
                        gemeentes[gemeenteid].Straten.Add(straten[straatid]);
                    }
                }
            }
            return Converteer(provincies.Values.ToList());
        }
        public List<string> GeefInhoudZip(string fileName)
        {
            using (var zipFile = ZipFile.OpenRead(fileName))
            {
                return zipFile.Entries.Select(x=>x.FullName).ToList();
            }
        }
    }
}
