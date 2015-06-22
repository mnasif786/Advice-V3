using System;
using System.Collections.Generic;
using System.Linq;
using Advice.Common.Models;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Application.Tests.Common;
using NUnit.Framework;

namespace Application.Tests.Implementations.TaskModifyingReasonTests
{
    [TestFixture]
    public class GetTaskModifyingReasonsForResetGroupTests : BaseTaskModifyingReasonServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            var taskModifyingReasons = GeTaskModifyingReasons();
            TaskModifyingReasonRepositorMock.Setup(
                x => x.GetTaskModifyingReasonsByGroupId(TaskModifyingReasonGroups.Reset)).Returns(taskModifyingReasons);

        }

        [Test]
        public void Given_TaskModifyingReasons_When_Get_By_Reset_Group_Returns_List_Of_TaskModifyingReasonModel_For_ResetGroup()
        {
            var taskModifyingReasonSerivce = GetTarget();
            var resetGroupModifyingReasons = taskModifyingReasonSerivce.GetTaskModifyingReasonsForResetGroup();
            Assert.That(resetGroupModifyingReasons.Count(), Is.EqualTo(2));
        }

        private IEnumerable<TaskModifyingReason> GeTaskModifyingReasons()
        {
            return new List<TaskModifyingReason>()
            {
                new TaskModifyingReason()
                {
                    TaskModifyingReasonID = 1,
                    TaskModifyingReasonGroupID = 4,
                    Description = "Client request"

                },

                new TaskModifyingReason()
                {
                    TaskModifyingReasonID = 2,
                    TaskModifyingReasonGroupID = 4,
                    Description = "Management decision"
                }
                
            };
        }
    }
}
