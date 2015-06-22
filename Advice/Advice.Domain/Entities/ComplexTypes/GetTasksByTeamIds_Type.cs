using System;

namespace Advice.Domain.Entities
{
    public class GetTasksByTeamIds_Type
    {

        public GetTasksByTeamIds_Type() { }

        public long Task_TaskID { get; set; }
        public Nullable<long> Task_TaskTypeID { get; set; }
        public string Task_Description { get; set; }
        public Nullable<System.DateTime> Task_DueDate { get; set; }
        public Nullable<long> Task_WarningWindow { get; set; }
        public string Task_AssignedUser { get; set; }
        public Nullable<long> Task_AssignedTeamID { get; set; }
        public Nullable<System.DateTime> Task_AssignedDate { get; set; }
        public string Task_CompletedBy { get; set; }
        public Nullable<System.DateTime> Task_CompletionDate { get; set; }
        public bool Task_Urgent { get; set; }
        public bool Task_ManualDueDate { get; set; }
        public Nullable<long> Task_RecordedClientID { get; set; }
        public string Task_CancelledReason { get; set; }
        public Nullable<System.DateTime> Task_CancelledDate { get; set; }
        public string Task_CancelledBy { get; set; }
        public Nullable<long> Task_PreviousTaskID { get; set; }
        public string Task_CreatedBy { get; set; }
        public System.DateTime Task_CreatedDate { get; set; }
        public string Task_LastModifiedBy { get; set; }
        public System.DateTime Task_LastModifiedDate { get; set; }
        public Nullable<long> Task_LastModifyingReasonID { get; set; }
        public string Task_LastModifiedComment { get; set; }
        public bool Task_Deleted { get; set; }
        public string Task_DeletedBy { get; set; }
        public Nullable<System.DateTime> Task_DeletedDate { get; set; }
        public string Task_EmailAddress { get; set; }
        public string Task_PhoneNumber { get; set; }
        public string Task_ContactName { get; set; }
        public bool Task_Completed { get; set; }
        public bool Task_Cancelled { get; set; }
        public string Task_ManualDueDateReason { get; set; }
        public Nullable<bool> Task_IsRead { get; set; }
        public Nullable<long> Task_DocumentCount { get; set; }
        public string Task_JobSubject { get; set; }
        public Nullable<long> Task_AcceptableWindow { get; set; }
        public string Task_SlaStatus { get; set; }
        public Nullable<byte> Task_AvVersion { get; set; }
        public long TaskType_TaskTypeID { get; set; }
        public string TaskType_Description { get; set; }
        public Nullable<bool> TaskType_ExternalTask { get; set; }
        public string TaskType_CreatedBy { get; set; }
        public System.DateTime TaskType_CreatedDate { get; set; }
        public string TaskType_LastModifiedBy { get; set; }
        public Nullable<System.DateTime> TaskType_LastModifiedDate { get; set; }
        public bool TaskType_Deleted { get; set; }
        public string TaskType_DeletedBy { get; set; }
        public Nullable<System.DateTime> TaskType_DeletedDate { get; set; }


    }//class
}//ns
