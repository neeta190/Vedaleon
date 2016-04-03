using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Infrastructure;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private int _pageSize = 4;
        private IRecordRepository _recordRepository = null;

        #region Ctor

        public HomeController(IRecordRepository recordRepositoryParam)
        {
            _recordRepository = recordRepositoryParam;
        }

        #endregion

        /// <summary>
        /// faciliates the listing of passengers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index(int page = 1)
        {
            var value = _recordRepository.ReadRecords();
            var passengerRecords = await ParsePassengerRecords(value);
            if (passengerRecords != null && passengerRecords.Any())
            {
                PassengerListViewModel model = new PassengerListViewModel
                {
                    PassengerRecords = passengerRecords.Skip((page - 1) * _pageSize).Take(_pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = _pageSize,
                        TotalItems = passengerRecords.Count()
                    }
                };
                return View(model);
            }

            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// facilitaes the addition of new record
        /// </summary>
        /// <param name="recordValue"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(string recordValue = null)
        {
            string sPattern = @"^[a-zA-Z0-9 ,.\/\-]+$";

            if (!string.IsNullOrWhiteSpace(recordValue))
            {
                if (Regex.IsMatch(recordValue, sPattern))
                {
                    ViewBag.Success = _recordRepository.WriteRecord(recordValue);
                }
                else
                {
                    ViewBag.InvalidInput = true;
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Search(string searchTerm = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var model = new List<PassengerRecord>(); // empty View model
                var value = _recordRepository.ReadRecords(searchTerm);

                if (!string.IsNullOrEmpty(value))
                    model = await ParsePassengerRecords(value);

                return View(model);
            }

            return View();
        }

        #region private methods

        /// <summary>
        /// method intenally makes call to the web API to fetch "Passenger Name List" or "PNL"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private async Task<List<PassengerRecord>> ParsePassengerRecords(string value)
        {
            List<PassengerRecord> passengerRecords = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebAPIBaseUrl"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var content = new StringContent(value);
                    HttpResponseMessage response = await client.PostAsync("/api/passenger", content);
                    if (response.IsSuccessStatusCode)
                    {
                        passengerRecords = await response.Content.ReadAsAsync<List<PassengerRecord>>();
                    }
                }
                catch (HttpRequestException ex)
                {
                    // Handle exception.
                }
            }

            return passengerRecords;
        }

        #endregion 
    }
}