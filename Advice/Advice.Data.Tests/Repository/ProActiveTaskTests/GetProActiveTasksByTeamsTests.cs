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
using Advice.Domain.Entities.Enums;
using Advice.Domain.Helper;
using Advice.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using Task = Advice.Domain.Entities.Task;

namespace Advice.Data.Tests.Repository.ProActiveTaskTests
{
    [TestFixture]
    public class GetProActiveTasksByTeamsTests :BaseTaskRepositoryTests
    {
        [SetUp]
        public new void SetUp()
        {
            var dbSetMockTask = SetupMockTaskData();
            AdviceEntities.Setup(x => x.Tasks).Returns(dbSetMockTask.Object);
           
            SetupMockUserData();
        }
        /*
        [Test]
        public void given_proactive_tasks_requested_for_a_single_team_by_Id_then_return_only_incomplete_proactive_tasks_for_that_team()
        {
            long[] teamIds = { 14 };

            var tasks = TaskRepository.GetProActiveTasksByTeams(teamIds);

            Assert.That(tasks.Count(), Is.EqualTo(1));
        }

        
        [Test]
        public void given_proactive_tasks_requested_for_multiple_teams_by_Id_then_return_incomplete_proactive_tasks_for_these_teams()
        {
            long[] teamIds = { 14, 3 };

            var tasks = TaskRepository.GetProActiveTasksByTeams(teamIds);

            Assert.That(tasks.Count(), Is.EqualTo(2));
        }

        [Test]
        public void given_proactive_tasks_requested_for_multiple_teams_by_Id_and_task_assigned_user_is_also_belong_to_one_of_the_team_then_return_incomplete_proactive_tasks_for_these_teams_user()
        {
            //user "Tom.Moody" belongs to teamid: 2 where Assigned teamId is 13 in the example data.
            long[] teamIds = { 2, 3 };

            var tasks = TaskRepository.GetProActiveTasksByTeams(teamIds);

            Assert.That(tasks.Count(), Is.EqualTo(3));
        }*/

        

        private Mock<System.Data.Entity.DbSet<Task>> SetupMockTaskData()
        {
            //Setting up data for task
            var taskData = new List<Task>()
            {
                
                new Task()
                {
                    TaskID = 101,
                    AcceptableWindow = null,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 13,
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
                    RecordedClientID = 11841,
                    TaskTypeID = 24,
                    TaskType = new TaskType{TaskTypeID = 24,Description = "Service Review"}
                },
                
                new Task()
                {
                    TaskID = 103,
                    AcceptableWindow = null,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 2,
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
                    RecordedClientID = 11841,
                    TaskTypeID = 24,
                    TaskType = new TaskType{TaskTypeID = 24,Description = "Service Review"}
                },

                new Task()
                {
                    TaskID = 104,
                    AcceptableWindow = null,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 3,
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
                    RecordedClientID = 11841,
                    TaskTypeID = 24,
                    TaskType = new TaskType{TaskTypeID = 24,Description = "Service Review"}
                },

                new Task()
                {
                    TaskID = 105,
                    AcceptableWindow = null,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 3,
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
                    TaskTypeID = 24,
                    TaskType = new TaskType{TaskTypeID = 24,Description = "Service Review"}
                },

                new Task()
                {
                    TaskID = 107,
                    AcceptableWindow = null,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 14,
                    AssignedUser = "Muhammad.Asif",
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
                    TaskTypeID = 24,
                    TaskType = new TaskType{TaskTypeID = 24,Description = "Service Review"}
                },

                new Task()
                {
                    TaskID = 107,
                    AcceptableWindow = null,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 14,
                    AssignedUser = "Muhammad.Asif",
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
                    TaskTypeID = 24,
                    TaskType = new TaskType{TaskTypeID = 24,Description = "Service Review"}
                },
                
            };

            return DbSetInitialisedMockFactory<Task>.CreateDbSetInitalisedMock(taskData);
        }

       
        private void SetupMockUserData()
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
                },

                new User()
                {
                    UserID = 3,
                    Username = "Muhammad.Asif",
                    CreatedBy = "David.Boon",
                    CreatedDate = DateTime.Now.AddDays(-7),
                    Deleted = false,
                    LastModifiedBy = "David.Boon",
                    LastModifiedDate = DateTime.Now.AddDays(-7),
                    RoleID = 1,
                    TeamID = 14
                }
            };

            var dbSetMockUser = DbSetInitialisedMockFactory<User>.CreateDbSetInitalisedMock(userData);
            AdviceEntities.Setup(x => x.Users).Returns(dbSetMockUser.Object);
        }

        //private List<Constants> GetProactiveConstants()
        //{
        //    var constantsList = new List<Constants>();
        //    constantsList.Add(new Constants());

        //    return constantsList;
        //}
    }
}
