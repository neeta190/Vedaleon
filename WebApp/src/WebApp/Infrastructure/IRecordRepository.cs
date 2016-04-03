using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Infrastructure
{
    /// <summary>
    /// contract for the record repository
    /// </summary>
    public interface IRecordRepository
    {
        string ReadRecords();

        /// <summary>
        /// fetches only the matching records
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        string ReadRecords(string pattern);

        bool WriteRecord(string recordValue);
    }
}
