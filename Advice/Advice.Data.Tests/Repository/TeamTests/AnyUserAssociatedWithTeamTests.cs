using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Tests.Common;
using NUnit.Framework;

namespace Advice.Data.Tests.Repository.TeamTests
{
    [TestFixture]
    public class AnyUserAssociatedWithTeamTests : BaseTeamRepositoryTests
    {
        [Test]
        public void Given_Users_And_TeamId_1_When_Requested_For_test_Of_Associated_User_True_Is_returned()
        {
            var userAssociated = TeamRepository.AnyUserAssociatedWithTeam(1);
            Assert.IsTrue(userAssociated);
        }

        [Test]
        public void Given_Users_And_TeamId_2_When_Requested_For_test_Of_Associated_User_False_Is_returned()
        {
            var userAssociated = TeamRepository.AnyUserAssociatedWithTeam(2);
            Assert.IsFalse(userAssociated);
        }

        [Test]
        public void Given_Users_And_TeamId_3_When_Requested_For_test_Of_Associated_User_False_Is_returned()
        {
            var userAssociated = TeamRepository.AnyUserAssociatedWithTeam(3);
            Assert.IsFalse(userAssociated);
        }
    }
}
