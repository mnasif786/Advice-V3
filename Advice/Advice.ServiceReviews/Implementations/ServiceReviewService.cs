using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Advice.Domain.Entities.Parameters;
using Advice.Domain.Helper;
using Advice.Domain.RepositoryContracts;
using Advice.Domain.RepositoryContracts.Services;
using Advice.ServiceReviews.Common;
using Advice.ServiceReviews.Contracts;
using Peninsula.Domain.RepositoryContracts;


namespace Advice.ServiceReviews.Implementations
{
    public class ServiceReviewService : IServiceReviewService
    {
        private readonly IAdviceDbContextManager _adviceDbContextManager;
        private readonly IServiceReviewServiceJobScheduleRepository _scheduleJobsRepository;
        private readonly ICorporatePriorityRepository _corporatePriorityRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITblCustomerRepository _tblCustomerRepository;

        public ServiceReviewService(
            IAdviceDbContextManager adviceDbContextManager,
            ITblCustomerRepository tblCustomerRepository, 
            ICorporatePriorityRepository corporatePriorityRepository, 
            ITaskRepository taskRepository, 
            IUserRepository userRepository, 
            IServiceReviewServiceJobScheduleRepository serviceReviewServiceJobScheduleRepository
            )
        {
            _adviceDbContextManager = adviceDbContextManager;
            _corporatePriorityRepository = corporatePriorityRepository;
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _scheduleJobsRepository = serviceReviewServiceJobScheduleRepository;
           _tblCustomerRepository = tblCustomerRepository;
            
        }


        public ServiceResponse ProcessScheduledJobs()
        {
            var scheduledJobs = _scheduleJobsRepository.GetScheduledJobs(DateTime.Now.Date.Day, DateTime.Now.Date.Month).ToList();
            return ProcessJobs(scheduledJobs);
        }

        public ServiceResponse ProcessSKippedJobs()
        {
            var skippedJobs = _scheduleJobsRepository.GetSkippedJobs(DateTime.Now).ToList();
            return ProcessJobs(skippedJobs);
        }

        private ServiceResponse ProcessJobs(IEnumerable<ServiceReviewServiceSchedule> jobs)
        {
            var jobsRun = 0;
            var recordsFound = 0;
            var recordsProcessed = 0;

            foreach (ServiceReviewServiceSchedule job in jobs)
            {
                var cansList =
                    _corporatePriorityRepository.GetCorporatePriorityByCans(job.ServiceReviewServiceCanGroup.Regex)
                        .ToList();

                if (cansList.Count > 0)
                {
                    var serviceReviewtasks = CreateServiceReviewTasks(cansList);
                    _taskRepository.InsertRange(serviceReviewtasks);
                    recordsProcessed += serviceReviewtasks.Count;
                }

                //saves records per job
                job.LastRun = DateTime.Now; //updates the last run date
                _scheduleJobsRepository.Update(job);
                _adviceDbContextManager.Save();

                recordsFound += cansList.Count;
                jobsRun++;
            }

            return ServiceResponse.Create(jobsRun, recordsFound, recordsProcessed);
        }

       private IList<Task> CreateServiceReviewTasks(IEnumerable<CorporatePriorityByCansQueryResult> queryResults)
       {
          //The departmentId could be the departmentId of the Assigned User. But we assumed that Lead consulatant would always
           //belongs to EmploymentServices department that why we are passing this value Departments.EmploymentServices. Otherwise need to get departmentId of the user and pass that.
           var taskTypeSla = _taskRepository.GetTaskTypeSlaByTaskTypeIdAndDepartmentId((long)TaskTypeIds.ServiceReview, (long)Departments.EmploymentServices);
           var dueDate = WorkingHours.CalculateDueDate(taskTypeSla.DefaultSLATime);

           var newServiceReviewTasksList = new List<Task>();
           
           foreach (CorporatePriorityByCansQueryResult result in queryResults)
           {
               var user = _userRepository.GetById(result.UserId);
               var assignedUser = (user != null && !user.Deleted) ? user.Username : "Tina.Ayres";

               var client = _tblCustomerRepository.GetCustomerByCustomerKey(result.Can);
               var clientId = client != null ? (long?)client.CustomerID : null;

               var serviceReviewTaskParameter = ServiceReviewTaskParameters.Create(clientId, result.ContractEndDate, dueDate, taskTypeSla.DefaultWarningWindow, assignedUser, null, taskTypeSla.DefaultAcceptableWindow);
               var task = Task.Create(serviceReviewTaskParameter);
               newServiceReviewTasksList.Add(task);
            }

           return newServiceReviewTasksList;
        }
        
    }
       
}
