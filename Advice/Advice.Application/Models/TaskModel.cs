using System;
using Advice.Domain.Entities;
using Task = Advice.Domain.Entities.Task;

namespace Advice.Application.Models
{
    public class TaskModel
    {
        public long? TaskId { get;  set; }
        public string Can { get; set; }
        public long? TaskTypeId { get;  set; }
        public string TaskTypeDescription { get; set; }
        public string Description { get;  set; }
        public DateTime? DueDate { get;  set; }
        public string AssignedUser { get;  set; }
        public long? AssignedTeamId { get;  set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public long? DocumentCount { get;  set; }
        public DerivedTaskStatusForDisplay Status { get; set; }
        public bool Deleted { get; set; }
        public bool Cancelled { get; set; }
        public bool Completed { get; set; }
       

       

        //public long? WarningWindow { get;  set; }
        //public long? AcceptableWindow { get;  set; }
        
        //public DateTime? AssignedDate { get;  set; }
        //public string CompletedBy { get;  set; }
        //public DateTime? CompletionDate { get;  set; }
        //public bool? Urgent { get;  set; }
        //public bool? ManualDueDate { get;  set; }
        //public long? RecordedClientID { get;  set; }
        //public string CancelledReason { get;  set; }
        //public DateTime? CancelledDate { get;  set; }
        //public string CancelledBy { get;  set; }
        //public long? PreviousTaskID { get;  set; }
        //public long? LastModifyingReasonID { get;  set; }
        //public string LastModifiedComment { get;  set; }
        //public string EmailAddress { get;  set; }
        //public string PhoneNumber { get;  set; }
        //public string ContactName { get;  set; }
        //public bool? Completed { get;  set; }
        //public bool? Cancelled { get;  set; }
        //public string ManualDueDateReason { get;  set; }
        //public bool? IsRead { get;  set; }
        //public string JobSubject { get;  set; }
        //public EmailModel EmailTask { get; set; }
        //public TaskTypeModel TaskType { get; set; }
        //public IEnumerable<TaskActionModel> TaskActions { get; set; }
        //public IEnumerable<TaskArchiveModel> TaskArchives { get; set; }
        //public IEnumerable<TaskDocumentModel> TaskDocuments { get; set; }
        //public IEnumerable<TaskJobsModel> TaskJobs { get; set; } 

        public string AssignedTeam { get; set; }

        public static TaskModel Create(Task task, string can)
        {
            if (task == null)
                return null;

            var taskModel = new TaskModel()
            {
                TaskId = task.TaskID,
                TaskTypeId = task.TaskTypeID,
                TaskTypeDescription = task.TaskType.Description,
                Can = can,
                Description = !string.IsNullOrEmpty(task.Description) && task.Description.Length > 100 ? task.Description.Substring(0, 99) : task.Description,
                DueDate = task.DueDate,
                AssignedUser = task.AssignedUser,
                CreatedBy = task.CreatedBy,
                CreatedDate = task.CreatedDate,
                DocumentCount = task.DocumentCount,
                Status = task.GetTaskStatus(),
                AssignedTeamId = task.AssignedTeamID,              
                Deleted = task.Deleted,
                Cancelled = task.Cancelled,
                Completed = task.Completed,
                //WarningWindow = task.WarningWindow,
                //AcceptableWindow = task.AcceptableWindow,
                
                //AssignedDate = task.AssignedDate,
                //CompletedBy = task.CompletedBy,
                //CompletionDate = task.CompletionDate,
                //Urgent = task.Urgent,
                //ManualDueDate = task.ManualDueDate,
                //RecordedClientID = task.RecordedClientID,
                //CancelledReason = task.CancelledReason,
                //CancelledDate = task.CancelledDate,
                //CancelledBy = task.CancelledBy,
                //PreviousTaskID = task.PreviousTaskID,
                //LastModifyingReasonID = task.LastModifyingReasonID,
                //LastModifiedComment = task.LastModifiedComment,
                //EmailAddress = task.EmailAddress,
                //PhoneNumber = task.PhoneNumber,
                //ContactName = task.ContactName,
                //Completed = task.Completed,
                //Cancelled = task.Cancelled,
                //ManualDueDateReason = task.ManualDueDateReason,
                
                //JobSubject = task.JobSubject,
                //EmailTask = EmailModel.CreateFromEmailTask(task.EmailTask),
                //TaskActions = task.TaskActions.Select(TaskActionModel.CreateFromTaskAction),
                //TaskType = TaskTypeModel.CreateTaskModelFromTask(task.TaskType),
                //TaskArchives = task.TaskArchives.Select(TaskArchiveModel.CreateTaskArchiveModelFromTask),
                //TaskDocuments = task.TaskDocuments.Select(TaskDocumentModel.CreateTaskDocumentModelFromTask),
                //TaskJobs = task.TaskJobs.Select(TaskJobsModel.CreateTaskJobsModelFromTask)

                AssignedTeam = task.AssignedTeam == null ? null : task.AssignedTeam.Description
            };

            return taskModel;
        }
        
    }
}
