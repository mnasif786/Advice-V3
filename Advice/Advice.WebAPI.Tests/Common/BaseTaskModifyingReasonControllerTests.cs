using Advice.Application.Contracts;
using Advice.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace Advice.WebAPI.Tests.Common
{
    public abstract class BaseTaskModifyingReasonControllerTests
    {
        protected Mock<ITaskModifyingReasonService> TaskModifyingReasonServiceMock;

        [SetUp]
        protected void BaseSetUp()
        {
            TaskModifyingReasonServiceMock = new Mock<ITaskModifyingReasonService>();
        }

        protected TaskModifyingReasonController GetTarget()
        {
            return new TaskModifyingReasonController(TaskModifyingReasonServiceMock.Object);
        }

    }
}
