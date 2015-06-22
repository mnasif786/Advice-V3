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

namespace Advice.Data.Tests.Repository
{
    [TestFixture]
    public class GetPermissionsTests
    {
        private IAdviceDbContextManager _adviceDbContextManager;
        private Mock<AdviceEntities> _adviceEntities;
        private IPermissionRepository _permissionRepository;
        private List<Permission> _permissions;
        private List<PermissionRole> _permissionRoles;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _adviceEntities = new Mock<AdviceEntities>();
            _adviceDbContextManager = new AdviceDbContextManager(_adviceEntities.Object);

            _permissions = new List<Permission>()
            {
                new Permission()
                {
                  PermissionID   =  1,
                  Description = "Delete Task - Own",
                },
                new Permission()
                {
                    PermissionID   =  2,
                  Description = "Delete Task - Other User"
                },
                new Permission()
                {
                    PermissionID   =  3,
                    Description = "View QA"
                }
            };

            _permissionRoles = new List<PermissionRole>()
            {
                new PermissionRole()
                {
                    RoleID = 1,
                    Permission = _permissions[0]
                },

                new PermissionRole()
                {
                    RoleID = 1,
                    Permission = _permissions[1]
                },

                new PermissionRole()
                {
                    RoleID = 2,
                    Permission = _permissions[2]
                }
            };

            foreach (var permission in _permissions)
            {
                permission.PermissionRoles = _permissionRoles;
            }

            _permissionRepository = new PermissionRepository(_adviceDbContextManager);

            var dbSetMockPermissions = DbSetInitialisedMockFactory<Permission>.CreateDbSetInitalisedMock(_permissions);
            _adviceEntities.Setup(x => x.Permissions).Returns(dbSetMockPermissions.Object);
            _adviceEntities.Setup(x => x.Set<Permission>()).Returns(dbSetMockPermissions.Object);

            var dbSetMockPermissionRoles = DbSetInitialisedMockFactory<PermissionRole>.CreateDbSetInitalisedMock(_permissionRoles);
            _adviceEntities.Setup(x => x.PermissionRoles).Returns(dbSetMockPermissionRoles.Object);
            _adviceEntities.Setup(x => x.Set<PermissionRole>()).Returns(dbSetMockPermissionRoles.Object);

        }

        [Test]
        public void Given_Permission_Roles_And_Permissions_Then_Returns_Permissions_By_Role_Id()
        {
            var permissions = _permissionRepository.GetPermissionsByRoleId(1);
            Assert.That(permissions.Count(), Is.EqualTo(2));
        }
    }
}
