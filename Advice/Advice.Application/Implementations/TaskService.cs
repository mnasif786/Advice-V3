using System;
using System.Collections.Generic;
using System.Linq;
using Advice.Application.Contracts;
using Advice.Common.Models;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Advice.Domain.Helper;
using Advice.Domain.RepositoryContracts;
using Advice.ExchangeEmails;
using Peninsula.Domain.Entities;
using Peninsula.Domain.RepositoryContracts;
using StructureMap.Building;

namespace Advice.Application.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ITaskArchiveRepository _taskArchiveRepository;
        private readonly ITblCustomerRepository _tblCustomerRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IExchangeEmailService _exchangeEmailService;
    
        public TaskService(
            ITaskRepository taskRepository, 
            ITeamRepository teamRepository,
            ITaskArchiveRepository taskArchiveRepository,
            ITblCustomerRepository tblCustomerRepository, 
            IDepartmentRepository departmentRepository,
            IExchangeEmailService exchangeEmailService)
        {
            _taskRepository = taskRepository;
            _teamRepository = teamRepository;
            _taskArchiveRepository = taskArchiveRepository;
            _tblCustomerRepository = tblCustomerRepository;
            _departmentRepository = departmentRepository;
            _exchangeEmailService = exchangeEmailService;
        }

        public TaskModel GetById(long id)
        {
            var task = _taskRepository.GetById(id);

            string can = string.Empty;
            if (task.RecordedClientID.HasValue)
            {
                can = _tblCustomerRepository.GetById(task.RecordedClientID).CustomerKey;
            }

            var taskModel = TaskModel.Create(task, can);

            return taskModel;
        }

        public IEnumerable<TaskModel> GetAllTasksByUser(string userName)
        {
            var tasks = _taskRepository.GetTasksByUserName(userName);
            return GetTaskModelListFromTaskList(tasks);
        }

        public IEnumerable<TaskModel> GetAllTasksByTeamName(string teamName)
        {
            var teamId = _teamRepository.GetTeamIdByTeamName(teamName);

            if (teamId == null) return null;
           
            return GetAllTasksByTeamId(new long[]{teamId.Value});
        }

        public IEnumerable<TaskModel> GetAllTasksByTeamId(long[] teamIdArray)
        {
            var tasks = _taskRepository.GetTasksByTeamIds(teamIdArray);
            return GetTaskModelListFromTaskList(tasks);
        }

        public TaskDetailsModel GetTaskWithDetailsByTaskId(long taskId, string userName)
        {
            
            var task = _taskRepository.GetTaskWithDetailByTaskId(taskId);

            var taskdetailsModel = TaskDetailsModel.Create(task);

            var isProactiveTask = Constants.ProActiveTaskTypes.Contains((TaskTypeIds)task.TaskType.TaskTypeID);
            if (isProactiveTask)
            {
                taskdetailsModel.TaskJobs = GetTaskJobsWithJobDetails(task);
            }


            if (taskdetailsModel.EmailTask != null && taskdetailsModel.EmailTask.MessageId.HasValue)
            {
                var response =_exchangeEmailService.GetOutlookEmailMailMessage(taskdetailsModel.EmailTask.MessageId.Value);

                taskdetailsModel.EmailTask.OutlookEmailMessage = OutlookEmailMessageModel.Create(response); 
            }

            if (taskdetailsModel.HroTask != null && taskdetailsModel.HroTask.DepartmentId.HasValue)
            {
                taskdetailsModel.HroTask.DepartmentDescription =
                    GetDepartmentDescriptionByDepartmentId(taskdetailsModel.HroTask.DepartmentId);
            }

            if (taskdetailsModel.BusinessWiseTask != null && taskdetailsModel.BusinessWiseTask.DepartmentId.HasValue)
            {
                taskdetailsModel.BusinessWiseTask.DepartmentDescription =
                    GetDepartmentDescriptionByDepartmentId(taskdetailsModel.BusinessWiseTask.DepartmentId);
            }

            task.MarkAsRead(userName);
            _taskRepository.SaveChanges();

            return taskdetailsModel;
        }

        private IEnumerable<TaskJobsModel> GetTaskJobsWithJobDetails(Task task)
        {
            
            var taskJobModelList = new List<TaskJobsModel>();

            foreach (var taskJob in task.TaskJobs)
            {
                if (taskJob != null && taskJob.Job != null && taskJob.Job.ContactID.HasValue)
                {
                    var companyContact = _tblCustomerRepository.GetCompanyContactWithContactId(taskJob.Job.ContactID.Value);
                    var taskJobModel = TaskJobsModel.CreateWithJob(taskJob);
                    if (taskJobModel.Job != null)
                    {
                        taskJobModel.Job.Contact = CompanyContactModel.Create(companyContact);
                    }

                    taskJobModelList.Add(taskJobModel);
                }
            }
            
            return taskJobModelList;
        }

        public TaskTimelineModel GetTaskListTimelineByTeamId(int teamId)
        {
            var taskListSummary = _taskRepository.GetTasksByTeamId(teamId);
            return new TaskTimelineModel(taskListSummary);
        }

        public TaskTimelineModel GetTaskListTimelineByTeamIds(long[] teamIds)
        {
            var taskListSummary = _taskRepository.GetTasksByTeamIds(teamIds);
            return new TaskTimelineModel(taskListSummary);          
        }

        public TaskTimelineModel GetTaskListTimelineByUsername(string userName)
        {            
            var taskListSummary = _taskRepository.GetTasksByUserName(userName);
            return new TaskTimelineModel(taskListSummary);          
        }

        public string RestoreEmailToOutlook(long taskId, long messageId, string userName)
        {
            var emailTask = _taskRepository.GetTaskByTaskId(taskId);

            var response = _exchangeEmailService.RestoreMessageToOutlook(messageId, emailTask.AssignedUser);
            emailTask.MarkAsDeleted(userName);
            _taskRepository.Update(emailTask);
            _taskRepository.SaveChanges();

            return response;
        }
        public void RestoreBulkEmailsToOutlook(long[] taskIds, string userName)
        {
            foreach (var taskId in taskIds)
            {
                var task = _taskRepository.GetTaskByTaskId(taskId);
                if (task is EmailTask)
                {
                    var emailTask = (EmailTask) task;

                    if (emailTask.MessageId.HasValue)
                    {
                        _exchangeEmailService.RestoreMessageToOutlook(emailTask.MessageId.Value, emailTask.AssignedUser);
                        task.MarkAsDeleted(userName);
                        _taskRepository.Update(task);
                        _taskRepository.SaveChanges();
                    }
                }
            }
        }
      
        public void Delete(long taskId, string userName)
        {
            var task = _taskRepository.GetTaskByTaskId(taskId);
            task.MarkAsDeleted(userName);
            _taskRepository.Update(task);
            _taskRepository.SaveChanges();
        }

        public void ReinstateTask(long taskId, string userName)
        {
            var task = _taskRepository.GetById(taskId);

            if (task != null)
            {
                task.Reinstate(userName);
                _taskRepository.Update(task);
                _taskRepository.SaveChanges();
            }

        }

        public IEnumerable<TaskArchiveModel> GetTaskHistoryByTaskId(long taskId)
        {          
            var taskArchives = _taskArchiveRepository.GetTaskArchivesByTaskId(taskId);
            var task = _taskRepository.GetTaskWithModifyingReasonAndTeamByTaskId(taskId);
            var currentTaskStatusToBeAddedToTaskArchive = TaskArchiveModel.Create(task);
            var taskArchiveList = taskArchives.Select(TaskArchiveModel.Create)
                                  .Union(new List<TaskArchiveModel> { currentTaskStatusToBeAddedToTaskArchive })
                                  .OrderByDescending(o => o.LastModifiedDate);


            return taskArchiveList;
        }

        private string GetClientCanByCustomerId(IEnumerable<TBLCustomer> customers, long? customerId)
        {
            if (customerId.HasValue)
            {
                var customer = customers.FirstOrDefault(cust=> cust.CustomerID == customerId);
                if (customer != null)
                {
                    return customer.CustomerKey;
                }
            }

            return string.Empty;
        }

        private IEnumerable<TaskModel> GetTaskModelListFromTaskList(IEnumerable<Task> tasks)
        {
            var taskList = tasks as IList<Task> ?? tasks.ToList();
            var customerIds = taskList.Select(x => Convert.ToInt32(x.RecordedClientID)).Distinct().ToList();
            var customers = _tblCustomerRepository.GetCustomersByCustomerIds(customerIds).ToList();
            var taskListModel = taskList.Select(task => TaskModel.Create(task, GetClientCanByCustomerId(customers, task.RecordedClientID)));

            return taskListModel;
        } 

        private string GetDepartmentDescriptionByDepartmentId(long? departmentId)
        {
            string deptDescription = null;

            if (departmentId.HasValue)
            {
                deptDescription = _departmentRepository.GetDepartmentDiscriptionByDepartmentId(departmentId.Value);
                
            }

            return deptDescription;
        }

        public IEnumerable<TaskModel> GetDeletedTasksByTeamIds(long[] teamIds, int sinceLastDays)
        {
            var tasks = _taskRepository.GetDeletedTasksByTeamIds(teamIds, sinceLastDays);
            return GetTaskModelListFromTaskList(tasks);
        }

        public IEnumerable<TaskModel> GetDeletedTasksByUserName(string userName, int sinceLastDays)
        {
            var tasks = _taskRepository.GetDeletedTasksByUserName(userName, sinceLastDays);
            return GetTaskModelListFromTaskList(tasks);
        }

        public void DeleteBulkTasks(long[] taskIds, string userName)
        {
            _taskRepository.DeleteTasksByTaskIds(taskIds, userName);
            _taskRepository.SaveChanges();
        }

        public void ResetTaskSla(long taskId, DateTime dueDate, bool urgent, int taskModifyingReasonId, string comments, string userName)
        {
            var task = _taskRepository.GetById(taskId);
            task.ResetTaskSla(dueDate, urgent, taskModifyingReasonId, comments, userName);
            _taskRepository.Update(task);
            _taskRepository.SaveChanges();
        }

        public void ReassignTask(ReassignTaskModel reassignTaskModel)
        {
            var task = _taskRepository.GetById(reassignTaskModel.TaskId);

            if (task == null) 
                throw new Exception("Task Not Found. TaskId: " + reassignTaskModel.TaskId);

            var previousTeamId = task.AssignedTeamID;
            var previousUser = task.AssignedUser;
            
            task.Reassign(reassignTaskModel.AssignedUser,
                reassignTaskModel.AssignedTeamId,
                reassignTaskModel.Urgent,reassignTaskModel.Comments,
                reassignTaskModel.ReAssignedByUser, 
                reassignTaskModel.ClientId,
                reassignTaskModel.DueDate,
                reassignTaskModel.ReasonId,
                previousTeamId,
                previousUser);


            _taskRepository.Update(task);
            _taskRepository.SaveChanges();
        }

        public TaskSearchResultModel GetProActiveTasksByUserName(string userName)
        {
            var tasks = _taskRepository.GetProActiveTasksByUserName(userName).ToList();
            var tasksList  =  GetTaskModelListFromTaskList(tasks);
            var tasksTimeline = new TaskTimelineModel(tasks);
            var tasksModel = TaskSearchResultModel.Create(tasksList, tasksTimeline);
            return tasksModel;
        }

        public TaskSearchResultModel GetTasksByUser(string userName)
        {
            var tasks = _taskRepository.GetTasksByUserName(userName).ToList();
            var tasksList = GetTaskModelListFromTaskList(tasks);
            var tasksTimeline = TaskTimelineModel.Create(tasks);
            var tasksModel = TaskSearchResultModel.Create(tasksList, tasksTimeline);
            return tasksModel;
        }

        public TaskSearchResultModel GetTasksByTeams(long[] teamIds)
        {
            var tasks = _taskRepository.GetTasksByTeamIds(teamIds).ToList();
            var tasksList = GetTaskModelListFromTaskList(tasks);
            var tasksTimeline = TaskTimelineModel.Create(tasks);
            var tasksModel = TaskSearchResultModel.Create(tasksList, tasksTimeline);
            return tasksModel;
        }

        public TaskSearchResultModel GetProActiveTasksByTeams(long[] teamIds)
        {
            var tasks = _taskRepository.GetProActiveTasksByTeams(teamIds).ToList();
            var tasksList = GetTaskModelListFromTaskList(tasks);
            var tasksTimeline = new TaskTimelineModel(tasks);
            var tasksModel = TaskSearchResultModel.Create(tasksList, tasksTimeline);
            return tasksModel;
        }
    }
}
