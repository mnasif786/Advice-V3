//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Advice.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Task
    {
        public Task()
        {
            this.TaskActions = new HashSet<TaskAction>();
            this.TaskArchives = new HashSet<TaskArchive>();
            this.TaskDocuments = new HashSet<TaskDocument>();
            this.TaskJobs = new HashSet<TaskJob>();
            this.ReassignTaskEvents = new HashSet<ReassignTaskEvent>();
        }
    
        public long TaskID { get; set; }
        public Nullable<long> TaskTypeID { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<long> WarningWindow { get; set; }
        public string AssignedUser { get; set; }
        public Nullable<long> AssignedTeamID { get; set; }
        public Nullable<System.DateTime> AssignedDate { get; set; }
        public string CompletedBy { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public bool Urgent { get; set; }
        public bool ManualDueDate { get; set; }
        public Nullable<long> RecordedClientID { get; set; }
        public string CancelledReason { get; set; }
        public Nullable<System.DateTime> CancelledDate { get; set; }
        public string CancelledBy { get; set; }
        public Nullable<long> PreviousTaskID { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public System.DateTime LastModifiedDate { get; set; }
        public Nullable<long> LastModifyingReasonID { get; set; }
        public string LastModifiedComment { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactName { get; set; }
        public bool Completed { get; set; }
        public bool Cancelled { get; set; }
        public string ManualDueDateReason { get; set; }
        public Nullable<bool> IsRead { get; set; }
        public Nullable<long> DocumentCount { get; set; }
        public string JobSubject { get; set; }
        public Nullable<long> AcceptableWindow { get; set; }
        public string SlaStatus { get; set; }
        public Nullable<byte> AvVersion { get; set; }
    
        public virtual TaskType TaskType { get; set; }
        public virtual ICollection<TaskAction> TaskActions { get; set; }
        public virtual ICollection<TaskArchive> TaskArchives { get; set; }
        public virtual ICollection<TaskDocument> TaskDocuments { get; set; }
        public virtual ICollection<TaskJob> TaskJobs { get; set; }
        public virtual TaskModifyingReason TaskModifyingReason { get; set; }
        public virtual Team AssignedTeam { get; set; }
        public virtual ICollection<ReassignTaskEvent> ReassignTaskEvents { get; set; }
    }
}