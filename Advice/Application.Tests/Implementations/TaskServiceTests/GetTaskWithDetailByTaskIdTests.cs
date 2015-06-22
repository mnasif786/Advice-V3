using System;
using System.Collections.Generic;
using System.Linq;
using Advice.Application.Implementations;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;
using Advice.ExchangeEmails;
using Application.Tests.Common;
using EmailServer.Responses;
using Moq;
using NUnit.Framework;
using Peninsula.Domain.Entities;
using Peninsula.Domain.RepositoryContracts;
using ExchangeEmailService = Exchange.ExchangeEmailService;

namespace Application.Tests.Implementations.TaskServiceTests
{
    [TestFixture]
    public class GetTaskWithDetailByTaskIdTests : BaseTaskServiceTests
    {
        private Task _task;
        private long _taskId;
        private const string UserName = "George.Bailey";

        [SetUp]
        public void TestFixtureSetup()
        {
            _taskId = 1;

            _task = new Task
            {
                    TaskID = _taskId,
                    AssignedUser = "User1",
                    JobSubject = "Task1",
                    RecordedClientID = 11004,
                    AssignedTeamID = 1,
                    ContactName = "Fred Flintstone"
            };

            _task.TaskType = new TaskType { CreatedBy = "Steve.Waugh", TaskTypeID = 7, Tasks = new List<Task> { _task } };
            _task.TaskDocuments.Add(new TaskDocument { TaskID = _taskId, CreatedBy = "Na", Task = _task });
            _task.TaskJobs.Add(new TaskJob { TaskID = _taskId, Task = _task, CreatedBy = "Na", JobID = 1, Job = new Job { JobID = 1, Closed = false, Subject = "Abc", ContactID = 1} });
            //_task.HroTask = new HroTask { GroupName = "Abc" };
            //_task.BusinessWiseTask = new BusinessWiseTask { GroupName = "Abc" };

            TaskRepositoryMock.Setup(x => x.GetTaskWithDetailByTaskId(_taskId)).Returns(_task);
        }

        [Test]
        public void Given_Task_Requested_With_Detail_Then_Task_Type_Is_Returned()
        {
            TaskRepositoryMock.Setup(x => x.GetTaskWithDetailByTaskId(_taskId)).Returns(_task);

            var taskService = GetTarget();

            var task = taskService.GetTaskWithDetailsByTaskId(_taskId, UserName);

            Assert.That(task, Is.Not.Null);
            Assert.That(task.TaskType, Is.Not.Null);
        }

        [Test]
        public void Given_Task_Requested_With_Detail_Then_Hro_Task_Is_Returned()
        {
            const int taskId = 2;
            var hroTask = new HroTask { TaskID = taskId, TaskType = new TaskType { TaskTypeID = 20 }};

            TaskRepositoryMock.Setup(x => x.GetTaskWithDetailByTaskId(taskId)).Returns(hroTask);
            var taskService = GetTarget();

            var task = taskService.GetTaskWithDetailsByTaskId(taskId, UserName);

            Assert.That(task, Is.Not.Null);
            Assert.That(task.HroTask, Is.Not.Null);
        }

        [Test]
        public void Given_Task_Requested_With_Detail_Then_BusinessWise_Task_Is_Returned()
        {
            const int taskId = 3;
            var businesswiseTask = new BusinessWiseTask { TaskID = taskId, TaskType = new TaskType { TaskTypeID = 1 } };

            TaskRepositoryMock.Setup(x => x.GetTaskWithDetailByTaskId(taskId)).Returns(businesswiseTask);

            var taskService = GetTarget();

            var task = taskService.GetTaskWithDetailsByTaskId(taskId, UserName);

            Assert.That(task, Is.Not.Null);
            Assert.That(task.BusinessWiseTask, Is.Not.Null);
        }

        [Test]
        public void Given_Task_Requested_With_Detail_Then_Email_Task_Is_Returned()
        {
            const int taskId = 4;
            var emailTask = new EmailTask { TaskID = taskId, TaskType = new TaskType { TaskTypeID = 7 } };

            TaskRepositoryMock.Setup(x => x.GetTaskWithDetailByTaskId(taskId)).Returns(emailTask);

            var taskService = GetTarget();

            var task = taskService.GetTaskWithDetailsByTaskId(taskId, UserName);

            Assert.That(task, Is.Not.Null);
            Assert.That(task.EmailTask, Is.Not.Null);
        }

        [Test]
        public void Given_Task_Requested_With_Detail_Then_Task_Documents_Are_Returned()
        {
            TaskRepositoryMock.Setup(x => x.GetTaskWithDetailByTaskId(_taskId)).Returns(_task);

            var taskService = GetTarget();

            var task = taskService.GetTaskWithDetailsByTaskId(_taskId, UserName);

            Assert.That(task, Is.Not.Null);
            Assert.That(task.TaskDocuments.Count(), Is.GreaterThan(0));
        }

        [Test]
        public void Given_non_proactive_Task_Requested_With_Detail_Then_Task_Jobs_without_job_detail_are_Returned()
        {
            TaskRepositoryMock.Setup(x => x.GetTaskWithDetailByTaskId(_taskId)).Returns(_task);

            var taskService = GetTarget();

            var task = taskService.GetTaskWithDetailsByTaskId(_taskId, UserName);

            var taskJobs = task.TaskJobs.ToList();
            Assert.That(taskJobs, Is.Not.Null);
            Assert.That(taskJobs.Count(), Is.GreaterThan(0));
            Assert.That(taskJobs[0].Job, Is.Null);
        }


        [Test]
        public void Given_proactive_Task_with_open_job_requested_then_job_with_details_is_Returned()
        {

            var proactiveTask = new Task {TaskID = 1, TaskTypeID = 25, TaskType = new TaskType { TaskTypeID = 25 }};
            proactiveTask.TaskJobs.Add(new TaskJob { TaskID = _taskId, Task = _task, CreatedBy = "Na", JobID = 1, Job = new Job { JobID = 1, Closed = false, Subject = "Abc", ContactID = 1 } });

            TaskRepositoryMock.Setup(x => x.GetTaskWithDetailByTaskId(proactiveTask.TaskID)).Returns(proactiveTask);

            var contact = new TBLCompanyContact() { CompanyContactID = 1, EMail = "na@na.com" };
            TblCustomerRepositoryMock.Setup(x => x.GetCompanyContactWithContactId(1)).Returns(contact);


            var taskService = GetTarget();

            var task = taskService.GetTaskWithDetailsByTaskId(proactiveTask.TaskID, UserName);

            var taskJobs = task.TaskJobs.ToList();
            Assert.That(taskJobs, Is.Not.Null);
            Assert.That(taskJobs.Count(), Is.GreaterThan(0));
            Assert.That(taskJobs[0].Job, !Is.Null);
            Assert.That(taskJobs[0].Job.JobId, Is.EqualTo(1));
            Assert.That(taskJobs[0].Job.Subject, Is.EqualTo("Abc"));
            Assert.That(taskJobs[0].Job.ContactId, Is.EqualTo(contact.CompanyContactID));

            //Contact Information
            Assert.IsNotNull(taskJobs[0].Job.Contact);
            Assert.That(taskJobs[0].Job.Contact.CompanyContactId, Is.EqualTo(contact.CompanyContactID));
            Assert.That(taskJobs[0].Job.Contact.EmailAddress, Is.EqualTo(contact.EMail));
        }

        [Test]
        public void Given_proactive_Task_with_Closed_job_requested_then_no_job_is_Returned()
        {

            var proactiveTask = new Task { TaskID = 1, TaskTypeID = 25, TaskType = new TaskType { TaskTypeID = 25 } };
            proactiveTask.TaskJobs.Add(new TaskJob { TaskID = _taskId, Task = _task, CreatedBy = "Na", JobID = 1, Job = new Job { JobID = 1, Closed = true, Subject = "Abc", ContactID = 1 } });

            TaskRepositoryMock.Setup(x => x.GetTaskWithDetailByTaskId(proactiveTask.TaskID)).Returns(proactiveTask);

            var taskService = GetTarget();

            var task = taskService.GetTaskWithDetailsByTaskId(proactiveTask.TaskID, UserName);

            var taskJobs = task.TaskJobs.ToList();
            
            Assert.That(taskJobs[0].Job, Is.Null);
            
        }

        [Test]
        public void Given_Task_Requested_With_Detail_And_task_Is_an_Email_task_Then_Recipients_Are_Returned()
        {
            const int taskId = 4;

            var outlookMessageResponse = new GetArchivedMessageResponse { Subject = "Abc", Body = "<p>this is a body</p>", ToRecipients = new List<string>{"na@na.com","abc@abc.com"}};

            var emailTask = new EmailTask { TaskID = taskId, MessageId = 1, TaskType = new TaskType { TaskTypeID = 7 } };

            ExchangeEmailServiceMock.Setup(x => x.GetOutlookEmailMailMessage(It.IsAny<long>()))
                .Returns(outlookMessageResponse);

            TaskRepositoryMock.Setup(x => x.GetTaskWithDetailByTaskId(taskId)).Returns(emailTask);


            var taskService = GetTarget();

            var task = taskService.GetTaskWithDetailsByTaskId(taskId, UserName);
            
            Assert.That(task.EmailTask, Is.Not.Null);
            Assert.That(task.EmailTask.OutlookEmailMessage, Is.Not.Null);
            Assert.That(task.EmailTask.OutlookEmailMessage.Recipients.Count, Is.GreaterThan(0));
            Assert.That(task.EmailTask.OutlookEmailMessage.Recipients[0], Is.EqualTo(outlookMessageResponse.ToRecipients[0]));
        }
		
        [Test]
        public void Given_Task_Requested_With_Detail_Then_Contact_Name_is_Returned()
        {
            var taskService = GetTarget();

            var task = taskService.GetTaskWithDetailsByTaskId(_taskId, UserName);

            Assert.That(task, Is.Not.Null);
            Assert.AreEqual(task.ContactName, "Fred Flintstone");
        }
        
    }
}
