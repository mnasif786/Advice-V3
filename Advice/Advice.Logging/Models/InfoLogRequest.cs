using System;

namespace Advice.Logging.Models
{
    public class InfoLogRequest
    {
        public IndexType IndexType { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
    }
}
