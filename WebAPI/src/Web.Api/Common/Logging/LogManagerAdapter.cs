using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Api.Common.Logging
{
    /// <summary>
    /// adapter class for logging
    /// </summary>
    public class LogManagerAdapter : ILogManager
    {
        public ILog GetLog(Type typeAssociatedWithRequestedLog)
        {
            var log = LogManager.GetLogger(typeAssociatedWithRequestedLog);
            return log;
        }
    }
}