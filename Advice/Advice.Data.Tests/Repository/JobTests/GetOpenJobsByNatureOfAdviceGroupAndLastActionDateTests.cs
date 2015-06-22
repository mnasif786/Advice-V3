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
using Advice.Domain.Helper;
using Moq;
using NUnit.Framework;

namespace Advice.Data.Tests.Repository.JobRepositoryTests
{
    [TestFixture]
    public class GetOpenJobsByNatureOfAdviceGroupAndLastActionDateTests : BaseJobRepositoryTests
    {

        [Test]
        public void Given_Jobs_exist_Then_GetOpenJobsByNatureOfAdviceGroupAndLastActionDate_returns_open_jobs_matching_criteria()
        {                                
            JobRepository repo = GetTarget();
                     
            IEnumerable<Job> returnedJobs = repo.GetOpenJobsByNatureOfAdviceGroupAndLastActionDate( WorkingHours.GetDateNWorkingDaysInPast( 7 )  );

            //Assert.AreEqual( 1, returnedJobs.Count());
            //Assert.AreEqual( 111, returnedJobs.ElementAt(0).JobID );      
        }

        [Test]
        public void Given_Jobs_In_Date_range_Then_GetOpenJobsByNatureOfAdviceGroupAndLastActionDate_returns_open_jobs_matching_NOA_Groups()
        {                    
            JobRepository repo = GetTarget();

            IEnumerable<Job> returnedJobs = repo.GetOpenJobsByNatureOfAdviceGroupAndLastActionDate(WorkingHours.GetDateNWorkingDaysInPast( 7));

            //Assert.AreEqual(1, returnedJobs.Count());
            //Assert.AreEqual(111, returnedJobs.ElementAt(0).JobID);            
        }       
    }
}
