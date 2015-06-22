using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Data.Repository;
using Advice.Data.Tests.TestHelpers;
using Advice.Domain;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Advice.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using Action = Advice.Domain.Entities.Action;

namespace Advice.Data.Tests.Common
{
    public abstract class BaseJobRepositoryTests
    {
        private IAdviceDbContextManager _adviceDbContextManager;
        protected Mock<AdviceEntities> AdviceEntities;
        protected IJobRepository JobRepository;
        private List<Job> _jobs;

        [SetUp]
        protected void BaseSetUp()
        {
            var noaGroupData = new List<NatureOfAdviceGroup>()
            {
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.Capability,      Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.CorporationTax,  Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.Absence,         Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.Discrimination,  Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.FamilyFriendlyEntitlements,  Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.General,                     Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.YoungPersonWorking,          Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.Other,                       Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.P00,  Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.PXX,  Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.Retirement,  Deleted = false}, // 10
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.TermsAndConditions,  Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.TradeUnion,  Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.CollectiveAgreement,  Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.Payroll,  Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.Hrface2face,  Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.NonUtilisation,  Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.ProActiveCaseManagement,  Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.MiscGroup,  Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.EarlyConciliation,  Deleted = false},
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.DocumentationGeneral,  Deleted = false}, //20
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.DocumentationPIW,  Deleted = false},
                
                new NatureOfAdviceGroup() {NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.Conduct,  Deleted = false},
            };

            var noaData = new List<NatureOfAdvice>()
            {
                new NatureOfAdvice() {NatureOfAdviceID = 1, NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.Absence,                    NatureOfAdviceGroup = noaGroupData[2]}, 
                new NatureOfAdvice() {NatureOfAdviceID = 2, NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.CorporationTax,             NatureOfAdviceGroup = noaGroupData[0]}, 
                new NatureOfAdvice() {NatureOfAdviceID = 3, NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.Absence,                    NatureOfAdviceGroup = noaGroupData[2]}, 
                new NatureOfAdvice() {NatureOfAdviceID = 4, NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.Capability,                 NatureOfAdviceGroup = noaGroupData[1]}, 
                new NatureOfAdvice() {NatureOfAdviceID = 5, NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.Discrimination,             NatureOfAdviceGroup = noaGroupData[3]}, 
                new NatureOfAdvice() {NatureOfAdviceID = 6, NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.FamilyFriendlyEntitlements, NatureOfAdviceGroup = noaGroupData[4]}, 
                new NatureOfAdvice() {NatureOfAdviceID = 7, NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.General,                    NatureOfAdviceGroup = noaGroupData[5]}, 
                new NatureOfAdvice() {NatureOfAdviceID = 8, NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.YoungPersonWorking,         NatureOfAdviceGroup = noaGroupData[6]}, 

                new NatureOfAdvice() {NatureOfAdviceID = (long)NatureOfAdvices.ConductSuspension,   NatureOfAdviceGroupID = (long)NatureOfAdviceGroupId.Conduct, NatureOfAdviceGroup = noaGroupData[22]}
            };
           
            _jobs = new List<Job>()
            {
                new Job() { Subject = "111 - NOAGroup:Capability - NOA:CorporationTax - 8 days Ago", 
                            JobID = 111, Deleted = false, Closed = false, LastActionDate = DateTime.Today.AddDays(-8),  CurrentNatureOfAdviceID = 2, NatureOfAdvice = noaData[1], ProActiveCallBackCreated = false },

                new Job() { Subject = "222 - NOAGroup:Absence - NOA:Absence - 8 days Ago",
                            JobID = 222, Deleted = false, Closed = false, LastActionDate = DateTime.Today.AddDays(-8),  CurrentNatureOfAdviceID = 3, NatureOfAdvice = noaData[2], ProActiveCallBackCreated = false },

                new Job() { Subject = "888 - NOAGroup:Capability - NOA:CorporationTax - 10 days Ago -  CLOSED - ",
                            JobID = 888, Deleted = false, Closed = true,  LastActionDate = DateTime.Today.AddDays(-10), CurrentNatureOfAdviceID = 2, NatureOfAdvice = noaData[1], ProActiveCallBackCreated = false },

                new Job() { Subject = "999 - NOAGroup:CorporationTax - NOA:Capability - 8 days Ago",
                            JobID = 999, Deleted = false, Closed = false, LastActionDate = DateTime.Today.AddDays(-4),  CurrentNatureOfAdviceID = 4, NatureOfAdvice = noaData[3], ProActiveCallBackCreated = false },
                

                new Job() { Subject = "333 - NOAGroup:Absence - NOA:Absence - 32 days Ago",
                            JobID = 333, Deleted = false, Closed = false, LastActionDate = DateTime.Today.AddDays(-32), CurrentNatureOfAdviceID = 3, NatureOfAdvice = noaData[2], ProActiveCallBackCreated = false },
                
                new Job() { Subject = "555 - NOAGroup:Discrimination - NOA:Discrimination - 33 days Ago",
                            JobID = 555, Deleted = false, Closed = false, LastActionDate = DateTime.Today.AddDays(-33), CurrentNatureOfAdviceID = 5, NatureOfAdvice = noaData[4], ProActiveCallBackCreated = false },
                
                new Job() { Subject = "777 - NOAGroup:FamilyFriendlyEntitlements - NOA:FamilyFriendlyEntitlements - 34 days Ago",
                            JobID = 777, Deleted = false, Closed = false, LastActionDate = DateTime.Today.AddDays(-34), CurrentNatureOfAdviceID = 6, NatureOfAdvice = noaData[5], ProActiveCallBackCreated = false },

                
                // Suspension related jobs
                new Job() { Subject = "1000 - NOAGroup: Conduct - NOA:ConductSuspension - 3 days Ago",
                            JobID = 1000, Deleted = false, Closed = false, LastActionDate = DateTime.Today.AddDays(-3), CurrentNatureOfAdviceID = (long)NatureOfAdvices.ConductSuspension, NatureOfAdvice = noaData[8], ProActiveCallBackCreated = false },

                new Job() { Subject = "1001 - NOAGroup: Conduct - NOA:ConductSuspension - 3 days Ago - CLOSED",
                            JobID = 1001, Deleted = false, Closed = true, LastActionDate = DateTime.Today.AddDays(-3), CurrentNatureOfAdviceID = (long)NatureOfAdvices.ConductSuspension, NatureOfAdvice = noaData[8], ProActiveCallBackCreated = false },
    
                new Job() { Subject = "1002 - NOAGroup: Conduct - NOA:ConductSuspension - 1 days Ago",
                            JobID = 1002, Deleted = false, Closed = false, LastActionDate = DateTime.Today.AddDays(-1), CurrentNatureOfAdviceID = (long)NatureOfAdvices.ConductSuspension, NatureOfAdvice = noaData[8], ProActiveCallBackCreated = false }
            };

            AdviceEntities = new Mock<AdviceEntities>();
            _adviceDbContextManager = new AdviceDbContextManager(AdviceEntities.Object);
            JobRepository = new JobRepository(_adviceDbContextManager);

            var dbSetMockNatureOfAdviceGroup = DbSetInitialisedMockFactory<NatureOfAdviceGroup>.CreateDbSetInitalisedMock(noaGroupData);
            AdviceEntities.Setup(x => x.NatureOfAdviceGroups).Returns(dbSetMockNatureOfAdviceGroup.Object);
            AdviceEntities.Setup(x => x.Set<NatureOfAdviceGroup>()).Returns(dbSetMockNatureOfAdviceGroup.Object);

            var dbSetMockNatureOfAdvice = DbSetInitialisedMockFactory<NatureOfAdvice>.CreateDbSetInitalisedMock(noaData);
            AdviceEntities.Setup(x => x.NatureOfAdvices).Returns(dbSetMockNatureOfAdvice.Object);
            AdviceEntities.Setup(x => x.Set<NatureOfAdvice>()).Returns(dbSetMockNatureOfAdvice.Object);

            var dbSetMockJob = DbSetInitialisedMockFactory<Job>.CreateDbSetInitalisedMock(_jobs);
            AdviceEntities.Setup(x => x.Jobs).Returns(dbSetMockJob.Object);
            AdviceEntities.Setup(x => x.Set<Job>()).Returns(dbSetMockJob.Object);           
        }

        //protected void InitMockJobData(List<Job> jobData)
        //{
        //    var dbSetMockJobs = DbSetInitialisedMockFactory<Job>.CreateDbSetInitalisedMock(jobData);
        //    _adviceEntities
        //        .Setup(x => x.Jobs)
        //        .Returns(dbSetMockJobs.Object);
        //}

        //protected void InitMockNatureOfAdviceGroupData(List<NatureOfAdviceGroup> natureOfAdviceGroupData)
        //{
        //    var dbSetMockNOAGroups = DbSetInitialisedMockFactory<NatureOfAdviceGroup>.CreateDbSetInitalisedMock(natureOfAdviceGroupData);
        //    _adviceEntities
        //        .Setup(x => x.NatureOfAdviceGroups)
        //        .Returns(dbSetMockNOAGroups.Object);
        //}

        //protected void InitMockNatureOfAdviceData(List<NatureOfAdvice> natureOfAdviceData)
        //{
        //    var dbSetMockNatureofAdvice = DbSetInitialisedMockFactory<NatureOfAdvice>.CreateDbSetInitalisedMock(natureOfAdviceData);
        //    _adviceEntities
        //        .Setup(x => x.NatureOfAdvices)
        //        .Returns(dbSetMockNatureofAdvice.Object);
        //}

        public JobRepository GetTarget()
        {
            return new JobRepository(_adviceDbContextManager);
        }
    }
}
