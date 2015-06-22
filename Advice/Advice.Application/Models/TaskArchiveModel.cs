using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Entities;

namespace Advice.Application.Models
{
    public class TaskArchiveModel
    {
        public long? TaskId { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string AssignedTo { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedComment { get; set; }
        public string TaskModifyingReason { get; set; }
        public string TaskModifyingReasonGroup { get; set; }

        public static TaskArchiveModel Create(TaskArchive taskArchive)
        {
            if (taskArchive == null)
                return null;

            var taskArchiveModel = new TaskArchiveModel()
            {
                TaskId = taskArchive.TaskID,
                Description = taskArchive.Description,
                DueDate = taskArchive.DueDate,
                AssignedTo = taskArchive.AssignedUser ?? (taskArchive.Team != null? taskArchive.Team.Description : null),
                LastModifiedBy = taskArchive.LastModifiedBy,
                LastModifiedComment = taskArchive.LastModifiedComment,
                LastModifiedDate = taskArchive.LastModifiedDate,
                TaskModifyingReason         = (taskArchive.TaskModifyingReason != null) ? taskArchive.TaskModifyingReason.Description: null,
                TaskModifyingReasonGroup    = (taskArchive.TaskModifyingReason != null && taskArchive.TaskModifyingReason.TaskModifyingReasonGroup != null) ? taskArchive.TaskModifyingReason.TaskModifyingReasonGroup.Description : null
                
            };
           

            return taskArchiveModel;

        }

        public static TaskArchiveModel Create(Domain.Entities.Task task)
        {
            if (task == null)
                return null;

            var taskArchiveModel = new TaskArchiveModel()
            {
                TaskId = task.TaskID,
                Description = task.Description,
                DueDate = task.DueDate,
                AssignedTo = task.AssignedUser ?? (task.AssignedTeam != null ? task.AssignedTeam.Description : null),
                LastModifiedBy = task.LastModifiedBy,
                LastModifiedComment = task.LastModifiedComment,
                LastModifiedDate = task.LastModifiedDate,
                TaskModifyingReason = (task.TaskModifyingReason != null) ? task.TaskModifyingReason.Description : null,
                TaskModifyingReasonGroup = (task.TaskModifyingReason != null && task.TaskModifyingReason.TaskModifyingReasonGroup != null) ? task.TaskModifyingReason.TaskModifyingReasonGroup.Description : null

            };

            return taskArchiveModel;

        }
    }

    
}
