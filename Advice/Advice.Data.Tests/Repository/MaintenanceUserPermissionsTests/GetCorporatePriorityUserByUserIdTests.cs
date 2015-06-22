using Advice.Data.Tests.Common;
using NUnit.Framework;

namespace Advice.Data.Tests.Repository.MaintenanceUserPermissionsTests
{
    [TestFixture]
    public class GetCorporatePriorityUserByUserIdTests : BaseMaintenanceUserPermissionRepositoryTests
    {
        [Test]
        public void Given_CorporatePriorityUsers_If_Requested_By_UserId_Then_Returns_CorporatePriorityUser()
        {
            var maintenanceUserPermissions = MaintenanceUserPermissionRepository.GetCorporatePriorityUserByUserId(1);
            Assert.IsNotNull(maintenanceUserPermissions);
        }
    }
}
