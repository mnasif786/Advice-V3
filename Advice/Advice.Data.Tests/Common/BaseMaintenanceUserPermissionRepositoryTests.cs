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

namespace Advice.Data.Tests.Common
{
    public abstract class BaseMaintenanceUserPermissionRepositoryTests
    {
        private IAdviceDbContextManager _adviceDbContextManager;
        private Mock<AdviceEntities> _adviceEntities;
        protected IMaintenanceUserPermissionRepository MaintenanceUserPermissionRepository;
        private List<MaintenanceUserPermission> _maintenanceUserPermissions;
        private List<User> _users;

        [SetUp]
        protected void SetUp()
        {
            _adviceEntities = new Mock<AdviceEntities>();
            _adviceDbContextManager = new AdviceDbContextManager(_adviceEntities.Object);
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
                    Username = "Tassavur.Ali"
                }
            };

            _maintenanceUserPermissions = new List<MaintenanceUserPermission>()
            {
                new MaintenanceUserPermission()
                {
                    MaintenanceUserPermissionId = 1,
                    MaintenancePermissionId = 1,
                    Deleted = false,
                    UserId = 1,
                    User = _users[0]
                },

                new MaintenanceUserPermission()
                {
                    MaintenanceUserPermissionId = 2,
                    MaintenancePermissionId = 2,
                    Deleted = false,
                    UserId = 1,
                    User = _users[0]
                },

                new MaintenanceUserPermission()
                {
                    MaintenanceUserPermissionId = 3,
                    MaintenancePermissionId = 3,
                    Deleted = false,
                    UserId = 1,
                    User = _users[0]
                },

                new MaintenanceUserPermission()
                {
                    MaintenanceUserPermissionId = 4,
                    MaintenancePermissionId = 3,
                    Deleted = true,
                    UserId = 1,
                    User = _users[0]
                },

                new MaintenanceUserPermission()
                {
                    MaintenanceUserPermissionId = 5,
                    MaintenancePermissionId = 3,
                    Deleted = false,
                    UserId = 2,
                    User = _users[1]
                }


            };
           
            MaintenanceUserPermissionRepository = new MaintenanceUserPermissionRepository(_adviceDbContextManager);

            var dbSetMockUser = DbSetInitialisedMockFactory<User>.CreateDbSetInitalisedMock(_users);
            _adviceEntities.Setup(x => x.Users).Returns(dbSetMockUser.Object);
            _adviceEntities.Setup(x => x.Set<User>()).Returns(dbSetMockUser.Object);

            var dbSetMockMaintenanceUserPermission = DbSetInitialisedMockFactory<MaintenanceUserPermission>.CreateDbSetInitalisedMock(_maintenanceUserPermissions);
            _adviceEntities.Setup(x => x.MaintenanceUserPermissions).Returns(dbSetMockMaintenanceUserPermission.Object);
            _adviceEntities.Setup(x => x.Set<MaintenanceUserPermission>()).Returns(dbSetMockMaintenanceUserPermission.Object);
        }
    }
}
