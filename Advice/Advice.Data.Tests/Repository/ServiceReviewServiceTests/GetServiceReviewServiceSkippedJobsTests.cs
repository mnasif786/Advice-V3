using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Tests.Common;
using Advice.Data.Tests.TestHelpers;
using Advice.Domain.Entities;
using Moq;
using NUnit.Framework;
using Task = Advice.Domain.Entities.Task;

namespace Advice.Data.Tests.Repository.ServiceReviewServiceTests
{
    public class GetServiceReviewServiceSkippedJobsTests: BaseServiceReviewServiceTests
    {
        //We asssume current date is 10/03/2015 for all these tests.
        private readonly DateTime _currentDateTime = new DateTime(2015, 03, 10);
        [SetUp]
        public new void SetUp()
        {
            var dbSetMockScheduledJobs = GetMockedSkippedJobs();
            AdviceEntities.Setup(x => x.ServiceReviewServiceSchedules).Returns(dbSetMockScheduledJobs.Object);
            AdviceEntities.Setup(x => x.Set<ServiceReviewServiceSchedule>()).Returns(dbSetMockScheduledJobs.Object);
        }

        [Test]
        public void given_when_GetSkippedJobs_is_called_then_only_previous_dates_jobs_are_returned()
        {
            var jobs = ServiceReviewServiceJobScheduleRepository.GetSkippedJobs(_currentDateTime);
            Assert.That(jobs.ToList().Count, Is.EqualTo(3));
        }

        private Mock<System.Data.Entity.DbSet<ServiceReviewServiceSchedule>> GetMockedSkippedJobs()
        {
            var scheduledJobs = new List<ServiceReviewServiceSchedule>
            {
                //not run first time yet
                new ServiceReviewServiceSchedule {DayToRun = 1, MonthToRun = 1, LastRun = null, Deleted = false, ServiceReviewServiceCansGroupId = 1, ServiceReviewServiceScheduleId = 1},
                //ran last year
                new ServiceReviewServiceSchedule {DayToRun = 1, MonthToRun = 2, LastRun = DateTime.Parse("1/2/"+DateTime.Now.AddYears(-1).Year), Deleted = false, ServiceReviewServiceCansGroupId = 4, ServiceReviewServiceScheduleId = 4},
                //Last run date is less than expected run/schedule date
                new ServiceReviewServiceSchedule {DayToRun = 5, MonthToRun = 3, LastRun =  DateTime.Parse("04/02/" +_currentDateTime.Year), Deleted = false, ServiceReviewServiceCansGroupId = 5, ServiceReviewServiceScheduleId = 5},
                //schduled in future
                new ServiceReviewServiceSchedule {DayToRun = 5, MonthToRun = 9, LastRun =  DateTime.Parse("05/09/" +_currentDateTime.Year), Deleted = false, ServiceReviewServiceCansGroupId = 5, ServiceReviewServiceScheduleId = 6},
                //Last run date is higher than scheduled date.
               new ServiceReviewServiceSchedule {DayToRun = 4, MonthToRun = 3, LastRun = DateTime.Parse("5/3/"+_currentDateTime.Year), Deleted = false, ServiceReviewServiceCansGroupId = 5, ServiceReviewServiceScheduleId = 7},
                
            };

            return DbSetInitialisedMockFactory<ServiceReviewServiceSchedule>.CreateDbSetInitalisedMock(scheduledJobs);
        }
    }
}
