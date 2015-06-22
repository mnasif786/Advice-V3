using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web.Http.Results;
using Advice.Application;
using Advice.Application.Contracts;
using Advice.Common.Models;
using Advice.Web.Controllers;
using Advice.Web.Helpers;
using Moq;
using NUnit.Framework;

namespace Advice.WebAPI.Tests
{
    [TestFixture]
    public class TeamControllerTests
    {
        private Mock<ITeamService> _teamService;
        private Mock<IUserService> _userService;
        private Mock<IUserIdentityFactory> _userIdentity;
        private List<TeamModel> _teams;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _teams = new List<TeamModel>()
            {
                new TeamModel(){ TeamId = 1, Description= "Caroline Nolan Team", DepartmentId = 2, ManagerId =1, DepartmentDescription = "Health & Safety", DivisionDescription = "ROI"},             
                new TeamModel(){ TeamId = 2, Description= "Payroll Advice Team", DepartmentId = 2, ManagerId =1, DepartmentDescription = "Employment Services", DivisionDescription = "NI" },            
                new TeamModel(){ TeamId = 3, Description= "Health & Safety Out Of Hours", DepartmentId = 2, ManagerId =1, DepartmentDescription = "Taxwise", DivisionDescription = "Complimentary Advice" },            
                new TeamModel(){ TeamId = 4, Description= "Caroline Nolan Team", DepartmentId = 2, ManagerId =1, DepartmentDescription = "Employment Services", DivisionDescription = "ES Consultancy" }             
            };

            _teamService = new Mock<ITeamService>();

            _userService = new Mock<IUserService>();


            var identity = new Mock<IIdentity>();
            identity.Setup(x => x.Name).Returns("domain\\user.name");

            var principal = new Mock<IPrincipal>();
            principal.Setup(x => x.Identity).Returns(identity.Object);

            Thread.CurrentPrincipal = principal.Object;

            _userIdentity = new Mock<IUserIdentityFactory>();
            _userIdentity.Setup(x => x.GetUserIdentity(It.IsAny<IPrincipal>()))
                .Returns(new UserIdentity(Thread.CurrentPrincipal));

            var user = GetUser();
            _userService.Setup(u => u.GetUserByNameWithTeamAndPermissions(It.IsAny<string>())).Returns(user);
        }

        [Test]
        public void Given_Team_Exists_When_GetAll_Then_Return_All_Teams()
        {
            _teamService
                .Setup( x => x.GetTeamsByDepartmentId( It.IsAny<long>() ) )
                .Returns( _teams );

            var teamController = GetTarget();

            var actionResult = teamController.GetAllTeams();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<TeamModel>>;

            Assert.AreEqual( contentResult.Content.ToList().Count, _teams.Count() );
        }

        [Test]
        public void Given_Teams_Exists_When_GetAllTeamsWithDivisionAndDepartment_Then_Return_All_Teams_With_Division_And_Department()
        {
            _teamService
                .Setup(x => x.GetAllTeamsWithDivisionAndDepartment())
                .Returns(_teams);

            var teamController = GetTarget();

            var actionResult = teamController.GetAllTeamsWithDivisionAndDepartment();
            var contentResult = actionResult as OkNegotiatedContentResult<IOrderedEnumerable<TeamModel>>;

            Assert.AreEqual(contentResult.Content.ToList().Count, _teams.Count());
        }

        [Test]
        public void Given_Teams_Exists_When_GetAllTeamsWithDivisionAndDepartment_Then_Returns_Data_In_Sorted_Order_By_Description()
        {
            _teamService
                .Setup(x => x.GetAllTeamsWithDivisionAndDepartment())
                .Returns(_teams);

            var teamController = GetTarget();

            var actionResult = teamController.GetAllTeamsWithDivisionAndDepartment();
            var contentResult = actionResult as OkNegotiatedContentResult<IOrderedEnumerable<TeamModel>>;

            var teams = contentResult.Content.ToList();

            Assert.That(teams, Is.Ordered.By("Description"));
        }

        private UserModel GetUser()
        {

            return new UserModel()
            {
                UserId = 1,
                Username = "na.asif",
                RoleId = 1,
                TeamId = 1,
                Team = new TeamModel{ TeamId = 1, Description = "Dev Team", DepartmentId = 2}, 
                Permissions = new UserPermissionModel {AccessCallRecorder = true}
            };
        }

        private TeamController GetTarget()
        {
            return new TeamController(_teamService.Object, _userIdentity.Object, _userService.Object);
        }     
    }
}
