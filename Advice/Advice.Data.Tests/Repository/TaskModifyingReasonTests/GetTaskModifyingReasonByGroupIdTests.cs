using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Tests.Common;
using Advice.Domain.Entities.Enums;
using NUnit.Framework;

namespace Advice.Data.Tests.Repository.TaskModifyingReasonTests
{
    [TestFixture]
    public class GetTaskModifyingReasonByGroupIdTests : BaseTaskModifyingReasonRepositoryTests
    {
        [Test]
        public void Given_TaskModifyingReasons_Get_TaskModifyingReason_By_ResetGroupId_Then_ResetGroup_Reasons_Are_Returned()
        {
            var taskModifyingReasons = TaskModifyingReasonRepository.GetTaskModifyingReasonsByGroupId(TaskModifyingReasonGroups.Reset);
            Assert.That(taskModifyingReasons.Count(), Is.EqualTo(2));
        }
    }
}
