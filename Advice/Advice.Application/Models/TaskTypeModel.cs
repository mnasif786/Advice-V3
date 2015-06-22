using System;
using System.Collections.Generic;
using System.Linq;
using Advice.Domain.Entities;

namespace Advice.Application.Models
{
    public class TaskTypeModel
    {
        public long TaskTypeId { get; set; }
        public string Description { get; set; }
        public bool? ExternalTask { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public virtual ICollection<TaskTypeSLAModel> TaskTypeSLAs { get; set; }

        public static TaskTypeModel Create(TaskType taskType)
        {
            if (taskType == null)
                return null;

            var typeModel = new TaskTypeModel()
            {
                TaskTypeId = taskType.TaskTypeID,
                Description = taskType.Description,
                ExternalTask = taskType.ExternalTask,
                CreatedBy = taskType.CreatedBy,
                CreatedDate = taskType.CreatedDate,
                LastModifiedBy = taskType.LastModifiedBy,
                LastModifiedDate = taskType.LastModifiedDate,
                Deleted = taskType.Deleted,
                DeletedBy = taskType.DeletedBy,
                DeletedDate = taskType.DeletedDate,
                TaskTypeSLAs = taskType.TaskTypeSLAs.Select(TaskTypeSLAModel.CreateTaskTypeSlaFromTask).ToList(), 
            };

            return typeModel;
        }
    }
}
