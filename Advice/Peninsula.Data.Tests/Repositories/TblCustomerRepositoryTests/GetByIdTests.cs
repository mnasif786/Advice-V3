using NUnit.Framework;
using Peninsula.Data.Tests.Common;

namespace Peninsula.Data.Tests.Repositories.TblCustomerRepositoryTests
{
    [TestFixture]
    public class GetByIdTests : BaseTblCustomerRepositoryTests
    {
        [Test]
        public void Given_Customers_Requested_By_CustomeryId_Then_Returns_Correct_Customer()
        {
            var customer = TblCustomerRepository.GetById(1);
            Assert.IsNotNull(customer);
        }
    }
}
