using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Contracts;
using Advice.Data.Repository.Services;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;
using Advice.Domain.RepositoryContracts.Services;
using Advice.ServiceReviews.Implementations;
using Moq;
using NUnit.Framework;
using Peninsula.Domain.RepositoryContracts;

namespace Advice.ServiceReviews.Tests
{
    [TestFixture]
    public class ServiceReviewTests
    {
        public Mock<IAdviceDbContextManager> AdviceDbContextManagerMock; 
        public Mock<ITaskRepository> TaskRepositoryMock;
        public Mock<IUserRepository> UserRepositoryMock;
        public Mock<ICorporatePriorityRepository> CorporatePriorityRepositoryMock;
        public Mock<IServiceReviewServiceJobScheduleRepository> ServiceReviewServiceJobScheduleRepositoryMock;
        public Mock<ITblCustomerRepository> TblCustomerRepository;
        

        [SetUp]
        public void SetUp()
        {
            AdviceDbContextManagerMock = new Mock<IAdviceDbContextManager>();
            TaskRepositoryMock = new Mock<ITaskRepository>();
            UserRepositoryMock = new Mock<IUserRepository>();
            CorporatePriorityRepositoryMock = new Mock<ICorporatePriorityRepository>();
            ServiceReviewServiceJobScheduleRepositoryMock = new Mock<IServiceReviewServiceJobScheduleRepository>();
            TblCustomerRepository = new Mock<ITblCustomerRepository>();
        }

        [Test]
        public void Given_three_scheduled_jobs_to_be_processed_when_call_ProcessScheduledJobs_then_total_jobs_Run_count_is_3()
        {
            var jobs = GetSchdeduledJobsList();

            ServiceReviewServiceJobScheduleRepositoryMock.Setup(
                x => x.GetScheduledJobs(It.IsAny<int>(), It.IsAny<int>())).Returns(jobs);

           var serviceReviewService = GetTarget();

            var serviceResponse = serviceReviewService.ProcessScheduledJobs();

            Assert.That(serviceResponse.TotalJobsRun, Is.EqualTo(3));
        }

        [Test]
        public void Given_two_skipped_jobs_to_be_processed_when_call_ProcessScheduledJobs_then_total_jobs_Run_count_is_2()
        {
            var jobs = GetSkippedJobsList();

            ServiceReviewServiceJobScheduleRepositoryMock
                            .Setup( x => x.GetSkippedJobs(It.IsAny<DateTime>()))
                            .Returns(jobs);
            
            var serviceReviewService = GetTarget();

            var serviceResponse = serviceReviewService.ProcessSKippedJobs();

            Assert.That(serviceResponse.TotalJobsRun, Is.EqualTo(2));

        }

        [Test]
        public void  Given_three_scheduled_jobs_to_be_processed_when_call_ProcessScheduledJobs_then_total_records_processed_count_is_5()
        {
            var jobs = GetSchdeduledJobsList();

            ServiceReviewServiceJobScheduleRepositoryMock.Setup(
                x => x.GetScheduledJobs(It.IsAny<int>(), It.IsAny<int>())).Returns(jobs);

            CorporatePriorityRepositoryMock
                .Setup(x => x.GetCorporatePriorityByCans(jobs[0].ServiceReviewServiceCanGroup.Regex))
                .Returns(GetcorporatePrioritiesByCanforJob1());

            CorporatePriorityRepositoryMock
                .Setup(x => x.GetCorporatePriorityByCans(jobs[1].ServiceReviewServiceCanGroup.Regex))
                .Returns(GetcorporatePrioritiesByCanforJob2());

            CorporatePriorityRepositoryMock
                .Setup(x => x.GetCorporatePriorityByCans(jobs[2].ServiceReviewServiceCanGroup.Regex))
                .Returns(GetcorporatePrioritiesByCanforJob3());

            TaskRepositoryMock.Setup(
                x => x.GetTaskTypeSlaByTaskTypeIdAndDepartmentId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new TaskTypeSLA() { DefaultSLATime = 240 });

            var serviceReviewService = GetTarget();

            var serviceResponse = serviceReviewService.ProcessScheduledJobs();

            Assert.That(serviceResponse.TotalRecordsProcessed, Is.EqualTo(5));
            Assert.That(serviceResponse.TotalRecordsFound, Is.EqualTo(5));
            
        }

        public ServiceReviewService GetTarget()
        {
            return new ServiceReviewService(
                AdviceDbContextManagerMock.Object, 
                TblCustomerRepository.Object, 
                CorporatePriorityRepositoryMock.Object, 
                TaskRepositoryMock.Object, 
                UserRepositoryMock.Object, 
                ServiceReviewServiceJobScheduleRepositoryMock.Object);
        }

       private IEnumerable<CorporatePriorityByCansQueryResult> GetcorporatePrioritiesByCanforJob1()
        {
            var corporatePriorities = new List<CorporatePriorityByCansQueryResult>
            {
                new CorporatePriorityByCansQueryResult
                {
                    Can = "A1",
                    ContractValue = 2,
                    UserId = 1
                }
            };

            return corporatePriorities;
        }

       private List<CorporatePriorityByCansQueryResult> GetcorporatePrioritiesByCanforJob2()
        {
            var corporatePriorities = new List<CorporatePriorityByCansQueryResult>();
            corporatePriorities.Add(new CorporatePriorityByCansQueryResult { Can = "B1", ContractValue = 2, UserId = 1 });
            corporatePriorities.Add(new CorporatePriorityByCansQueryResult { Can = "B2", ContractValue = 2, UserId = 1 });

            return corporatePriorities;
        }

       private List<CorporatePriorityByCansQueryResult> GetcorporatePrioritiesByCanforJob3()
        {
            var corporatePriorities = new List<CorporatePriorityByCansQueryResult>();
            corporatePriorities.Add(new CorporatePriorityByCansQueryResult { Can = "I1", ContractValue = 2, UserId = 2 });
            corporatePriorities.Add(new CorporatePriorityByCansQueryResult { Can = "J1", ContractValue = 2, UserId = 2 });

            return corporatePriorities;
        }

        private List<ServiceReviewServiceSchedule> GetSchdeduledJobsList()
        {
            var canGroup_AA_AB = new ServiceReviewServiceCanGroup { ServiceReviewServiceCansGroupId = 1, Description = "AA_AB", Regex = "a[ab]%" };
            var canGroup_AC_AD = new ServiceReviewServiceCanGroup { ServiceReviewServiceCansGroupId = 2, Description = "AC_AD", Regex = "a[cd]%" };
            var canGroup_AE_AK = new ServiceReviewServiceCanGroup { ServiceReviewServiceCansGroupId = 3, Description = "AE-AK", Regex = "a[efghijk]%" };

            var JobsList = new List<ServiceReviewServiceSchedule>();
            JobsList.Add(new ServiceReviewServiceSchedule { ServiceReviewServiceScheduleId = 1, ServiceReviewServiceCansGroupId = 1, ServiceReviewServiceCanGroup = canGroup_AA_AB, DayToRun = 1, MonthToRun = 1, Deleted = false });
            JobsList.Add(new ServiceReviewServiceSchedule { ServiceReviewServiceScheduleId = 2, ServiceReviewServiceCansGroupId = 2, ServiceReviewServiceCanGroup = canGroup_AC_AD, DayToRun = 1, MonthToRun = 1, Deleted = false });
            JobsList.Add(new ServiceReviewServiceSchedule { ServiceReviewServiceScheduleId = 3, ServiceReviewServiceCansGroupId = 3, ServiceReviewServiceCanGroup = canGroup_AE_AK, DayToRun = 1, MonthToRun = 1, Deleted = false });


            return JobsList;
        }

        private List<ServiceReviewServiceSchedule> GetSkippedJobsList()
        {
            var canGroup_AA_AB = new ServiceReviewServiceCanGroup { ServiceReviewServiceCansGroupId = 1, Description = "AA_AB", Regex = "a[ab]%" };
            var canGroup_AC_AD = new ServiceReviewServiceCanGroup { ServiceReviewServiceCansGroupId = 2, Description = "AC_AD", Regex = "a[cd]%" };
            
            var JobsList = new List<ServiceReviewServiceSchedule>();
            JobsList.Add(new ServiceReviewServiceSchedule { ServiceReviewServiceScheduleId = 1, ServiceReviewServiceCansGroupId = 1, ServiceReviewServiceCanGroup = canGroup_AA_AB, DayToRun = 1, MonthToRun = 1, Deleted = false });
            JobsList.Add(new ServiceReviewServiceSchedule { ServiceReviewServiceScheduleId = 2, ServiceReviewServiceCansGroupId = 2, ServiceReviewServiceCanGroup = canGroup_AC_AD, DayToRun = 1, MonthToRun = 1, Deleted = false });
            
            return JobsList;
        }

    }


}
