using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArinWhois.Model
{
    public class Company
    {
        public Customer Customer { get; set; }
    }
    public class Customer
    {
        [JsonProperty("@termsOfUse")]
        public string TermsOfUse { get; set; }

        [JsonProperty("@copyrightNotice")]
        public string CopyrightNotice { get; set; }

        [JsonProperty("registrationDate")]
        public ValueWrapper<DateTime> RegistrationDate { get; set; }

        [JsonProperty("name")]
        public ValueWrapper<string> Name { get; set; }

        [JsonProperty("postalCode")]
        public ValueWrapper<string> PostalCode { get; set; }

        [JsonProperty("city")]
        public ValueWrapper<string> City { get; set; }

        [JsonProperty("handle")]
        public ValueWrapper<string> Handle { get; set; }

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


        [JsonProperty("updateDate")]
        public ValueWrapper<DateTime> UpdateDate { get; set; }

        [JsonProperty("nets")]
        public Nets Nets { get; set; }

        [JsonProperty("parentOrgRef")]
        public Parentorgref ParentOrgRef { get; set; }
    }



    public class Nets
    {
        public Netref netRef { get; set; }
    }


    public class Netref
    {
        [JsonProperty("@handle")]
        public string Handle { get; set; }

        [JsonProperty("@name")]
        public string Name { get; set; }

        [JsonProperty("@startAddress")]
        public string StartAddress { get; set; }

        [JsonProperty("@endAddress")]
        public string EndAddress { get; set; }

        [JsonProperty("$")]
        public string Ref { get; set; }
    }

    public class Parentorgref
    {
        [JsonProperty("@handle")]
        public string Handle { get; set; }

        [JsonProperty("@name")]
        public string Name { get; set; }

        [JsonProperty("$")]
        public string Ref { get; set; }

    }
}
