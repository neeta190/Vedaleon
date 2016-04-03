using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models
{
    /// <summary>
    /// captures the association between record locator & passenger record
    /// 1 to 1 
    /// </summary>
    public class Record
    {
        public string LocatorTag { get; set; }

        public string Passenger { get; set; }
    }
}