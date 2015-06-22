using NUnit.Framework;
using Peninsula.Data.Tests.Common;

namespace Peninsula.Data.Tests.Repositories.TblCustomerRepositoryTests
{
    public class GetCustomerByCompanyNameTests : BaseTblCustomerRepositoryTests
    {
        [Test]
        public void Given_Customers_Requested_By_CustomeryName_Then_Returns_Correct_Customer()
        {
            var customer = TblCustomerRepository.GetCustomerByCompanyName("Peninsula Business Services");
            Assert.That(customer.CompanyName, Is.EqualTo("Peninsula Business Services"));
        }
    }
}
