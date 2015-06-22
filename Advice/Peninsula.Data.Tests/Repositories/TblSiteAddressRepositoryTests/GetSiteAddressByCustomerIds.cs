using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Peninsula.Data.Tests.Common;

namespace Peninsula.Data.Tests.Repositories.TblSiteAddressRepositoryTests
{
    [TestFixture]
    public class GetSiteAddressByCustomerIds : BaseTblSiteAddressRepositoryTests
    {
        [Test]
        public void Given_Customers_When_Requested_Their_Addresses_By_Ids_Then_All_Customers_Addresses_Are_Returned()
        {
            var customerIds = new List<int>() {3, 4, 5, 6};
            var addresses = TblSiteAddressRepository.GetSiteAddressByCustomerIds(customerIds);

            Assert.That(addresses.Count(), Is.EqualTo(4));

        }

        [Test]
        public void Given_Customers_When_Requested_Their_Addresses_By_Ids_Then_Only_Customers_Addresses_With_Main_SiteAddress_Are_Returned()
        {
            var customerIds = new List<int>() {3, 4, 5, 6, 10};
            var addresses = TblSiteAddressRepository.GetSiteAddressByCustomerIds(customerIds);

            Assert.That(addresses.Count(), Is.EqualTo(4));

        }
    }
}
