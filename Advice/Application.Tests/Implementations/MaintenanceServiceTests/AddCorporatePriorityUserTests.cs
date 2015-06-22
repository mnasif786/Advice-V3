using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Entities;
using Application.Tests.Common;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Implementations.MaintenanceServiceTests
{
    [TestFixture]
    public class AddCorporatePriorityUserTests : BaseMaintenanceServiceTests
    {
        private List<User> _users;
        private const string CreatedBy = "rana.khan";
        private MaintenanceUserPermission _maintenanceUserPermission;

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
                    MaintenancePermissionId = 1,
                    Deleted = false,
                    UserId = 2,
                    User = _users[1]
                }
            };

            MaintenanceUserPermissionRepositoryMock.Setup(x => x.GetCorporatePriorityUserByUserId(_users[0].UserID))
                .Returns(corporatePriorityUsers[0]);

            MaintenanceUserPermissionRepositoryMock.Setup(x => x.GetCorporatePriorityUserByUserId(_users[1].UserID))
                .Returns((MaintenanceUserPermission) null);

            MaintenanceUserPermissionRepositoryMock.Setup(x => x.Insert(It.IsAny<MaintenanceUserPermission>()))
                .Callback<MaintenanceUserPermission>(m => _maintenanceUserPermission = m);
        }

        [Test]
        public void Given_CorporatePriorityUser_Exists_When_Requested_to_Add_CorporatePriorityUser_Returns_False()
        {
            var userAdded = MaintenanceService.AddCorporatePriorityUser(_users[0].UserID, CreatedBy);
            Assert.IsFalse(userAdded);
        }

        [Test]
        public void Given_CorporatePriorityUser_DoesNot_Exists_When_Requested_Then_Adds_Requested_User()
        {
            MaintenanceService.AddCorporatePriorityUser(_users[1].UserID, CreatedBy);
            Assert.That(_maintenanceUserPermission, Is.Not.Null);
        }

        [Test]
        public void Given_CorporatePriorityUser_DoesNot_Exists_When_Requested_to_Add_CorporatePriorityUser_Then_Returns_True()
        {
            var userAdded = MaintenanceService.AddCorporatePriorityUser(_users[1].UserID, CreatedBy);
            Assert.IsTrue(userAdded);
        }
    }
}
