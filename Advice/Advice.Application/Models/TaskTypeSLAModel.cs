using System;
using Advice.Domain.Entities;

namespace Advice.Application.Models
{
    public class TaskTypeSLAModel
    {
        public long TaskTypeSLAID { get; set; }
        public long TaskTypeID { get; set; }
        public long DepartmentID { get; set; }
        public int DefaultSLATime { get; set; }
        public int DefaultWarningWindow { get; set; }
        public bool? UserCanRaise { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int DefaultAcceptableWindow { get; set; }
        
        // Leave this out for now causing self referencing loop error, worry about it if needed
        //public virtual Department Department { get; set; }

        public static TaskTypeSLAModel CreateTaskTypeSlaFromTask(TaskTypeSLA taskTypeSla)
        {
            if (taskTypeSla == null)
            {
                return null;
            }

            var taskSla = new TaskTypeSLAModel()
            {
                TaskTypeSLAID = taskTypeSla.TaskTypeSLAID,
                TaskTypeID = taskTypeSla.TaskTypeID,
                DepartmentID = taskTypeSla.DepartmentID,
                DefaultSLATime = taskTypeSla.DefaultSLATime,
                DefaultWarningWindow = taskTypeSla.DefaultWarningWindow,
                UserCanRaise = taskTypeSla.UserCanRaise,
                CreatedBy = taskTypeSla.CreatedBy,
                CreatedDate = taskTypeSla.CreatedDate,
                LastModifiedBy = taskTypeSla.LastModifiedBy,
                LastModifiedDate = taskTypeSla.LastModifiedDate,
                Deleted = taskTypeSla.Deleted,
                DeletedBy = taskTypeSla.DeletedBy,
                DeletedDate = taskTypeSla.DeletedDate,
                DefaultAcceptableWindow = taskTypeSla.DefaultAcceptableWindow,
                //Department = taskTypeSla.Department
            };

            return taskSla;
        }

    }
}
