using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ArinWhois.Client;
using ArinWhois.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArinWhoisAspnetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WhoIsController : ControllerBase
    {

        private readonly ILogger<WhoIsController> _logger;

        public WhoIsController(ILogger<WhoIsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Find WhoIs for the IP Address
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <returns>List of AzCountryProvince</returns>
        [HttpGet("WhoIsIP")]
        public Response WhoIsIP(string ipAddress)
        {
            var arinClient = new ArinClient();
            var ipResponse = arinClient.QueryIpAsync(IPAddress.Parse(ipAddress)).Result;
            return ipResponse;
        }
    }
}
