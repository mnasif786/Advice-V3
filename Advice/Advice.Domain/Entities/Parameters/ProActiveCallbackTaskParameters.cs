using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Entities.Enums;
using Advice.Domain.Helper;

namespace Advice.Domain.Entities.Parameters
{
    public class ProActiveCallbackTaskParameters
    {
        public string CreatedBy
        {
            get { return "AV3 Pro-Active"; }
        }

        public string LastModifiedBy
        {
            get { return "AV3 Pro-Active"; }
        }

        public long TaskTypeId
        {
            get { return (long)TaskTypeIds.ProActiveCallback; }
        }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public string AssignedUser { get; set; }

        public long WarningWindow { get; set; }
    
        public long? AcceptableWindow { get; set; }

        public int ClientID { get; set; }

        public long JobId { get; set; }
    
        public ProActiveCallbackTaskParameters(Job job, TaskTypeSLA taskTypeSla, string assignedUserName)     
        {
            Description = String.Format( "Pro-Active Callback : {0}", job.Subject);            
            DueDate = WorkingHours.CalculateDueDate(taskTypeSla.DefaultSLATime);
            WarningWindow = taskTypeSla.DefaultWarningWindow;
            AcceptableWindow = taskTypeSla.DefaultAcceptableWindow;
            AssignedUser = assignedUserName;
            ClientID = job.ClientID;
            JobId = job.JobID;
        }
    }
}
