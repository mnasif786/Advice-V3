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
using Task = Advice.Domain.Entities.Task;

namespace Advice.Data.Tests.Repository.TaskTests
{
    [TestFixture]
    public class GetDeletedTasksTests
    {

        private IAdviceDbContextManager _adviceDbContextManager;
        private Mock<AdviceEntities> _adviceEntities;
        private ITaskRepository _taskRepository;       

         [SetUp]
        public void TestSetup()
         {
            //Setup Task data
            var taskList = GetDeletedTasksList();

            var dbSetMockTask = DbSetInitialisedMockFactory<Task>.CreateDbSetInitalisedMock(taskList);
            _adviceEntities = new Mock<AdviceEntities>();
            _adviceEntities.Setup(x => x.Tasks).Returns(dbSetMockTask.Object);

            _adviceDbContextManager = new AdviceDbContextManager(_adviceEntities.Object);

            Mock<ITaskStoredProcRunner> mockStoredProcRunner = new Mock<ITaskStoredProcRunner>();
            _taskRepository = new TaskRepository(_adviceDbContextManager, mockStoredProcRunner.Object);

            var userData = GetUserData();
            var dbSetMockUser = DbSetInitialisedMockFactory<User>.CreateDbSetInitalisedMock(userData);
            _adviceEntities.Setup(x => x.Users).Returns(dbSetMockUser.Object);
        }

        [Test]
        public void given__deleted_tasks_requested_for_team_by_id_then_return_deleted_tasks_for_that_team()
        {
            long[] teamIdArray = {1};
            var tasks = _taskRepository.GetDeletedTasksByTeamIds(teamIdArray,7);
            Assert.That(tasks.Count(), Is.EqualTo(2));
        }

        [Test]
        public void given__deleted_tasks_requested_for_multiple_teams_by_id_then_return_deleted_tasks_for_teams()
        {
            long[] teamIdArray = { 1,2 };
            var tasks = _taskRepository.GetDeletedTasksByTeamIds(teamIdArray,7);
            
            Assert.That(tasks.Count(), Is.EqualTo(4));
        }

        [Test]
        public void given_deleted_tasks_requested_for_teams_id_when_task_not_assigned_to_team_but_to_user_in_team_then_display_those_tasks()
        {
            long[] teamIdArray = { 2 };
            var tasks = _taskRepository.GetDeletedTasksByTeamIds(teamIdArray, 7);

            long[] ecpectedTaskIds = { 5, 8 };

            Assert.That(tasks.Count(), Is.EqualTo(2));
            Assert.That(ecpectedTaskIds.Contains(tasks.First().TaskID), Is.True);
            Assert.That(ecpectedTaskIds.Contains(tasks.Last().TaskID), Is.True);
        }

        [Test]
        public void given_deleted_tasks_requested_for_a_user_by_name_then_return_deleted_tasks_for_that_user()
        {
            var username = "Mark.Brown";
            var tasks = _taskRepository.GetDeletedTasksByUserName(username,7); //Last 7 days

            Assert.That(tasks.ToList().Count, Is.EqualTo(2));
        }

        private List<User> GetUserData()
        {
            //setting up data for User
            var userData = new List<User>()
            {
                new User()
                {
                    UserID = 1,
                    Username = "Tom.Moody",
                    CreatedBy = "David.Boon",
                    CreatedDate = DateTime.Now.AddDays(-7),
                    Deleted = false,
                    LastModifiedBy = "David.Boon",
                    LastModifiedDate = DateTime.Now.AddDays(-7),
                    RoleID = 1,
                    TeamID = 2
                },

                new User()
                {
                    UserID = 2,
                    Username = "Mark.Brown",
                    CreatedBy = "David.Boon",
                    CreatedDate = DateTime.Now.AddDays(-7),
                    Deleted = false,
                    LastModifiedBy = "David.Boon",
                    LastModifiedDate = DateTime.Now.AddDays(-7),
                    RoleID = 1,
                    TeamID = 1
                },

                new User()
                {
                    UserID = 3,
                    Username = "Andy.White",
                    CreatedBy = "David.Boon",
                    CreatedDate = DateTime.Now.AddDays(-7),
                    Deleted = false,
                    LastModifiedBy = "David.Boon",
                    LastModifiedDate = DateTime.Now.AddDays(-7),
                    RoleID = 1,
                    TeamID = 2
                }
            };

            return userData;
            
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
                Completed = false
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
                Completed = false
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
                Completed = false
            };

            var task4 = new Task
            {
                TaskID = 4,
                AssignedTeamID = 1,
                AssignedUser = "Mark.Brown",
                EmailAddress = "na@peninsula-uk.com",
                PhoneNumber = "016199009977",
                Deleted = false,
                Completed = false
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
                Completed = false
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
                Completed = false
            };

            var task7 = new Task
            {
                TaskID = 7,
                AssignedTeamID = 2,
                AssignedUser = "Andy.White",
                EmailAddress = "na@peninsula-uk.com",
                PhoneNumber = "016199009977",
                Deleted = false,
                Completed = false
            };

            var task8 = new Task
            {
                TaskID = 8,
                AssignedTeamID = null,
                AssignedUser = "Andy.White",
                EmailAddress = "na@peninsula-uk.com",
                PhoneNumber = "016199009977",
                Deleted = true,
                DeletedDate = DateTime.Now.AddDays(-7),
                Completed = true
                
            };

            var task9 = new Task
            {
                TaskID = 9,
                AssignedTeamID = 2,
                AssignedUser = "Andy.White",
                EmailAddress = "na@peninsula-uk.com",
                PhoneNumber = "016199009977",
                Deleted = false,
                Completed = true
            };

            var taskList = new List<Task> { task1, task2, task3, task4, task5, task6, task7, task8, task9 };

            return taskList;
        }

    }
}
