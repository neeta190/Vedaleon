using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Web.Api.Models;

namespace Web.Api.Processors
{
    /// <summary>
    /// Processes the string and returns collecton of record locators
    /// </summary>
    public class RecordProcessor : IRecordProcessor
    {
        public IEnumerable<Record> Parse(string value)
        {
            List<Record> result = new List<Record>();

            string[] alllines = value.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            Parallel.For(0, alllines.Length, x =>
            {
                if (alllines[x].StartsWith("1"))
                {
                    int offset = alllines[x].IndexOf(".L/"); //Record Locator part
                    if (offset > 0)
                    {
                        var locatorTag = alllines[x].Substring(offset + 3, 6).Trim();
                        var passengerNameLength = alllines[x].IndexOf('-') > 0 ? (alllines[x].IndexOf('-') - 1) : (offset - 1);
                        var record = new Record()
                        {
                            LocatorTag = locatorTag,
                            Passenger = alllines[x].Substring(1, passengerNameLength).Trim()
                        };
                        result.Add(record);
                    }
                }
            });

            return result;
        }
    }
}