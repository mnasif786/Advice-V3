using System;
using Advice.Domain.Entities;

namespace Advice.Common.Models
{
    public class TaskJobsModel
    {
        public long TaskJobID { get; set; }
        public long JobID { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public System.DateTime LastModifiedDate { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public JobModel Job { get; set; }

        public static TaskJobsModel Create(TaskJob taskJob)
        {
            if (taskJob == null)
                return null;
          
            var taskModel = new TaskJobsModel()
            {
                TaskJobID = taskJob.TaskJobID,
                JobID = taskJob.JobID,
                CreatedBy = taskJob.CreatedBy,
                CreatedDate = taskJob.CreatedDate,
                LastModifiedBy = taskJob.LastModifiedBy,
                LastModifiedDate = taskJob.LastModifiedDate,
                Deleted = taskJob.Deleted,
                DeletedBy = taskJob.DeletedBy,
                DeletedDate = taskJob.DeletedDate
            };

            return taskModel;
        }

        public static TaskJobsModel CreateWithJob(TaskJob taskJob)
        {
            if (taskJob == null)
                return null;

            var taskModel = new TaskJobsModel()
            {
                TaskJobID = taskJob.TaskJobID,
                JobID = taskJob.JobID,
                CreatedBy = taskJob.CreatedBy,
                CreatedDate = taskJob.CreatedDate,
                LastModifiedBy = taskJob.LastModifiedBy,
                LastModifiedDate = taskJob.LastModifiedDate,
                Deleted = taskJob.Deleted,
                DeletedBy = taskJob.DeletedBy,
                DeletedDate = taskJob.DeletedDate,
                Job = (taskJob.Job != null && !taskJob.Job.Deleted && !taskJob.Job.Closed) ? JobModel.Create(taskJob.Job) : null
            };

            return taskModel;
        }


    }
}
