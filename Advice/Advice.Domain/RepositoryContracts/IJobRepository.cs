using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Common;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;

namespace Advice.Domain.RepositoryContracts
{
    public interface IJobRepository : IAdviceRepository<Job>
    {
        IEnumerable<Job> GetOpenJobsByNatureOfAdviceGroupAndLastActionDate(DateTime lastActionDate);
        IEnumerable<Job> GetOpenJobsByNonNatureOfAdviceGroupAndLastActionDate(DateTime lastActionDate);
        IEnumerable<Job> GetOpenConductSuspensionJobsByLastActionDate(DateTime lastActionDate);
        IEnumerable<Job> GetOpenJobsForWorkingUserGroupByNonNatureOfAdviceGroupAndLastActionDate(DateTime lastActionDate);
        IEnumerable<Job> GetOpenConductSuspensionJobsForWorkingUserGroupByLastActionDate(DateTime lastActionDate);
        IEnumerable<Job> GetOpenJobsForWorkingUserGroupByNatureOfAdviceGroupAndLastActionDate(DateTime lastActionDate);
    }
}
