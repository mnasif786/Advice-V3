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
    public class ResetTaskSlaModel
    {
        public long TaskId { get; set; }
        public DateTime DueDate { get; set; }
        public bool Urgent { get; set; }
        public int TaskModifyingReasonId { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }

    }

    [TestFixture]
    public class ResetTaskSlaTests : BaseTaskServiceTests
    {
        private Task _task;
        //private string _userName = "Shaun.Hughes";
        private ResetTaskSlaModel _resetTaskSlaModel;

        [SetUp]
        public new void SetUp()
        {
            _task = GetTask();

            _resetTaskSlaModel = new ResetTaskSlaModel()
            {
                TaskId = 1,
                DueDate = DateTime.Now.AddDays(2),
                Urgent = true,
                TaskModifyingReasonId = 6,
                Description = "Resetting Task Sla",
                UserName = "Shaun.Miller"
            };

            TaskRepositoryMock.Setup(t => t.GetById(It.IsAny<long>()))
                .Returns(_task);
        }

        [Test]
        public void Given_ResetSlaTask_When_DueDate_Is_Set_Then_DueDate_Is_Updated()
        {
            var taskService = GetTarget();

            taskService.ResetTaskSla(_resetTaskSlaModel.TaskId, _resetTaskSlaModel.DueDate, _resetTaskSlaModel.Urgent,
               _resetTaskSlaModel.TaskModifyingReasonId, _resetTaskSlaModel.Description, _resetTaskSlaModel.UserName);

            Assert.AreEqual(_resetTaskSlaModel.DueDate, _task.DueDate);
        }

        [Test]
        public void Given_ResetSlaTask_When_Urgent_Is_Set_Then_Urgent_is_Updated()
        {
            var taskService = GetTarget();

            taskService.ResetTaskSla(_resetTaskSlaModel.TaskId, _resetTaskSlaModel.DueDate, _resetTaskSlaModel.Urgent,
                _resetTaskSlaModel.TaskModifyingReasonId, _resetTaskSlaModel.Description, _resetTaskSlaModel.UserName);

            Assert.AreEqual(_resetTaskSlaModel.Urgent, _task.Urgent);
        }

        [Test]
        public void Given_ResetSlaTask_When_TaskModifyingReasonId_Is_Set_Then_LastModifyingReasonID_is_Updated()
        {
            var taskService = GetTarget();

            taskService.ResetTaskSla(_resetTaskSlaModel.TaskId, _resetTaskSlaModel.DueDate, _resetTaskSlaModel.Urgent,
                _resetTaskSlaModel.TaskModifyingReasonId, _resetTaskSlaModel.Description, _resetTaskSlaModel.UserName);

            Assert.AreEqual(_resetTaskSlaModel.TaskModifyingReasonId, _task.LastModifyingReasonID);
        }

        [Test]
        public void Given_ResetSlaTask_When_Description_Is_Set_Then_LastModifiedComment_is_Updated()
        {
            var taskService = GetTarget();

            taskService.ResetTaskSla(_resetTaskSlaModel.TaskId, _resetTaskSlaModel.DueDate, _resetTaskSlaModel.Urgent,
                _resetTaskSlaModel.TaskModifyingReasonId, _resetTaskSlaModel.Description, _resetTaskSlaModel.UserName);

            Assert.AreEqual(_resetTaskSlaModel.Description, _task.LastModifiedComment);
        }

        [Test]
        public void Given_ResetSlaTask_When_UserName_Is_Set_Then_LastModifiedBy_is_Updated()
        {
            var taskService = GetTarget();

            taskService.ResetTaskSla(_resetTaskSlaModel.TaskId, _resetTaskSlaModel.DueDate, _resetTaskSlaModel.Urgent,
                _resetTaskSlaModel.TaskModifyingReasonId, _resetTaskSlaModel.Description, _resetTaskSlaModel.UserName);

            Assert.AreEqual(_resetTaskSlaModel.UserName, _task.LastModifiedBy);
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
                Urgent = false,
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
