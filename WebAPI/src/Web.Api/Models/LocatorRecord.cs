using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Models
{
    public class LocatorRecord
    {
        public string RecordTag { get; set; }

        public IEnumerable<string> Passengers { get; set; }
    }
}