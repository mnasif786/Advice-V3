using System.Collections.Generic;
using Advice.Domain.Common;
using Advice.Domain.Entities;

namespace Advice.Domain.RepositoryContracts
{
    public interface ITaskRepository: IAdviceRepository<Task>
    {
        IEnumerable<Task> GetTasksByClientId(int clientId);
        
        // SGG - Do we need these??
        //IEnumerable<Task> GetTaskListForTimeline(int teamId);
        //IEnumerable<Task> GetTaskListForTimeline(string userName);
        
        IEnumerable<Task> GetTasksByUserName(string userName);
        IEnumerable<Task> GetTasksByTeamId(long teamId);
        IEnumerable<Task> GetTasksByTeamIds(long[] teamId);
        Task GetTaskByTaskId(long taskId);
        Task GetTaskWithModifyingReasonAndTeamByTaskId(long taskId);
        void DeleteTasksByTaskIds(long[] taskIds, string userName);
        Task GetTaskWithDetailByTaskId(long taskId);
        IEnumerable<Task> GetDeletedTasksByTeamIds(long[] teamId, int sinceLastDays);
        IEnumerable<Task> GetDeletedTasksByUserName(string userName, int sinceLastDays);
        IEnumerable<Task> GetProActiveTasksByUserName(string userName);

        TaskTypeSLA GetTaskTypeSlaByTaskTypeIdAndDepartmentId(long taskTypeId, long departmentId);
        IEnumerable<Task> GetProActiveTasksByTeams(long[] teamIds);
    }
}

