using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Data.Repository;
using Advice.Data.Tests.TestHelpers;
using Advice.Domain;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Advice.Data.Tests.Repository.TaskTests
{
    [TestFixture]
    public class DeleteTaskByIdTests
    {
        private IAdviceDbContextManager _adviceDbContextManager;
        private Mock<AdviceEntities> _adviceEntities;
        private ITaskRepository _taskRepository;
        private List<Task> _tasks;
        private const string UserName = "Tom.McMillan";

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
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = false,
                    CompletedBy = null,
                    Deleted = false,
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
        public void Given_TaskId_When_Task_is_deleted_then_Task_is_Marked_as_Deleted()
        {
            var task = _tasks[0];

            task.MarkAsDeleted(UserName);
            _taskRepository.Update(task);
            var updatedTask = _adviceDbContextManager.Context.Tasks.AsQueryable();
            Assert.IsTrue(updatedTask.First().Deleted);
            Assert.That(updatedTask.First().DeletedBy, Is.EqualTo(UserName));
        }

    }
}
