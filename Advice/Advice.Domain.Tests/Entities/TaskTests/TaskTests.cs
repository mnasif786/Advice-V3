using System;
using System.Collections.ObjectModel;
using System.Linq;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Advice.Domain.Helper;
using NUnit.Framework;

namespace Advice.Domain.Tests.Entities.TaskTests
{
    [TestFixture]
    public class TaskTests
    {
        private const string UserName = "Tom.McMillan";

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
        }

        [Test]
        public void Given_Current_Date_Past_DueDate_And_Task_Not_Completed_GetStatus_Returns_Red()
        {
            var task = new Task()
            {
                TaskID = 1,
                CreatedDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(-1),
                CompletionDate = null
            };
            
            Assert.That(task.GetTaskStatus(), Is.EqualTo(DerivedTaskStatusForDisplay.Red));
        }
        
        [Test]
        public void Given_Task_DueDate_Past_Created_Date_And_Task_Completed_Day_Later_Than_DueDate_GetStatus_Returns_Red()
        {
            var task = new Task()
            {
                TaskID = 1,
                CreatedDate = DateTime.Now.AddDays(-4),
                DueDate = DateTime.Now.AddDays(-2),
                CompletionDate = DateTime.Now.AddDays(-1),
            };

            Assert.That(task.GetTaskStatus(), Is.EqualTo(DerivedTaskStatusForDisplay.Red));
        }
        
        [Test]
        public void Given_Task_DueDate_Not_Past_And_Current_Time_Within_Warning_Time_Return_Amber()
        {
            var task = new Task()
            {
                TaskID = 1,
                CreatedDate = DateTime.Now.AddHours(-1),
                WarningWindow = 61,
                DueDate = DateTime.Now.AddHours(1)
            };

            Assert.That(task.GetTaskStatus(), Is.EqualTo(DerivedTaskStatusForDisplay.Amber));
        }
        
        [Test]
        public void Given_CurrentTime_Within_Acceptable_Window_Time_Return_Platinum()
        {
            var task = new Task()
            {
                TaskID = 1,
                AcceptableWindow = 60,
                DueDate = DateTime.Now.AddMinutes(70)
            };

            Assert.That(task.GetTaskStatus(), Is.EqualTo(DerivedTaskStatusForDisplay.Platinum));
        }

        [Test]
        public void Given_CurrentTime_After_Acceptable_Window_Time_And_Before_WarningWindow_Time_Return_Green()
        {
            var task = new Task()
            {
                TaskID = 1,
                AcceptableWindow = 160,
                WarningWindow = 60,
                DueDate = DateTime.Now.AddHours(2)
            };

            Assert.That(task.GetTaskStatus(), Is.EqualTo(DerivedTaskStatusForDisplay.Green));
        }

   

        [Test]
        public void Given_CurrentTime_Before_Acceptable_Window_Time_Where_DueDate_Is_Tomorrow_Return_Platinum()
        {
            DateTime thisTimeTomorrow = DateTime.Now.AddDays(1);

            var task = new Task()
            {
                TaskID = 1,
                AcceptableWindow = (long)((23 * 60) - WorkingHours.TimeClosedOvernight.TotalMinutes),
                WarningWindow = (long)((22 * 60) - WorkingHours.TimeClosedOvernight.TotalMinutes),
                DueDate = thisTimeTomorrow
            };

            Assert.That( task.GetTaskStatus(), Is.EqualTo( DerivedTaskStatusForDisplay.Platinum ) );
        }



        [Test]
        public void Given_CurrentTime_After_Acceptable_Window_Time_And_Before_WarningWindow_Time_Where_DueDate_Is_Tomorrow_Return_Green()
        {
            DateTime thisTimeTomorrow = DateTime.Now.AddDays(1);            
            
            var task = new Task()
            {
                TaskID              = 1,
                AcceptableWindow    = (long)((24.5 * 60) - WorkingHours.TimeClosedOvernight.TotalMinutes),
                WarningWindow       = (long)((23.5 * 60) - WorkingHours.TimeClosedOvernight.TotalMinutes),
                DueDate             = thisTimeTomorrow
            };

            Assert.That(task.GetTaskStatus(), Is.EqualTo(DerivedTaskStatusForDisplay.Green));
        }


        [Test]
        public void Given_CurrentTime_After_Warning_Window_Time_And_Before_DueDate_Where_DueDate_Is_Tomorrow_Return_Amber()
        {
            DateTime thisTimeTomorrow = DateTime.Now.AddDays(1);

            var task = new Task()
            {
                TaskID = 1,
                AcceptableWindow = (long)((25 * 60) - WorkingHours.TimeClosedOvernight.TotalMinutes),
                WarningWindow = (long)((24.5 * 60) - WorkingHours.TimeClosedOvernight.TotalMinutes),
                DueDate = thisTimeTomorrow
            };

            Assert.That(task.GetTaskStatus(), Is.EqualTo(DerivedTaskStatusForDisplay.Amber));
        }

        [Test]
        public void Given_Task_Is_An_Email_Task_Then_Email_Task_Is_Set()
        {
            const int taskId = 1;
            var task = new EmailTask()
            {
                TaskID = taskId,
                Subject = "My Test Task", 
                CrossPosted = true 
            };

            Assert.IsTrue(task is EmailTask);
            Assert.That(task.TaskID, Is.EqualTo(taskId));
            Assert.IsTrue(task.CrossPosted);
        }

        [Test]
        public void Given_Task_has_Task_Type_Then_Task_Type_is_Set()
        {
            const int taskId = 1;
            var task = new Task()
            {
                TaskID = taskId,
                TaskType = new TaskType() { CreatedBy = "NA", ExternalTask = false, TaskTypeSLAs = new Collection<TaskTypeSLA>()}
            };

            task.TaskType.TaskTypeSLAs.Add(new TaskTypeSLA { DepartmentID = 2});

            Assert.IsNotNull(task.TaskType);
            Assert.IsFalse(task.TaskType.ExternalTask.Value);
            Assert.That(task.TaskType.TaskTypeSLAs.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Given_Task_is_deleted_then_delete_flag_is_set()
        {
            const int taskId = 1;
            var task = new Task()
            {
                TaskID = taskId,
                TaskType = new TaskType() { CreatedBy = "NA", ExternalTask = false, TaskTypeSLAs = new Collection<TaskTypeSLA>() }
            };
            
            task.MarkAsDeleted(UserName);

            Assert.IsTrue(task.Deleted);
            Assert.That(task.DeletedBy, Is.EqualTo(UserName));
            
        }

        [Test]
        public void Given_Task_is_reassigned_then_task_is_updated_with_coreect_values()
        {
            var task = new Task
            {
                TaskID = 1,
                AssignedTeamID = null,
                AssignedUser = "scott.gil",
                TaskModifyingReason = new TaskModifyingReason { TaskModifyingReasonID = 3 },
                LastModifyingReasonID = 3,
                LastModifiedBy = "na.asif",
                LastModifiedDate = DateTime.Now.AddDays(-1),
                LastModifiedComment = "na c",

            };

            var newTeamId = 3;
            var newComments = "xyz";
            var reassignedByUser = "na.na";
            var newLastModifiedDate = DateTime.Now;
            var newLastModifiyingReasonId = 4;

            var previousTeamId = task.AssignedTeamID;
            var previousUser = task.AssignedUser;


            task.Reassign(null, newTeamId, false, newComments, reassignedByUser, null, newLastModifiedDate, newLastModifiyingReasonId, previousTeamId, previousUser);

            Assert.That(task.TaskID, Is.EqualTo(task.TaskID));
            Assert.That(task.AssignedTeamID, Is.EqualTo(newTeamId));
            Assert.That(task.AssignedUser, Is.EqualTo(null));
            Assert.That(task.LastModifiedBy, Is.EqualTo(reassignedByUser));
            Assert.That(task.LastModifiedDate.Date, Is.EqualTo(newLastModifiedDate.Date));
            Assert.That(task.LastModifiedComment, Is.EqualTo(newComments));
            Assert.That(task.LastModifyingReasonID, Is.EqualTo(newLastModifiyingReasonId));
            Assert.IsFalse(task.IsRead.Value);

        }

        [Test]
        public void Given_Task_is_reassigned_then_task_is_added_to_reassign_task_events()
        {
            var task = new Task
            {
                TaskID = 1,
                AssignedTeamID = null,
                AssignedUser = "scott.gil",
                TaskModifyingReason = new TaskModifyingReason { TaskModifyingReasonID = 3 },
                LastModifyingReasonID = 3,
                LastModifiedBy = "na.asif",
                LastModifiedDate = DateTime.Now.AddDays(-1),
                LastModifiedComment = "na c",

            };

            var newTeamId = 3;
            var newComments = "xyz";
            var reassignedByUser = "na.na";
            var newLastModifiedDate = DateTime.Now;
            var newLastModifiyingReasonId = 4;

            var previousTeamId = task.AssignedTeamID;
            var previousUser = task.AssignedUser;


            task.Reassign(null, newTeamId, false, newComments, reassignedByUser, null, newLastModifiedDate, newLastModifiyingReasonId, previousTeamId, previousUser);

            var reassignTaskEvent = task.ReassignTaskEvents.First();

           Assert.IsTrue(task.ReassignTaskEvents.Count > 0);
           Assert.That(reassignTaskEvent.TaskId, Is.EqualTo(task.TaskID));
           Assert.That(reassignTaskEvent.NewTeamId, Is.EqualTo(newTeamId));
           Assert.That(reassignTaskEvent.NewUser, Is.EqualTo(task.AssignedUser));
           Assert.That(reassignTaskEvent.PreviousTeamId, Is.EqualTo(previousTeamId));
           Assert.That(reassignTaskEvent.PreviousUser, Is.EqualTo(previousUser));
           Assert.That(reassignTaskEvent.ReassignedBy, Is.EqualTo(task.LastModifiedBy));
           Assert.That(reassignTaskEvent.Comment, Is.EqualTo(task.LastModifiedComment));
           Assert.That(reassignTaskEvent.ReasonId, Is.EqualTo(task.LastModifyingReasonID));

        }
    }
}
