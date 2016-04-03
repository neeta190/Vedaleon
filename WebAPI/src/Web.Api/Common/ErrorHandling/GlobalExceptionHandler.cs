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