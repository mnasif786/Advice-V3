using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Data.Repository;
using Advice.Data.Tests.Common;
using Advice.Data.Tests.TestHelpers;
using Advice.Domain;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Moq;
using NUnit.Framework;

namespace Advice.Data.Tests.Repository.JobRepositoryTests
{
    [TestFixture]
    public class GetOpenConductSuspensionJobsByLastActionDateTests : BaseJobRepositoryTests
    {
        // TODO: Refactor and move data to common base class
        [Test]
        public void Given_Jobs_With_Suspension_NOA_Type_Then_GetOpenJobsByNatureOfAdviceAndLastActionDate_returns_open_jobs_matching_criteria()
        {
            JobRepository repo = GetTarget();
        

            IEnumerable<Job> returnedJobs = repo.GetOpenConductSuspensionJobsByLastActionDate( DateTime.Today.AddDays(-2));

            //Assert.AreEqual( 1, returnedJobs.Count());
            //Assert.AreEqual( 1000, returnedJobs.ElementAt(0).JobID);            
        }       
             
    }
}
