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
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace Advice.Data.Tests.Repository.TaskTests
{
    [TestFixture]
    public class TaskRepositoryTests
    {
        private IAdviceDbContextManager _adviceDbContextManager;
        private Mock<AdviceEntities> _adviceEntities;
        private ITaskRepository _taskRepository;
        private const string UserName = "Tom.McMillan";

        [SetUp]
        public void TestFixtureSetup()
        {
            _adviceEntities = new Mock<AdviceEntities>();
            _adviceDbContextManager = new AdviceDbContextManager(_adviceEntities.Object);
            _taskRepository = new TaskRepository(_adviceDbContextManager, new Mock<ITaskStoredProcRunner>().Object);

            SetupMockTaskData();

            SetupMockUserData();

            SetupMockTaskArchiveData();
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
                }
            };

            var dbSetMockUser = DbSetInitialisedMockFactory<User>.CreateDbSetInitalisedMock(userData);
            _adviceEntities.Setup(x => x.Users).Returns(dbSetMockUser.Object);
        }

        private void SetupMockTaskData()
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
                    RecordedClientID = 11841,
                    TaskTypeID = 3,
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"}
                },

                new Task()
                {
                    TaskID = 2,
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
                    DueDate = new DateTime(2014, 07, 27),
                    WarningWindow = 35,
                    RecordedClientID = 11841,
                    TaskTypeID = 3,
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"}
                },

                new Task()
                {
                    TaskID = 3,
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
                    DueDate = new DateTime(2014, 09, 09),
                    WarningWindow = 90,
                    RecordedClientID = 11843,
                    TaskTypeID = 3,
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"}
                },
                 new Task()
                {
                    TaskID = 4,
                    AcceptableWindow = 150,
                    AssignedDate = DateTime.Now.AddDays(-7),
                    AssignedTeamID = 2,
                    AssignedUser = "Mark.Brown",
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
                    TaskTypeID = 3,
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"}
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
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"}
                },
                 new Task()
                {
                    TaskID = 7,
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
                    TaskTypeID = 3,
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"}
                },
                 new Task()
                {
                    TaskID = 8,
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
                    TaskTypeID = 3,
                    TaskType = new TaskType{TaskTypeID = 3,Description = "Callback 1 Hour"}
                },
            };

            var dbSetMockTask = DbSetInitialisedMockFactory<Task>.CreateDbSetInitalisedMock(taskData);

            _adviceEntities.Setup(x => x.Tasks).Returns(dbSetMockTask.Object);
            _adviceEntities.Setup(x => x.Set<Task>()).Returns(dbSetMockTask.Object);

        }


        private void SetupMockTaskArchiveData()
        {
            var taskArchiveData = new List<TaskArchive>()
            {
                new TaskArchive()
                {
                    TaskID = 1,
                    Deleted = false,
                    Description = "First Archive element",
                    CreatedDate = DateTime.Today.AddDays(-3)
                   
                },

                 new TaskArchive()
                {
                    TaskID = 1,
                     Deleted = false,
                    Description = "Second Archive element",
                    CreatedDate = DateTime.Today.AddDays(-2)
                  
                },

                 new TaskArchive()
                {
                    TaskID = 1,
                     Deleted = false,
                    Description = "Third Archive element",
                    CreatedDate = DateTime.Today.AddDays(-1)
                   
                },


                   new TaskArchive()
                {
                    TaskID = 2,
                     Deleted = false,
                    Description = "Task Two Archive element",
                    CreatedDate = DateTime.Today.AddDays(-1)
                   
                },

                   new TaskArchive()
                {
                    TaskID = 1,
                     Deleted = true,
                    Description = "Deleted task 1 Archive element",
                    CreatedDate = DateTime.Today.AddDays(-1)
                   
                }
            };

            var dbSetMockTaskArchive = DbSetInitialisedMockFactory<TaskArchive>.CreateDbSetInitalisedMock(taskArchiveData);

            

            _adviceEntities.Setup(x => x.TaskArchives).Returns(dbSetMockTaskArchive.Object);
            _adviceEntities.Setup(x => x.Set<TaskArchive>()).Returns(dbSetMockTaskArchive.Object);        
        }
        
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TaskRepository_Throws_exception_on_null_IStoredProcRunner()
        {
            TaskRepository testRepository = new TaskRepository(_adviceDbContextManager, null);
        }
        
        [Test]
        public void TaskRepository_GetTasksByTeamIds_calls_expected_stored_proc_with_given_team_ids()
        {
            List<GetTasksByTeamIds_Type> mockReturnTasks = null;

            Mock<ITaskStoredProcRunner> testRunner = new Mock<ITaskStoredProcRunner>();
            testRunner.Setup(t => t.ExecuteQuery(It.IsAny<ObjectContext>(), It.IsAny<String>(), It.IsAny<long[]>())).Returns(mockReturnTasks);

            TaskRepository testRepository = new TaskRepository(_adviceDbContextManager, testRunner.Object);

            long[] teamIds = new long[] { 0 };

            testRepository.GetTasksByTeamIds(teamIds);

            testRunner.Verify(x => x.ExecuteQuery(It.IsAny<ObjectContext>(), "GetTasksByTeamIds", teamIds));
        }
        
        [Test]
        public void TaskRepository_GetTasksByTeamIds_returns_empty_list_given_null_stored_proc_response()
        {
            List<GetTasksByTeamIds_Type> mockReturnTasks = null;

            Mock<ITaskStoredProcRunner> testRunner = new Mock<ITaskStoredProcRunner>();
            testRunner.Setup(t => t.ExecuteQuery(It.IsAny<ObjectContext>(), It.IsAny<String>(), It.IsAny<long[]>())).Returns(mockReturnTasks);

            TaskRepository testRepository = new TaskRepository(_adviceDbContextManager, testRunner.Object);

            IEnumerable<Task> returnedTasks = testRepository.GetTasksByTeamIds(null);
            Assert.AreEqual(0, returnedTasks.Count());
        }

        [Test]
        public void TaskRepository_GetTasksByTeamIds_returns_empty_list_given_empty_list_stored_proc_response()
        {
            List<GetTasksByTeamIds_Type> mockReturnTasks = new List<GetTasksByTeamIds_Type>();

            Mock<ITaskStoredProcRunner> testRunner = new Mock<ITaskStoredProcRunner>();
            testRunner.Setup(t => t.ExecuteQuery(It.IsAny<ObjectContext>(), It.IsAny<String>(), It.IsAny<long[]>())).Returns(mockReturnTasks);

            TaskRepository testRepository = new TaskRepository(_adviceDbContextManager, testRunner.Object);

            IEnumerable<Task> returnedTasks = testRepository.GetTasksByTeamIds(null);
            Assert.AreEqual(0, returnedTasks.Count());
        }

        [Test]
        public void TaskRepository_GetTasksByTeamIds_returns_single_task_given_single_stored_proc_response()
        {
            List<GetTasksByTeamIds_Type> mockReturnTasks = GenerateMockTasks(1);

            Mock<ITaskStoredProcRunner> testRunner = new Mock<ITaskStoredProcRunner>();
            testRunner.Setup(t => t.ExecuteQuery(It.IsAny<ObjectContext>(), It.IsAny<String>(), It.IsAny<long[]>())).Returns(mockReturnTasks);

            TaskRepository testRepository = new TaskRepository(_adviceDbContextManager, testRunner.Object);

            List<Task> returnedTasks = testRepository.GetTasksByTeamIds(null).ToList();
            Assert.AreEqual(1, returnedTasks.Count());
            Assert.AreEqual(returnedTasks[0].TaskID, mockReturnTasks[0].Task_TaskID);
        }

        [Test]
        public void TaskRepository_GetTasksByTeamIds_returns_multiple_tasks_given_multiple_stored_proc_response()
        {
            const int taskCount = 2;
            List<GetTasksByTeamIds_Type> mockReturnTasks = GenerateMockTasks(taskCount);
            
            Mock<ITaskStoredProcRunner> testRunner = new Mock<ITaskStoredProcRunner>();
            testRunner.Setup(t => t.ExecuteQuery(It.IsAny<ObjectContext>(), It.IsAny<String>(), It.IsAny<long[]>())).Returns(mockReturnTasks);

            TaskRepository testRepository = new TaskRepository(_adviceDbContextManager, testRunner.Object);

            List<Task> returnedTasks = testRepository.GetTasksByTeamIds(null).ToList();

            Assert.AreEqual(taskCount, returnedTasks.Count());

            for (int taskIndex = 0; taskIndex < taskCount; ++taskIndex)
            {
                Assert.AreEqual(returnedTasks[taskIndex].TaskID, mockReturnTasks[taskIndex].Task_TaskID);
            }
        }

        [Test]
        public void TaskRepository_GetProActiveTasksByTeams_calls_expected_stored_procedure_with_given_team_ids()
        {
            Mock<ITaskStoredProcRunner> testRunner = new Mock<ITaskStoredProcRunner>();
            TaskRepository testRepository = new TaskRepository(_adviceDbContextManager, testRunner.Object);
            
            long[] teamIds = new long[] { 0 };

            testRepository.GetProActiveTasksByTeams(teamIds);

            testRunner.Verify(x => x.ExecuteQuery(It.IsAny<ObjectContext>(), "GetTasksByTeamIdsProactive", teamIds));
        }

        [Test]
        public void TaskRepository_GetProActiveTasksByTeams_returns_empty_list_given_null_stored_proc_response()
        {
            List<GetTasksByTeamIds_Type> mockReturnTasks = null;

            Mock<ITaskStoredProcRunner> testRunner = new Mock<ITaskStoredProcRunner>();
            testRunner.Setup(x => x.ExecuteQuery(It.IsAny<ObjectContext>(), It.IsAny<String>(), It.IsAny<long[]>())).Returns(mockReturnTasks);
            
            TaskRepository testRepository = new TaskRepository(_adviceDbContextManager, testRunner.Object);
            IEnumerable<Task> returnedTasks = testRepository.GetProActiveTasksByTeams(null);

            Assert.AreEqual(0, returnedTasks.Count());
        }

        [Test]
        public void TaskRepository_GetProActiveTasksByTeams_returns_empty_list_given_empty_list_stored_proc_response()
        {
            List<GetTasksByTeamIds_Type> mockReturnTasks = new List<GetTasksByTeamIds_Type>();

            Mock<ITaskStoredProcRunner> testRunner = new Mock<ITaskStoredProcRunner>();
            testRunner.Setup(x => x.ExecuteQuery(It.IsAny<ObjectContext>(), It.IsAny<String>(), It.IsAny<long[]>())).Returns(mockReturnTasks);

            TaskRepository testRepository = new TaskRepository(_adviceDbContextManager, testRunner.Object);
            IEnumerable<Task> returnedTasks = testRepository.GetProActiveTasksByTeams(null);

            Assert.AreEqual(0, returnedTasks.Count());
        }

        [Test]
        public void TaskRepository_GetProActiveTasksByTeams_returns_single_task_given_single_item_stored_proc_response()
        {
            List<GetTasksByTeamIds_Type> mockReturnTasks = GenerateMockTasks(1);

            Mock<ITaskStoredProcRunner> testRunner = new Mock<ITaskStoredProcRunner>();
            testRunner.Setup(x => x.ExecuteQuery(It.IsAny<ObjectContext>(), It.IsAny<String>(), It.IsAny<long[]>())).Returns(mockReturnTasks);

            TaskRepository testRepository = new TaskRepository(_adviceDbContextManager, testRunner.Object);

            List<Task> returnedTasks = testRepository.GetProActiveTasksByTeams(null).ToList();
            Assert.AreEqual(1, returnedTasks.Count());

            Assert.AreEqual(returnedTasks[0].TaskID, mockReturnTasks[0].Task_TaskID);
        }

        [Test]
        public void TaskRepository_GetProActiveTasksByTeams_returns_multiple_tasks_given_multi_item_stored_proc_response()
        {
            const int taskCount = 2;
            List<GetTasksByTeamIds_Type> mockReturnTasks = GenerateMockTasks(taskCount);

            Mock<ITaskStoredProcRunner> testRunner = new Mock<ITaskStoredProcRunner>();
            testRunner.Setup(x => x.ExecuteQuery(It.IsAny<ObjectContext>(), It.IsAny<String>(), It.IsAny<long[]>())).Returns(mockReturnTasks);

            TaskRepository testRepository = new TaskRepository(_adviceDbContextManager, testRunner.Object);

            List<Task> returnedTasks = testRepository.GetProActiveTasksByTeams(null).ToList();
            Assert.AreEqual(taskCount, returnedTasks.Count());

            for (int taskIndex = 0; taskIndex < taskCount; ++taskIndex)
            {
                Assert.AreEqual(returnedTasks[taskIndex].TaskID, mockReturnTasks[taskIndex].Task_TaskID);
            }
        }

        private static List<GetTasksByTeamIds_Type> GenerateMockTasks(int taskCount)
        {
            List<GetTasksByTeamIds_Type> mockTasks = new List<GetTasksByTeamIds_Type>();

            for (int taskIndex = 0; taskIndex < taskCount; ++taskIndex)
            {
                GetTasksByTeamIds_Type mockTask = new GetTasksByTeamIds_Type();
                mockTask.Task_TaskID = taskIndex + 1;

                mockTasks.Add(mockTask);
            }

            return mockTasks;
        }

        [Test]
        public void Given_Tasks_Requested_For_User_Then_Return_All_Tasks()
        {
            var tasks = _taskRepository.GetTasksByUserName("Tom.Moody").ToList();
            Assert.That(tasks.Count, Is.EqualTo(4));
        }

        [Test]
        public void Give_When_Get_by_Client_Id_is_called_then_all_relevant_Tasks_returned()
        {
            var tasks = _taskRepository.GetTasksByClientId(11841).ToList();
            Assert.That(tasks.First().TaskID, Is.EqualTo(2));
        }

        [Test]
        public void given_tasks_requested_for_team_by_id_then_return_tasks()
        {
            var tmp = _taskRepository.GetTasksByTeamId(1);

            var tasks = tmp.ToList();

            Assert.That(tasks.Count, Is.EqualTo(4));
        }

        [Test]
        public void given_tasks_requested_for_team_by_id_then_return_dont_return_completed_tasks()
        {
            var tasks = _taskRepository.GetTasksByTeamId(3).ToList();
            Assert.That(tasks.Count, Is.EqualTo(2));
        }

        [Test]
        public void given_tasks_requested_for_team_by_id_then_return_tasks_with_Task_type()
        {
            var tmp = _taskRepository.GetTasksByTeamId(1);

            var tasks = tmp.ToList();

            Assert.NotNull(tasks[0].TaskType);
            Assert.That(tasks[0].TaskTypeID, Is.EqualTo(3));
        }

        [Test]
        [Ignore]
        // Ignore this test until we can figure out how to mock abstract base class (AdviceRepository)
        public void given_tasks_to_be_bulk_deleted_then_set_tasks_deleted()
        {
            Assert.That(_adviceEntities.Object.Tasks.Any(x => x.Deleted == true), Is.False);

            var taskIds = new long[] 
            {
                2,3,4
            };
          
            _taskRepository.DeleteTasksByTaskIds(taskIds, UserName);

            var deletedTasks = _adviceEntities.Object.Tasks.Where(x => taskIds.ToList().Contains(x.TaskID)).ToList();

            Assert.AreEqual(deletedTasks.Count, taskIds.Length);
            Assert.That(deletedTasks.Any(x => x.Deleted == false), Is.False);
            Assert.That(deletedTasks.All(x=>x.DeletedBy == UserName), Is.True);
        }
    }
}
