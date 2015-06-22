using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Tests.Common;
using Advice.Data.Tests.TestHelpers;
using NUnit.Framework;
using Action = Advice.Domain.Entities.Action;
namespace Advice.Data.Tests.Repository.CustomEntityTests
{
    [TestFixture]
    public class GetAdvisorNameWithMostRecentActionsByJobIdTests : BaseCustomEntityRepositoryTests
    {
        private List<Action> _actions;
        private const string User1 = "Sarfarz.Ahmed";
        private const string User2 = "Ahmed.Shehzad";
        private const string User3 = "Younis.Khan";
        private const string JobCreatedBy = "Alan.Donald";

        [SetUp]
        public void SetUp()
        {
            _actions = new List<Action>()
            {
                new Action() { ActionID = 1, LastModifiedBy = User1, LastModifiedDate = DateTime.Now.AddDays(-1).AddMinutes(30), JobID = 1},
                new Action() { ActionID = 2, LastModifiedBy = User1, LastModifiedDate = DateTime.Now.AddDays(-1).AddMinutes(60), JobID = 1},
                new Action() { ActionID = 3, LastModifiedBy = User2, LastModifiedDate = DateTime.Now.AddDays(-1).AddMinutes(90), JobID = 1},

                new Action() { ActionID = 4, LastModifiedBy = User2, LastModifiedDate = DateTime.Now.AddDays(-1).AddMinutes(30), JobID = 2},
                new Action() { ActionID = 5, LastModifiedBy = User2, LastModifiedDate = DateTime.Now.AddDays(-1).AddMinutes(60), JobID = 2},
                new Action() { ActionID = 6, LastModifiedBy = User2, LastModifiedDate = DateTime.Now.AddDays(-1).AddMinutes(90), JobID = 2},
                new Action() { ActionID = 7, LastModifiedBy = User1, LastModifiedDate = DateTime.Now.AddDays(-2).AddMinutes(30), JobID = 2},
                new Action() { ActionID = 8, LastModifiedBy = User1, LastModifiedDate = DateTime.Now.AddDays(-1).AddMinutes(60), JobID = 2},
                new Action() { ActionID = 9, LastModifiedBy = User1, LastModifiedDate = DateTime.Now.AddDays(-2).AddMinutes(90), JobID = 2},
                new Action() {ActionID = 10, LastModifiedBy = User3, LastModifiedDate = DateTime.Now.AddDays(-2).AddMinutes(90), JobID = 2},
            };

            var dbSetMockAction = DbSetInitialisedMockFactory<Action>.CreateDbSetInitalisedMock(_actions);
            AdviceEntities.Setup(x => x.Actions).Returns(dbSetMockAction.Object);
            AdviceEntities.Setup(x => x.Set<Action>()).Returns(dbSetMockAction.Object);
        }

        [Test]
        public void Given_Actions_When_Advisor_Is_Requested_Returns_Advisors_With_Most_Actions()
        {
            string mostActionAdvisor = CustomEntityRepository.GetAdvisorNameWithMostRecentActionsByJobId(1, JobCreatedBy);
            Assert.AreEqual(mostActionAdvisor, User1);
        }

        [Test]
        public void Given_Actions_With_Equal_Advisor_On_Job_When_Advisor_Is_Requested_Returns_Advisors_With_Most_Recent_Action()
        {
            string mostActionAdvisor = CustomEntityRepository.GetAdvisorNameWithMostRecentActionsByJobId(2, JobCreatedBy);
            Assert.AreEqual(mostActionAdvisor, User2);
        }

        [Test]
        public void Given_Job_With_No_Action_When_Advisor_Is_Requested_Returns_Advisors_Who_Created_The_Job()
        {
            string mostActionAdvisor = CustomEntityRepository.GetAdvisorNameWithMostRecentActionsByJobId(3, JobCreatedBy);
            Assert.AreEqual(mostActionAdvisor, "Alan.Donald");
        }
    }
}
