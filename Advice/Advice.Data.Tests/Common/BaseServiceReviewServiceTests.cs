using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Data.Repository;
using Advice.Data.Repository.Services;
using Advice.Data.Tests.TestHelpers;
using Advice.Domain;
using Advice.Domain.RepositoryContracts.Services;
using Moq;
using NUnit.Framework;

namespace Advice.Data.Tests.Common
{
    public abstract class BaseServiceReviewServiceTests
    {
        private IAdviceDbContextManager _adviceDbContextManager;
        protected internal Mock<AdviceEntities> AdviceEntities;
        protected internal IServiceReviewServiceJobScheduleRepository ServiceReviewServiceJobScheduleRepository;

        [SetUp]
        protected void SetUp()
        {
            AdviceEntities = new Mock<AdviceEntities>();
            _adviceDbContextManager = new AdviceDbContextManager(AdviceEntities.Object);
            ServiceReviewServiceJobScheduleRepository = new ServiceReviewServiceJobScheduleRepository(_adviceDbContextManager);
        } 
    }
}
