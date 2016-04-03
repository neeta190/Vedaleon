using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;


namespace WebApp.Infrastructure
{
    /// <summary>
    /// faciliatets the interaction with the underlying record repositiory ( aka passenger list)
    /// </summary>
    public sealed class RecordRepository : IRecordRepository
    {
        /// <summary>
        /// file path of the flat file
        /// </summary>
        private string filePath;

        #region Ctor

        public RecordRepository(string filePathParam)
        {
            this.filePath = (HttpContext.Current != null) ? HttpContext.Current.Server.MapPath(filePathParam) : filePathParam;
        }

        #endregion

        public string ReadRecords()
        {
            try
            {
                using (StreamReader sr = File.OpenText(this.filePath))
                {
                    string result = sr.ReadToEnd();
                    return result;
                }
            }
            catch (OutOfMemoryException)
            {
                // Not enough memory.
            }
            catch (Exception) { }

            return null;
        }

        /// <summary>
        /// faciliatets the reading of a file.
        /// fetches only those records which have specified search pattern match
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public string ReadRecords( string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
                return ReadRecords(filePath);

            StringBuilder sb = new StringBuilder();

            using (StreamReader sr = File.OpenText(this.filePath))
            {
                string s = String.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    if (s.Like(pattern))
                    {
                        sb.AppendLine(s);
                    }
                }
            }

            return sb.ToString();
        }

    /// <summary>
    /// faciltates writing of new record a file
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="recordValue"></param>
    /// <returns></returns>
    public bool WriteRecord(string recordValue)
    {
            bool success = false;
            if (string.IsNullOrEmpty(recordValue))
                return success;

            if (!File.Exists(this.filePath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(recordValue);
                    success = true;
                }
            }

            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(recordValue);
                success = true;
            }
            return success;
        }

    }
}