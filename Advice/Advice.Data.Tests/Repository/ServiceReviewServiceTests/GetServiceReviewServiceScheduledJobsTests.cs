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
    public class GetServiceReviewServiceScheduledJobsTests: BaseServiceReviewServiceTests
    {
        [SetUp]
        public new void SetUp()
        {
            var dbSetMockScheduledJobs = GetMockedScheduledJobs();
            AdviceEntities.Setup(x => x.ServiceReviewServiceSchedules).Returns(dbSetMockScheduledJobs.Object);
            AdviceEntities.Setup(x => x.Set<ServiceReviewServiceSchedule>()).Returns(dbSetMockScheduledJobs.Object);
        }

        [Test]
        public void given_when_service_review_schedule_is_requested_on_day_1_and_month_1_then_return_jobs_for_this_day()
        {
            var jobs = ServiceReviewServiceJobScheduleRepository.GetScheduledJobs(1, 1);
            Assert.That(jobs.ToList().Count, Is.EqualTo(1));
        }

        [Test]
        public void given_when_service_review_schedule_is_requested_on_day_1_and_month_2_then_return_jobs_for_this_day()
        {
            var jobs = ServiceReviewServiceJobScheduleRepository.GetScheduledJobs(1, 2);
            Assert.That(jobs.ToList().Count, Is.EqualTo(2));
        }

        [Test]
        public void given_when_service_review_schedule_is_requested_on_day_4_and_month_3_and_schedule_job_is_not_yet_run_return_jobs_for_this_day()
        {
            var jobs = ServiceReviewServiceJobScheduleRepository.GetScheduledJobs(4, 3);
            Assert.That(jobs.ToList().Count, Is.EqualTo(1));
        }

        [Test]
        public void given_when_service_review_schedule_is_requested_on_day_5_and_month_3_and_schedule_job_is_already_run_on_same_day_then_do_not_return_jobs_for_this_days()
        {
            var jobs = ServiceReviewServiceJobScheduleRepository.GetScheduledJobs(5, 3);
            Assert.That(jobs.ToList().Count, Is.EqualTo(0));
        }

        private Mock<System.Data.Entity.DbSet<ServiceReviewServiceSchedule>> GetMockedScheduledJobs()
        {
            var scheduledJobs = new List<ServiceReviewServiceSchedule>
            {
                new ServiceReviewServiceSchedule {DayToRun = 1, MonthToRun = 1, Deleted = false, ServiceReviewServiceCansGroupId = 1, ServiceReviewServiceScheduleId = 1},
                new ServiceReviewServiceSchedule {DayToRun = 2, MonthToRun = 1, Deleted = false, ServiceReviewServiceCansGroupId = 2, ServiceReviewServiceScheduleId = 2},
                new ServiceReviewServiceSchedule {DayToRun = 3, MonthToRun = 1, Deleted = false, ServiceReviewServiceCansGroupId = 3, ServiceReviewServiceScheduleId = 3},
                new ServiceReviewServiceSchedule {DayToRun = 1, MonthToRun = 2, Deleted = false, ServiceReviewServiceCansGroupId = 4, ServiceReviewServiceScheduleId = 4},
                new ServiceReviewServiceSchedule {DayToRun = 1, MonthToRun = 2, Deleted = false, ServiceReviewServiceCansGroupId = 5, ServiceReviewServiceScheduleId = 5},
                new ServiceReviewServiceSchedule {DayToRun = 1, MonthToRun = 2, Deleted = true, ServiceReviewServiceCansGroupId = 5, ServiceReviewServiceScheduleId = 6},
                new ServiceReviewServiceSchedule {DayToRun = 4, MonthToRun = 3, Deleted = false, ServiceReviewServiceCansGroupId = 6, ServiceReviewServiceScheduleId = 7},
                new ServiceReviewServiceSchedule {DayToRun = 5, MonthToRun = 3, Deleted = false, ServiceReviewServiceCansGroupId = 7, ServiceReviewServiceScheduleId = 8, LastRun = DateTime.Now} 
                    
            };

            return DbSetInitialisedMockFactory<ServiceReviewServiceSchedule>.CreateDbSetInitalisedMock(scheduledJobs);
        }
    }
}
