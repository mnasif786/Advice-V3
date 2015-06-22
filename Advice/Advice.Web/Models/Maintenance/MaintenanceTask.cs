using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Advice.Web.Models.Maintenance
{
    // SGG: temporary hack for maintenance page
    public class MaintenanceTask
    {        
        public long TaskTypeID { get;  set; }

        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public long WarningWindow { get; set; }
        public string AssignedUser { get; set; }
        public long AssignedTeamID { get; set; }
        public DateTime? AssignedDate { get; set; }
        public string CompletedBy { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int Urgent { get; set; }        
        public int ManualDueDate { get; set; }
        public long RecordedClientID { get; set; }
        public string CancelledReason { get; set; }
        public DateTime? CancelledDate { get; set; }
        public string CancelledBy { get; set; }
        public long PreviousTaskID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public long LastModifyingReasonID { get; set; }
        public string LastModifiedComment { get; set; }
        public int Deleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactName { get; set; }
        public int Completed { get; set; }
        public int Cancelled { get; set; }
        public string ManualDueDateReason { get; set; }
        public int IsRead { get; set; }
        public long DocumentCount { get; set; }
        public string JobSubject { get; set; }
        public long AcceptableWindow { get; set; }
    }
}
