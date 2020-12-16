using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PackageFileReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> nugDict = GetCurrentDictionary(ConfigurationManager.AppSettings["NugetFilePath"].ToString());
            List<string> versionList = GetVersionList(nugDict, ConfigurationManager.AppSettings["SolutionPath"].ToString());
            CreateCSV(versionList, ConfigurationManager.AppSettings["OutPutPath"].ToString());
        }

        private static List<string> GetVersionList(Dictionary<string, string> nugDict, string PackageFilePath)
        {
            string[] files = Directory.GetFiles(PackageFilePath, "packages.config", SearchOption.AllDirectories);

            List<string> csv = new List<string>();
            HashSet<string> set = new HashSet<string>();
            foreach (string file in files)
            {
                var xml = XDocument.Load(file);

                var elements = xml.Root.Descendants("package");

                foreach (XElement element in elements)
                {
                    var id = element.Attribute(XName.Get("id", "")).Value;
                    var version = element.Attribute(XName.Get("version", "")).Value;
                    var targetFramework = element.Attribute(XName.Get("targetFramework", "")).Value;
                    var latestVersion = string.Empty;
                    if (nugDict.ContainsKey(id.ToLower()))
                        latestVersion = nugDict[id.ToLower()];

                    var patharr = file.Split('\\');
                    var path = patharr[patharr.Length - 2];

                    if (set.Contains(id) || id == null)
                        continue;

                    if (id != null && id.Length > 6 && id.Substring(0, 6).ToLower() == "system")
                        continue;
                    set.Add(id);
                    csv.Add(id + "," + version + "," + targetFramework + "," + path + "," + latestVersion);
                }
            }
            return csv;
        }

        private static void CreateCSV(List<string> csv, string CsvFilePath)
        {
            File.WriteAllLines(CsvFilePath, csv);
        }

        private static Dictionary<string, string> GetCurrentDictionary(string JsonFilePath)
        {
            string jsonFile = File.ReadAllText(JsonFilePath);
            var json = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonFile);
            Root root = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(jsonFile);
            Dictionary<string, string> nugDict = new Dictionary<string, string>();

            foreach (var data in root.data)
            {
                var keyarr = data.id.Split('/');
                var key = keyarr[0].ToLower();
                if (!nugDict.ContainsKey(key))
                    nugDict.Add(key, data.version);
            }

            return nugDict;
        }
    }
}
