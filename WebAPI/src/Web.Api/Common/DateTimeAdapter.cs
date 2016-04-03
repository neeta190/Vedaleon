using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Common
{
    public class DateTimeAdapter : IDateTime
    {
        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
    }
}