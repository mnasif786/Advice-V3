using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Common.Models;
using Advice.Domain.Entities;
using Application.Tests.Common;
using NUnit.Framework;
using Peninsula.Domain.Entities;

namespace Application.Tests.Implementations.MaintenanceServiceTests
{
    [TestFixture]
    public class GetAllCorporatePrioritiesTests : BaseMaintenanceServiceTests
    {
        private List<CorporatePriority> _corporatePriorities;
        private List<TBLCustomer> _tblCustomers;
        private readonly User _user = new User() {Username = "Long.Manchester"};
        [SetUp]
        public void SetUp()
        {
            _corporatePriorities = new List<CorporatePriority>()
            {
                new CorporatePriority()
                {
                    CorporatePriorityId = 1,
                    Can="DEMO001",
                    ContractDetail = "Test Details 1",
                    ContractEndDate = DateTime.Now.AddDays(45),
                    ContractValue = 100,
                    CreatedBy = "Mike.Henry",
                    CreatedDate = DateTime.Now.AddDays(-5),
                    Deleted = false,
                    User = _user
                },
                new CorporatePriority()
                {
                    CorporatePriorityId = 2,
                    Can="DEMO002",
                    ContractDetail = "Test Details 2",
                    ContractEndDate = DateTime.Now.AddDays(5),
                    ContractValue = 105,
                    CreatedBy = "Mike.Henry",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    Deleted = false,
                    User = _user
                },
                new CorporatePriority()
                {
                    CorporatePriorityId = 3,
                    Can="DEMO003",
                    ContractDetail = "Test Details 3",
                    ContractEndDate = DateTime.Now.AddDays(7),
                    ContractValue = 1000,
                    CreatedBy = "Mike.Henry",
                    CreatedDate = DateTime.Now.AddDays(-15),
                    Deleted = false,
                    User = _user
                },
                new CorporatePriority()
                {
                    CorporatePriorityId = 4,
                    Can="DEMO004",
                    ContractDetail = "Test Details 4",
                    ContractEndDate = DateTime.Now.AddDays(4),
                    ContractValue = 1000,
                    CreatedBy = "Mike.Henry",
                    CreatedDate = DateTime.Now.AddDays(-5),
                    Deleted = false,
                    User = _user
                },
            };  
  
            _tblCustomers = new List<TBLCustomer>()
            {
                new TBLCustomer()
                {
                    CustomerKey = "DEMO001",
                    CompanyName = "Intranet Demonstration"
                },

                new TBLCustomer()
                {
                    CustomerKey = "DEMO002",
                    CompanyName = "Peninsula Business Services"
                },

                new TBLCustomer()
                {
                    CustomerKey = "DEMO003",
                    CompanyName = "BusinessSafe Online"
                },

                new TBLCustomer()
                {
                    CustomerKey = "DEMO004",
                    CompanyName = "Safe Check Online"
                }
            };
        }

        [Test]
        public void Given_Corporate_Priorities_Requested_Then_Returns_All()
        {
            CorporatePriorityRepositoryMock.Setup(x => x.GetAllCorporatePriorities()).Returns(_corporatePriorities);
            var customerKeys = _tblCustomers.Select(x => x.CustomerKey).ToList();
            TblCustomerRepositoryMock.Setup(x => x.GetCustomersByCustomerKeys(customerKeys)).Returns(_tblCustomers);
            var coporatePriorities = MaintenanceService.GetAllCorporatePriorities();
            Assert.That(coporatePriorities.Count(), Is.EqualTo(4));
        }

        [Test]
        public void Given_Corporate_Priorities_Requested_Then_Returns_All_With_Correct_ClientNames()
        {
            CorporatePriorityRepositoryMock.Setup(x => x.GetAllCorporatePriorities()).Returns(_corporatePriorities);
            var customerKeys = _tblCustomers.Select(x => x.CustomerKey).ToList();
            TblCustomerRepositoryMock.Setup(x => x.GetCustomersByCustomerKeys(customerKeys)).Returns(_tblCustomers);
            var coporatePriorities = MaintenanceService.GetAllCorporatePriorities();
            var corporatePriorityList = coporatePriorities as IList<CorporatePriorityModel> ?? coporatePriorities.ToList();
            Assert.That(corporatePriorityList[0].ClientName, Is.EqualTo("Intranet Demonstration"));
            Assert.That(corporatePriorityList[1].ClientName, Is.EqualTo("Peninsula Business Services"));
            Assert.That(corporatePriorityList[2].ClientName, Is.EqualTo("BusinessSafe Online"));
            Assert.That(corporatePriorityList[3].ClientName, Is.EqualTo("Safe Check Online"));
        }

        [Test]
        public void Given_Corporate_Priorities_Requested_Then_Returns_All_With_Correct_LeadConsultant()
        {
            CorporatePriorityRepositoryMock.Setup(x => x.GetAllCorporatePriorities()).Returns(_corporatePriorities);
            var customerKeys = _tblCustomers.Select(x => x.CustomerKey).ToList();
            TblCustomerRepositoryMock.Setup(x => x.GetCustomersByCustomerKeys(customerKeys)).Returns(_tblCustomers);
            var coporatePriorities = MaintenanceService.GetAllCorporatePriorities();
            var corporatePriorityList = coporatePriorities as IList<CorporatePriorityModel> ?? coporatePriorities.ToList();
            Assert.That(corporatePriorityList[0].LeadConsultant, Is.EqualTo(_user.Username));
            Assert.That(corporatePriorityList[1].LeadConsultant, Is.EqualTo(_user.Username));
            Assert.That(corporatePriorityList[2].LeadConsultant, Is.EqualTo(_user.Username));
            Assert.That(corporatePriorityList[3].LeadConsultant, Is.EqualTo(_user.Username));
        }

        [Test]
        public void Given_Corporate_Priorities_Requested_Then_Returns_All_Ordered_By_CAN()
        {
            CorporatePriorityRepositoryMock.Setup(x => x.GetAllCorporatePriorities()).Returns(_corporatePriorities);
            var customerKeys = _tblCustomers.Select(x => x.CustomerKey).ToList();
            TblCustomerRepositoryMock.Setup(x => x.GetCustomersByCustomerKeys(customerKeys)).Returns(_tblCustomers);
            var coporatePriorities = MaintenanceService.GetAllCorporatePriorities();
            Assert.That(coporatePriorities, Is.Ordered.By("Can"));
        }
    }
}
