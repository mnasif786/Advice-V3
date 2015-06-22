using System;
using System.Collections.Generic;
using Advice.Application.Contracts;
using Advice.Common.Models;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Advice.Domain.RepositoryContracts;
using System.Linq;

namespace Advice.Application.Implementations
{
    public class TaskModifyingReasonService : ITaskModifyingReasonService
    {
        private readonly ITaskModifyingReasonRepository _taskModifyingReasonRepositoryRepository;

        public TaskModifyingReasonService(ITaskModifyingReasonRepository taskModifyingReasonRepositoryRepository)
        {
            _taskModifyingReasonRepositoryRepository = taskModifyingReasonRepositoryRepository;
        }

        public IEnumerable<TaskModifyingReasonModel> GetTaskModifyingReasonsForResetGroup()
        {
            return
                _taskModifyingReasonRepositoryRepository.GetTaskModifyingReasonsByGroupId(
                    TaskModifyingReasonGroups.Reset).Select(Map);
        }

        public IEnumerable<TaskModifyingReasonModel> GetTaskModifyingReasonsForReassignGroup()
        {
            return
                _taskModifyingReasonRepositoryRepository.GetTaskModifyingReasonsByGroupId(
                    TaskModifyingReasonGroups.Reassign).Select(Map);
        }

        private TaskModifyingReasonModel Map(TaskModifyingReason taskModifyingReasons)
        {
            return new TaskModifyingReasonModel((int)taskModifyingReasons.TaskModifyingReasonID, taskModifyingReasons.Description);
        }
    }
}
