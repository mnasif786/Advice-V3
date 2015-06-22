using System;
using System.Security.Policy;

namespace Advice.Logging.Models
{
    internal class ExceptionLog
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string AdditionalMessage { get; set; }
        public string Type { get; set; }
        public int Code { get; set; }
        public string Error { get; set; }
        public string Details { get; set; }
        public string InnerException { get; set; }
        public string StackTrace { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string CallingClass { get; set; }
        public string CallingMethod { get; set; }
        public string SourceFilePath { get; set; }
        public int SourceLineNumber { get; set; }

        public ServerVariables ServerVariables { get; set; }

    }
}
