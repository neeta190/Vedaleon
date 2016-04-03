using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    /// <summary>
    /// View specific model faciliating the display
    /// </summary>
    public class PassengerListViewModel
    {
        public IEnumerable<PassengerRecord> PassengerRecords { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}