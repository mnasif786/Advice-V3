using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Entities;

namespace Advice.Common.Models
{
    public class CorporatePriorityModel
    {
        public int CorporatePriorityId { get; set; }
        public string Can { get; set; }
        public string ClientName { get; set; }
        public decimal? ContractValue { get; set; }
        public string ContractDetail { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public long UserId { get; set; }
        public string LeadConsultant { get; set; }
        

        public static CorporatePriorityModel Create(CorporatePriority corporatePriority, string clientName, string leadConsultant)
        {
            var corporatePriorityModel = new CorporatePriorityModel
            {
                CorporatePriorityId = corporatePriority.CorporatePriorityId,
                Can = corporatePriority.Can,
                ClientName = clientName,
                ContractValue = corporatePriority.ContractValue,
                ContractDetail = corporatePriority.ContractDetail,
                ContractEndDate = corporatePriority.ContractEndDate,
                UserId = corporatePriority.UserId,
                LeadConsultant = leadConsultant
            };

            return corporatePriorityModel;
        }

        public static CorporatePriorityModel Create(CorporatePriority corporatePriority)
        {
            var corporatePriorityModel = new CorporatePriorityModel
            {
                CorporatePriorityId = corporatePriority.CorporatePriorityId,
                Can = corporatePriority.Can,
                ContractValue = corporatePriority.ContractValue,
                ContractDetail = corporatePriority.ContractDetail,
                ContractEndDate = corporatePriority.ContractEndDate,
                UserId = corporatePriority.UserId
            };

            return corporatePriorityModel;
        }
    }
}
