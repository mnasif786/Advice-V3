using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Application.Contracts;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Application.Tests.Common;
using NUnit.Framework;

namespace Application.Tests.Implementations.TeamServiceTests
{
    [TestFixture]
    public class GetAllTeamsWithDivisionAndDepartmentTests : BaseTeamServiceTests
    {
        private List<Team> _teams;
        private ITeamService _teamService;
        [SetUp]
        public void Setup()
        {
            var departments = new List<Department>()
            {
                new Department()
                {
                    DepartmentID = 1,
                    Description = "Taxwise"
                },
                new Department()
                {
                    DepartmentID = 1,
                    Description = "Employment Services"
                }
            };

            var divisions = new List<Division>()
            {
                new Division()
                {
                    DivisionId = 1,
                    Description = "NI"
                },

                new Division()
                {
                    DivisionId = 2,
                    Description = "ROI"
                }
            };

            _teams = new List<Team>()
            {
                new Team
                {
                    TeamID = 1, 
                    Description = "Developement", 
                    Department = departments[0],
                    Division = divisions[0]
                },

                new Team
                {
                    TeamID = 1, 
                    Description = "Caroline Nolan Team", 
                    Department = departments[1],
                    Division = divisions[1]
                },

                new Team
                {
                    TeamID = 1, 
                    Description = "Corporate Team", 
                    Department = departments[0],
                    Division = divisions[1]
                },
            };

            TeamRepositoryMock.Setup(x => x.GetAllTeamsWithDivisionAndDepartment()).Returns(_teams);
            _teamService = GetTarget();
        }

        [Test]
        public void Given_Teams_Requested_Returns_All_Teams_As_TeamModel()
        {
            var teamsModel = _teamService.GetAllTeamsWithDivisionAndDepartment();
            Assert.That(teamsModel.Count(), Is.EqualTo(3));
        }

        [Test]
        public void Given_Teams_Requested_Returns_TeamModel_With_DepartmentDescription()
        {
            var teamsModel = _teamService.GetAllTeamsWithDivisionAndDepartment().ToList();
            var team = teamsModel[0];
            
            Assert.That(team.DepartmentDescription, Is.EqualTo("Taxwise"));
        }

        [Test]
        public void Given_Teams_Requested_Returns_TeamModel_With_DivisionDescription()
        {
            var teamsModel = _teamService.GetAllTeamsWithDivisionAndDepartment().ToList();
            var team = teamsModel[0];

            Assert.That(team.DivisionDescription, Is.EqualTo("NI"));
        }
    }
}
