using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Advice.Application.Implementations;
using Advice.Application.Models;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace Advice.Application.Tests
{
    [TestFixture]
    public class TeamServiceTests
    {
        private Mock<ITeamRepository> _teamRepository;
        private List<Team> _teams;
            
        [SetUp]
        public void TestFixtureSetup()
        {
            _teams = new List<Team>()
            {
                new Team() { Description = "Dept 1 Team 1", TeamID = 1, DepartmentID = 1},
                new Team() { Description = "Dept 1 Team 2", TeamID = 2, DepartmentID = 1},
                new Team() { Description = "Dept 2 Team 1", TeamID = 3, DepartmentID = 2},
                new Team() { Description = "Dept 2 Team 2", TeamID = 4, DepartmentID = 2}
            };
         
            _teamRepository = new Mock<ITeamRepository>();
            _teamRepository.Setup(x => x.GetTeamIdByTeamName("Team2")).Returns(_teams[1].TeamID);
        }
     

        [Test]
        public void Given_DepartmentId_then_return_all_teams_in_department()
        {
            long departmentId = 1;

            var teams = new List<Team>()
            {
                new Team() { Description = "Dept 1 Team 1", TeamID = 1, DepartmentID = 1},
                new Team() { Description = "Dept 1 Team 2", TeamID = 2, DepartmentID = 1}                
            };

            _teamRepository
                .Setup(x => x.GetTeamsByDepartmentId(departmentId))
                .Returns( teams );

            var teamService = GetTarget();

            IList<TeamModel> teamList = teamService.GetTeamsByDepartmentId(departmentId).ToList();

            Assert.That(teamList, Is.Not.Null);
            Assert.AreEqual(teamList.Count(), 2 );

            Assert.AreEqual(teamList[0].TeamId, teams[0].TeamID );
            Assert.AreEqual(teamList[0].Description, teams[0].Description);
            Assert.AreEqual(teamList[0].DepartmentId, teams[0].DepartmentID );
            Assert.AreEqual(teamList[0].Description, teams[0].Description );          
        }

        [Test]
        public void Given_DepartmentId_then_return_all_Special_Service_teams_in_department()
        {
            long departmentId = 3;

            var teams = new List<Team>()
            {
                new Team() { Description = "Dept 1 Team 1", TeamID = 1, DepartmentID = 3},
                new Team() { Description = "Dept 1 Team 2", TeamID = 2, DepartmentID = 3}                
            };

            _teamRepository
                .Setup(x => x.GetSpecialServiceTeamsByDepartmentId(departmentId))
                .Returns(teams);

            var teamService = GetTarget();

            IList<TeamModel> teamList = teamService.GetTeamsByDepartmentId(departmentId).ToList();

            Assert.That(teamList, Is.Not.Null);
            Assert.AreEqual(teamList.Count(), 2);

            Assert.AreEqual(teamList[0].TeamId, teams[0].TeamID);
            Assert.AreEqual(teamList[0].Description, teams[0].Description);
            Assert.AreEqual(teamList[0].DepartmentId, teams[0].DepartmentID);
            Assert.AreEqual(teamList[0].Description, teams[0].Description);
        }

        private TeamService GetTarget()
        {
            return new TeamService( _teamRepository.Object );
        }
    }
}
