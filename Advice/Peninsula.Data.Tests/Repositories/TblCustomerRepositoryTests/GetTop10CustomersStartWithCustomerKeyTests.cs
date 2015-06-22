using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Peninsula.Data.Tests.Common;

namespace Peninsula.Data.Tests.Repositories.TblCustomerRepositoryTests
{
    [TestFixture]
    public class GetTop10CustomersStartWithCustomerKeyTests : BaseTblCustomerRepositoryTests
    {
        [Test]
        public void Given_Customers_Requested_By_CustomerKey_With_Initial_Characters_Then_Returns_Customers_Starting_With_Correct_Initials()
        {
            const string initials = "CPEN";
            var customers = TblCustomerRepository.GetTop10CustomersStartWithCustomerKey(initials);
            Assert.That(customers.Count(), Is.EqualTo(2));
            foreach (var tblCustomer in customers)
            {
                Assert.IsTrue(tblCustomer.CustomerKey.StartsWith(initials));
            }
        }

        [Test]
        public void Given_Customers_Requested_With_Can_Of_Initial_Characters_Then_Returns_Customers_Starting_With_Correct_Number_Of_Records()
        {
            const string initials = "CPEN";
            var customers = TblCustomerRepository.GetTop10CustomersStartWithCustomerKey(initials);

            Assert.That(customers.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Given_Customers_Requested_By_Initial_Characters_Then_Returns_Customers_Not_More_Than_10_Records()
        {
            const string initials = "ZPEN";
            var customers = TblCustomerRepository.GetTop10CustomersStartWithCustomerKey(initials);

            Assert.IsTrue(customers.Count() <= 10);

        }
    }
}
