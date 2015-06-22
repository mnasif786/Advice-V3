using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Data.Repository;
using Advice.Data.Tests.TestHelpers;
using Advice.Domain;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Task = Advice.Domain.Entities.Task;

namespace Advice.Data.Tests.Repository.TaskTests
{
    [TestFixture]
    public class GetTaskWithDetailByTaskIdTests
    {
        private IAdviceDbContextManager _adviceDbContextManager;
        private Mock<AdviceEntities> _adviceEntities;
        private ITaskRepository _taskRepository;
        private long _taskId;
        private long _emailTaskId;
        private long _hroTaskId;
        private long _businesswiseTaskId;

        [SetUp]
        public void TestSetup()
        {
            //Setup Task data
            var taskList = CreateNewTaskWithDetailList();

            var dbSetMockTask = DbSetInitialisedMockFactory<Task>.CreateDbSetInitalisedMock(taskList);
            dbSetMockTask.Setup(m => m.Include("TaskType")).Returns(dbSetMockTask.Object);
            dbSetMockTask.Setup(m => m.Include("TaskDocuments")).Returns(dbSetMockTask.Object);
            dbSetMockTask.Setup(m => m.Include("TaskJobs")).Returns(dbSetMockTask.Object);
            
            _adviceEntities = new Mock<AdviceEntities>();
            _adviceEntities.Setup(x => x.Tasks).Returns(dbSetMockTask.Object);
            
            _adviceDbContextManager = new AdviceDbContextManager(_adviceEntities.Object);

            Mock<ITaskStoredProcRunner> mockStoredProcRunner = new Mock<ITaskStoredProcRunner>();
            _taskRepository = new TaskRepository(_adviceDbContextManager, mockStoredProcRunner.Object);
        }

        [Test]
        public void given_is_an_email_task_when_task_with_detail_requested_then_email_Task_is_returned()
        {

            var task = _taskRepository.GetTaskWithDetailByTaskId(_emailTaskId);

            Assert.IsTrue(task is EmailTask);
        }

        [Test]
        public void given_task_has_documents_when_task_with_detail_requested_then_Task_Documents_are_returned()
        {

            var task = _taskRepository.GetTaskWithDetailByTaskId(_taskId);

            Assert.NotNull(task.TaskDocuments);
            Assert.That(task.TaskDocuments.Count, Is.GreaterThan(0));
        }

        [Test]
        public void given_task_with_job_exists_when_task_with_detail_requested_then_Task_Jobs_are_returned()
        {

            var task = _taskRepository.GetTaskWithDetailByTaskId(_taskId);

            Assert.NotNull(task.TaskJobs);
            Assert.That(task.TaskJobs.Count, Is.GreaterThan(0));
        }

        [Test]
        public void given_task_is_an_hro_Task_when_task_with_detail_requested_then_hro_Task_is_returned()
        {

            var task = _taskRepository.GetTaskWithDetailByTaskId(_hroTaskId);

            Assert.IsTrue(task is HroTask);
        }

        [Test]
        public void given_hro_Task_has_nature_of_Advice_Group_when_task_with_detail_requested_then_hro_Task_with_Nature_of_Advice_Group_is_returned()
        {

            var task = _taskRepository.GetTaskWithDetailByTaskId(_hroTaskId);

            Assert.IsTrue(task is HroTask);
            Assert.NotNull(((HroTask)task).HroNatureOfAdviceGroup);
        }

        [Test]
        public void given_task_is_an_busineswise_Task_when_task_with_detail_requested_then_busineswise_Task_is_returned()
        {

            var task = _taskRepository.GetTaskWithDetailByTaskId(_businesswiseTaskId);

            Assert.IsTrue(task is BusinessWiseTask);
        }
        
        private List<Task> CreateNewTaskWithDetailList()
        {
            _taskId = 1;

            var task = new Task {
                TaskID = _taskId,
                EmailAddress = "na@peninsula-uk.com",
                PhoneNumber = "016199009977",
                Deleted = false,
                Completed = false
            };

            task.TaskType = new TaskType {CreatedBy = "Steve.Waugh", TaskTypeID = 2, Tasks = new List<Task> {task}};
            task.TaskDocuments.Add(new TaskDocument { TaskID = _taskId, CreatedBy = "Na", Task = task });
            task.TaskJobs.Add(new TaskJob { TaskID = _taskId, Task = task, CreatedBy = "Na" });

            _hroTaskId = 2;

            var hroTask = new HroTask
            {
                TaskID = _hroTaskId,
                EmailAddress = "na@peninsula-uk.com",
                PhoneNumber = "016199009977",
                Deleted = false,
                Completed = false,
                HroNatureOfAdviceGroup = new BusinessWiseNatureOfAdviceGroup { Description = "Nature Of Advice group" }
            };

            hroTask.TaskType = new TaskType { CreatedBy = "Steve.Waugh", TaskTypeID = 20, Tasks = new List<Task> { hroTask } };

            _businesswiseTaskId = 3;

            var businessWisetask = new BusinessWiseTask
            {
                TaskID = _businesswiseTaskId,
                EmailAddress = "na@peninsula-uk.com",
                PhoneNumber = "016199009977",
                Deleted = false,
                Completed = false
            };

            businessWisetask.TaskType = new TaskType { CreatedBy = "Steve.Waugh", TaskTypeID = 1, Tasks = new List<Task> { businessWisetask } };

            _emailTaskId = 4;

            var emailtask = new EmailTask
            {
                TaskID = _emailTaskId,
                EmailAddress = "na@peninsula-uk.com",
                PhoneNumber = "016199009977",
                Deleted = false,
                Completed = false
            };

            emailtask.TaskType = new TaskType { CreatedBy = "Steve.Waugh", TaskTypeID = 7, Tasks = new List<Task> { emailtask } };

            var taskList = new List<Task> { task, hroTask, businessWisetask, emailtask };

            return taskList;
        }
    }
}
