using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Data.CustomExceptions;
using Advice.Data.Helpers;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Advice.Domain.Helper;
using Advice.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using Task = Advice.Domain.Entities.Task;

namespace Advice.Data.Repository
{
    public class TaskRepository : AdviceRepository<Task>, ITaskRepository
    {
        private ITaskStoredProcRunner _storedProcRunner;

        public TaskRepository(IAdviceDbContextManager adviceDbContextManager, ITaskStoredProcRunner storedProcRunner)
            : base(adviceDbContextManager)
        {
            if(null == storedProcRunner)
            {
                throw new ArgumentNullException("storedProcRunner must not be null");
            }

            _storedProcRunner = storedProcRunner;
        }
        public IEnumerable<Task> GetTasksByClientId(int clientId)
        {
            return Context.Tasks.Where(task => task.RecordedClientID == clientId
                                               && task.Completed != true 
                                               && task.Deleted == false                                         
                                               && !Constants.ProActiveTaskTypes.Contains((TaskTypeIds)task.TaskTypeID))
                .OrderBy(x => x.DueDate);
        }

        public IEnumerable<Task> GetTasksByUserName(string userName)
        {
            return Context.Tasks.Where(task => task.AssignedUser == userName 
                                        && task.Completed != true 
                                        && task.Deleted == false
                                        && !Constants.ProActiveTaskTypes.Contains((TaskTypeIds)task.TaskTypeID))
                                .OrderBy(x => x.DueDate)
                                .Include(t=>t.TaskType);
        }

        public IEnumerable<Task> GetTasksByTeamId(long teamId)
        {
            var tasks = from t in Context.Tasks.Where(x => !x.Deleted && !x.Completed
                                                            && !Constants.ProActiveTaskTypes.Contains((TaskTypeIds)x.TaskTypeID))
                        join u in Context.Users.Where(u => !u.Deleted)
                        on t.AssignedUser equals u.Username into tasklist
                        from t1 in tasklist.DefaultIfEmpty()
                        where t.AssignedTeamID == teamId || t1.TeamID == teamId
                        select t;

            return tasks.OrderBy(x => x.DueDate);
        }

        public IEnumerable<Task> GetTasksByTeamIds(long[] teamIds)
        {
            String storedProcedureName = "GetTasksByTeamIds";

            IEnumerable<GetTasksByTeamIds_Type> storedProcResult = _storedProcRunner.ExecuteQuery(((IObjectContextAdapter) Context).ObjectContext, 
                                                                                                  storedProcedureName, 
                                                                                                  teamIds);

            return GenerateTasksFromGetTasksByTeamIds_Type(storedProcResult);
        }


        public IEnumerable<Task> GetDeletedTasksByTeamIds(long[] teamIds, int sinceLastDays)
        {           
            var tasks = (from t in Context.Tasks.Where(x => x.Deleted)
                        join u in Context.Users.Where(u => !u.Deleted)
                        on t.AssignedUser equals u.Username into tasklist
                        from t1 in tasklist.DefaultIfEmpty()
                        where ((t.AssignedTeamID != null && teamIds.Contains(t.AssignedTeamID.Value)) || (teamIds.Contains(t1.TeamID.Value)))
                         && (DatabaseFunctions.TruncateTime(t.DeletedDate.Value) >= DatabaseFunctions.TruncateTime(DatabaseFunctions.AddDays(DateTime.Now, -sinceLastDays)))
                         select t).Include(tt => tt.TaskType);

            return tasks.OrderBy(x => x.DeletedDate);
        }

        public IEnumerable<Task> GetDeletedTasksByUserName(string userName, int sinceLastDays)
        {
            return Context.Tasks.Where(t => t.AssignedUser == userName && t.Deleted
                && t.DeletedDate.HasValue                
                && DatabaseFunctions.TruncateTime(t.DeletedDate.Value) >= DatabaseFunctions.TruncateTime(DatabaseFunctions.AddDays(DateTime.Now, -sinceLastDays)))
                .Include(tt => tt.TaskType)
                .OrderBy(x => x.DeletedDate);
        }

        public Task GetTaskByTaskId(long taskId)
        {
            try
            { 
                return Context.Tasks.SingleOrDefault(task => task.TaskID == taskId && task.Completed != true && task.Deleted == false);
            }
            catch (Exception ex)
            {
                throw new MultipleTaskFoundException(taskId, ex.Message);
            }
        }

        public Task GetTaskWithModifyingReasonAndTeamByTaskId(long taskId)
        {
            //Delete flag is not included deliberately as this method could be called after task is deleted
            return Context.Tasks.Where(task => task.TaskID == taskId && !task.Completed)
                       .Include(m=> m.TaskModifyingReason)
                       .Include(g=>g.TaskModifyingReason.TaskModifyingReasonGroup)
                       .Include(t=>t.AssignedTeam)
                       .SingleOrDefault();
        }

        public void DeleteTasksByTaskIds(long[] taskIds, string userName)
        {
            Context.Tasks.Where(x => taskIds.Contains(x.TaskID)).ToList().ForEach(x =>
            {
                    x.MarkAsDeleted(userName);
                    Update(x);
            });
        }

        public Task GetTaskWithDetailByTaskId(long taskId)
        {
            try
            {
                var task = Context.Tasks                
                            .Include(t => t.TaskType)
                            .Include(t => t.TaskDocuments)
                            .Include(t => t.TaskJobs)
                            .SingleOrDefault(t => t.TaskID == taskId);

                return task;
            }
            catch (Exception ex)
            {
                throw new MultipleTaskFoundException(taskId, ex.Message);
            }
        }       

        public IEnumerable<Task> GetProActiveTasksByUserName(string userName)
        {           
            return Context.Tasks.Where(task => task.AssignedUser == userName
                                                && task.Completed != true 
                                                && task.Deleted == false
                                                && Constants.ProActiveTaskTypes.Contains((TaskTypeIds)task.TaskTypeID)
                                                )
                                .OrderBy(x => x.DueDate)
                                .Include(t => t.TaskType);
        }

        public TaskTypeSLA GetTaskTypeSlaByTaskTypeIdAndDepartmentId(long taskTypeId, long departmentId)
        {
            return
                Context.TaskTypeSLAs.SingleOrDefault(
                    s => s.TaskTypeID == taskTypeId && s.DepartmentID == departmentId && s.Deleted == false);
        }

        public IEnumerable<Task> GetProActiveTasksByTeams(long[] teamIds)
        {
            String storedProcName = "GetTasksByTeamIdsProactive";
            IEnumerable<GetTasksByTeamIds_Type> storedProcResult = _storedProcRunner.ExecuteQuery(((IObjectContextAdapter) Context).ObjectContext,
                                                                                                  storedProcName,
                                                                                                  teamIds);

            return GenerateTasksFromGetTasksByTeamIds_Type(storedProcResult);
        }

        private static List<Task> GenerateTasksFromGetTasksByTeamIds_Type(IEnumerable<GetTasksByTeamIds_Type> taskByTeamIds)
        {
            List<Task> taskList = new List<Task>();

            if (null != taskByTeamIds)
            {
                foreach (GetTasksByTeamIds_Type row in taskByTeamIds)
                {
                    taskList.Add(Task.Create(row));
                }
            }

            return taskList;
        }

        private IQueryable<Task> GetProactiveTasksByTeamsQuery(long[] teamIds)
        {
            var taskByTeamQuery = (from t in Context.Tasks.Where(x => !x.Deleted
                                                                        && !x.Completed
                                                                        && Constants.ProActiveTaskTypes.Contains((TaskTypeIds)x.TaskTypeID))
                                   join u in Context.Users.Where(u => !u.Deleted)
                                   on t.AssignedUser equals u.Username into tasklist
                                   from t1 in tasklist.DefaultIfEmpty()
                                   where (t.AssignedTeamID != null && teamIds.Contains(t.AssignedTeamID.Value)) || (t1.TeamID != null && teamIds.Contains(t1.TeamID.Value))
                                   select t);

            return taskByTeamQuery;
        }

    }
}
