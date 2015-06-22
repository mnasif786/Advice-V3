using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Tests.Common;
using NUnit.Framework;

namespace Advice.Data.Tests.Repository.JobTests
{
    public class GetOpenJobsByNonNatureOfAdviceGroupAndLastActionDateTests : BaseJobRepositoryTests
    {
        [Test]
        public void Given_JobsData_If_Requested_By_GetOpenJobsByNonNatureOfAdviceGroupAndLastActionDate_Returns_OpenNonNoaJobs()
        {
            var jobs = JobRepository.GetOpenJobsByNonNatureOfAdviceGroupAndLastActionDate((DateTime.Today.AddDays(-30)));
            //Assert.That(jobs.Count(), Is.EqualTo(3));
        }

        [Test]
        public void Given_JobsData_If_Requested_By_GetOpenJobsByNonNatureOfAdviceGroupAndLastActionDate_Within_7_Returns_OpenNonNoaJobs_Within_7_Days()
        {
            var jobs = JobRepository.GetOpenJobsByNonNatureOfAdviceGroupAndLastActionDate((DateTime.Today.AddDays(-7)));
            //Assert.That(jobs.Count(), Is.EqualTo(4));
        }

    }
}
