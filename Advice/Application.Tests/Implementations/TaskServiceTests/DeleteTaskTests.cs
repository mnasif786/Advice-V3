using System;
using System.Collections.Generic;
using Advice.Domain.Entities;
using Application.Tests.Common;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Implementations.TaskServiceTests
{
    public class DeleteTaskTests : BaseTaskServiceTests
    {
        //private Task _task;
        //[Test]
        //public void Given_DeleteTask_When_Deleting_Task_TaskArchive_Is_Set_With_Task_Data()
        //{
        //    var taskService = GetTarget();
        //    _task = GetTask();
        //    var taskArchive = new TaskArchive();
        //    TaskRepositoryMock.Setup(x => x.GetTaskByTaskId(It.IsAny<long>())).Returns(_task);
        //    TaskArchiveRepositoryMock.Setup(x => x.Insert(taskArchive));
        //    taskService.Delete(_task.TaskID, _task.AssignedUser);
        //    Assert.That(taskArchive.TaskID, Is.EqualTo(_task.TaskID));
        //    Assert.That(taskArchive.AcceptableWindow, Is.EqualTo(_task.AcceptableWindow));
        //    Assert.That(taskArchive.AssignedDate, Is.EqualTo(_task.AssignedDate));
        //    Assert.That(taskArchive.AssignedTeamID, Is.EqualTo(_task.AssignedTeamID));
        //    Assert.That(taskArchive.AssignedUser, Is.EqualTo(_task.AssignedUser));
        //    Assert.That(taskArchive.Cancelled, Is.EqualTo(_task.Cancelled));
        //}

        private Task GetTask()
        {
            return new Task
            {
                TaskID = 1,
                TaskTypeID = 1,
                Description = "Test",
                DueDate = DateTime.Now.AddDays(-1),
                WarningWindow = 1,
                AcceptableWindow = 1,
                AssignedUser = "Tom.McMillan",
                AssignedTeamID = 1,
                AssignedDate = DateTime.Now.AddDays(-7),
                CompletedBy = null,
                CompletionDate = null,
                Urgent = true,
                ManualDueDate = false,
                RecordedClientID = 111211,
                CancelledReason = string.Empty,
                CancelledDate = null,
                CancelledBy = null,
                PreviousTaskID = null,
                CreatedBy = "Michelle.Leone",
                CreatedDate = DateTime.Now.AddDays(-14),
                LastModifiedBy = "Michelle.Leone",
                LastModifiedDate = DateTime.Now.AddDays(-7),
                LastModifyingReasonID = 1,
                LastModifiedComment = "Deleting",
                Deleted = false,
                DeletedBy = null,
                DeletedDate = null,
                EmailAddress = null,
                PhoneNumber = null,
                ContactName = null,
                Completed = false,
                Cancelled = false,
                ManualDueDateReason = null,
                IsRead = true,
                DocumentCount = 1,
                JobSubject = null
            };
        }
    }
}
