using System.Collections.Generic;
using NUnit.Framework;
using Peninsula.Data.Tests.Common;
using Peninsula.Data.Tests.TestHelpers;
using Peninsula.Domain.Entities;

namespace Peninsula.Data.Tests.Repositories.TblCustomerRepositoryTests
{
    [TestFixture]
    public class GetCompanyContactByIdTests : BaseTblCustomerRepositoryTests
    {
        [Test]
        public void Given_Company_Contact_is_Requested_By_Company_Contact_Id_Then_Returns_Correct_Contact()
        {
            var contact = new TBLCompanyContact
            {
                CompanyContactID = 1,
                Title = "Mr.",
                Forename = "Muhammad",
                Surname = "Asif",
                Initial = "N",
                EMail = "mna@abc.com",
                TelNoMain = "07666767676",
                TelNoAlt = "07666767676"
            };

            var companyContacts = new List<TBLCompanyContact> { contact};

            var dbSetMockCompanyContact = DbSetInitialisedMockFactory<TBLCompanyContact>.CreateDbSetInitalisedMock(companyContacts);
            _peninsulaEntities.Setup(x => x.TBLCompanyContacts).Returns(dbSetMockCompanyContact.Object);

            var compayContact = TblCustomerRepository.GetCompanyContactWithContactId(1);
            Assert.IsNotNull(compayContact);
            Assert.That(compayContact.CompanyContactID, Is.EqualTo(1));

            Assert.That(compayContact.Title, Is.EqualTo(contact.Title));
            Assert.That(compayContact.Forename, Is.EqualTo(contact.Forename));
            Assert.That(compayContact.Surname, Is.EqualTo(contact.Surname));
            Assert.That(compayContact.Initial, Is.EqualTo(contact.Initial));
            Assert.That(compayContact.TelNoMain, Is.EqualTo(contact.TelNoMain));
            Assert.That(compayContact.TelNoAlt, Is.EqualTo(contact.TelNoAlt));
            Assert.That(compayContact.EMail, Is.EqualTo(contact.EMail));
        }
    }
}
