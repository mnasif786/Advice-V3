using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Common.Models.Maintenance;
using Advice.Domain.Entities;
using Application.Tests.Common;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Implementations.MaintenanceServiceTests
{
    [TestFixture]
    public class GetCorporatePriorityUsersExcludingThisTests : BaseMaintenanceServiceTests
    {
        private List<User> _users;

        [SetUp]
        public void SetUp()
        {
            _users = new List<User>()
            {
                new User()
                {
                    UserID = 1,
                    Username = "Takbeer.Shah"
                },

                new User()
                {
                    UserID = 2,
                    Username = "Farrukh.Ali"
                },

                 new User()
                {
                    UserID = 3,
                    Username = "Kaira.Mushtaq"
                }
            };

            var corporatePriorityUsers = new List<MaintenanceUserPermission>()
            {
                new MaintenanceUserPermission()
                {
                    MaintenanceUserPermissionId = 1,
                    MaintenancePermissionId = 3,
                    Deleted = false,
                    UserId = 1,
                    User = _users[0]
                },
            
                new MaintenanceUserPermission()
                {
                    MaintenanceUserPermissionId = 2,
                    MaintenancePermissionId = 3,
                    Deleted = false,
                    UserId = 2,
                    User = _users[1]
                },

                new MaintenanceUserPermission()
                {
                    MaintenanceUserPermissionId = 3,
                    MaintenancePermissionId = 3,
                    Deleted = false,
                    UserId = 3,
                    User = _users[2]
                }


            };

            UserRepositoryMock.Setup(x => x.GetUserByName(It.IsAny<string>())).Returns(_users[0]);
            MaintenanceUserPermissionRepositoryMock.Setup(x => x.GetCorporatePriorityUsers())
                .Returns(corporatePriorityUsers);
        }

        [Test]
        public void Given_CorporatePriorityUsers_When_Requested_through_GetCorporatePriorityUsersExcludingThis_Returns_Users()
        {
            var corporatePriorityUsers = MaintenanceService.GetCorporatePriorityUsersExcludingThis(_users[0].Username);
            Assert.That(corporatePriorityUsers.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Given_CorporatePriorityUsers_When_Requested_through_GetCorporatePriorityUsersExcludingThis_Excludes_Calling_CorporatePriorityUser()
        {
            var corporatePriorityUsers = MaintenanceService.GetCorporatePriorityUsersExcludingThis(_users[0].Username);
            var corporatePriorityUserModels = corporatePriorityUsers as IList<CorporatePriorityUserModel> ?? corporatePriorityUsers.ToList();
            foreach (var corporatePriorityUserModel in corporatePriorityUserModels)
            {
                Assert.IsFalse(corporatePriorityUserModel.UserName == _users[0].Username);
            }
        }

        [Test]
        public void Given_CorporatePriorityUsers_When_Requested_through_GetCorporatePriorityUsersExcludingThis_Returns_CorporatePriorityUsers_OrderBy_UserName()
        {
            var corporatePriorityUsers = MaintenanceService.GetCorporatePriorityUsersExcludingThis(_users[0].Username);
            Assert.That(corporatePriorityUsers, Is.Ordered.By("UserName"));
        }
    }
}
