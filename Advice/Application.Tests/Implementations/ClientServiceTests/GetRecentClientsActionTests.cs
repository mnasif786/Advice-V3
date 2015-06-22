using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Common.Models;
using Advice.Domain.Entities;
using Application.Tests.Common;
using NUnit.Framework;

namespace Application.Tests.Implementations.ClientServiceTests
{
    [TestFixture]
    public class GetRecentClientsActionTests : BaseClientServiceTests
    {
        private IList<RecentCustomerAction> _recentCustomerActions;

        [SetUp]
        public void SetUp()
        {
            _recentCustomerActions = new List<RecentCustomerAction>()
            {
                new RecentCustomerAction()
                {
                    CustomerId = 29252,
                    ActionDate = DateTime.Now.AddHours(-6)
                },

                new RecentCustomerAction()
                {
                    CustomerId = 62692,
                    ActionDate = DateTime.Now.AddHours(-2)
                }
            };
        }

        [Test]
        public void Given_Recent_Clients_Action_Requested_Then_All_Clients_Action_Are_Returned()
        {
            const string userName = "Allan.Barcley";
            var customerIds = _recentCustomerActions.Select(x => x.CustomerId).ToList();
            CustomEntityRepositoryMock.Setup(x => x.GetRecentCustomersAction(userName)).Returns(_recentCustomerActions);
            TblCustomerRepositoryMock.Setup(x => x.GetCustomersByCustomerIds(customerIds)).Returns(Customers.Where(x => customerIds.Contains(x.CustomerID)));
            var recentClientActions = ClientService.GetRecentClientsAction(userName);

            Assert.That(recentClientActions.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Given_Recent_Clients_Action_Requested_Then_All_Clients_Action_Are_Returned_In_Descending_Order()
        {
            const string userName = "Allan.Barcley";
            var customerIds = _recentCustomerActions.Select(x => x.CustomerId).ToList();
            CustomEntityRepositoryMock.Setup(x => x.GetRecentCustomersAction(userName)).Returns(_recentCustomerActions);
            TblCustomerRepositoryMock.Setup(x => x.GetCustomersByCustomerIds(customerIds)).Returns(Customers.Where(x => customerIds.Contains(x.CustomerID)));
            var recentClientActions = ClientService.GetRecentClientsAction(userName);

            Assert.That(recentClientActions, Is.Ordered.Descending.By("LastAction"));
            
        }
    }
}
