using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Http.ExceptionHandling;

namespace Web.Api.Common.ErrorHandling
{
    /// <summary>
    /// responsible for constructing the SimpleErrorResult instances.
    /// custom exception handler, used to replace the ASP.NET Web API default exception handler
    /// allow customization the HTTP response sent when an unhandled application exception
    /// </summary>
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var exception = context.Exception;
            var httpException = exception as HttpException;

            if (httpException != null)
            {
                context.Result = new SimpleErrorResult(context.Request,
                (HttpStatusCode)httpException.GetHttpCode(), httpException.Message);
                return;
            }

            context.Result = new SimpleErrorResult(context.Request, HttpStatusCode.InternalServerError,exception.Message);
        }
    }
}