using System;
using Newtonsoft.Json;

namespace ArinWhois.Model
{
    public class PointOfContacts
    {
        public Pocs Pocs { get; set; }
    }

    public class Pocs
    {
        [JsonProperty("@termsOfUse")]
        public string TermsOfUse { get; set; }

        [JsonProperty("@copyrightNotice")]
        public string CopyrightNotice { get; set; }

        [JsonProperty("pocLinkRef")]
        public Poclinkref[] PocLinkRefs { get; set; }
    }

    public class Poclinkref
    {
        [JsonProperty("@description")]
        public string Description { get; set; }

        [JsonProperty("@function")]
        public string Function { get; set; }

        [JsonProperty("@handle")]
        public string Handle { get; set; }

        [JsonProperty("$")]
        public string PocRef { get; set; }
    }

}