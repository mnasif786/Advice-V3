using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Data.Repository;
using Advice.Data.Tests.TestHelpers;
using Advice.Domain;
using Advice.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace Advice.Data.Tests.Common
{
 
    public  abstract class BaseTaskRepositoryTests
    {
        private IAdviceDbContextManager _adviceDbContextManager;
        protected internal Mock<AdviceEntities> AdviceEntities;
        protected internal ITaskRepository TaskRepository;

        [SetUp]
        protected void SetUp()
        {
            AdviceEntities = new Mock<AdviceEntities>();
            _adviceDbContextManager = new AdviceDbContextManager(AdviceEntities.Object);

            Mock<ITaskStoredProcRunner> mockStoredProcRunner = new Mock<ITaskStoredProcRunner>();
            TaskRepository = new TaskRepository(_adviceDbContextManager, mockStoredProcRunner.Object);

            var dbSetMockUser = MockUsersFactory.GetMockedUsers();
            AdviceEntities.Setup(x => x.Users).Returns(dbSetMockUser.Object);

        }
    }
}
