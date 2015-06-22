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
using Advice.Domain.Entities.Enums;
using Advice.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;


namespace Advice.Data.Tests.Common
{
    public abstract class BaseTaskModifyingReasonRepositoryTests
    {
        private IAdviceDbContextManager _adviceDbContextManager;
        private Mock<AdviceEntities> _adviceEntities;
        protected ITaskModifyingReasonRepository TaskModifyingReasonRepository;
        private List<TaskModifyingReason> _taskModifyingReasons;

        [SetUp]
        protected void SetUp()
        {
            _adviceEntities = new Mock<AdviceEntities>();
            _adviceDbContextManager = new AdviceDbContextManager(_adviceEntities.Object);

            _taskModifyingReasons = new List<TaskModifyingReason>()
            {
                new TaskModifyingReason()
                {
                    TaskModifyingReasonID = 1,
                    TaskModifyingReasonGroupID = 4,
                    Description = "Client request"

                },

                new TaskModifyingReason()
                {
                    TaskModifyingReasonID = 2,
                    TaskModifyingReasonGroupID = 4,
                    Description = "Management decision"
                },

                new TaskModifyingReason()
                {
                    TaskModifyingReasonID = 3,
                    TaskModifyingReasonGroupID = 1,
                    Description = "Delete"
                }
            };

            TaskModifyingReasonRepository = new TaskModifyingReasonRepository(_adviceDbContextManager);

            var dbSetMockTaskModifyingReason = DbSetInitialisedMockFactory<TaskModifyingReason>.CreateDbSetInitalisedMock(_taskModifyingReasons);
            _adviceEntities.Setup(x => x.TaskModifyingReasons).Returns(dbSetMockTaskModifyingReason.Object);
            _adviceEntities.Setup(x => x.Set<TaskModifyingReason>()).Returns(dbSetMockTaskModifyingReason.Object);
        }
    }
}
