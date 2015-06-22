using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Common.Models.Request.CorporatePriority
{
    public class CorporatePriorityAddRequest
    {
        public string Can { get; set; }
        public decimal? ContractValue { get; set; }
        public string ContractDetail { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public long UserId { get; set; }
    }
}
