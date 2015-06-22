using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Common.Models;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Application.Tests.Common;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Implementations.TeamServiceTests
{
    [TestFixture]
    public class GetTeamsByDepartmentTests: BaseTeamServiceTests
    {
       private IEnumerable<Team> _teamList;

        [SetUp]
        public new void SetUp()
        {

            _teamList = GetTeams();
        }

        [Test]
        public void given_list_of_teams_is_requested_by_employment_service_department_then_only_returns_team_that_belongs_to_employment_service_department()
        {
            TeamRepositoryMock.Setup(x => x.GetTeamsByDepartmentId((long)Departments.EmploymentServices)).Returns(_teamList.Where(t => t.DepartmentID == (long)Departments.EmploymentServices));

            var target = GetTarget();
            var departmentId = (long) Departments.EmploymentServices;

            var teamsList = target.GetTeamsByDepartmentId(departmentId);


            Assert.That(teamsList.Count(), Is.EqualTo(2));
        }

        [Test]
        public void given_list_of_teams_is_requested_by_H_and_S_department_then_only_returns_team_that_belongs_to_H_and_S_department()
        {
            TeamRepositoryMock.Setup(x => x.GetTeamsByDepartmentId((long)Departments.HealthAndSafety)).Returns(_teamList.Where(t => t.DepartmentID == (long)Departments.HealthAndSafety));

            var target = GetTarget();

            var departmentId = (long)Departments.HealthAndSafety;

            var teamsList = target.GetTeamsByDepartmentId(departmentId);
            
            Assert.That(teamsList.Count(), Is.EqualTo(1));
        }

        [Test]
        public void given_list_of_teams_is_requested_by_taxwise_department_then_only_returns_team_that_belongs_to_taxwise_department()
        {

            TeamRepositoryMock.Setup(x => x.GetTeamsByDepartmentId((long)Departments.Taxwise)).Returns(_teamList.Where(t => t.DepartmentID == (long)Departments.Taxwise));

            var target = GetTarget();

            var departmentId = (long)Departments.Taxwise;

            var teamsList = target.GetTeamsByDepartmentId(departmentId);

            Assert.That(teamsList.Count(), Is.EqualTo(1));
        }

        private IEnumerable<Team> GetTeams()
        {
            var teamList = new List<Team>();

            teamList.Add(new Team {TeamID = 1, Description = "Developement", DepartmentID = (long)Departments.EmploymentServices}); //Emp Services
            teamList.Add(new Team { TeamID = 2, Description = "Developement", DepartmentID = (long)Departments.EmploymentServices });
            teamList.Add(new Team { TeamID = 3, Description = "Hs", DepartmentID = (long)Departments.HealthAndSafety }); //H & S
            teamList.Add(new Team { TeamID = 4, Description = "Tax", DepartmentID = (long)Departments.Taxwise }); //Tax Wise

            return teamList.AsEnumerable();
        }

    }
}
