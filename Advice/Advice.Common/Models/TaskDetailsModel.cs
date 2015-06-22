using System;
using System.Collections.Generic;
using System.Linq;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Advice.Domain.Helper;

namespace Advice.Common.Models
{
    public class TaskDetailsModel
    {
        public long TaskId { get; set; }
        public string Department { get;  set; }
        public string EmailAddress { get;  set; }
        public string PhoneNumber { get;  set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public string JobSubject { get;  set; }
        public string Recipients { get; set; }
        public EmailModel EmailTask { get; set; }
        public bool IsEmailTask { get; set; }
        public bool IsHroTask { get; set; }
        public bool IsBusinessWiseTask { get; set; }
        public TaskTypeModel TaskType { get; set; }
        public HroTaskModel HroTask { get; set; }
        public BusinessWiseTaskModel BusinessWiseTask { get; set; }
        public IEnumerable<TaskDocumentModel> TaskDocuments { get; set; }
        public IEnumerable<TaskJobsModel> TaskJobs { get; set; }
        public string ContactName { get; set; }
        public string TaskModifyingReason { get; set; }
        public string TaskModifyingReasonGroup { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public long? LastModifyingReasonID { get; set; }
        public string LastModifiedComment { get; set; }
        public string AssignedUser { get; set; }
        public string AssignedTeam { get; set; }
        public long? RecordedClientId { get; set; }
        public bool Urgent { get; set; }
        public bool IsProactiveTask { get; set; }

        public static TaskDetailsModel Create(Task task)
        {
            if (task == null)
                return null;

            
            var taskDetailsModel = new TaskDetailsModel()
            {
                TaskId = task.TaskID,
                EmailAddress =task.EmailAddress,
                PhoneNumber = task.PhoneNumber,
                Description = task.Description,
                DueDate = task.DueDate.HasValue ? task.DueDate.Value.ToString("dd/MM/yyyy HH:mm") : null,
                JobSubject = task.JobSubject,
				ContactName = task.ContactName,
                IsEmailTask = (task is EmailTask),
                IsHroTask = (task is HroTask),
                IsBusinessWiseTask = (task is BusinessWiseTask),
                EmailTask = (task is EmailTask) ? EmailModel.Create(((EmailTask)task)) : null, // EmailModel.Create(task.EmailTask),
                TaskType = TaskTypeModel.Create(task.TaskType),
                HroTask = task is HroTask ? HroTaskModel.Create(((HroTask)task)) : null, //HroTaskModel.Create(task.HroTask),
                BusinessWiseTask = task is BusinessWiseTask ? BusinessWiseTaskModel.Create(((BusinessWiseTask)task)) : null, //BusinessWiseTaskModel.Create(task.BusinessWiseTask),
                TaskDocuments = task.TaskDocuments.Select(TaskDocumentModel.Create),
                IsProactiveTask =  Constants.ProActiveTaskTypes.Contains((TaskTypeIds) task.TaskType.TaskTypeID),
                TaskJobs = task.TaskJobs.Select(TaskJobsModel.Create),
                Urgent =  task.Urgent,
                LastModifiedDate = task.LastModifiedDate,
                LastModifiedComment = task.LastModifiedComment,
                LastModifiedBy = task.LastModifiedBy,
                AssignedUser = task.AssignedUser,   
                AssignedTeam = task.AssignedTeam == null ? null : task.AssignedTeam.Description,
                RecordedClientId = task.RecordedClientID,
                TaskModifyingReason         = (task.TaskModifyingReason != null) ? task.TaskModifyingReason.Description: null,
                TaskModifyingReasonGroup    = (task.TaskModifyingReason != null && task.TaskModifyingReason.TaskModifyingReasonGroup != null) ? task.TaskModifyingReason.TaskModifyingReasonGroup.Description : null
            };
           

            return taskDetailsModel;
        }
    }
}
