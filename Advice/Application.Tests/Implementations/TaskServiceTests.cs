using System;
using System.Collections.Generic;
using System.Linq;
using Advice.Application.Implementations;
using Advice.Common.Models;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Advice.ExchangeEmails;
using Application.Tests.Common;
using Moq;
using NUnit.Framework;
using Peninsula.Domain.Entities;
using TaskType = Advice.Domain.Entities.TaskType;

namespace Advice.Application.Tests
{
    [TestFixture]
    public class TaskServiceTests : BaseTaskServiceTests
    {
        private List<Task> _tasks;
        private List<Team> _teams;
        private List<TBLCustomer> _tblCustomers;
        private const string UserName = "Tom.McMillan";
            
        [SetUp]
        public void TestFixtureSetup()
        {
            _tblCustomers = new List<TBLCustomer>()
            {
                new TBLCustomer()
                {
                    CustomerID = 11004,
                    CustomerKey = "ZPEN001",
                    CompanyName = "The Peninsula Building"
                },
                new TBLCustomer()
                {
                    CustomerID = 11005,
                    CustomerKey = "ZPEN002",
                    CompanyName = "Customer Care"
                },
                new TBLCustomer()
                {
                    CustomerID = 11006,
                    CustomerKey = "ZPEN003",
                    CompanyName = "Special Projects"
                }
            };

            _teams = new List<Team>()
            {
                new Team() { Description = "Team1", TeamID = 1, DepartmentID = 1},
                new Team() { Description = "Team2", TeamID = 2, DepartmentID = 1}
            };
            
            _tasks = new List<Task>()
            {
                new Task() {TaskID = 1, AssignedUser = "User1", JobSubject = "Task1", RecordedClientID = 11004, AssignedTeamID = 1, TaskTypeID = 3, TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"}, Deleted = false},
                new Task() {TaskID = 2, AssignedUser = "User1", JobSubject = "Task2", RecordedClientID = 11005, AssignedTeamID = 2, TaskTypeID = 3, TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 2 Hour"}, Deleted = false},
                new Task() {TaskID = 3, AssignedUser = "User1", JobSubject = "Task3", RecordedClientID = 11006, AssignedTeamID = 1, TaskTypeID = 3, TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"}, Deleted = false},
                new Task() {TaskID = 4, AssignedUser = "User2", JobSubject = "Task4", RecordedClientID = 11005, AssignedTeamID = 1, TaskTypeID = 3, TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"}, Deleted = false},
                new Task() {TaskID = 5, AssignedUser = "User2", JobSubject = "Task5", RecordedClientID = 11005, AssignedTeamID = 2, TaskTypeID = 3, TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"}, Deleted = false},
                new Task() {TaskID = 6, AssignedUser = "User3", JobSubject = "Task6", RecordedClientID = 11006, AssignedTeamID = 2, TaskTypeID = 3, TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"}, Deleted = false},
                new Task() 
                {
                    TaskID = 7,
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
                    DueDate = DateTime.Now.AddDays(-7),
                    WarningWindow = 10,
                    RecordedClientID = 11004,
                    TaskTypeID = 3,
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"},
                    LastModifiedBy = UserName,
                    LastModifiedDate = DateTime.Now,
                    LastModifiedComment = "SLA updated",
                    LastModifyingReasonID = 6
                },

                new Task()
                {
                    TaskID = 8,
                    AcceptableWindow = 45,
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
                    DueDate = DateTime.Now.AddDays(-5),
                    WarningWindow = 35,
                    RecordedClientID = 11004,
                    TaskTypeID = 3,
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"}
                },

                new Task()
                {
                    TaskID = 9,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 1,
                    AssignedUser = "Mark.Brown",
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = false,
                    CompletedBy = null,
                    Deleted = false,
                    DueDate = DateTime.Now.AddDays(7),
                    WarningWindow = 90,
                    RecordedClientID = 11005,
                    TaskTypeID = 3,
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"}
                },

                new Task()
                {
                    TaskID = 10,
                    AcceptableWindow = 150,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 1,
                    AssignedUser = "Mark.Brown",
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = false,
                    CompletedBy = null,
                    Deleted = false,
                    DueDate = DateTime.Now.AddMinutes(30),
                    WarningWindow = 90,
                    RecordedClientID = 11006,
                    TaskTypeID = 3,
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"}
                },

                new Task()
                {
                    TaskID = 11,
                    AcceptableWindow = 150,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 1,
                    AssignedUser = "Chris.Cole",
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = false,
                    CompletedBy = null,
                    Deleted = false,
                    DueDate = DateTime.Now.AddMinutes(90),
                    WarningWindow = 60,
                    RecordedClientID = 11006,
                    TaskTypeID = 3,
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"}
                },

                new Task()
                {
                    TaskID = 12,
                    AcceptableWindow = 30,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 1,
                    AssignedUser = "Allan.Lloyd",
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = false,
                    CompletedBy = null,
                    Deleted = false,
                    DueDate = DateTime.Now.AddMinutes(90),
                    WarningWindow = 60,
                    RecordedClientID = 11006,
                    TaskTypeID = 3,
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"}
                },

                new EmailTask()
                {
                    TaskID = 13,
                    AcceptableWindow = 30,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 1,
                    AssignedUser = "Brian.Shaw",
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = false,
                    CompletedBy = null,
                    Deleted = false,
                    DueDate = DateTime.Now.AddMinutes(90),
                    WarningWindow = 60,
                    RecordedClientID = 11006,
                    TaskTypeID = 3,
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"},
                    Sender = "Mike.Atherton",
                    Subject = "Urgent Task",
                    CrossPosted = true,
                    MessageId = 100067
                },

                 new EmailTask()
                {
                    TaskID = 14,
                    AcceptableWindow = 30,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 1,
                    AssignedUser = "Tony.Huddersfield",
                    Cancelled = false,
                    CancelledBy = null,
                    CancelledDate = null,
                    CancelledReason = null,
                    Completed = false,
                    CompletedBy = null,
                    Deleted = false,
                    DueDate = DateTime.Now.AddMinutes(90),
                    WarningWindow = 60,
                    RecordedClientID = 11006,
                    TaskTypeID = 3,
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"},
                    Sender = "Alex.Stewart",
                    Subject = "Important Task",
                    CrossPosted = true,
                    MessageId = 100068
                }
            };

         
            TaskRepositoryMock.Setup(x => x.GetAll()).Returns(_tasks);
            TaskRepositoryMock.Setup(x => x.GetTasksByTeamId(1)).Returns(_tasks.Where(x => x.AssignedTeamID == 1));
            TaskRepositoryMock.Setup(x => x.GetTasksByTeamId(2)).Returns(_tasks.Where(x => x.AssignedTeamID == 2));
            TeamRepositoryMock.Setup(x => x.GetTeamIdByTeamName("Team2")).Returns(_teams[1].TeamID);           
        }

        [Test]
        public void Given_TaskId_then_return_task()
        {
            TblCustomerRepositoryMock.Setup(x => x.GetById(_tasks[2].RecordedClientID)).Returns(_tblCustomers[2]);
            TaskRepositoryMock.Setup(x => x.GetById((long)3)).Returns(_tasks[2]);

            var taskService = GetTarget();
          
            var task = taskService.GetById((long)3);

            Assert.That(task, Is.Not.Null);
            Assert.That(task.AssignedTeamId, Is.EqualTo(1));
        }

        [Test]
        public void Given_User_requests_All_User_Tasks_Then_Return_All()
        {
            var taskList = GetTarget();
            TblCustomerRepositoryMock.Setup(x => x.GetById(11004)).Returns(_tblCustomers[0]);
            TblCustomerRepositoryMock.Setup(x => x.GetById(11005)).Returns(_tblCustomers[1]);
            TaskRepositoryMock.Setup(x => x.GetTasksByUserName("User1")).Returns(_tasks.Where(x => x.AssignedUser == "User1"));
            var allTasks = taskList.GetAllTasksByUser("User1").ToList();

            Assert.That(allTasks.Count, Is.EqualTo(3));
            Assert.That(allTasks.First().AssignedUser, Is.EqualTo("User1"));
            Assert.That(allTasks.Last().AssignedUser, Is.EqualTo("User1"));
        }

        [Test]
        public void Given_User_requests_All_User_Tasks_Then_Returns_TaskList_With_CAN()
        {
            var taskList = GetTarget();
            var customerIds = new List<int>() {11004, 11005, 11006};
            TblCustomerRepositoryMock.Setup(x => x.GetById(11004)).Returns(_tblCustomers[0]);
            TblCustomerRepositoryMock.Setup(x => x.GetById(11005)).Returns(_tblCustomers[1]);
            TblCustomerRepositoryMock.Setup(x => x.GetById(11006)).Returns(_tblCustomers[2]);
            TaskRepositoryMock.Setup(x => x.GetTasksByUserName("User1")).Returns(_tasks.Where(x => x.AssignedUser == "User1"));
            TblCustomerRepositoryMock.Setup(x => x.GetCustomersByCustomerIds(customerIds)).Returns(_tblCustomers);
            var allTasks = taskList.GetAllTasksByUser("User1").ToList();

            Assert.That(allTasks.Count, Is.EqualTo(3));
            Assert.That(allTasks[0].Can, Is.EqualTo("ZPEN001"));
            Assert.That(allTasks[1].Can, Is.EqualTo("ZPEN002"));
            Assert.That(allTasks[2].Can, Is.EqualTo("ZPEN003"));
        }

        [Test]
        public void Given_A_TaskSummaryList_Correct_Timeline_Values_Are_Calculated_By_TeamId()
        {

            var taskService = GetTarget();
            var taskIds = new List<long>() {7, 8, 9, 10, 11, 12};
            //_taskRepository
            //    .Setup(x => x.GetTaskListForTimeline(1))
            //    .Returns(tasks.Where(x => taskIds.Contains(x.TaskID)));

            TaskRepositoryMock
                .Setup(x => x.GetTasksByTeamId(1))
                .Returns( _tasks.Where( x => taskIds.Contains(x.TaskID) ) );

            var timeline = taskService.GetTaskListTimelineByTeamId(1);

            Assert.That(timeline.Total, Is.EqualTo(6));
            Assert.That(timeline.Red, Is.EqualTo(2));
            Assert.That(timeline.Amber, Is.EqualTo(1));
            Assert.That(timeline.Green, Is.EqualTo(2));
            Assert.That(timeline.Platinum, Is.EqualTo(1));
        }

        [Test]
        public void Given_A_TaskSummaryList_Correct_Timeline_Values_Are_Calculated_When_List_Of_TeamId_Is_passed()
        {

            var taskService = GetTarget();
            var taskIds = new List<long>() { 7, 8, 9, 10, 11, 12 };
            
            long[] teamIds = { 1, 2 };

            TaskRepositoryMock
                .Setup(x => x.GetTasksByTeamIds(teamIds))
                .Returns(_tasks.Where(x => taskIds.Contains(x.TaskID)));

            
            var timeline = taskService.GetTaskListTimelineByTeamIds(teamIds);

            Assert.That(timeline.Total, Is.EqualTo(6));
            Assert.That(timeline.Red, Is.EqualTo(2));
            Assert.That(timeline.Amber, Is.EqualTo(1));
            Assert.That(timeline.Green, Is.EqualTo(2));
            Assert.That(timeline.Platinum, Is.EqualTo(1));
        }

        [Test]
        public void Given_A_TaskSummaryList_Correct_Timeline_Values_Are_Calculated_By_UserName()
        {

            var taskService = GetTarget();
            TaskRepositoryMock
                .Setup(x => x.GetTasksByUserName("Mark.Brown"))
                .Returns(_tasks.Where(x => x.AssignedUser == "Mark.Brown"));

            var timeline = taskService.GetTaskListTimelineByUsername("Mark.Brown");

            Assert.That(timeline.Total, Is.EqualTo(2));
            Assert.That(timeline.Red, Is.EqualTo(0));
            Assert.That(timeline.Amber, Is.EqualTo(1));
            Assert.That(timeline.Green, Is.EqualTo(1));
            Assert.That(timeline.Platinum, Is.EqualTo(0));
        }

        [Test]
        public void Given_teamname_then_return_tasks_for_that_team()
        {
            var customerIds = new List<int>() { 11005 };
            var teamIds = new long[] { 2 };
            TblCustomerRepositoryMock.Setup(x => x.GetCustomersByCustomerIds(customerIds)).Returns(_tblCustomers.Where(x => customerIds.Contains(x.CustomerID)));
            TaskRepositoryMock.Setup(x => x.GetTasksByTeamIds(teamIds)).Returns(_tasks.Where(x => teamIds.Contains((long)x.AssignedTeamID)));
            var taskService = GetTarget();
            
            var tasks = taskService.GetAllTasksByTeamName("Team2").ToList();

            TeamRepositoryMock.Verify(x => x.GetTeamIdByTeamName("Team2"),Times.Once());
            TaskRepositoryMock.Verify(x => x.GetTasksByTeamIds(teamIds), Times.Once());

            Assert.That(tasks.Count, Is.EqualTo(3));
        }

        [Test]
        public void Given_task_status_set_then_return_name_of_status()
        {
            var customerIds = new List<int>() { 11004, 11005 };
            TblCustomerRepositoryMock.Setup(x => x.GetCustomersByCustomerIds(customerIds)).Returns(_tblCustomers.Where(x => customerIds.Contains(x.CustomerID)));
            var taskService = GetTarget();
            var teamIds = new long[] {2};
            TaskRepositoryMock.Setup(x => x.GetTasksByTeamIds(teamIds)).Returns(_tasks.Where(x => teamIds.Contains(x.TaskID)));
            var tasks = taskService.GetAllTasksByTeamId(teamIds).ToList();

            Assert.That(tasks.First().Status,Is.EqualTo(DerivedTaskStatusForDisplay.Red));
        }

        [Test]
        public void Given_Tasks_Requested_For_Multiple_Teams_Return_Tasks_For_All()
        {
            var taskService = GetTarget();
            var teamIds = new long[] { 2, 1 };

            var customerIds = new List<int>() { 11004, 11005 };
            TblCustomerRepositoryMock.Setup(x => x.GetCustomersByCustomerIds(customerIds)).Returns(_tblCustomers.Where(x => customerIds.Contains(x.CustomerID)));
            TaskRepositoryMock.Setup(x => x.GetTasksByTeamIds(teamIds)).Returns(_tasks.Where(x => teamIds.Contains(x.TaskID)));
            var tasks = taskService.GetAllTasksByTeamId(teamIds).ToList();

            Assert.That(tasks.Count, Is.EqualTo(2));
            Assert.That(tasks.First(x => x.TaskId == 1).AssignedTeamId, Is.EqualTo(1));
            Assert.That(tasks.First(x => x.TaskId == 2).AssignedTeamId, Is.EqualTo(2));
        }

        [Test]
        public void Given_Tasks_Requested_For_Multiple_Teams_Return_Tasks_For_All_With_Task_Type_Description()
        {
            var taskService = GetTarget();
            var teamIds = new long[] {2, 1 };
            var customerIds = new List<int>() {11004, 11005};
            TblCustomerRepositoryMock.Setup(x => x.GetCustomersByCustomerIds(customerIds)).Returns(_tblCustomers.Where(x=> customerIds.Contains(x.CustomerID)));
            TaskRepositoryMock.Setup(x => x.GetTasksByTeamIds(teamIds)).Returns(_tasks.Where(x => teamIds.Contains(x.TaskID)));
            var tasks = taskService.GetAllTasksByTeamId(teamIds).ToList();

            
            Assert.That(tasks.First(x => x.TaskId == 1).TaskTypeDescription, Is.EqualTo("Callback 1 Hour"));
            Assert.That(tasks.First(x => x.TaskId == 2).TaskTypeDescription, Is.EqualTo("Callback 2 Hour"));
        }

        [Test]
        public void Given_Tasks_Requested_For_Multiple_Teams__And_None_Exist_Return_Empty_List()
        {
            TaskRepositoryMock.Setup(x => x.GetTasksByTeamId(1)).Returns(new List<Task>());
            TaskRepositoryMock.Setup(x => x.GetTasksByTeamId(2)).Returns(new List<Task>());

            var taskService = GetTarget();
            var teamIds = new long[] { (long)2, (long)1 };
            var tasks = taskService.GetAllTasksByTeamId(teamIds).ToList();

            Assert.That(tasks.Count, Is.EqualTo(0));
        }

        [Test]
        public void Given_Message_Restored_For_Task_Then_Retrieve_Task_Email_Details()
        {
            //To be fixed by Gareth as discussed
            //var taskService = GetTarget();

            //var response = taskService.RestoreEmailToOutlook(1,1);

            //_taskRepository.Verify(x => x.GetTaskWithDetailByTaskId(It.IsAny<long>()), Times.Once);
        }

        [Test]
        public void Given_Message_Restore_For_Task_Then_Assert_ExchangeService_Called_With_Correct_Parameters()
        {
            TaskRepositoryMock.Setup(x => x.GetTaskByTaskId(It.IsAny<long>())).Returns(_tasks.First);
           
            string userToRestoreTo = "";
            long messageId = 0;
            
            ExchangeEmailServiceMock.Setup(x => x.RestoreMessageToOutlook(It.IsAny<long>(), It.IsAny<string>()))
                .Callback<long,string> ((y,z) =>
            {
                messageId = y;
                userToRestoreTo = z;
            });

            var taskService = GetTarget();
            
            var response = taskService.RestoreEmailToOutlook(1, 1001, UserName);

            Assert.AreEqual(1001, messageId);
            Assert.AreEqual(_tasks.First().AssignedUser, userToRestoreTo);
            Assert.AreEqual(_tasks.First().DeletedBy, UserName);
        }


        [Test]
        public void Given_Message_Restore_For_Task_Then_Task_is_marked_as_Deleted()
        {
            TaskRepositoryMock.Setup(x => x.GetTaskByTaskId(It.IsAny<long>())).Returns(_tasks.First);

            string userToRestoreTo = "";
            long messageId = 0;

            ExchangeEmailServiceMock.Setup( x => x.RestoreMessageToOutlook( It.IsAny<long>(), It.IsAny<string>() ) )
                .Callback<long, string>( (y, z) =>
                                            {
                                                messageId = y;
                                                userToRestoreTo = z;
                                            });
            
            var taskService = GetTarget();
            var response = taskService.RestoreEmailToOutlook(1, 1001, UserName);

            Assert.IsTrue(_tasks.First().Deleted);
            Assert.That(_tasks.First().DeletedBy, Is.EqualTo(UserName));
        }

        [Test]
        public void Given_Bulk_RestoreToOutlook_For_Tasks_Then_Tasks_are_marked_as_Deleted()
        {
            var testEmailTasks = _tasks.Where(x => new List<long>() {12, 13, 14}.Contains(x.TaskID));
            long[] taskIds = new long[3];
            int i = 0;
            var emailTasks = testEmailTasks as IList<Task> ?? testEmailTasks.ToList();
            foreach (var testEmailTask in emailTasks)
            {
                taskIds[i++] = testEmailTask.TaskID;
                TaskRepositoryMock.Setup(x => x.GetTaskByTaskId(testEmailTask.TaskID)).Returns(testEmailTask);
            }

            string userToRestoreTo = "";
            long messageId = 0;

            ExchangeEmailServiceMock.Setup(x => x.RestoreMessageToOutlook(It.IsAny<long>(), It.IsAny<string>())).Callback<long, string>((y, z) =>
            {
                messageId = y;
                userToRestoreTo = z;
            });

            var taskService = GetTarget();
             taskService.RestoreBulkEmailsToOutlook(taskIds, UserName);

            Assert.IsFalse(emailTasks.ToList().First(x => x.TaskID == 12).Deleted);
            Assert.IsNull(emailTasks.ToList().First(x => x.TaskID == 12).DeletedBy);
            Assert.IsNull(emailTasks.ToList().First(x => x.TaskID == 12).DeletedDate);


            Assert.IsTrue(emailTasks.ToList().First(x => x.TaskID == 13).Deleted);
            Assert.IsNotNull(emailTasks.ToList().First(x => x.TaskID == 13).DeletedBy);
            Assert.IsNotNull(emailTasks.ToList().First(x => x.TaskID == 13).DeletedDate);

            Assert.IsTrue(emailTasks.ToList().First(x => x.TaskID == 14).Deleted);
            Assert.IsNotNull(emailTasks.ToList().First(x => x.TaskID == 14).DeletedBy);
            Assert.IsNotNull(emailTasks.ToList().First(x => x.TaskID == 14).DeletedDate);
            
        }

        [Test]
        public void Given_tasks_to_be_deleted_when_bulk_delete_then_set_delete_property_of_task()
        {
            TaskService taskService = GetTarget();

            var taskList = _tasks.Select(x => x.TaskID);            
        }
    }
}
