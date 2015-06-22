using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Application.Implementations;
using Advice.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Common
{
    public abstract class BaseTaskModifyingReasonServiceTests
    {
        protected Mock<ITaskModifyingReasonRepository> TaskModifyingReasonRepositorMock;

        [SetUp]
        protected void BaseSetUp()
        {
            TaskModifyingReasonRepositorMock = new Mock<ITaskModifyingReasonRepository>();
        }

        protected TaskModifyingReasonService GetTarget()
        {
            return new TaskModifyingReasonService(TaskModifyingReasonRepositorMock.Object);
        }
    }
}
