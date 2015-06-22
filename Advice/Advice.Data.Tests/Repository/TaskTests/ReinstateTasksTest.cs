using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Data.Repository;
using Advice.Data.Tests.TestHelpers;
using Advice.Domain;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Advice.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Advice.Data.Tests.Repository.TaskTests
{
    public class ReinstateTasksTest
    {
        private IAdviceDbContextManager _adviceDbContextManager;
        private Mock<AdviceEntities> _adviceEntities;
        private ITaskRepository _taskRepository;
        private List<Task> _tasks;
        private const string UserName = "Jonty.Rhodes";

        [SetUp]
        public void Setup()
        {
            _adviceEntities = new Mock<AdviceEntities>();
            _adviceDbContextManager = new AdviceDbContextManager(_adviceEntities.Object);

            _tasks = new List<Task>()
            {
                 new Task
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
                }
            };

            var dbSetMockTask = DbSetInitialisedMockFactory<Task>.CreateDbSetInitalisedMock(_tasks);

            _adviceEntities.Setup(x => x.Tasks).Returns(dbSetMockTask.Object);
            _adviceEntities.Setup(x => x.Set<Task>()).Returns(dbSetMockTask.Object);

            Mock<ITaskStoredProcRunner> mockStoredProcRunner = new Mock<ITaskStoredProcRunner>();
            _taskRepository = new TaskRepository(_adviceDbContextManager, mockStoredProcRunner.Object);
        }

        [Test]
        public void Given_Deleted_Task_When_Reinstate_Then_Delete_is_set_to_false()
        {
            var task = _tasks[0];

            task.Reinstate(UserName);
            _taskRepository.Update(task);
            var updatedTask = _adviceDbContextManager.Context.Tasks.AsQueryable();
            Assert.IsFalse(updatedTask.First().Deleted);
        }

        [Test]
        public void Given_Deleted_Task_When_Reinstate_Then_Deleted_Date_is_set_to_null()
        {
            var task = _tasks[0];

            task.Reinstate(UserName);
            _taskRepository.Update(task);
            var updatedTask = _adviceDbContextManager.Context.Tasks.AsQueryable();
            Assert.IsNull(updatedTask.First().DeletedDate);
        }

        [Test]
        public void Given_Deleted_Task_When_Reinstate_Then_Deleted_By_is_set_to_null()
        {
            var task = _tasks[0];

            task.Reinstate(UserName);
            _taskRepository.Update(task);
            var updatedTask = _adviceDbContextManager.Context.Tasks.AsQueryable();
            Assert.IsNull(updatedTask.First().DeletedBy);
        }

        [Test]
        public void Given_Deleted_Task_When_Reinstate_Then_Delete_Date_is_set_to_null()
        {
            var task = _tasks[0];

            task.Reinstate(UserName);
            _taskRepository.Update(task);
            var updatedTask = _adviceDbContextManager.Context.Tasks.AsQueryable();
            Assert.IsNull(updatedTask.First().DeletedDate);
        }

        [Test]
        public void Given_Deleted_Task_When_Reinstate_Then_Last_Modified_Reason_Id_is_Not_Null()
        {
            var task = _tasks[0];

            task.Reinstate(UserName);
            _taskRepository.Update(task);
            var updatedTask = _adviceDbContextManager.Context.Tasks.AsQueryable();
            Assert.IsNotNull(updatedTask.First().LastModifyingReasonID);
            Assert.That(updatedTask.First().LastModifyingReasonID, Is.EqualTo((long)TaskModifyingReasons.Reinstate));
        }

        [Test]
        public void Given_Deleted_Task_When_Reinstate_Then_Last_Modified_Date_is_Set_To_Current_Date()
        {
            var task = _tasks[0];

            task.Reinstate(UserName);
            _taskRepository.Update(task);
            var updatedTask = _adviceDbContextManager.Context.Tasks.AsQueryable();
            Assert.That(updatedTask.First().LastModifiedDate.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Given_Deleted_Task_When_Reinstate_Then_Last_Modified_By_is_Set_To_Current_User()
        {
            var task = _tasks[0];

            task.Reinstate(UserName);
            _taskRepository.Update(task);
            var updatedTask = _adviceDbContextManager.Context.Tasks.AsQueryable();
            Assert.That(updatedTask.First().LastModifiedBy, Is.EqualTo(UserName));
        }

       [Test]
        public void Given_Deleted_Task_When_Reinstate_Then_Completed_is_Set_To_False()
        {
            var task = _tasks[0];

            task.Reinstate(UserName);
            _taskRepository.Update(task);
            var updatedTask = _adviceDbContextManager.Context.Tasks.AsQueryable();
            Assert.IsFalse(updatedTask.First().Completed);
        }

       [Test]
       public void Given_Deleted_Task_When_Reinstate_Then_Last_modified_Comment_is_Set_To_Null()
       {
           var task = _tasks[0];

           task.Reinstate(UserName);
           _taskRepository.Update(task);
           var updatedTask = _adviceDbContextManager.Context.Tasks.AsQueryable();
           Assert.IsNull(updatedTask.First().LastModifiedComment);
       }
    }
}
