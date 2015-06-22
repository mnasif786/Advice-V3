using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http.ExceptionHandling;
using Advice.Logging;
using Advice.Logging.Contracts;
using Advice.Logging.Models;

namespace Advice.Web.ExceptionHandling
{
    
    public class ElkExceptionLogger : ExceptionLogger
    {
        private const string HttpContextBaseKey = "MS_HttpContext";
        public static readonly IElkLog ElkLog = ElkLogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override void Log(ExceptionLoggerContext context)
        {
                // Retrieve the current HttpContext instance for this request.
                HttpContext httpContext = GetHttpContext(context.Request);

                if (httpContext == null)
                {
                    return;
                }
           
                var exceptionLogRequest = new ExceptionLogRequest()
                {
                    AdditionalMessage = "Unhandled Exception occured in the system",
                    ServerVariables = httpContext.Request.ServerVariables,
                    Exception = context.Exception,
                };

                ElkLog.LogException(exceptionLogRequest);
        }

        private static HttpContext GetHttpContext(HttpRequestMessage request)
        {
            HttpContextBase contextBase = GetHttpContextBase(request);

            if (contextBase == null)
            {
                return null;
            }

            return ToHttpContext(contextBase);
        }

        private static HttpContextBase GetHttpContextBase(HttpRequestMessage request)
        {
            if (request == null)
            {
                return null;
            }

            object value;

            if (!request.Properties.TryGetValue(HttpContextBaseKey, out value))
            {
                return null;
            }

            return value as HttpContextBase;
        }

        private static HttpContext ToHttpContext(HttpContextBase contextBase)
        {
            return contextBase.ApplicationInstance.Context;
        }
    }
}