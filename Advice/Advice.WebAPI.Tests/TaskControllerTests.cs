using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Results;
using Advice.Application;
using Advice.Application.Contracts;
using Advice.Common.Models;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Advice.Web.Controllers;
using Advice.Web.Helpers;
using Moq;
using NUnit.Framework;

namespace Advice.WebAPI.Tests
{
    [TestFixture]
    public class TaskControllerTests
    {
        private Mock<ITaskService> _taskService;
        private Mock<IUserIdentityFactory> _userIdentity;
        private List<TaskModel> tasks;
            
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            tasks = new List<TaskModel>()
            {
                new TaskModel() {TaskId = 1, AssignedUser = "User1", AssignedTeamId = 1, Status = DerivedTaskStatusForDisplay.Red},
                new TaskModel() {TaskId = 2, AssignedUser = "User1", AssignedTeamId = 1},
                new TaskModel() {TaskId = 3, AssignedUser = "User1", AssignedTeamId = 1},
                new TaskModel() {TaskId = 4, AssignedUser = "User2", AssignedTeamId = 2},
                new TaskModel() {TaskId = 5, AssignedUser = "User2" },
                new TaskModel() {TaskId = 6, AssignedUser = "User3"}, 
                new TaskModel() {TaskId = 7, AssignedUser = "User2"}, 
                new TaskModel() {TaskId = 8, AssignedUser = "user.name"},
                new TaskModel() {TaskId = 9, AssignedUser = "user.name"}
            };

           _taskService = new Mock<ITaskService>();
          
           _taskService.Setup(x => x.GetAllTasksByUser(It.IsAny<string>())).Returns(tasks);

            var identity = new Mock<IIdentity>();
            identity.Setup(x => x.Name).Returns("domain\\user.name");

            var principal = new Mock<IPrincipal>();
            principal.Setup(x => x.Identity).Returns(identity.Object);

            Thread.CurrentPrincipal = principal.Object;

            _userIdentity = new Mock<IUserIdentityFactory>();
            _userIdentity.Setup(x => x.GetUserIdentity(It.IsAny<IPrincipal>()))
                .Returns(new UserIdentity(Thread.CurrentPrincipal));
        }

        [Test]
        public void Given_Task_Exists_When_GetAll_Then_Return_All_Tasks()
        {
            var taskList = new TaskController(_taskService.Object, _userIdentity.Object);

            var actionResult = taskList.GetAllTasks();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<TaskModel>>;

            Assert.That(contentResult.Content.ToList().Count, Is.EqualTo(9));
        }

        [Test]
        public void Given_Task_When_Id_Requested_Then_Return_Task()
        {
            _taskService.Setup(x => x.GetById(It.IsAny<long>())).Returns(tasks[2]);

            var taskList = new TaskController(_taskService.Object, _userIdentity.Object);

            var actionResult = taskList.GetTask(3);
            var contentResult = actionResult as OkNegotiatedContentResult<TaskModel>;

            Assert.That(contentResult.Content.AssignedUser, Is.EqualTo("User1"));
        }


        [Test]
        public void Given_Task_When_User_Requested_Then_Return_Tasks_For_User()
        {
            _taskService.Setup(x => x.GetAllTasksByUser(It.IsAny<string>())).Returns(tasks.GetRange(7, 2));
           
            var taskController = new TaskController(_taskService.Object, _userIdentity.Object);

            var actionResult = taskController.GetTasksByUserName("user.name");
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<TaskModel>>;

            Assert.That(contentResult.Content.ToList()[0].TaskId, Is.EqualTo(8));
            Assert.That(contentResult.Content.ToList()[1].TaskId, Is.EqualTo(9));
        }

        [Test]
        public void Given_Task_When_Team_Name_Requested_Then_Return_Tasks_For_Team()
        {
            _taskService.Setup(x => x.GetAllTasksByTeamName(It.IsAny<string>())).Returns(tasks.GetRange(0, 3));

            var taskController = new TaskController(_taskService.Object, _userIdentity.Object);

            var actionResult = taskController.GetTasksByTeamName("user.name");
            
            _taskService.Verify(x => x.GetAllTasksByTeamName("user.name"), Times.Once());
            
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<TaskModel>>;

            Assert.That(contentResult.Content.ToList()[0].TaskId, Is.EqualTo(1));
            Assert.That(contentResult.Content.ToList()[1].TaskId, Is.EqualTo(2));
            Assert.That(contentResult.Content.ToList().Count, Is.EqualTo(3));
        }

        [Test]
        public void Given_Tasks_Exist_For_Team_When_Timeline_Requested_By_Team_Then_Timeline_Returned()
        {
            int red =1;
            int amber = 2; 
            int green = 3; 
            int platinum = 4; 
            int total = 10;

            TaskTimelineModel model = new TaskTimelineModel(red, amber, green, platinum, total);

            _taskService
                .Setup( x => x.GetTaskListTimelineByTeamId(It.IsAny<int>()) )
                .Returns( model );

            var taskList = new TaskController(_taskService.Object, _userIdentity.Object);

            int teamId = 123;
            var actionResult = taskList.GetTimelineByTeam( teamId );
            var contentResult = actionResult as OkNegotiatedContentResult<TaskTimelineModel>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);

            Assert.That(contentResult.Content.Red, Is.EqualTo(1));
            Assert.That(contentResult.Content.Amber, Is.EqualTo(2));
            Assert.That(contentResult.Content.Green, Is.EqualTo(3));
            Assert.That(contentResult.Content.Platinum, Is.EqualTo(4));
            Assert.That(contentResult.Content.Total, Is.EqualTo(10));
        }

        [Test]
        public void Given_Task_Status_Overdue_Then_Return_Task_Status_As_Red()
        {
            _taskService.Setup(x => x.GetAllTasksByTeamName(It.IsAny<string>())).Returns(tasks.GetRange(0,1));

            var taskController = new TaskController(_taskService.Object, _userIdentity.Object);
            var actionResult = taskController.GetTasksByTeamName("team");
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<TaskModel>>;

            Assert.That(contentResult.Content.ToList()[0].Status, Is.EqualTo(DerivedTaskStatusForDisplay.Red));
        }

        [Test]
        public void Given_Tasks_Exist_For_User_When_Timeline_Requested_By_Username_Then_Timeline_Returned()
        {
            int red = 1;
            int amber = 2;
            int green = 3;
            int platinum = 4;
            int total = 10;

            TaskTimelineModel model = new TaskTimelineModel(red, amber, green, platinum, total);

            _taskService
                .Setup(x => x.GetTaskListTimelineByUsername(It.IsAny<string>()))
                .Returns(model);

            var taskController = new TaskController(_taskService.Object, _userIdentity.Object);

            string username = "Barney.Rubble";
            var actionResult = taskController.GetTimelineByUser(username);
            var contentResult = actionResult as OkNegotiatedContentResult<TaskTimelineModel>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);

            Assert.That(contentResult.Content.Red, Is.EqualTo(1));
            Assert.That(contentResult.Content.Amber, Is.EqualTo(2));
            Assert.That(contentResult.Content.Green, Is.EqualTo(3));
            Assert.That(contentResult.Content.Platinum, Is.EqualTo(4));
            Assert.That(contentResult.Content.Total, Is.EqualTo(10));
        }

        [Test]
        public void Given_Task_History_Exists_Then_Return_Task_History()
        {
            var taskHistory = new List<TaskArchiveModel>()
            {
                new TaskArchiveModel()
                {                    
                    TaskId = 1,
                    Description = "First Archive element",
                   
                },

                 new TaskArchiveModel()
                {
                    TaskId = 1,
                    Description = "Second Archive element",
                },

                 new TaskArchiveModel()
                {
                    TaskId = 1,
                    Description = "Third Archive element",
                }               
            };

            _taskService.Setup( x => x.GetTaskHistoryByTaskId(It.IsAny<long>()))
                        .Returns(taskHistory);

            var taskController = new TaskController(_taskService.Object, _userIdentity.Object);
            var actionResult = taskController.GetTasksHistoryByTaskId( 123 );
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<TaskArchiveModel>>;

            Assert.NotNull( contentResult.Content );
            Assert.AreEqual(3, contentResult.Content.ToList().Count());

        }
    }
}
