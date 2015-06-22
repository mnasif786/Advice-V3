using System;
using System.Collections.Generic;
using System.Linq;
using Advice.Domain.Entities;
using Application.Tests.Common;
using NUnit.Framework;


namespace Application.Tests.Implementations.TaskServiceTests
{
    [TestFixture]
    public class GetDeletedTasksTests : BaseTaskServiceTests
    {
        private IEnumerable<Task> _tasksList;

        [SetUp]
        public void TestFixtureSetup()
        {
            _tasksList = GetDeletedTasksList();
        }

        [Test]
        public void given_deleted_tasks_requested_for_a_user_by_name_then_return_deleted_tasks_for_that_user_since_last_7_days()
        {
            var username = "Mark.Brown";
            var sinceLastDays = 7;

            TaskRepositoryMock.Setup(x => x.GetDeletedTasksByUserName(username, sinceLastDays))
                .Returns(_tasksList.Where(t => t.AssignedUser == username && t.Deleted && t.DeletedDate.HasValue && t.DeletedDate.Value.Date >= DateTime.Now.Date.AddDays(-sinceLastDays)));

            var taskService = GetTarget();
            var tasks = taskService.GetDeletedTasksByUserName(username, sinceLastDays); //Last 7 days

            Assert.That(tasks.Count(), Is.EqualTo(2));
        }

        [Test]
        public void given_deleted_tasks_requested_for_multiple_teams_by_team_ids_then_return_deleted_tasks_for_all_the_teams_since_last_7_days()
        {
            long[] teamIdArray = { 1, 2 };
            var sinceLastDays = 7;

            TaskRepositoryMock.Setup(x => x.GetDeletedTasksByTeamIds(teamIdArray, sinceLastDays))
                .Returns(_tasksList.Where(x =>
                    x.AssignedTeamID.HasValue && 
                    teamIdArray.Contains(x.AssignedTeamID.Value) &&
                    x.Deleted && 
                    x.DeletedDate.HasValue && 
                    x.DeletedDate.Value.Date >= DateTime.Now.Date.AddDays(-sinceLastDays)
                    ));

            var taskService = GetTarget();
            var tasks = taskService.GetDeletedTasksByTeamIds(teamIdArray, sinceLastDays).ToList(); //Last 7 days

            Assert.That(tasks.Count(), Is.EqualTo(3));
        }

        private List<Task> GetDeletedTasksList()
        {
            var task1 = new Task
            {
                TaskID = 1,
                AssignedTeamID = 1,
                AssignedUser = "Mark.Brown",
                EmailAddress = "na@peninsula-uk.com",
                PhoneNumber = "016199009977",
                Deleted = true,
                DeletedDate = DateTime.Now.AddDays(-5),
                Completed = false,
                TaskType = new TaskType { TaskTypeID = 3, Description = "Callback 1 Hour" }
            };


            var task2 = new Task
            {
                TaskID = 2,
                AssignedTeamID = 1,
                AssignedUser = "Mark.Brown",
                EmailAddress = "na@peninsula-uk.com",
                PhoneNumber = "016199009977",
                Deleted = true,
                DeletedDate = DateTime.Now.Date.AddDays(-7),
                Completed = false,
                TaskType = new TaskType { TaskTypeID = 3, Description = "Callback 1 Hour" }
            };

            var task3 = new Task
            {
                TaskID = 3,
                AssignedTeamID = 1,
                AssignedUser = "Mark.Brown",
                EmailAddress = "na@peninsula-uk.com",
                PhoneNumber = "016199009977",
                Deleted = true,
                DeletedDate = DateTime.Now.AddDays(-10),
                Completed = false,
                TaskType = new TaskType { TaskTypeID = 3, Description = "Callback 1 Hour" }
            };

            var task4 = new Task
            {
                TaskID = 4,
                AssignedTeamID = 1,
                AssignedUser = "Mark.Brown",
                EmailAddress = "na@peninsula-uk.com",
                PhoneNumber = "016199009977",
                Deleted = false,
                Completed = false,
                TaskType = new TaskType { TaskTypeID = 3, Description = "Callback 1 Hour" }
            };

            var task5 = new Task
            {
                TaskID = 5,
                AssignedTeamID = 2,
                AssignedUser = "Tom.Moody",
                EmailAddress = "na@peninsula-uk.com",
                PhoneNumber = "016199009977",
                Deleted = true,
                DeletedDate = DateTime.Now.AddDays(-7),
                Completed = false,
                TaskType = new TaskType { TaskTypeID = 3, Description = "Callback 1 Hour" }
            };

            var task6 = new Task
            {
                TaskID = 6,
                AssignedTeamID = 2,
                AssignedUser = "Andy.White",
                EmailAddress = "na@peninsula-uk.com",
                PhoneNumber = "016199009977",
                Deleted = true,
                DeletedDate = DateTime.Now.AddDays(-11),
                Completed = false,
                TaskType = new TaskType { TaskTypeID = 3, Description = "Callback 1 Hour" }
            };

            var task7 = new Task
            {
                TaskID = 7,
                AssignedTeamID = 2,
                AssignedUser = "Andy.White",
                EmailAddress = "na@peninsula-uk.com",
                PhoneNumber = "016199009977",
                Deleted = false,
                Completed = false,
                TaskType = new TaskType { TaskTypeID = 3, Description = "Callback 1 Hour" }
            };

            var taskList = new List<Task> { task1, task2, task3, task4, task5, task6, task7 };

            return taskList;
        }
    }
}
