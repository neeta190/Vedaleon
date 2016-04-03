using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Web.Api.Models;
using Web.Api.Processors;

namespace Web.Api.Controllers
{
    /// <summary>
    /// Passenger  web api
    /// </summary>
    [RoutePrefixAttribute("api/passenger")]
    public class PassengerController : ApiController
    {

        private readonly IRecordProcessor _recordProcessor;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="recordProcessor"></param>
        public PassengerController(IRecordProcessor recordProcessor)
        {
            _recordProcessor = recordProcessor;
        }

        /// <summary>
        /// Dummy method for checking the API working status
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// API accepts text in prespecfied format as input and returns a list of record Locators and the passengers associated with them. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(HttpRequestMessage request)
        {
            var value = await request.Content.ReadAsStringAsync();
            if (!string.IsNullOrWhiteSpace(value))
            {
                IEnumerable<Record> result = _recordProcessor.Parse(value);
                // passange name List 
                var pnl = from r in result
                          group r by r.LocatorTag into bylocatorTagGroup
                          select new LocatorRecord { RecordTag = bylocatorTagGroup.Key, Passengers = bylocatorTagGroup.Select(x => x.Passenger) };

                return Ok(pnl);
            }

            return BadRequest("No input paramter specified");
        }
    }
}
