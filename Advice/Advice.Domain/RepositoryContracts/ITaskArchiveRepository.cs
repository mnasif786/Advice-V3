using System;
using System.Collections.Generic;
using Advice.Domain.Common;
using Advice.Domain.Entities;

namespace Advice.Domain.RepositoryContracts
{
    public interface ITaskArchiveRepository : IAdviceRepository<TaskArchive>
    {
        IEnumerable<TaskArchive> GetTaskArchivesByTaskId(long taskId);
    }
}
