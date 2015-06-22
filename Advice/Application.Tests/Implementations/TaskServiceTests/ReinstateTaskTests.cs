using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Advice.Application.Implementations;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Advice.Domain.RepositoryContracts;
using Advice.ExchangeEmails;
using Application.Tests.Common;
using EmailServer.Responses;
using Moq;
using NUnit.Framework;
using Peninsula.Domain.Entities;
using Peninsula.Domain.RepositoryContracts;

namespace Application.Tests.Implementations.TaskServiceTests
{
    [TestFixture]
    public class ReinstateTaskTests : BaseTaskServiceTests
    {
        private Task _task;
        private string _userName = "nauman.asif";

        [SetUp]
        public new void SetUp()
        {
            _task = GetTask();

            TaskRepositoryMock.Setup(t => t.GetById(It.IsAny<long>()))
                .Returns(_task);

        }

        [Test]
        public void Given_Deleted_Task_When_Reinstate_Then_Delete_is_false()
        {
            var task = _task;
            var taskService = GetTarget();

            taskService.ReinstateTask(task.TaskID, _userName);

            Assert.IsFalse(_task.Deleted);
        }

        [Test]
        public void Given_Deleted_Task_When_Reinstate_Then_Deleted_Date_is_null()
        {
            var task = _task;
            var taskService = GetTarget();

            taskService.ReinstateTask(task.TaskID, _userName);
            Assert.IsNull(_task.DeletedDate);
        }

        [Test]
        public void Given_Deleted_Task_When_Reinstate_Then_Deleted_By_is_null()
        {
            var task = _task;
            var taskService = GetTarget();

            taskService.ReinstateTask(task.TaskID, _userName);
            Assert.IsNull(_task.DeletedBy);
        }

        [Test]
        public void Given_Deleted_Task_When_Reinstate_Then_Delete_Date_is_null()
        {
            var task = _task;
            var taskService = GetTarget();

            taskService.ReinstateTask(task.TaskID, _userName);
            Assert.IsNull(_task.DeletedDate);
        }

        [Test]
        public void Given_Deleted_Task_When_Reinstate_Then_Last_Modified_Reason_Id_is_Reinstate()
        {
            var task = _task;
            var taskService = GetTarget();

            taskService.ReinstateTask(task.TaskID, _userName);

            Assert.IsNotNull(_task.LastModifyingReasonID);
            Assert.That(_task.LastModifyingReasonID, Is.EqualTo((long)TaskModifyingReasons.Reinstate));
        }

        [Test]
        public void Given_Deleted_Task_When_Reinstate_Then_Last_Modified_Date_is_Current_Date()
        {
            var task = _task;
            var taskService = GetTarget();

            taskService.ReinstateTask(task.TaskID, _userName);
            Assert.That(_task.LastModifiedDate.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Given_Deleted_Task_When_Reinstate_Then_Last_Modified_By_is_Set_To_Current_User()
        {
            var task = _task;
            var taskService = GetTarget();

            taskService.ReinstateTask(task.TaskID, _userName);
            Assert.That(_task.LastModifiedBy, Is.EqualTo(_userName));
        }

        [Test]
        public void Given_Deleted_Task_When_Reinstate_Then_Completed_is_False()
        {
            var task = _task;
            var taskService = GetTarget();

            taskService.ReinstateTask(task.TaskID, _userName);
            Assert.IsFalse(_task.Completed);
        }

        [Test]
        public void Given_Deleted_Task_When_Reinstate_Then_Last_modified_Comment_is_Null()
        {
            var task = _task;
            var taskService = GetTarget();

            taskService.ReinstateTask(task.TaskID, _userName);
            Assert.IsNull(_task.LastModifiedComment);
        }


        private Task GetTask()
        {
            return new Task
            {
                TaskID = 1,
                AcceptableWindow = null,
                AssignedDate = DateTime.Now.AddDays(-7),
                AssignedTeamID = 1,
                AssignedUser = "Tom.Moody",
                Cancelled = true,
                CancelledBy = null,
                CancelledDate = null,
                CancelledReason = null,
                Completed = false,
                CompletedBy = null,
                Deleted = true,
                DeletedBy = "Mark.Jepson",
                LastModifyingReasonID = null,
                LastModifiedDate = DateTime.Now.AddDays(-17),
                LastModifiedBy = "Tom.Moody",
                LastModifiedComment = "Task Deleted.",
                DueDate = new DateTime(2014, 08, 09),
                WarningWindow = 10,
                RecordedClientID = 11841
            };
        }

    }
}
