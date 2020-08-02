using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ArinWhois.Model;
using Newtonsoft.Json;

namespace ArinWhois.Client
{
    public class ArinClient
    {
        public enum ResourceType
        {
            Unknown = 0,
            Network = 1,
            Organization = 2,
            PointOfContact = 3
        }

        private const string BaseUrl = "http://whois.arin.net/rest";

        private readonly HttpClient _httpClient = new HttpClient();

        private readonly JsonSerializerSettings _serializerSettings =
            new JsonSerializerSettings {DateFormatHandling = DateFormatHandling.IsoDateFormat};

        public async Task<Response> QueryIpAsync(IPAddress ip)
        {
            try
            {
                var query = $"ip/{ip}";
                var url = GetRequestUrl(query);
                var jsonString = await _httpClient.GetStringAsync(url);
                Response response = JsonConvert.DeserializeObject<Response>(jsonString, _serializerSettings);

                Organization organization = await QueryOrganizationAsync(response.Network.OrgRef.Handle);
                response.Organization = organization;

                PointOfContacts pointOfContactRefs = await QueryPointOfContactsAsync(response.Network.OrgRef.Handle);

                IList<PointOfContact> pointOfContacts = new List<PointOfContact>();

                foreach (Poclinkref poclinkref in pointOfContactRefs.Pocs.PocLinkRefs)
                {
                    //Check if PoC for the same type exist in the list. If so, no need to query again.
                    PointOfContact pocCheck=pointOfContacts.Where(c => c?.poc?.Handle?.Value == poclinkref.Handle).FirstOrDefault();
                    if(pocCheck == null)
                    {
                        PointOfContact pointOfContact = await QueryPointOfContactAsync(poclinkref.Handle);
                        if(pointOfContact!=null)
                        {
                            pointOfContacts.Add(pointOfContact);
                        }
                    }  
                }

                if (response.Organization !=null)
                {
                    response.Organization.PointOfContacts = pointOfContacts;
                }

                return response;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Organization> QueryOrganizationAsync(string handle)
        {
            try
            {
                var query = $"org/{handle}";
                var jsonString = await _httpClient.GetStringAsync(GetRequestUrl(query));
                Organization organization = JsonConvert.DeserializeObject<Organization>(jsonString, _serializerSettings);
                return organization;
            }
            catch
            {
                return null;
            }
        }

        public async Task<PointOfContacts> QueryPointOfContactsAsync(string handle)
        {
            try
            {
                var query = $"org/{handle}/pocs";
                var jsonString = await _httpClient.GetStringAsync(GetRequestUrl(query));
                PointOfContacts pointOfContacts = JsonConvert.DeserializeObject<PointOfContacts>(jsonString, _serializerSettings);
                return pointOfContacts;
            }
            catch
            {
                return null;
            }
        }


        public async Task<PointOfContact> QueryPointOfContactAsync(string handle)
        {
            try
            {
                var query = $"poc/{handle}";
                var jsonString = await _httpClient.GetStringAsync(GetRequestUrl(query));
                PointOfContact pointOfContact = JsonConvert.DeserializeObject<PointOfContact>(jsonString, _serializerSettings);
                return pointOfContact;
            }
            catch
            {
                return null;
            }
        }

        private static Uri GetRequestUrl(string query)
        {
            return new Uri($"{BaseUrl}/{query}.json");
        }
    }
}