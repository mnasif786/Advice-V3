using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Entities.Enums;
using Advice.Domain.Helper;
using Advice.Domain.Entities.Parameters;
using Advice.Domain.Entities;

// ReSharper disable once CheckNamespace
namespace Advice.Domain.Entities
{
    public partial class Task
    {
        //Need to create overloaded Task Create Methods with different parameters depends upon the requirements
        public static Task Create(ServiceReviewTaskParameters parameters)
        {
            var task = new Task()
            {
                TaskTypeID = parameters.TaskTypeId,
                Description = parameters.Description,
                DueDate = parameters.DueDate,
                WarningWindow = parameters.WarningWindow,
                AssignedUser = parameters.AssignedUser,
                AssignedTeamID = parameters.AssignedTeamId,
                RecordedClientID = parameters.ClientId,
                CreatedDate = DateTime.Now,
                CreatedBy = parameters.CreatedBy,
                LastModifiedDate = DateTime.Now,
                LastModifiedBy = parameters.LastModifiedBy,
                LastModifyingReasonID = (long) TaskModifyingReasons.Created,
                AcceptableWindow = parameters.AcceptableWindow

            };

            return task;
        }

        public static Task Create(ProActiveCallbackTaskParameters parameters)
        {
            var task = new Task()
            {
                TaskTypeID = parameters.TaskTypeId,
                Description = parameters.Description,
                DueDate = parameters.DueDate,
                AssignedUser = parameters.AssignedUser,
                WarningWindow = parameters.WarningWindow,                
                AcceptableWindow = parameters.AcceptableWindow,
                RecordedClientID = parameters.ClientID,
                CreatedDate = DateTime.Now,
                CreatedBy = parameters.CreatedBy,
                LastModifiedDate = DateTime.Now,
                LastModifiedBy = parameters.LastModifiedBy,
                LastModifyingReasonID = (long)TaskModifyingReasons.Created,
                TaskJobs = new List<TaskJob>() { TaskJob.Create(parameters.JobId, CreatedUser.Create(parameters.CreatedBy), ModifiedUser.Create(parameters.CreatedBy)) }
            };

            return task;
        }

        public static Task Create(GetTasksByTeamIds_Type parameters)
        {
            var task = new Task()
            {

                TaskID = parameters.Task_TaskID,
                TaskTypeID = parameters.Task_TaskTypeID,
                Description = parameters.Task_Description,
                DueDate = parameters.Task_DueDate,
                WarningWindow = parameters.Task_WarningWindow,
                AssignedUser = parameters.Task_AssignedUser,
                AssignedTeamID = parameters.Task_AssignedTeamID,
                AssignedDate = parameters.Task_AssignedDate,
                CompletedBy = parameters.Task_CompletedBy,
                CompletionDate = parameters.Task_CompletionDate,
                Urgent = parameters.Task_Urgent,
                ManualDueDate = parameters.Task_ManualDueDate,
                RecordedClientID = parameters.Task_RecordedClientID,
                CancelledReason = parameters.Task_CancelledReason,
                CancelledDate = parameters.Task_CancelledDate,
                CancelledBy = parameters.Task_CancelledBy,
                PreviousTaskID = parameters.Task_PreviousTaskID,
                CreatedBy = parameters.Task_CreatedBy,
                CreatedDate = parameters.Task_CreatedDate,
                LastModifiedBy = parameters.Task_LastModifiedBy,
                LastModifiedDate = parameters.Task_LastModifiedDate,
                LastModifyingReasonID = parameters.Task_LastModifyingReasonID,
                LastModifiedComment = parameters.Task_LastModifiedComment,
                Deleted = parameters.Task_Deleted,
                DeletedBy = parameters.Task_DeletedBy,
                DeletedDate = parameters.Task_DeletedDate,
                EmailAddress = parameters.Task_EmailAddress,
                PhoneNumber = parameters.Task_PhoneNumber,
                ContactName = parameters.Task_ContactName,
                Completed = parameters.Task_Completed,
                Cancelled = parameters.Task_Cancelled,
                ManualDueDateReason = parameters.Task_ManualDueDateReason,
                IsRead = parameters.Task_IsRead,
                DocumentCount = parameters.Task_DocumentCount,
                JobSubject = parameters.Task_JobSubject,
                AcceptableWindow = parameters.Task_AcceptableWindow,
                SlaStatus = parameters.Task_SlaStatus,
                AvVersion = parameters.Task_AvVersion

            };

            var taskType = new TaskType()
            {
                TaskTypeID = parameters.TaskType_TaskTypeID,
                Description = parameters.TaskType_Description,
                ExternalTask = parameters.TaskType_ExternalTask,
                CreatedBy = parameters.TaskType_CreatedBy,
                CreatedDate = parameters.TaskType_CreatedDate,
                LastModifiedBy = parameters.TaskType_LastModifiedBy,
                LastModifiedDate = parameters.TaskType_LastModifiedDate,
                Deleted = parameters.TaskType_Deleted,
                DeletedBy = parameters.TaskType_DeletedBy,
                DeletedDate = parameters.TaskType_DeletedDate
            };

            // Set TaskType
            task.TaskType = taskType;

            return task;
        }

        public DerivedTaskStatusForDisplay GetTaskStatus()
        {
            if (IsOverDue())
                return DerivedTaskStatusForDisplay.Red;

            if (IsApproachingSLA())
                return DerivedTaskStatusForDisplay.Amber;

            if (IsWithinSLA())
                return DerivedTaskStatusForDisplay.Green;

            if (IsJustStarted())
                return DerivedTaskStatusForDisplay.Platinum;

            // Should never get here ...
            return DerivedTaskStatusForDisplay.Red;
        }

        public void MarkAsRead(string userName)
        {
            AddToArchive();
            IsRead = true;
            LastModifiedBy = userName;
            LastModifiedDate = DateTime.Now;
            LastModifyingReasonID = (long)TaskModifyingReasons.Read;
            LastModifiedComment = null;
            SlaStatus = GetTaskStatus().ToString();
            AvVersion =  (byte?) AvVersions.Av3;
        }
        public void MarkAsDeleted(string userName)
        {
            AddToArchive();
            Deleted = true;
            DeletedBy = userName;
            DeletedDate = DateTime.Now;
            LastModifiedBy = userName;
            LastModifiedDate = DateTime.Now;
            LastModifyingReasonID = (long)TaskModifyingReasons.Delete; 
            LastModifiedComment = null;
            SlaStatus = GetTaskStatus().ToString();
            AvVersion = (byte?)AvVersions.Av3;
        }

        public void Reinstate(string userName)
        {
            AddToArchive();
            Deleted = false;
            DeletedBy = null;
            DeletedDate = null;
            LastModifiedBy = userName;
            LastModifiedDate = DateTime.Now;
            LastModifiedComment = null;
            LastModifyingReasonID = (long)TaskModifyingReasons.Reinstate;
            SlaStatus = GetTaskStatus().ToString();
            AvVersion = (byte?)AvVersions.Av3;
        }

        public void Reassign(string newAssignedUser, long? newAssignedTeamId, bool urgent, string comments, string reAssignedByUser, long? clientId, DateTime? dueDate, long reasonId, long? previousAssignedTeamId, string previousAssignedUser)
        {
            AddToArchive();
            AssignedUser = newAssignedUser;
            AssignedTeamID = newAssignedTeamId;
            Urgent = urgent;
            LastModifiedComment = comments;
            LastModifiedBy = reAssignedByUser;
            LastModifiedDate = DateTime.Now;
            LastModifyingReasonID = reasonId;
            SlaStatus = GetTaskStatus().ToString();
            AvVersion = (byte?)AvVersions.Av3;
            IsRead = false;

            if (dueDate.HasValue)
            {
                DueDate = dueDate;
                ManualDueDate = true;
            }

            if (clientId.HasValue && clientId.Value > 0)
            {
                RecordedClientID = clientId.Value;
            }


            var reassignTaskEvent = ReassignTaskEvent.Create(this, previousAssignedTeamId, previousAssignedUser);
            AddToReassignTaskEvent(reassignTaskEvent);

        }

        public void ResetTaskSla(DateTime dueDate, bool urgent, int taskModifyingReasonId, string comments, string userName)
        {
            AddToArchive();
            DueDate = dueDate;
            Urgent = urgent;
            LastModifyingReasonID = taskModifyingReasonId;
            LastModifiedComment = comments;
            LastModifiedBy = userName;
            LastModifiedDate = DateTime.Now;
            SlaStatus = GetTaskStatus().ToString();
            AvVersion = (byte?)AvVersions.Av3;
        }




        private DateTime StartOfWarningWindow()
        {
            DateTime warningWindowStart = DateTime.Now.AddMinutes(WarningWindow.Value);

            if (warningWindowStart > WorkingHours.ClosingTimeToday)
            {
                warningWindowStart = warningWindowStart.AddMinutes(WorkingHours.TimeClosedOvernight.TotalMinutes);
            }

            return warningWindowStart;
        }

        private DateTime StartOfAcceptableWindow()
        {
            DateTime acceptableWindowStart = DateTime.Now.AddMinutes(AcceptableWindow.Value);

            if (acceptableWindowStart > WorkingHours.ClosingTimeToday)
            {
                acceptableWindowStart = acceptableWindowStart.AddMinutes(WorkingHours.TimeClosedOvernight.TotalMinutes);
            }

            return acceptableWindowStart;
        }

        public bool IsOverDue()
        {
            return (DueDate.HasValue && DueDate < DateTime.Now);
        }

        public bool IsWithinSLA()
        {
            return ((DueDate.HasValue && WarningWindow.HasValue
                    && DueDate > StartOfWarningWindow() )
                    && (!AcceptableWindow.HasValue || DueDate < StartOfAcceptableWindow()));
        }

        public bool IsApproachingSLA()
        {
            return (DueDate.HasValue && WarningWindow.HasValue 
                    && DueDate > DateTime.Now
                    && DueDate < StartOfWarningWindow() );            
        }
        public bool IsJustStarted()
        {
            return (DueDate.HasValue && AcceptableWindow.HasValue 
                    && DueDate > DateTime.Now 
                    && DueDate > StartOfAcceptableWindow());
        }

        private void AddToReassignTaskEvent(ReassignTaskEvent reassignTaskEvent)
        {
            ReassignTaskEvents.Add(reassignTaskEvent);
        }

        private void AddToArchive()
        {
            TaskArchives.Add(TaskArchive.Create(this));
        }
    }
}
