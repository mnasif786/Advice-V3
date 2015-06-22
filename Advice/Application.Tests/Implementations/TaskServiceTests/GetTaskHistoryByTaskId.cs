using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advice.Application.Implementations;
using Advice.Common.Models;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Application.Tests.Common;
using Moq;
using NUnit.Framework;
using Peninsula.Domain.Entities;
using TaskType = Advice.Domain.Entities.TaskType;

namespace Application.Tests.Implementations.TaskServiceTests
{
    [TestFixture]
    public class GetTaskHistoryByTaskId : BaseTaskServiceTests
    {
        private List<Task> _tasks;
        private List<Team> _teams;
        private List<TaskArchive> _taskArchives;
        private List<TaskModifyingReason> _taskModifyingReasons;
        private List<TaskModifyingReasonGroup> _taskModifyingReasonGroups;
        private TaskService _taskService;
       
        private const string UserName = "Tom.McMillan";

        [SetUp]
        public void TestFixtureSetup()
        {
            _teams = new List<Team>()
            {
                new Team() { Description = "Team1", TeamID = 1, DepartmentID = 1},
                new Team() { Description = "Team2", TeamID = 2, DepartmentID = 1}
            };

            _taskModifyingReasonGroups = new List<TaskModifyingReasonGroup>()
            {
                new TaskModifyingReasonGroup()
                {
                    TaskModifyingReasonGroupID = 1,
                    Description = "Delete"
                },
                new TaskModifyingReasonGroup()
                {
                    TaskModifyingReasonGroupID = 2,
                    Description = "Reset SLA"
                }
            };

            _taskModifyingReasons = new List<TaskModifyingReason>()
            {
                new TaskModifyingReason()
                {
                    TaskModifyingReasonID = 1,
                    Description = "Delete",
                    TaskModifyingReasonGroup = _taskModifyingReasonGroups[0]
                },

                new TaskModifyingReason()
                {
                    TaskModifyingReasonID = 1,
                    Description = "Management Decision",
                    TaskModifyingReasonGroup = _taskModifyingReasonGroups[1]
                }
            };

            _tasks = new List<Task>()
            {
                new Task()
                {
                    TaskID = 1, 
                    AssignedUser = UserName,
                    AcceptableWindow = null,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 1,
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = false,
                    CompletedBy = null,
                    Deleted = false,
                    DueDate = DateTime.Now.AddDays(-7),
                    WarningWindow = 10,
                    RecordedClientID = 11004,
                    TaskTypeID = 3,
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"},
                    LastModifiedBy = UserName,
                    LastModifiedDate = DateTime.Now,
                    LastModifiedComment = "SLA updated",
                    TaskModifyingReason = _taskModifyingReasons[1],
                    SlaStatus = "Green",
                    AvVersion = (byte?) AvVersions.Av3
                },
               
                new Task() 
                {
                    TaskID = 7,
                    AcceptableWindow = null,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 1,
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = false,
                    CompletedBy = null,
                    Deleted = false,
                    DueDate = DateTime.Now.AddDays(-7),
                    WarningWindow = 10,
                    RecordedClientID = 11004,
                    TaskTypeID = 3,
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"},
                    LastModifiedBy = UserName,
                    LastModifiedDate = DateTime.Now,
                    LastModifiedComment = "SLA updated",
                    TaskModifyingReason = _taskModifyingReasons[0],
                    AssignedTeam = _teams[0],
                    SlaStatus = "Red"
                }
            };

            

            _taskArchives = new List<TaskArchive>()
            {
                new TaskArchive()
                {
                    TaskID = 1,
                    Description = "First Archive element",
                    CreatedDate = DateTime.Today.AddDays(-3),
                    Team = _teams[0],
                    TaskModifyingReason = _taskModifyingReasons[0],
                    LastModifiedDate = DateTime.Now.AddDays(-1),
                    SlaStatus = "Red",
                    AvVersion = (byte?) AvVersions.Av3
                },

                new TaskArchive()
                {
                    TaskID = 7,
                    Description = "Second Archive element",
                    CreatedDate = DateTime.Today.AddDays(-2),
                    AssignedUser = UserName,
                    TaskModifyingReason = _taskModifyingReasons[0],
                    LastModifiedDate = DateTime.Now.AddDays(-2),
                    SlaStatus = "Amber"
                  
                },

                new TaskArchive()
                {
                    TaskID = 7,
                    Description = "Third Archive element",
                    AssignedUser = UserName,
                    CreatedDate = DateTime.Today.AddDays(-1),
                    TaskModifyingReason = _taskModifyingReasons[1],
                    LastModifiedDate = DateTime.Now.AddDays(-3),
                     SlaStatus = "Green"
                   
                }               
            };

            _taskService = GetTarget();

        }

        [Test]
        public void Given_User_Requests_Task_History_Then_Task_History_Is_Returned()
        {
            const long taskId = 1;
            TaskArchiveRepositoryMock.Setup(x => x.GetTaskArchivesByTaskId(1)).Returns(_taskArchives);
            TaskRepositoryMock.Setup(x => x.GetTaskWithModifyingReasonAndTeamByTaskId(It.IsAny<long>()))
                .Returns(_tasks.SingleOrDefault(t => t.TaskID == 7));
            IEnumerable<TaskArchiveModel> result = _taskService.GetTaskHistoryByTaskId(taskId);

            Assert.NotNull(result);

            Assert.AreEqual(4, result.Count());
        }

        [Test]
        public void Given_Task_History_When_Task_Is_Assigned_To_Team_Then_Returns_TeamName()
        {
            TaskArchiveRepositoryMock.Setup(x => x.GetTaskArchivesByTaskId(1)).Returns(_taskArchives);
            TaskRepositoryMock.Setup(x => x.GetTaskWithModifyingReasonAndTeamByTaskId(It.IsAny<long>()))
                .Returns(_tasks.SingleOrDefault(t => t.TaskID == 7));
            var result = _taskService.GetTaskHistoryByTaskId(1).First();

            Assert.AreEqual(result.AssignedTo, "Team1");
        }

        [Test]
        public void Given_Task_History_When_Task_Is_Assigned_To_User_Then_Returns_UserName()
        {
            TaskArchiveRepositoryMock.Setup(x => x.GetTaskArchivesByTaskId(1)).Returns(_taskArchives);
            TaskRepositoryMock.Setup(x => x.GetTaskWithModifyingReasonAndTeamByTaskId(It.IsAny<long>()))
                .Returns(_tasks.SingleOrDefault(t => t.TaskID == 1));
            var result = _taskService.GetTaskHistoryByTaskId(1).First();

            Assert.AreEqual(result.AssignedTo, UserName);
        }

        [Test]
        public void Given_Task_History_When_Task_Is_Assigned_To_User_Then_Returns_Correct_TaskModifyingReason()
        {
            TaskArchiveRepositoryMock.Setup(x => x.GetTaskArchivesByTaskId(1)).Returns(_taskArchives);
            TaskRepositoryMock.Setup(x => x.GetTaskWithModifyingReasonAndTeamByTaskId(It.IsAny<long>()))
                .Returns(_tasks.SingleOrDefault(t => t.TaskID == 1));
            var result = _taskService.GetTaskHistoryByTaskId(1).First();

            Assert.AreEqual(result.TaskModifyingReason, "Management Decision");
            Assert.AreEqual(result.TaskModifyingReasonGroup, "Reset SLA");
        }

        [Test]
        public void Given_Task_History_When_TaskArchive_Is_Assigned_To_Team_Then_Returns_TeamName()
        {
            TaskArchiveRepositoryMock.Setup(x => x.GetTaskArchivesByTaskId(1)).Returns(_taskArchives.Where(x=>x.TaskID == 1));
            TaskRepositoryMock.Setup(x => x.GetTaskWithModifyingReasonAndTeamByTaskId(It.IsAny<long>()))
                .Returns(_tasks.SingleOrDefault(t => t.TaskID == 1));
            var result = _taskService.GetTaskHistoryByTaskId(1).Last();

            Assert.AreEqual(result.AssignedTo, "Team1");
        }

        [Test]
        public void Given_Task_History_When_TaskArchive_Is_Assigned_To_User_Then_Returns_UserName()
        {
            TaskArchiveRepositoryMock.Setup(x => x.GetTaskArchivesByTaskId(7)).Returns(_taskArchives.Where(x => x.TaskID == 7));
            TaskRepositoryMock.Setup(x => x.GetTaskWithModifyingReasonAndTeamByTaskId(It.IsAny<long>()))
                .Returns(_tasks.SingleOrDefault(t => t.TaskID == 7));
            var result = _taskService.GetTaskHistoryByTaskId(7).Last();

            Assert.AreEqual(result.AssignedTo, UserName);
        }

        [Test]
        public void Given_Task_History_When_TaskArchive_Is_Assigned_To_User_Then_Returns_Correct_TaskModifyingReason()
        {
            TaskArchiveRepositoryMock.Setup(x => x.GetTaskArchivesByTaskId(7)).Returns(_taskArchives.Where(x => x.TaskID == 7));
            TaskRepositoryMock.Setup(x => x.GetTaskWithModifyingReasonAndTeamByTaskId(It.IsAny<long>()))
                .Returns(_tasks.SingleOrDefault(t => t.TaskID == 7));
            var result = _taskService.GetTaskHistoryByTaskId(7).First();

            Assert.AreEqual(result.TaskModifyingReason, "Delete");
            Assert.AreEqual(result.TaskModifyingReasonGroup, "Delete");
        }

        [Test]
        public void Given_Task_History_When_TaskArchiveList_Is_returned_It_is_in_descending_order()
        {
            TaskArchiveRepositoryMock.Setup(x => x.GetTaskArchivesByTaskId(7)).Returns(_taskArchives.Where(x => x.TaskID == 7));
            TaskRepositoryMock.Setup(x => x.GetTaskWithModifyingReasonAndTeamByTaskId(It.IsAny<long>()))
                .Returns(_tasks.SingleOrDefault(t => t.TaskID == 7));
            var result = _taskService.GetTaskHistoryByTaskId(7);

            Assert.That(result, Is.Ordered.Descending.By("LastModifiedDate"));
        }
        
        [Test]
        public void Given_when_Task_History_is_requested_then_slastatus__for_current_Task_is_not_Null()
        {
            const long taskId = 7;
            TaskArchiveRepositoryMock.Setup(x => x.GetTaskArchivesByTaskId(taskId)).Returns(_taskArchives.Where(x => x.TaskID == taskId));
            TaskRepositoryMock.Setup(x => x.GetTaskWithModifyingReasonAndTeamByTaskId(taskId))
                .Returns(_tasks.SingleOrDefault(t => t.TaskID == 7));
            IEnumerable<TaskArchiveModel> result = _taskService.GetTaskHistoryByTaskId(taskId);
            Assert.That(result.ToList()[0].SlaStatus, Is.EqualTo("Red"));
        }

        [Test]
        public void Given_when_AvVersion_is_Null_And_Task_History_is_requested_then_sla_status_for_Archive_is_returned_As_Null()
        {
            const long taskId = 7;
            TaskArchiveRepositoryMock.Setup(x => x.GetTaskArchivesByTaskId(taskId)).Returns(_taskArchives.Where(x => x.TaskID == taskId));
            TaskRepositoryMock.Setup(x => x.GetTaskWithModifyingReasonAndTeamByTaskId(taskId))
                .Returns(_tasks.SingleOrDefault(t => t.TaskID == 7));
            IEnumerable<TaskArchiveModel> result = _taskService.GetTaskHistoryByTaskId(taskId);
            Assert.IsNull(result.ToList()[1].SlaStatus);
            Assert.IsNull(result.ToList()[2].SlaStatus);
        }

        [Test]
        public void Given_when_AvVersion_is_Not_Null_And_Task_History_is_requested_then_sla_status_for_Archive_is_returned_As_NotNull()
        {
            const long taskId = 1;
            TaskArchiveRepositoryMock.Setup(x => x.GetTaskArchivesByTaskId(taskId)).Returns(_taskArchives.Where(x => x.TaskID == taskId));
            TaskRepositoryMock.Setup(x => x.GetTaskWithModifyingReasonAndTeamByTaskId(taskId))
                .Returns(_tasks.SingleOrDefault(t => t.TaskID == 7));
            IEnumerable<TaskArchiveModel> result = _taskService.GetTaskHistoryByTaskId(taskId);
            Assert.IsNotNull(result.ToList()[0].SlaStatus);
            Assert.IsNotNull(result.ToList()[1].SlaStatus);
        }
    }
}
