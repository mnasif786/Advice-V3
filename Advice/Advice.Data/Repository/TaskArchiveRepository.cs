using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;

namespace Advice.Data.Repository
{
    public class TaskArchiveRepository : AdviceRepository<TaskArchive>, ITaskArchiveRepository
    {
        public TaskArchiveRepository(IAdviceDbContextManager adviceDbContextManager): base(adviceDbContextManager)
        {
            
        }
        public IEnumerable<TaskArchive> GetTaskArchivesByTaskId(long taskId)
        {
            var taskArchive = Context.TaskArchives.Where(task => task.TaskID == taskId)
                              .Include(t=> t.Team)
                              .Include(x=>x.TaskModifyingReason)
                              .Include(x=>x.TaskModifyingReason.TaskModifyingReasonGroup);
            return taskArchive;
        }
    }
}
