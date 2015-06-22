using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.ServiceReviews.Common;

namespace Advice.ServiceReviews.Contracts
{
    public interface IServiceReviewService
    {
        ServiceResponse ProcessScheduledJobs();
        ServiceResponse ProcessSKippedJobs();

    }
}
