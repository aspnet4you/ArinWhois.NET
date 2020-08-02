using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArinWhois.Model
{
    public class Country
    {
        [JsonProperty("code2")]
        public ValueWrapper<string> Code2 { get; set; }

        [JsonProperty("code3")]
        public ValueWrapper<string> Code3 { get; set; }

        [JsonProperty("name")]
        public ValueWrapper<string> Name { get; set; }

        [JsonProperty("e164")]
        public ValueWrapper<string> E164 { get; set; }
    }
}
