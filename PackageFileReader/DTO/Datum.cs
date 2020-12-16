using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageFileReader
{
    public class Datum
    {
        [JsonProperty("@id")]
        public string Id { get; set; }
        [JsonProperty("@type")]
        public string Type { get; set; }
        public string registration { get; set; }
        public string id { get; set; }
        public string version { get; set; }
        public string description { get; set; }
        public string summary { get; set; }
        public string title { get; set; }
        public string iconUrl { get; set; }
        public string licenseUrl { get; set; }
        public string projectUrl { get; set; }
        public List<string> tags { get; set; }
        public List<string> authors { get; set; }
        public int totalDownloads { get; set; }
        public bool verified { get; set; }
        public List<PackageType> packageTypes { get; set; }
        public List<Version> versions { get; set; }
    }
}
