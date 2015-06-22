using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.ServiceReviews.Common
{
    public class ServiceResponse
    {
        public int TotalJobsRun { get; set; }
        public int TotalRecordsFound { get; set; }
        public int TotalRecordsProcessed { get; set; }
        public static ServiceResponse Create(int totalJobsRun, int totalRecordsFound, int totalRecordsProcessed)
        {
            return new ServiceResponse()
            {
                TotalJobsRun = totalJobsRun,
                TotalRecordsFound = totalRecordsFound,
                TotalRecordsProcessed = totalRecordsProcessed
            };
        }
    }

 

}
