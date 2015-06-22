using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Common;
using Advice.Domain.Entities;
using Task = Advice.Domain.Entities.Task;

namespace Advice.Domain.RepositoryContracts.Services
{
    public interface IServiceReviewServiceJobScheduleRepository : IAdviceRepository<ServiceReviewServiceSchedule>
    {
        IEnumerable<ServiceReviewServiceSchedule> GetScheduledJobs(int forDay, int forMonth);
        IEnumerable<ServiceReviewServiceSchedule> GetSkippedJobs(DateTime currentDateTime);
    }
}
