using NUnit.Framework;
using Peninsula.Data.Tests.Common;

namespace Peninsula.Data.Tests.Repositories.TblCustomerRepositoryTests
{
    [TestFixture]
    public class GetCustomersByCustomerKeyTests : BaseTblCustomerRepositoryTests
    {
        [Test]
        public void Given_Customers_Requested_By_CustomeryKey_Then_Returns_Correct_Customer()
        {
            var customer = TblCustomerRepository.GetCustomerByCustomerKey("ZPEN01");
            Assert.IsNotNull(customer);
        }
    }
}
