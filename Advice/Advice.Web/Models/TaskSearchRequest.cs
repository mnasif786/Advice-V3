using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Advice.Web.Models
{
    public class TaskSearchRequest
    {
        public long[] TaskIds { get; set; }
    }
}