using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Advice.Domain.Entities
{
    public partial class TaskArchive
    {
        public static TaskArchive Create(Task task)
        {
            var taskArchive = new TaskArchive
            {
                TaskID = task.TaskID,
                TaskTypeID = task.TaskTypeID,
                Description = task.Description,
                DueDate = task.DueDate,
                WarningWindow = task.WarningWindow,
                AcceptableWindow = task.AcceptableWindow,
                AssignedUser = task.AssignedUser,
                AssignedTeamID = task.AssignedTeamID,
                AssignedDate = task.AssignedDate,
                CompletedBy = task.CompletedBy,
                CompletionDate = task.CompletionDate,
                Urgent = task.Urgent,
                ManualDueDate = task.ManualDueDate,
                RecordedClientID = task.RecordedClientID,
                CancelledReason = task.CancelledReason,
                CancelledDate = task.CancelledDate,
                CancelledBy = task.CancelledBy,
                PreviousTaskID = task.PreviousTaskID,
                CreatedBy = task.CreatedBy,
                CreatedDate = task.CreatedDate,
                LastModifiedBy = task.LastModifiedBy,
                LastModifiedDate = task.LastModifiedDate,
                LastModifyingReasonID = task.LastModifyingReasonID,
                LastModifiedComment = task.LastModifiedComment,
                Deleted = task.Deleted,
                DeletedBy = task.DeletedBy,
                DeletedDate = task.DeletedDate,
                EmailAddress = task.EmailAddress,
                PhoneNumber = task.PhoneNumber,
                ContactName = task.ContactName,
                Completed = task.Completed,
                Cancelled = task.Cancelled,
                ManualDueDateReason = task.ManualDueDateReason,
                IsRead = task.IsRead,
                DocumentCount = task.DocumentCount,
                JobSubject = task.JobSubject,
                SlaStatus = task.SlaStatus,
                AvVersion = task.AvVersion
            };

            return taskArchive;
        }
    }
}
