using System;

namespace Advice.Logging.Models
{
    internal class InfoLog
    {
        public string Message { get; set; }
        public string User { get; set; }
        public string CallingClass  { get; set; }
        public string CallingMethod { get; set; }
        public string SourceFilePath { get; set; }
        public int SourceLineNumber { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
    }
}
