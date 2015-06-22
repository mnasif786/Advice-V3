using Advice.Application.Implementations;
using Advice.Domain.RepositoryContracts;
using Advice.ExchangeEmails;
using Moq;
using NUnit.Framework;
using Peninsula.Domain.RepositoryContracts;

namespace Application.Tests.Common
{
    public abstract class BaseTeamServiceTests
    {
        public Mock<ITeamRepository> TeamRepositoryMock;
       
        [SetUp]
        public void SetUp()
        {
            TeamRepositoryMock = new Mock<ITeamRepository>();
            
        }

        public TeamService GetTarget()
        {
            return new TeamService(TeamRepositoryMock.Object);
        }
    }
}
