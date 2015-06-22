using System;
using System.Linq;
using Advice.Common.Models;
using Advice.Domain.Entities;
using Application.Tests.Common;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Implementations.TaskServiceTests
{
    [TestFixture]
    public class ReassignTaskTest : BaseTaskServiceTests
    {
        private Task _task;

        [SetUp]
        public new void SetUp()
        {
            _task = GetTask();

            TaskRepositoryMock.Setup(t => t.GetById(It.IsAny<long>()))
                .Returns(_task);

        }

        [Test]
        public void Given_Task_is_Reassigned_to_a_team_Then_new_TeamId_is_assigned()
        {
            var taskService = GetTarget();

            var reassignTaskModel = GetReassignTaskModel(4, null);
            taskService.ReassignTask(reassignTaskModel);

            Assert.That(_task.AssignedTeamID, Is.EqualTo(reassignTaskModel.AssignedTeamId));
            Assert.IsNull(_task.AssignedUser);
        }


        [Test]
        public void Given_Task_is_Reassigned_to_a_user_Then_new_user_is_assigned()
        {
            var taskService = GetTarget();

            var reassignTaskModel = GetReassignTaskModel(null, "nauman.asif");
            taskService.ReassignTask(reassignTaskModel);

            Assert.That(_task.AssignedUser, Is.EqualTo(reassignTaskModel.AssignedUser));
            Assert.IsNull(_task.AssignedTeamID);
        }

        [Test]
        public void Given_Task_is_Reassigned_to_a_user_Then_task_is_set_as_unread()
        {
            var taskService = GetTarget();

            var reassignTaskModel = GetReassignTaskModel(null, "nauman.asif");
            taskService.ReassignTask(reassignTaskModel);
            
            Assert.IsFalse(_task.IsRead.Value);
        }

        [Test]
        public void Given_Task_is_Reassigned_to_a_user_if_Due_Date_provided_Then_Manual_Date_is_set_as_true()
        {
            var taskService = GetTarget();

            var reassignTaskModel = GetReassignTaskModel(null, "nauman.asif");
            taskService.ReassignTask(reassignTaskModel);

            Assert.IsTrue(_task.ManualDueDate);
        }
        

        [Test]
        public void Given_Task_is_Reassigned_to_a_user_Then_Last_Modifying_Reason_Id_is_set_to_new_value()
        {
            var taskService = GetTarget();

            var reassignTaskModel = GetReassignTaskModel(null, "nauman.asif");
            taskService.ReassignTask(reassignTaskModel);

            Assert.That(_task.LastModifyingReasonID, Is.EqualTo(reassignTaskModel.ReasonId));
        }

        [Test]
        public void Given_Task_is_Reassigned_to_a_user_Then_Task_Is_Added_To_Task_Reassign_Events()
        {
            var taskService = GetTarget();

            var reassignTaskModel = GetReassignTaskModel(null, "nauman.asif");
            taskService.ReassignTask(reassignTaskModel);

            Assert.That(_task.ReassignTaskEvents.Count, Is.GreaterThan(0));
            Assert.That(_task.ReassignTaskEvents.ToList()[0].TaskId, Is.EqualTo(_task.TaskID));
            Assert.That(_task.ReassignTaskEvents.ToList()[0].NewTeamId, Is.EqualTo(reassignTaskModel.AssignedTeamId));
            Assert.That(_task.ReassignTaskEvents.ToList()[0].NewUser, Is.EqualTo(reassignTaskModel.AssignedUser));
            Assert.That(_task.ReassignTaskEvents.ToList()[0].Comment, Is.EqualTo(reassignTaskModel.Comments));
            Assert.That(_task.ReassignTaskEvents.ToList()[0].ReasonId, Is.EqualTo(reassignTaskModel.ReasonId));
        }

        private Task GetTask()
        {
            return new Task
            {
                TaskID = 1,
                AssignedTeamID = null,
                AssignedUser = "scott.gil",
                TaskModifyingReason = new TaskModifyingReason { TaskModifyingReasonID = 3 },
                LastModifyingReasonID = 3,
                LastModifiedBy = "na.asif",
                LastModifiedDate = DateTime.Now.AddDays(-1),
                LastModifiedComment = "na c",
                IsRead = true,
                ManualDueDate = false
            };
        }

        private ReassignTaskModel GetReassignTaskModel(long? assignedTeamId, string assignedUser)
        {
            return new ReassignTaskModel
            {
                TaskId = _task.TaskID,
                AssignedTeamId = assignedTeamId,
                AssignedUser = assignedUser,
                ClientId = null,
                Comments = "new comments",
                DueDate = DateTime.Now,
                ReasonId = 4,
                ReAssignedByUser = "na.asif",
                Urgent = true
            };
        }

    }
}
