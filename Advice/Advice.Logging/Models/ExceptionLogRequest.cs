using System;
using System.Collections.Specialized;
using System.Net.Http;

namespace Advice.Logging.Models
{
    public class ExceptionLogRequest
    {
        public string User { get; set; }
        public string AdditionalMessage { get; set; }
        public Exception Exception { get; set; }
        public NameValueCollection ServerVariables { get; set; }
    }
}
