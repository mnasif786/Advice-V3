using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Application.Implementations;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Implementations
{
    public class PermissionServiceTests
    {
        private Mock<IPermissionRepository> _permissionRepository;
        private List<Permission> _permissions;

        [SetUp]
        public void TestFixtureSetup()
        {
            _permissions = new List<Permission>()
            {
                new Permission()
                {
                    PermissionID = 1,
                    Description = "Delete Task - Own"
                },

                new Permission()
                {
                    PermissionID = 2,
                    Description = "Delete Task - Other"
                }
            };

            _permissionRepository = new Mock<IPermissionRepository>();
            _permissionRepository.Setup(x => x.GetPermissionsByRoleId(1)).Returns(_permissions);
        }

        [Test]
        public void Given_Permissions_Then_Return_Permissions_By_RoleId()
        {
            var permissionService = GetTarget();
            var permissions = permissionService.GetPermissionsByRoleId(1);
            Assert.That(permissions.Count(), Is.EqualTo(2));
        }

        private PermissionService GetTarget()
        {
            return new PermissionService(_permissionRepository.Object);
        }
    }
}
