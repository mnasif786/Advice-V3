using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Application.Tests.Common;
using NUnit.Framework;
using Task = Advice.Domain.Entities.Task;

namespace Application.Tests.Implementations.TaskServiceTests
{
    public class GetTasksByUserTests: BaseTaskServiceTests
    {
        private IEnumerable<Task> _tasksList;

        [SetUp]
        public new void SetUp()
        {
            _tasksList = GetTaskList();
        }

        [Test]
        public void Given_Tasks_List_Then_return_tasks_for_a_user()
        {

            var userName = "Muhammadnauman.Asif";

            TaskRepositoryMock.Setup(x => x.GetTasksByUserName(userName))
                .Returns(_tasksList.Where(t => t.AssignedUser == userName));

            var taskService = GetTarget();

            var taskSearchResult = taskService.GetTasksByUser(userName);

            Assert.That(taskSearchResult.Tasks.Count(), Is.EqualTo(7));

        }

        [Test]
        public void Given_Tasks_List_Then_Correct_Timeline_Values_Are_Calculated_when_tasks_are_retireved_by_UserName()
        {

            var userName = "Muhammadnauman.Asif";

            TaskRepositoryMock.Setup(x => x.GetTasksByUserName(userName))
                .Returns(_tasksList.Where(t => t.AssignedUser == userName));

            var taskService = GetTarget();

            var taskSearchResult = taskService.GetTasksByUser(userName);

            Assert.That(taskSearchResult.Timeline.Red, Is.EqualTo(3));
            Assert.That(taskSearchResult.Timeline.Green, Is.EqualTo(2));
            Assert.That(taskSearchResult.Timeline.Amber, Is.EqualTo(1));
            Assert.That(taskSearchResult.Timeline.Platinum, Is.EqualTo(1));
            Assert.That(taskSearchResult.Timeline.Total, Is.EqualTo(7));
        }

        private List<Task> GetTaskList()
        {
            var taskList = new List<Task>()
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
                    DueDate = DateTime.Now.AddDays(-1), //overdue
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
                    DueDate = DateTime.Now.AddDays(-1), //overdue
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
                    DueDate = new DateTime(2014, 09, 09), //overdue
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
                },

                 new Task() //Within Sla Task
                {
                    TaskID = 7,
                    AcceptableWindow = null,
                    AssignedDate = DateTime.Now.AddDays(7),
                    AssignedTeamID = 1,
                    AssignedUser = "Muhammadnauman.Asif",
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = false,
                    CompletedBy = null,
                    Deleted = false,
                    WarningWindow = 10, 
                    DueDate = DateTime.Now.AddMinutes(15),
                    RecordedClientID = 11843,
                    TaskTypeID = 9,
                    TaskType = new TaskType{TaskTypeID = 9,Description = "Follow Up Task"}
                },

                new Task() //Within Sla Task using acceptable window
                {
                    TaskID = 8,
                    AcceptableWindow = 30,
                    AssignedDate = DateTime.Now.AddDays(7),
                    AssignedTeamID = 1,
                    AssignedUser = "Muhammadnauman.Asif",
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = false,
                    CompletedBy = null,
                    Deleted = false,
                    WarningWindow = 10, //Within Sla
                    DueDate = DateTime.Now.AddMinutes(20), //Within Sla
                    RecordedClientID = 11843,
                    TaskTypeID = 9,
                    TaskType = new TaskType{TaskTypeID = 9,Description = "Follow Up Task"}
                },

                new Task() //appraoching sla
                {
                    TaskID = 9,
                    AcceptableWindow = 30,
                    AssignedDate = DateTime.Now.AddDays(7),
                    AssignedTeamID = 1,
                    AssignedUser = "Muhammadnauman.Asif",
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = false,
                    CompletedBy = null,
                    Deleted = false,
                    WarningWindow = 30, //appraoching Sla
                    DueDate = DateTime.Now.AddMinutes(20), //appraoching Sla
                    RecordedClientID = 11843,
                    TaskTypeID = 9,
                    TaskType = new TaskType{TaskTypeID = 9,Description = "Follow Up Task"}
                },

                new Task() //just started
                {
                    TaskID = 10,
                    AcceptableWindow = 10, //just started
                    AssignedDate = DateTime.Now.AddDays(7),
                    AssignedTeamID = 1,
                    AssignedUser = "Muhammadnauman.Asif",
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = false,
                    CompletedBy = null,
                    Deleted = false,
                    WarningWindow = 5, 
                    DueDate = DateTime.Now.AddMinutes(20), //just started
                    RecordedClientID = 11843,
                    TaskTypeID = 9,
                    TaskType = new TaskType{TaskTypeID = 9,Description = "Follow Up Task"}
                },
                
            };

            return taskList;
        }
    }
}
