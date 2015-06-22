using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Data.Repository;
using Advice.Data.Tests.TestHelpers;
using Advice.Domain;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace Advice.Data.Tests.Common
{
    public abstract class BaseTaskArchiveRepositoryTests
    {
        private IAdviceDbContextManager _adviceDbContextManager;
        private Mock<AdviceEntities> _adviceEntities;
        protected ITaskArchiveRepository TaskArchiveRepository;
        private List<TaskArchive> _taskArchives;
        private List<Team> _teams;
        private List<TaskModifyingReason> _taskModifyingReasons;
        private List<TaskModifyingReasonGroup> _taskModifyingReasonGroups;
        protected const string UserName = "Tom.McMillan";

        [SetUp]
        protected void SetUp()
        {
            _adviceEntities = new Mock<AdviceEntities>();
            _adviceDbContextManager = new AdviceDbContextManager(_adviceEntities.Object);
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

            _taskArchives = new List<TaskArchive>()
            {
                new TaskArchive()
                {
                    TaskArchiveId = 1,
                    TaskID = 11,
                    Description = "Test1",
                    TaskModifyingReason = _taskModifyingReasons[0],
                    Team = _teams[0]
                },

                new TaskArchive()
                {
                    TaskArchiveId = 2,
                    TaskID = 12,
                    Description = "Test2",
                    TaskModifyingReason = _taskModifyingReasons[0],
                    AssignedUser = UserName
                },

                new TaskArchive()
                {
                    TaskArchiveId = 3,
                    TaskID = 12,
                    Description = "Test3",
                    TaskModifyingReason = _taskModifyingReasons[1],
                    AssignedUser = UserName
                }
            };

            TaskArchiveRepository = new TaskArchiveRepository(_adviceDbContextManager);

            var dbSetMockTaskArchive = DbSetInitialisedMockFactory<TaskArchive>.CreateDbSetInitalisedMock(_taskArchives);
            _adviceEntities.Setup(x => x.TaskArchives).Returns(dbSetMockTaskArchive.Object);
            _adviceEntities.Setup(x => x.Set<TaskArchive>()).Returns(dbSetMockTaskArchive.Object);
        }
    }
}
