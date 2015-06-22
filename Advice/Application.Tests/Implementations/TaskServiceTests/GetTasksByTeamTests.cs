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
    public class GetTasksByTeamTests : BaseTaskServiceTests
    {
        private IEnumerable<Task> _tasksList;

        [SetUp]
        public new void SetUp()
        {
            _tasksList = GetTaskList();
        }

        [Test]
        public void Given_Tasks_List_Then_return_tasks_for_a_team_Id_1()
        {

            var teamIds = new long[] { 1 };

            TaskRepositoryMock.Setup(x => x.GetTasksByTeamIds(teamIds))
                .Returns(_tasksList.Where(t => t.AssignedTeamID != null && teamIds.Contains((long)t.AssignedTeamID)));

            var taskService = GetTarget();

            var taskSearchResult = taskService.GetTasksByTeams(teamIds);

            Assert.That(taskSearchResult.Tasks.Count(), Is.EqualTo(4));

        }

        [Test]
        public void Given_Tasks_List_Then_return_tasks_for_team_Ids_1_and_2()
        {

            var teamIds = new long[] { 1,2 };

            TaskRepositoryMock.Setup(x => x.GetTasksByTeamIds(teamIds))
                .Returns(_tasksList.Where(t => t.AssignedTeamID != null && teamIds.Contains((long)t.AssignedTeamID)));

            var taskService = GetTarget();

            var taskSearchResult = taskService.GetTasksByTeams(teamIds);

            Assert.That(taskSearchResult.Tasks.Count(), Is.EqualTo(8));

        }

        [Test]
        public void Given_Tasks_List_Then_Correct_Timeline_Values_Are_Calculated_when_tasks_are_retireved_by_team_ids_1_and_2()
        {

            var teamIds = new long[] { 1, 2 };

            TaskRepositoryMock.Setup(x => x.GetTasksByTeamIds(teamIds))
                .Returns(_tasksList.Where(t => t.AssignedTeamID != null && teamIds.Contains((long)t.AssignedTeamID)));

            var taskService = GetTarget();

            var taskSearchResult = taskService.GetTasksByTeams(teamIds);

            Assert.That(taskSearchResult.Timeline.Red, Is.EqualTo(4));
            Assert.That(taskSearchResult.Timeline.Green, Is.EqualTo(2));
            Assert.That(taskSearchResult.Timeline.Amber, Is.EqualTo(1));
            Assert.That(taskSearchResult.Timeline.Platinum, Is.EqualTo(1));
            Assert.That(taskSearchResult.Timeline.Total, Is.EqualTo(8));
        }

        private List<Task> GetTaskList()
        {
            var taskList = new List<Task>()
            {
               new Task()
                {
                    TaskID = 3,
                    AcceptableWindow = 150,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 1,
                    AssignedUser = null,
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
                    AssignedTeamID = 1,
                    AssignedUser = null,
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
                    AssignedTeamID = 1,
                    AssignedUser = null,
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = true,
                    CompletedBy = null,
                    Deleted = false,
                    DueDate = new DateTime(2014, 08, 09), //overdue
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
                    AssignedTeamID = 1,
                    AssignedUser = null,
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = true,
                    CompletedBy = null,
                    Deleted = false,
                    DueDate = new DateTime(2014, 08, 09), //overdue
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
                    AssignedTeamID = 2,
                    AssignedUser = null,
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
                    AssignedTeamID = 2,
                    AssignedUser = null,
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
                    AssignedTeamID = 2,
                    AssignedUser = null,
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
                    AssignedTeamID = 2,
                    AssignedUser = null,
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
