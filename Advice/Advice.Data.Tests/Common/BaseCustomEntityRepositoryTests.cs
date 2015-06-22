using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Data.Repository;
using Advice.Domain;
using Advice.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace Advice.Data.Tests.Common
{
    public abstract class BaseCustomEntityRepositoryTests
    {
        private IAdviceDbContextManager _adviceDbContextManager;
        protected Mock<AdviceEntities> AdviceEntities;
        protected ICustomEntityRepository CustomEntityRepository;
            
       [SetUp]
        protected void BaseSetUp()
        {
            AdviceEntities = new Mock<AdviceEntities>();
            _adviceDbContextManager = new AdviceDbContextManager(AdviceEntities.Object);
           CustomEntityRepository = new CustomEntityRepository(_adviceDbContextManager);
        }
    }
}
