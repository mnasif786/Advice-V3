using System.Linq;
using NUnit.Framework;
using Peninsula.Data.Tests.Common;

namespace Peninsula.Data.Tests.Repositories.TblCustomerRepositoryTests
{
    [TestFixture]
    public class GetTop10CustomersStartWithCompanyNameTests : BaseTblCustomerRepositoryTests
    {
        [Test]
        public void Given_Customers_Requested_By_CamelCase_Initial_Characters_Then_Returns_Customers_Starting_With_Correct_Initials()
        {
            const string initials = "Client Pa";
            var customers = TblCustomerRepository.GetTop10CustomersStartWithCompanyName(initials);
            Assert.That(customers.Count(), Is.EqualTo(2));
            foreach (var tblCustomer in customers)
            {
                Assert.IsTrue(tblCustomer.CompanyName.StartsWith(initials));
            }
        }

        [Test]
        public void Given_Customers_Requested_By_CamelCase_Initial_Characters_Then_Returns_Customers_Starting_With_Correct_Number_Of_Records()
        {
            const string initials = "Client Pa";
            var customers = TblCustomerRepository.GetTop10CustomersStartWithCompanyName(initials);

            Assert.That(customers.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Given_Customers_Requested_By_Initial_Characters_Then_Returns_Customers_Starting_With_Correct_Initials()
        {
            const string initials = "Client";
            var customers = TblCustomerRepository.GetTop10CustomersStartWithCompanyName(initials);

            Assert.That(customers.Count(), Is.EqualTo(10));

            foreach (var tblCustomer in customers)
            {
                Assert.IsTrue(tblCustomer.CompanyName.StartsWith(initials));
            }
        }

        [Test]
        public void Given_Customers_Requested_By_Initial_Characters_Then_Returns_Customers_Starting_With_Correct_Number_Of_Records()
        {
            const string initials = "Peni";
            var customers = TblCustomerRepository.GetTop10CustomersStartWithCompanyName(initials);
           
            Assert.That(customers.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Given_Customers_Requested_By_Initial_Characters_Then_Returns_Customers_Not_More_Than_10_Records()
        {
            const string initials = "Client";
            var customers = TblCustomerRepository.GetTop10CustomersStartWithCompanyName(initials);

            Assert.IsTrue(customers.Count() <=10 );
            
        }
    }
}
