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
            PointOfContact = 3,
            Customer = 4
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

                Organization organization = null;
                Customer customer =null;
                PointOfContacts pointOfContactRefs =null;

                if (!String.IsNullOrEmpty(response.Network?.OrgRef?.Handle))
                {
                    organization = await QueryOrganizationAsync(response.Network.OrgRef.Handle);
                    response.Organization = organization;

                    pointOfContactRefs = await QueryPointOfContactsAsync(response.Network.OrgRef.Handle);
                }
                else if(!String.IsNullOrEmpty(response.Network?.CustomerRef?.Handle))
                {
                    customer = await QueryCustomerAsync(response.Network.CustomerRef.Handle);
                    response.Customer = customer;

                    pointOfContactRefs = await QueryPointOfContactsAsync(response.Customer.ParentOrgRef.Handle);
                }


                IList<PointOfContact> pointOfContacts = new List<PointOfContact>();

                foreach (Poclinkref poclinkref in pointOfContactRefs.Pocs.PocLinkRefs)
                {
                    //Check if PoC for the same type exist in the list. If so, no need to query again.
                    PointOfContact pocCheck = pointOfContacts.Where(c => c?.poc?.Handle?.Value == poclinkref.Handle).FirstOrDefault();
                    if (pocCheck == null)
                    {
                        PointOfContact pointOfContact = await QueryPointOfContactAsync(poclinkref.Handle);
                        if (pointOfContact != null)
                        {
                            pointOfContacts.Add(pointOfContact);
                        }
                    }
                }

                response.PointOfContacts = pointOfContacts;
                
                return response;
            }
            catch(Exception ex)
            {
                return new Response();
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

        public async Task<Customer> QueryCustomerAsync(string handle)
        {
            try
            {
                var query = $"customer/{handle}";

                var jsonString = await _httpClient.GetStringAsync(GetRequestUrl(query));
                Company company = JsonConvert.DeserializeObject<Company>(jsonString, _serializerSettings);
                return company.Customer;
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