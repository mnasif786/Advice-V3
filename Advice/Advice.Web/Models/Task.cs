using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Advice.Web.Models
{
    public class Task
    {
        public long Id { get; set; }
        public string CAN { get; set; }

        public string Type { get; set; }

        public string Advisor { get; set; }

        public string From { get; set; }

        public string Logged { get; set; }

        public string Due { get; set; }

        public string Status { get; set; }

    }
}