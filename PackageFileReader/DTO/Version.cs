using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageFileReader
{
    public class Version
    {
        public string version { get; set; }
        public int downloads { get; set; }
        [JsonProperty("@id")]
        public string Id { get; set; }
    }
}
