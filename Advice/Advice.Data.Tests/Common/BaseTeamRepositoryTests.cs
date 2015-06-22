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
    public class BaseTeamRepositoryTests
    {
        private IAdviceDbContextManager _adviceDbContextManager;
        private Mock<AdviceEntities> _adviceEntities;
        protected ITeamRepository TeamRepository;
        protected List<Team> Teams;

        [SetUp]
        protected void SetUp()
        {
            _adviceEntities = new Mock<AdviceEntities>();
            _adviceDbContextManager = new AdviceDbContextManager(_adviceEntities.Object);

            Teams = new List<Team>()
            {
                new Team()
                {
                    TeamID = 1,
                    DepartmentID = 1,
                    Description = "Team1"
                },
                new Team()
                {
                    TeamID = 2,
                    DepartmentID = 1,
                    Description = "Team2"
                },
                new Team()
                {
                    TeamID = 3,
                    DepartmentID = 2,
                    Description = "Team3"
                }
            };

            var users = new List<User>()
            {
                new User() {UserID = 1, Username = "Mickey Mouse", TeamID = 1, RoleID = 1, Deleted = false},
                new User() {UserID = 2, Username = "Donald Duck", TeamID = 1, RoleID = 1, Deleted = false},
                new User() {UserID = 3, Username = "Fred Flintstone", TeamID = 1, RoleID = 1, Deleted = false},
                new User() {UserID = 4, Username = "Scooby Doo", TeamID = 2, RoleID = 1, Deleted = true},

            };

            TeamRepository = new TeamRepository(_adviceDbContextManager);

            var dbSetMockTeam = DbSetInitialisedMockFactory<Team>.CreateDbSetInitalisedMock(Teams);
            _adviceEntities.Setup(x => x.Teams).Returns(dbSetMockTeam.Object);
            _adviceEntities.Setup(x => x.Set<Team>()).Returns(dbSetMockTeam.Object);

            var dbSetMockUser = DbSetInitialisedMockFactory<User>.CreateDbSetInitalisedMock(users);
            _adviceEntities.Setup(x => x.Users).Returns(dbSetMockUser.Object);
            _adviceEntities.Setup(x => x.Set<User>()).Returns(dbSetMockUser.Object);
        }
    }
}
