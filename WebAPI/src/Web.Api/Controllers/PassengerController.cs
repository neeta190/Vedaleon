using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Web.Api.Models;
using Web.Api.Processors;

namespace Web.Api.Controllers
{
    [RoutePrefixAttribute("api/passenger")]
    public class PassengerController : ApiController
    {

        private readonly IRecordProcessor _recordProcessor;

        public PassengerController(IRecordProcessor recordProcessor)
        {
            _recordProcessor = recordProcessor;
        }
        
        // GET: api/Passenger
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/Passenger
        public async Task<IHttpActionResult> Post(HttpRequestMessage request)
        {
            var value = await request.Content.ReadAsStringAsync();
            if (!string.IsNullOrWhiteSpace(value))
            {
                IEnumerable<Record> result = _recordProcessor.Parse(value);
                // passange name List 
                var pnl = from r in result
                          group r by r.LocatorTag into bylocatorTagGroup
                          select new { recordlocator = bylocatorTagGroup.Key, passengers = bylocatorTagGroup.Select(x => x.Passenger) };

                return Ok(pnl);
            }

            return BadRequest("No input paramter specified");
        }
    }
}
