using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArinWhois.Model
{
    public class PointOfContact
    {
       public Poc poc { get; set; }
    }

    public class Poc
    {
        [JsonProperty("@termsOfUse")]
        public string TermsOfUse { get; set; }

        [JsonProperty("@copyrightNotice")]
        public string CopyrightNotice { get; set; }

        [JsonProperty("registrationDate")]
        public ValueWrapper<DateTime> RegistrationDate { get; set; }

        [JsonProperty("updateDate")]
        public ValueWrapper<DateTime> UpdateDate { get; set; }

        [JsonProperty("handle")]
        public ValueWrapper<string> Handle { get; set; }

        [JsonProperty("firstName")]
        public ValueWrapper<string> FirstName { get; set; }

        [JsonProperty("lastName")]
        public ValueWrapper<string> LastName { get; set; }

        [JsonProperty("companyName")]
        public ValueWrapper<string> CompanyName { get; set; }

        [JsonProperty("postalCode")]
        public ValueWrapper<string> PostalCode { get; set; }

        [JsonProperty("city")]
        public ValueWrapper<string> City { get; set; }

        [JsonProperty("iso3166-2")]
        public ValueWrapper<string> StateOrProvince { get; set; }

        [JsonProperty("iso3166-1")]
        public Country IsoCounty { get; set; }

        [JsonProperty("streetAddress")]
        public Streetaddress StreetAddr { get; set; }
    }
}
