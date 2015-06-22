using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Advice.Web.Models
{
    public class ResetTaskSlaModel
    {
        public long TaskId { get; set; }
        public DateTime DueDate { get; set; }
        public bool Urgent { get; set; }
        public int TaskModifyingReasonId { get; set; }
        public string Comments { get; set; }

    }
}