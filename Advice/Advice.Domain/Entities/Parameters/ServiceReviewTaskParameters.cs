using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Entities.Enums;

namespace Advice.Domain.Entities.Parameters
{
    public class ServiceReviewTaskParameters
    {
        public string CreatedBy {
            get { return "AV3 Pro-Active"; }
        }

        public string LastModifiedBy
        {
            get { return "AV3 Pro-Active"; }
        }

        public long TaskTypeId
        {
            get { return (long)TaskTypeIds.ServiceReview; }
        }

        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public long WarningWindow { get; set; }
        public long? ClientId { get; set; }
        public string AssignedUser { get; set; }
        public long? AssignedTeamId { get; set; }
        public long? AcceptableWindow { get; set; }

        public static ServiceReviewTaskParameters Create(long? clientId, DateTime? contractEndDate, DateTime? duedate, long warningWindow, string assignedUser, long? assignedTeamId, long? acceptableWindow)
        {
            return new ServiceReviewTaskParameters
            {
                ClientId = clientId,
                Description = "Service Review " + (contractEndDate.HasValue ? contractEndDate.Value.ToShortDateString() : ""),
                AcceptableWindow = acceptableWindow,
                AssignedUser = assignedUser,
                AssignedTeamId = assignedTeamId,
                DueDate = duedate,
                WarningWindow = warningWindow
            };
        }

    }
}
