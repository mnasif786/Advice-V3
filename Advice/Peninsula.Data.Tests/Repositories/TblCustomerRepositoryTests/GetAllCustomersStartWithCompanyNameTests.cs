using System.Linq;
using NUnit.Framework;
using Peninsula.Data.Tests.Common;

namespace Peninsula.Data.Tests.Repositories.TblCustomerRepositoryTests
{
    [TestFixture]
    public class GetAllCustomersStartWithCompanyNameTests : BaseTblCustomerRepositoryTests
    {
        [Test]
        public void Given_Customers_Requested_By_Initial_Characters_Then_Returns_All_Customers_Starting_With_Given_CompanyName()
        {
            const string initials = "Client";
            var customers = TblCustomerRepository.GetAllCustomersStartWithCompanyName(initials);

            Assert.That(customers.Count(), Is.EqualTo(12));

        }
    }
}
