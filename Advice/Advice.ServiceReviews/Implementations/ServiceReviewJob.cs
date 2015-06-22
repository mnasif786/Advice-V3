using System;
using System.Linq;
using System.Reflection;
using Advice.ServiceReviews.Contracts;
using Advice.ServiceReviews.Helpers;
using Advice.Logging;
using Advice.Logging.Contracts;
using Advice.Logging.Models;



namespace Advice.ServiceReviews.Implementations
{
    public class ServiceReviewJob: IServiceReviewJob
    {
        
        private readonly IServiceReviewService _serviceReviewService;
        public ServiceReviewJob(IServiceReviewService serviceReviewService)
        {
            _serviceReviewService = serviceReviewService;
        }

        public void Run()
        {
            try
            {
                if (IsWeeked) return; // do not process any thing on weekends

                Console.Write("***************DO NOT CLOSE**************** \n");
                Console.Write("Processing service review jobs... \n");

                var response = _serviceReviewService.ProcessScheduledJobs();
                var scheduledJobslogMessage =  
                    string.Format("ServiceReview  scheduled Job(s) run successfully. Total Jobs processed:{0} Total Records Found:{1}, Total Records Processed:{2}",
                        response.TotalJobsRun, response.TotalRecordsFound, response.TotalRecordsProcessed);
                UtilityFunctions.WriteToLog(scheduledJobslogMessage);
               

                response = _serviceReviewService.ProcessSKippedJobs();
                var skippedJobslogMessage = 
                    string.Format("ServiceReview Skipped Job(s) run successfully. Total Jobs processed:{0} Total Records Found:{1}, Total Records Processed:{2}",
                        response.TotalJobsRun, response.TotalRecordsFound, response.TotalRecordsProcessed);
                UtilityFunctions.WriteToLog(skippedJobslogMessage);

                Console.Write("Finished processing service review jobs... \n");
            }
            catch (Exception ex)
            {
                UtilityFunctions.WriteToLog(ex.Message);
            }
        }

        private bool IsWeeked {
            get
            {
                return DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday;
            }
        }
    
    }
}
