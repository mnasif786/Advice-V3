using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Data.Helpers;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts.Services;

namespace Advice.Data.Repository.Services
{
    public class ServiceReviewServiceJobScheduleRepository : AdviceRepository<ServiceReviewServiceSchedule>, IServiceReviewServiceJobScheduleRepository
    {
        public ServiceReviewServiceJobScheduleRepository(IAdviceDbContextManager adviceDbContextManager) : base(adviceDbContextManager)
        {
        }


        public IEnumerable<ServiceReviewServiceSchedule> GetScheduledJobs(int forDay, int forMonth)
        {
            return
                Context.ServiceReviewServiceSchedules.Where(
                    j => j.DayToRun == forDay 
                        && j.MonthToRun == forMonth 
                        && j.Deleted == false
                        //By applying this filter we are making sure that jobs would not be pickedup more than once in a case if service accidentlay run more than one time on the same day
                        && (j.LastRun.HasValue == false || (DatabaseFunctions.TruncateTime(j.LastRun.Value) < DatabaseFunctions.TruncateTime(DateTime.Now)))
            ).Include( s => s.ServiceReviewServiceCanGroup);
        }

        public IEnumerable<ServiceReviewServiceSchedule> GetSkippedJobs(DateTime currentDateTime)
        {
            var currentYear = currentDateTime.Year;
            return
                Context.ServiceReviewServiceSchedules.Where(
                    j => j.Deleted == false
                        && 
                        //first filter: Picked all the jobs that are scheduled to run before current/execute date
                        (DatabaseFunctions.TruncateTime(DatabaseFunctions.CreateDateTime(currentYear, j.MonthToRun, j.DayToRun, 0, 0, 0)) < DatabaseFunctions.TruncateTime(currentDateTime))
                        //second filter: now pick all those jobs whose expected scheduled/run time has passed.
                        && (j.LastRun.HasValue == false || //not run even first time yet
                                (DatabaseFunctions.TruncateTime(j.LastRun.Value) <  //Last Run date is less than
                                    DatabaseFunctions.TruncateTime(DatabaseFunctions.CreateDateTime(currentYear, j.MonthToRun, j.DayToRun, 0, 0, 0))
                                    )) //Expected run value based on sheduled values
            ).Include(s => s.ServiceReviewServiceCanGroup);
        }
    }
}
