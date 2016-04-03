using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models
{
    /// <summary>
    /// model captures the locator tag & associated passenger records 
    /// 1 - to - Many
    /// </summary>
    public class LocatorRecord
    {
        public string RecordTag { get; set; }

        public IEnumerable<string> Passengers { get; set; }
    }
}