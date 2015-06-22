using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Advice.Domain.Entities
{
    public partial class TaskJob
    {
        public static TaskJob Create(long jobId, CreatedUser createdUser, ModifiedUser modifiedUser)
        {
            var taskJob = new TaskJob()
            {
                CreatedBy = createdUser.Name,
                CreatedDate = createdUser.CreatedDate,
                Deleted = false,
                JobID = jobId,
                LastModifiedBy = modifiedUser.Name,
                LastModifiedDate = modifiedUser.LastModifiedDate.Value
            };

            return taskJob;
        }
    }
}
