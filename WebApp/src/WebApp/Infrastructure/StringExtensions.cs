using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApp.Infrastructure
{
    public static class StringExtensions
    {
        /// <summary>
        /// Indicates if the regular expression specified in "pattern" is found in "text".
        /// </summary>
        /// <param name="text">The string the methdod is being called on</param>
        /// <param name="pattern">The pattern to find in the "text" string 
        /// (supports the *, ? and # wildcard characters).
        /// </param>
        /// <param name="ignoreCase">true [default] to ignore case.</param>
        /// <param name="useCharClasses">true to unescape "[", "[!", and "]" to allow using VB's character classes for like comparison. 
        /// False if any of those literal characters appears in the string pattern to match</param>
        /// <returns>Returns true if the regular expression finds a match otherwise returns false.</returns>
        /// <remarks>
        public static bool Like(this string text, string pattern, bool ignoreCase = true, bool useCharClasses = true)
        {
            // Check input parameters
            if (pattern == null || text == null || pattern.Length == 0 || text.Length == 0 || pattern == "*.*" == true)
            {
                // Default return is true
                return true;
            }

            // Escape all strings
            System.Text.StringBuilder regPattern = new System.Text.StringBuilder(Regex.Escape(pattern));

            // Replace the LIKE patterns with regular expression patterns
            regPattern = regPattern.Replace(Regex.Escape("*"), ".*");
            regPattern = regPattern.Replace(Regex.Escape("?"), @".");
            regPattern = regPattern.Replace(Regex.Escape("#"), @"[0-9]");
            regPattern = regPattern.Replace(Regex.Escape(" "), @"\s");
            //if pattern to compare includes VB character class markers and is not using character classes then don't unescape them
            if (useCharClasses)
            {
                regPattern = regPattern.Replace(Regex.Escape("[!"), @"[!");
                regPattern = regPattern.Replace(Regex.Escape("["), @"[");
                regPattern = regPattern.Replace(Regex.Escape("]"), @"]");
            }

            if (ignoreCase == false)
            {
                return Regex.IsMatch(text, regPattern.ToString());
            }
            else
            {
                return Regex.IsMatch(text, regPattern.ToString(), RegexOptions.IgnoreCase);
            }
        }
    }
}