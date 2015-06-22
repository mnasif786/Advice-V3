using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Contracts;
using Advice.Data.Tests.Common;
using Advice.Data.Tests.TestHelpers;
using Advice.Domain;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using Task = Advice.Domain.Entities.Task;

namespace Advice.Data.Tests.Repository.ProActiveTaskTests
{
    [TestFixture]
    public class GetProActiveTasksByUserTests :BaseTaskRepositoryTests
    {
        [SetUp]
        public new void SetUp()
        {
            var dbSetMockTask = SetupMockTaskData();
            AdviceEntities.Setup(x => x.Tasks).Returns(dbSetMockTask.Object);
            AdviceEntities.Setup(x => x.Set<Task>()).Returns(dbSetMockTask.Object);
        }

        [Test]
        public void given_proactive_tasks_requested_for_a_user_by_name_then_return_proactive_tasks_for_that_user()
        {
            var userName = "Muhammadnauman.Asif";
            var tasks = TaskRepository.GetProActiveTasksByUserName(userName);
           
            Assert.That(tasks.ToList().Count, Is.EqualTo(2));
        }

        private Mock<System.Data.Entity.DbSet<Task>> SetupMockTaskData()
        {
            //Setting up data for task
            var taskData = new List<Task>()
            {
                new Task()
                {
                    TaskID = 1,
                    AcceptableWindow = null,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 1,
                    AssignedUser = "Rana.Khan",
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = false,
                    CompletedBy = null,
                    Deleted = false,
                    DueDate = new DateTime(2014, 08, 09),
                    WarningWindow = 10,
                    RecordedClientID = 11841,
                    TaskTypeID = 9,
                    TaskType = new TaskType{TaskTypeID = 9,Description = "Follow Up Task"}
                },

                new Task()
                {
                    TaskID = 2,
                    AcceptableWindow = 45,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 1,
                    AssignedUser = "Muhammadnauman.Asif",
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = false,
                    CompletedBy = null,
                    Deleted = false,
                    DueDate = new DateTime(2014, 07, 27),
                    WarningWindow = 35,
                    RecordedClientID = 11841,
                    TaskTypeID = 9,
                    TaskType = new TaskType{TaskTypeID = 9,Description = "Follow Up Task"}
                },

                new Task()
                {
                    TaskID = 3,
                    AcceptableWindow = 150,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 1,
                    AssignedUser = "Muhammadnauman.Asif",
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = false,
                    CompletedBy = null,
                    Deleted = false,
                    DueDate = new DateTime(2014, 09, 09),
                    WarningWindow = 90,
                    RecordedClientID = 11843,
                    TaskTypeID = 9,
                    TaskType = new TaskType{TaskTypeID = 9,Description = "Follow Up Task"}
                },
                 new Task()
                {
                    TaskID = 4,
                    AcceptableWindow = 150,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 2,
                    AssignedUser = "Muhammadnauman.Asif",
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = false,
                    CompletedBy = null,
                    Deleted = false,
                    DueDate = new DateTime(2014, 09, 09),
                    WarningWindow = 90,
                    RecordedClientID = 11843,
                    TaskTypeID = 3,
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"}
                },
                 new Task()
                {
                    TaskID = 5,
                    AcceptableWindow = null,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 3,
                    AssignedUser = "Tom.Moody",
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = true,
                    CompletedBy = null,
                    Deleted = false,
                    DueDate = new DateTime(2014, 08, 09),
                    WarningWindow = 10,
                    RecordedClientID = 11841,
                    TaskTypeID = 9,
                    TaskType = new TaskType{TaskTypeID = 9,Description = "Callback 1 Hour"}
                },
                 new Task()
                {
                    TaskID = 6,
                    AcceptableWindow = null,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 3,
                    AssignedUser = "Tom.Moody",
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = true,
                    CompletedBy = null,
                    Deleted = false,
                    DueDate = new DateTime(2014, 08, 09),
                    WarningWindow = 10,
                    RecordedClientID = 11841,
                    TaskTypeID = 3,
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Follow Up Task"}
                }
                
            };

            return DbSetInitialisedMockFactory<Task>.CreateDbSetInitalisedMock(taskData);
        }
    }
}
