using Newtonsoft.Json;
using System.Collections.Generic;

namespace ArinWhois.Model
{
    internal class ResponseOuter
    {
        [JsonProperty("ns4:pft")]
        public Response ResponseInner { get; set; }
    }

    public class Response
    {
        [JsonProperty("net")]
        public Network Network { get; set; }

        [JsonProperty("org")]
        public Organization Organization { get; set; }
    }
}