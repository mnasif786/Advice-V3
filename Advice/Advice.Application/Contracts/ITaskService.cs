using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Common.Models;

namespace Advice.Application.Contracts
{
    public interface ITaskService
    {
        TaskModel GetById(long id);
        TaskTimelineModel GetTaskListTimelineByTeamId(int teamId);
        TaskTimelineModel GetTaskListTimelineByTeamIds(long[] teamIds);
        TaskTimelineModel GetTaskListTimelineByUsername(string userName);
        IEnumerable<TaskModel> GetAllTasksByUser(string userId);
        IEnumerable<TaskModel> GetAllTasksByTeamName(string teamName);
        IEnumerable<TaskModel> GetAllTasksByTeamId(long[] teamId);
        TaskDetailsModel GetTaskWithDetailsByTaskId(long teamId, string userName);
        IEnumerable<TaskArchiveModel> GetTaskHistoryByTaskId(long taskId);
        string RestoreEmailToOutlook(long taskId, long messageId, string userName);
        void RestoreBulkEmailsToOutlook(long[] taskIds, string userName);
        void Delete(long taskId, string userName);
        IEnumerable<TaskModel> GetDeletedTasksByTeamIds(long[] teamId, int sinceLastDays);
        IEnumerable<TaskModel> GetDeletedTasksByUserName(string userName, int sinceLastDays);
        void DeleteBulkTasks(long[] taskId, string userName);
        void ReinstateTask(long taskId, string userName);
        void ResetTaskSla(long taskId, DateTime dueDate, bool urgent, int taskModifyingReasonId, string comments, string userName);

        void ReassignTask(ReassignTaskModel reassignTaskModel);
        TaskSearchResultModel GetProActiveTasksByUserName(string userName);
        TaskSearchResultModel GetTasksByUser(string userId);
        TaskSearchResultModel GetTasksByTeams(long[] teamIds);
        TaskSearchResultModel GetProActiveTasksByTeams(long[] teamIds);

    }
}
