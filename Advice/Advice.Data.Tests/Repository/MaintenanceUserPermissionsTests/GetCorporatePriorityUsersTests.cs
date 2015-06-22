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
    public class GetCorporatePriorityUsersTests : BaseMaintenanceUserPermissionRepositoryTests
    {
        [Test]
        public void Given_CorporatePriorityUsers_If_Requested_through_GetCorporatePriorityUsers_Then_Returns_CorporatePriorityUsers()
        {
            var corporatePriorityUsers = MaintenanceUserPermissionRepository.GetCorporatePriorityUsers();
            Assert.That(corporatePriorityUsers.Count(), Is.EqualTo(2));
            
        }

        [Test]
        public void Given_CorporatePriorityUsers_If_Requested_through_GetCorporatePriorityUsers_Then_Returns_CorporatePriorityUsers_With_UserName()
        {
            var corporatePriorityUsers = MaintenanceUserPermissionRepository.GetCorporatePriorityUsers().ToList();
            Assert.That(corporatePriorityUsers[0].User.Username, Is.EqualTo("Takbeer.Shah"));
            Assert.That(corporatePriorityUsers[1].User.Username, Is.EqualTo("Tassavur.Ali"));

        }
    }
}
