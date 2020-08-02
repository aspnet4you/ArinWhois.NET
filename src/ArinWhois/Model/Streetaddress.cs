using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArinWhois.Model
{
    public class Streetaddress
    {
        [JsonProperty("line")]
        [JsonConverter(typeof(SingleValueArrayConverter<StreeAddressLine>))]
        public StreeAddressLine[] line { get; set; }
    }

    public class StreeAddressLine
    {
        [JsonProperty("@number")]
        public string Number { get; set; }

        [JsonProperty("$")]
        public string Value { get; set; }
    }
}
