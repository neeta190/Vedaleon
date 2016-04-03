using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Api.Controllers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Collections.Generic;
using Web.Api.Processors;
using System.Linq;
using Web.Api.Models;

namespace Web.Api.Tests
{
    [TestClass]
    public class PassengerControllerTest
    {
        #region Private Member

        private PassengerController _controller;
        private IRecordProcessor _recordProcessor;

        #endregion

        [TestInitialize]
        public void SetUp()
        {
            _recordProcessor = new RecordProcessor();
            _controller = new PassengerController(_recordProcessor);
        }

        [TestMethod]
        public async Task Post_returns_correct_PassangerList_response()
        {

            var requestMessage = HttpRequestMessageFactory.CreateRequestMessage( HttpMethod.Post);
            var value = @"1ARNOLD/NIGELMR-B2 .L/LVGVUP
.R/TKNE HK1 9244501028078/1
1ATKINSON/KARENMRS-M2 .L/LVKBTB
.R/TKNE HK1 9244501227666/1
1BALL/LINDAANNMRS-E2 .L/LVHZDG
.R/TKNE HK1 9249745692287/1
1BALL/STEPHENJOHNMR-E2 .L/LVHZDG
.R/TKNE HK1 9249745692286/1
1CLARKE/MICHAELMR-K2 .L/LVK6HA
.R/TKNE HK1 9244501213584/1
1CLARKE/TRACEYMRS-K2 .L/LVK6HA
.R/TKNE HK1 9244501213586/1
1CLIFFORD/DAVIDMR .L/LVKBCB
.R/TKNE HK1 9244501226608/1
1TAYLOR/HAYLEYMRS-B2 .L/LVGVUP
.R/TKNE HK1 9244501028080/1";

            var content = new StringContent(value);
            requestMessage.Content = content;
            var response = _recordProcessor.Parse(value);

            var actualResponse = await _controller.Post(requestMessage) as OkNegotiatedContentResult<IEnumerable<LocatorRecord>>;
            Assert.IsNotNull(actualResponse);
            Assert.AreEqual(response.FirstOrDefault().LocatorTag, actualResponse.Content.FirstOrDefault().RecordTag);
        }
    }

   
}
