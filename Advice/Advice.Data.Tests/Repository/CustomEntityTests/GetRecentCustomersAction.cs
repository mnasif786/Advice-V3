using System;
using System.Collections.Generic;
using System.Linq;
using Advice.Data.Tests.Common;
using Advice.Data.Tests.TestHelpers;
using Advice.Domain.Entities;
using NUnit.Framework;
using Action = Advice.Domain.Entities.Action;

namespace Advice.Data.Tests.Repository.CustomEntityTests
{
    [TestFixture]
    public class GetRecentCustomersAction : BaseCustomEntityRepositoryTests
    {
        private List<Action> _actions;
        private List<Job> _jobs;
        private List<ActionTypeGroup> _actionTypeGroups;
        private const string UserName = "Mike.Bailey";

        [SetUp]
        public void SetUp()
        {
            _jobs = new List<Job>()
            {
                new Job()
                {
                    JobID = 1,
                    ClientID = 13472
                },

                new Job()
                {
                    JobID = 2,
                    ClientID = 13472
                },

                new Job()
                {
                    JobID = 3,
                    ClientID = 9634
                },

                new Job()
                {
                    JobID = 4,
                    ClientID = 9634
                }
            };

            _actions = new List<Action>()
            {
                new Action()
                {
                    ActionID = 1,
                    ActionTypeID = 41,
                    JobID = 1,
                    CreatedDate = DateTime.Now.AddHours(-12),
                    CreatedBy = UserName
                },

                new Action()
                {
                    ActionID = 2,
                    ActionTypeID = 42,
                    JobID = 2,
                    CreatedDate = DateTime.Now.AddHours(-12),
                    CreatedBy = UserName
                },

                new Action()
                {
                    ActionID = 3,
                    ActionTypeID = 43,
                    JobID = 3,
                    CreatedDate = DateTime.Now.AddHours(-12),
                    CreatedBy = UserName
                },

                new Action()
                {
                    ActionID = 4,
                    ActionTypeID = 4,
                    JobID = 4,
                    CreatedDate = DateTime.Now.AddHours(-12),
                    CreatedBy = UserName
                }


            };

            _actionTypeGroups = new List<ActionTypeGroup>()
            {
                new ActionTypeGroup()
                {
                    ActionTypeGroupId = 1,
                    ActionTypeId = 41
                },

                new ActionTypeGroup()
                {
                    ActionTypeGroupId = 1,
                    ActionTypeId = 41
                },

                new ActionTypeGroup()
                {
                    ActionTypeGroupId = 1,
                    ActionTypeId = 41
                },

                new ActionTypeGroup()
                {
                    ActionTypeGroupId = 2,
                    ActionTypeId = 41
                },

                new ActionTypeGroup()
                {
                    ActionTypeGroupId = 2,
                    ActionTypeId = 41
                },

                new ActionTypeGroup()
                {
                    ActionTypeGroupId = 2,
                    ActionTypeId = 42
                },

                new ActionTypeGroup()
                {
                    ActionTypeGroupId = 1,
                    ActionTypeId = 42
                },

                new ActionTypeGroup()
                {
                    ActionTypeGroupId = 1,
                    ActionTypeId = 42
                },

                new ActionTypeGroup()
                {
                    ActionTypeGroupId = 1,
                    ActionTypeId = 42
                },

                new ActionTypeGroup()
                {
                    ActionTypeGroupId = 2,
                    ActionTypeId = 42
                },

                new ActionTypeGroup()
                {
                    ActionTypeGroupId = 2,
                    ActionTypeId = 42
                },

                new ActionTypeGroup()
                {
                    ActionTypeGroupId = 2,
                    ActionTypeId = 42
                },

                new ActionTypeGroup()
                {
                    ActionTypeGroupId = 1,
                    ActionTypeId = 43
                },

                new ActionTypeGroup()
                {
                    ActionTypeGroupId = 1,
                    ActionTypeId = 43
                },

                new ActionTypeGroup()
                {
                    ActionTypeGroupId = 1,
                    ActionTypeId = 43
                },

                new ActionTypeGroup()
                {
                    ActionTypeGroupId = 2,
                    ActionTypeId = 43
                },

                new ActionTypeGroup()
                {
                    ActionTypeGroupId = 2,
                    ActionTypeId = 43
                },

                new ActionTypeGroup()
                {
                    ActionTypeGroupId = 2,
                    ActionTypeId = 43
                }
            };

            var dbSetMockAction = DbSetInitialisedMockFactory<Action>.CreateDbSetInitalisedMock(_actions);
            AdviceEntities.Setup(x => x.Actions).Returns(dbSetMockAction.Object);
            AdviceEntities.Setup(x => x.Set<Action>()).Returns(dbSetMockAction.Object);

            var dbSetMockJob = DbSetInitialisedMockFactory<Job>.CreateDbSetInitalisedMock(_jobs);
            AdviceEntities.Setup(x => x.Jobs).Returns(dbSetMockJob.Object);
            AdviceEntities.Setup(x => x.Set<Job>()).Returns(dbSetMockJob.Object);

            var dbSetMockActionTypeGroup = DbSetInitialisedMockFactory<ActionTypeGroup>.CreateDbSetInitalisedMock(_actionTypeGroups);
            AdviceEntities.Setup(x => x.ActionTypeGroups).Returns(dbSetMockActionTypeGroup.Object);
            AdviceEntities.Setup(x => x.Set<ActionTypeGroup>()).Returns(dbSetMockActionTypeGroup.Object);
        }

        [Test]
        public void GetRecentCustomersActionTest()
        {
            //var customerEntity = new CustomEntityRepository(new AdviceDbContextManager(new AdviceEntities()));
            //The following call to function genereates exception the following exception. It will be revisited later.
            //This function can only be invoked from LINQ to Entities.
            
            
            //var actions = CustomEntityRepository.GetRecentCustomersAction(UserName);
           
        }
    }
}
