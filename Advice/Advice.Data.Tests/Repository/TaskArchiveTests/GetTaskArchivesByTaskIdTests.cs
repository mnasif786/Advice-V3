using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Tests.Common;
using NUnit.Framework;

namespace Advice.Data.Tests.Repository.TaskArchiveTests
{
    [TestFixture]
    public class GetTaskArchivesByTaskIdTests : BaseTaskArchiveRepositoryTests
    {
        [Test]
        public void Given_TaskArchives_Get_TaskArchives_By_TaskId_Then_Correct_Tasks_Are_Returned()
        {
            var taskArchives = TaskArchiveRepository.GetTaskArchivesByTaskId(12);
            Assert.That(taskArchives.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Given_TaskArchives_Get_TaskArchives_By_TaskId_Then_Correct_Team_Is_Returned()
        {
            var taskArchives = TaskArchiveRepository.GetTaskArchivesByTaskId(11);
            var team = taskArchives.First().Team.Description;

            Assert.AreEqual(team, "Team1");
        }

        [Test]
        public void Given_TaskArchives_Get_TaskArchives_By_TaskId_Then_Correct_AssignedUser_Is_Returned()
        {
            var taskArchives = TaskArchiveRepository.GetTaskArchivesByTaskId(12);
            var assignedUser = taskArchives.First().AssignedUser;

            Assert.AreEqual(assignedUser, UserName);
        }

        [Test]
        public void Given_TaskArchives_Get_TaskArchives_By_TaskId_Then_Correct_ModifyingReason_Is_Returned()
        {
            var taskArchives = TaskArchiveRepository.GetTaskArchivesByTaskId(11);
            var taskModifyingReason = taskArchives.First().TaskModifyingReason.Description;

            Assert.AreEqual(taskModifyingReason, "Delete");
        }

        [Test]
        public void Given_TaskArchives_Get_TaskArchives_By_TaskId_Then_Correct_ModifyingReasonGroup_Is_Returned()
        {
            var taskArchives = TaskArchiveRepository.GetTaskArchivesByTaskId(12);
            var taskModifyingReasonGroup = taskArchives.Last().TaskModifyingReason.TaskModifyingReasonGroup.Description;

            Assert.AreEqual(taskModifyingReasonGroup, "Reset SLA");
        }
    }
}
