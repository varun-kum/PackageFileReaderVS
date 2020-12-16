using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageFileReader
{
    public class Root
    {
        [JsonProperty("@context")]
        public Context Context { get; set; }
        public int totalHits { get; set; }
        public List<Datum> data { get; set; }
    }
}
