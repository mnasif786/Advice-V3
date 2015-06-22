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
    public class DivisionControllerTests
    {
        private Mock<IDivisionService> _divisionService;
        private Mock<IUserService> _userService;
        private Mock<IUserIdentityFactory> _userIdentity;
        private List<DivisionModel> _divisions;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _divisions = new List<DivisionModel>()
            {
                new DivisionModel(){ DivisionId = 1, Description = "Long Division"      },             
                new DivisionModel(){ DivisionId = 2, Description = "Cell Division"      },             
                new DivisionModel(){ DivisionId = 3, Description = "Premier Division"   }
            };

            _divisionService = new Mock<IDivisionService>();

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
        public void Given_Division_Exists_When_GetAll_Then_Return_All_Divisions()
        {
            _divisionService
                .Setup(x => x.GetAllDivisions())
                .Returns(_divisions);

            var divisionController = GetTarget();

            var actionResult = divisionController.GetAllDivisions();
            var contentResult = actionResult as OkNegotiatedContentResult<IOrderedEnumerable<DivisionModel>>;

            Assert.AreEqual(contentResult.Content.ToList().Count, _divisions.Count());
        }

        [Test]
        public void Given_Division_Exists_When_EditDivision__is_called_Then_Division_Is_Updated()
        {
            DivisionModel model = new DivisionModel() { DivisionId = 123, Description = "No Division" };            
            _divisionService
                .Setup( x => x.UpdateDivision( It.IsAny<DivisionModel>() ) )
                .Callback<DivisionModel>(
                ( (m) => 
                    { 
                        model = m;                     
                    }
                ));
            
            var divisionController = GetTarget();

            int updatedID = 456;
            string updatedDescription = "JoyDivision";
            DivisionModel updatedModel = new DivisionModel(){DivisionId = updatedID, Description = updatedDescription};
            var actionResult = divisionController.EditDivision( updatedModel );
           
            Assert.AreEqual( updatedID,             model.DivisionId);
            Assert.AreEqual( updatedDescription,    model.Description);                       
        }

       

        private UserModel GetUser()
        {

            return new UserModel()
            {
                UserId = 1,
                Username = "na.asif",
                RoleId = 1,
                TeamId = 1,
                Team = new TeamModel { TeamId = 1, Description = "Dev Team", DepartmentId = 2 },
                Permissions = new UserPermissionModel { AccessCallRecorder = true }
            };
        }

        private DivisionController GetTarget()
        {
            return new DivisionController( _divisionService.Object, _userIdentity.Object);
        }
    }
}
