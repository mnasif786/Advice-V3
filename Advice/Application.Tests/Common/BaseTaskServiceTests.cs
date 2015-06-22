using Advice.Application.Implementations;
using Advice.Domain.RepositoryContracts;
using Advice.ExchangeEmails;
using Moq;
using NUnit.Framework;
using Peninsula.Domain.RepositoryContracts;

namespace Application.Tests.Common
{
    public abstract class BaseTaskServiceTests
    {
        public Mock<ITaskRepository> TaskRepositoryMock;
        public Mock<ITeamRepository> TeamRepositoryMock;
        public Mock<ITaskArchiveRepository> TaskArchiveRepositoryMock;
        public Mock<ITblCustomerRepository> TblCustomerRepositoryMock;
        public Mock<IDepartmentRepository> DepartmentRepositoryMock;
        public Mock<IExchangeEmailService> ExchangeEmailServiceMock;

        [SetUp]
        public void SetUp()
        {
            TaskRepositoryMock = new Mock<ITaskRepository>();
            TeamRepositoryMock = new Mock<ITeamRepository>();
            TaskArchiveRepositoryMock = new Mock<ITaskArchiveRepository>();
            TblCustomerRepositoryMock = new Mock<ITblCustomerRepository>();
            DepartmentRepositoryMock = new Mock<IDepartmentRepository>();
            ExchangeEmailServiceMock = new Mock<IExchangeEmailService>();

        }

        public TaskService GetTarget()
        {
            return new TaskService(TaskRepositoryMock.Object, TeamRepositoryMock.Object, TaskArchiveRepositoryMock.Object, TblCustomerRepositoryMock.Object, DepartmentRepositoryMock.Object, ExchangeEmailServiceMock.Object);
        }
    }
}
