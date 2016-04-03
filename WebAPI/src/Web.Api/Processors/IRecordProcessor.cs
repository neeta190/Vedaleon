using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Models;

namespace Web.Api.Processors
{
    
    public interface IRecordProcessor
    {
        IEnumerable<Record> Parse(string value);
    }
}
