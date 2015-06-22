using System;
using System.Collections.ObjectModel;
using Advice.Domain.Entities;
using NUnit.Framework;

namespace Advice.Domain.Tests.Entities.TaskTests
{
    [TestFixture]
    public class ReassignTaskEventTests
    {
        private Task _task; 
        private long? _previousTeamId = 2;
        private string _previousUser = "tom.mc";

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _task = new Task()
            {
                TaskID = 1,
                AssignedTeamID = 1,
                AssignedUser=  null,
                TaskModifyingReason = new TaskModifyingReason {TaskModifyingReasonID = 3},
                LastModifyingReasonID = 3,
                LastModifiedBy = "na.asif",
                LastModifiedDate = DateTime.Now,
                LastModifiedComment = "na c",

            };

        }

        [Test]
        public void Given_When_Create_called_Then_Correct_ReassignTaskEvent_Returned()
        {

            var reassignTaskEvent = ReassignTaskEvent.Create(_task, _previousTeamId, _previousUser);

            Assert.That(reassignTaskEvent.TaskId, Is.EqualTo(_task.TaskID));
            Assert.That(reassignTaskEvent.NewTeamId, Is.EqualTo(_task.AssignedTeamID));
            Assert.That(reassignTaskEvent.PreviousTeamId, Is.EqualTo(_previousTeamId));
            Assert.That(reassignTaskEvent.NewUser, Is.EqualTo(_task.AssignedUser));
            Assert.That(reassignTaskEvent.PreviousUser, Is.EqualTo(_previousUser));
            Assert.That(reassignTaskEvent.ReasonId, Is.EqualTo(_task.LastModifyingReasonID));
            Assert.That(reassignTaskEvent.Comment, Is.EqualTo(_task.LastModifiedComment));
            Assert.That(reassignTaskEvent.ReassignedBy, Is.EqualTo(_task.LastModifiedBy));
            Assert.That(reassignTaskEvent.ReassignedDate.Value.Date, Is.EqualTo(_task.LastModifiedDate.Date));
        }
    }
}
