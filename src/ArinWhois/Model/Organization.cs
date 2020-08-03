using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArinWhois.Model
{
    public class Organization
    {
        public Org org { get; set; }
    }

    public class Org
    {
        [JsonProperty("@termsOfUse")]
        public string TermsOfUse { get; set; }

        [JsonProperty("@copyrightNotice")]
        public string CopyrightNotice { get; set; }

        [JsonProperty("registrationDate")]
        public ValueWrapper<DateTime> RegistrationDate { get; set; }

        [JsonProperty("handle")]
        public ValueWrapper<string> Handle { get; set; }

        [JsonProperty("name")]
        public ValueWrapper<string> Name { get; set; }

        [JsonProperty("postalCode")]
        public ValueWrapper<string> PostalCode { get; set; }

        [JsonProperty("city")]
        public ValueWrapper<string> City { get; set; }

        [JsonProperty("updateDate")]
        public ValueWrapper<DateTime> UpdateDate { get; set; }

        [JsonProperty("rdapRef")]
        public ValueWrapper<string> RdapRef { get; set; }

        [JsonProperty("ref")]
        public ValueWrapper<string> Ref { get; set; }

        [JsonProperty("iso3166-1")]
        public Country IsoCounty { get; set; }

        [JsonProperty("iso3166-2")]
        public ValueWrapper<string> StateOrProvince { get; set; }

        [JsonProperty("streetAddress")]
        public Streetaddress StreetAddr { get; set; }

    }
}