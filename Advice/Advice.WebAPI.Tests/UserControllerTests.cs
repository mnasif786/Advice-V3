using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web.Http.Results;
using Advice.Application;
using Advice.Application.Contracts;
using Advice.Common.Models;
using Advice.Domain.Entities;
using Advice.Web.Controllers;
using Advice.Web.Helpers;
using Moq;
using NUnit.Framework;

namespace Advice.WebAPI.Tests
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUserService> _userService;
        private Mock<IUserIdentityFactory> _userIdentity;
        private Mock<IPermissionService> _permissionService;
        private List<UserModel> _users;
        private List<PermissionModel> _permissions;

        private readonly long DEPT_EmploymentServices = 2;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _users = new List<UserModel>()
            {
                new UserModel(){   UserId = 1,  Username = "Adam Ant",          RoleId  = 1, TeamId  = DEPT_EmploymentServices, Permissions = new UserPermissionModel { AccessCallRecorder = true}},
                new UserModel(){   UserId = 2,  Username = "Billy Bragg",       RoleId  = 1, TeamId  = DEPT_EmploymentServices },
                new UserModel(){   UserId = 3,  Username = "Colin Campbell",    RoleId  = 1, TeamId  = DEPT_EmploymentServices },
                new UserModel(){   UserId = 4,  Username = "Donald Duck",       RoleId  = 1, TeamId  = DEPT_EmploymentServices }                
            };

            _permissions = new List<PermissionModel>()
            {
                new PermissionModel(1, "DeleteTask - Own"),
                new PermissionModel(2, "DeleteTask - Other"),
            };

            _userService = new Mock<IUserService>();
            _permissionService = new Mock<IPermissionService>();

            var identity = new Mock<IIdentity>();
            identity.Setup(x => x.Name).Returns("domain\\user.name");

            var principal = new Mock<IPrincipal>();
            principal.Setup(x => x.Identity).Returns(identity.Object);

            Thread.CurrentPrincipal = principal.Object;

            _userIdentity = new Mock<IUserIdentityFactory>();
            _userIdentity.Setup(x => x.GetUserIdentity(It.IsAny<IPrincipal>()))
                .Returns(new UserIdentity(Thread.CurrentPrincipal));
        }

        [Test]
        public void Given_Users_Exist_When_GetAll_Then_Return_All_Users()
        {

            _userService
                .Setup( x => x.GetAllUsers() )
                .Returns(_users);

            var userController = GetTarget();

            var actionResult = userController.GetAllUsers();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<UserModel>>;

            Assert.AreEqual(contentResult.Content.ToList().Count, _users.Count());
        }

        [Test]
        public void Given_User_Then_Return_All_Permissions()
        {
            _permissionService.Setup(x => x.GetPermissionsByRoleId(1)).Returns(_permissions);
            _userService.Setup(x => x.GetUserByNameWithTeamAndPermissions(It.IsAny<string>())).Returns(_users[0]);
            var userController = GetTarget();
            var actionResult = userController.GetUserWithPermissions();
            var contentResult = actionResult as OkNegotiatedContentResult<UserModel>;

            Assert.IsNotNull(contentResult.Content.Permissions);
        }

        [Test]
        public void Given_A_User_Logged_In_Then_Return_That_User()
        {
            var userController = GetTarget();
            var actionResult = userController.GetLoggedInUser();
            var contentResult = actionResult as OkNegotiatedContentResult<UserIdentity>;

            Assert.AreEqual(contentResult.Content.Username, new UserIdentity(Thread.CurrentPrincipal).Username);
        }

        private UserController GetTarget()
        {
            return new UserController(_userService.Object, _permissionService.Object, _userIdentity.Object);
        }
    }
}
