using System;
using Advice.Domain.Entities;

namespace Advice.Common.Models
{
    public class TaskActionModel
    {
        public long TaskActionID { get; set; }
        public long ActionID { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public System.DateTime LastModifiedDate { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public static TaskActionModel CreateFromTaskAction(TaskAction task)
        {
            if (task == null)
                return null;

            var taskAction = new TaskActionModel()
            {
                TaskActionID = task.TaskActionID,
                ActionID = task.ActionID,
                CreatedBy = task.CreatedBy,
                LastModifiedBy = task.LastModifiedBy,
                LastModifiedDate = task.LastModifiedDate,
                Deleted = task.Deleted,
                DeletedBy = task.DeletedBy,
                DeletedDate = task.DeletedDate
            };

            return taskAction;
        }
    }
}
