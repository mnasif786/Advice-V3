using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Tests.Common;
using NUnit.Framework;

namespace Advice.Data.Tests.Repository.MaintenanceUserPermissionsTests
{
    [TestFixture]
    public class GetMaintenanceUserPermissionsTests : BaseMaintenanceUserPermissionRepositoryTests
    {
        [Test]
        public void Given_MaintenanceUserPermissions_If_Requested_By_UserId_Then_Returns_MaintenanceUserPermissions_Of_UserId()
        {
            var maintenanceUserPermissions = MaintenanceUserPermissionRepository.GetMaintenanceUserPermissions(1);
            Assert.That(maintenanceUserPermissions.Count(), Is.EqualTo(3));
        }
    }
}
