using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Data.Helpers;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Advice.Domain.RepositoryContracts;

namespace Advice.Data.Repository
{
    public class JobRepository : AdviceRepository<Job>, IJobRepository
    {
        public JobRepository(IAdviceDbContextManager adviceDbContextManager)
            : base(adviceDbContextManager)
        {
        }

        public IEnumerable<Job> GetOpenJobsByNatureOfAdviceGroupAndLastActionDate( DateTime lastActionDate)
        {
            //entity framework was generating duplicate joins so has to use raw query
            const string sql = "Select j.* from [Advice].[dbo].[Job] j inner join NatureOfAdvice n on j.CurrentNatureOfAdviceID = n.NatureOfAdviceID inner join NatureOfAdviceGroup nn on n.NatureOfAdviceGroupID = nn.NatureOfAdviceGroupID where  j.LastactionDate < @lastActionDateOpenJobs and j.Closed = 0 and j.deleted = 0 and nn.Deleted = 0 and (j.ProActiveCallBackCreated = null or j.ProActiveCallBackCreated = 0) and nn.NatureOfAdviceGroupID in (@noaCapabilityParam, @noaGrievanceParam,@noaRedundancyParam,@noaSosrParam, @noaConductParam)";
            var lastActionDateOpenJobsParam = new SqlParameter("@lastActionDateOpenJobs", lastActionDate);
            var noaCapabilityParam = new SqlParameter("@noaCapabilityParam", (int) NatureOfAdviceGroupId.Capability);
            var noaGrievanceParam = new SqlParameter("@noaGrievanceParam", (int)NatureOfAdviceGroupId.Grievance);
            var noaRedundancyParam = new SqlParameter("@noaRedundancyParam", (int)NatureOfAdviceGroupId.Redundancy);
            var noaSosrParam = new SqlParameter("@noaSosrParam", (int)NatureOfAdviceGroupId.SOSR);
            var noaConductParam = new SqlParameter("@noaConductParam", (int)NatureOfAdviceGroupId.Conduct);
            return Context.Jobs.SqlQuery(sql, lastActionDateOpenJobsParam, noaCapabilityParam, noaGrievanceParam, noaRedundancyParam, noaSosrParam, noaConductParam);
        }

        public IEnumerable<Job> GetOpenJobsForWorkingUserGroupByNatureOfAdviceGroupAndLastActionDate(DateTime lastActionDate)
        {           
            return Context
                .Jobs
                .Where(job => !job.Closed
                              && !job.Deleted  
                              && job.LastActionDate < lastActionDate                             
                              && !job.NatureOfAdvice.NatureOfAdviceGroup.Deleted
                              && (job.ProActiveCallBackCreated == null || job.ProActiveCallBackCreated == false)
                              && NatureOfAdviceHelper.NatureOfAdviceWorkingUserGroup.Contains(job.LastModifiedBy) 
                              && NatureOfAdviceHelper.NatureOfAdviceGroups.Contains((NatureOfAdviceGroupId)job.NatureOfAdvice.NatureOfAdviceGroup.NatureOfAdviceGroupID)
                    )              
                .ToList();
        }         

        public IEnumerable<Job> GetOpenJobsByNonNatureOfAdviceGroupAndLastActionDate(DateTime lastActionDate)
        {
            //entity framework was generating duplicate joins so has to use raw query
            const string sql = "Select j.* from [Advice].[dbo].[Job] j inner join NatureOfAdvice n on j.CurrentNatureOfAdviceID = n.NatureOfAdviceID inner join NatureOfAdviceGroup nn on n.NatureOfAdviceGroupID = nn.NatureOfAdviceGroupID where  j.LastactionDate < @lastActionDate and j.Closed = 0 and j.deleted = 0 and nn.Deleted = 0 and (j.ProActiveCallBackCreated = null or j.ProActiveCallBackCreated = 0) and nn.NatureOfAdviceGroupID in (@nonNoaAbsenceParam, @nonNoaDiscriminationParam, @nonNoaFamilyFriendlyEntitlementsParam, @nonNoaGeneralParam, @nonNoaYoungPersonWorkingParam, @nonNoaOtherParam, @nonNoaP00Param, @nonNoaPxxParam, @nonNoaRetirementParam, @nonNoaTermsAndConditionsParam, @nonNoaTradeUnionParam, @nonNoaCollectiveAgreementParam, @nonNoaPayrollParam, @nonNoaHrFace2FaceParam, @nonNoaNonUtilisationParam, @nonNoaProActiveCaseManagementParam, @nonNoaMiscGroupParam, @nonNoaEarlyConciliationParam, @nonNoaDocumentationGeneralParam, @nonNoaDocumentationPiwParam)";
            var lastActionDateParam = new SqlParameter("@lastActionDate", lastActionDate);
            
            var nonNoaAbsenceParam = new SqlParameter("@nonNoaAbsenceParam", (int)NatureOfAdviceGroupId.Absence);
            var nonNoaDiscriminationParam = new SqlParameter("@nonNoaDiscriminationParam", (int)NatureOfAdviceGroupId.Discrimination);
            var nonNoaFamilyFriendlyEntitlementsParam = new SqlParameter("@nonNoaFamilyFriendlyEntitlementsParam", (int)NatureOfAdviceGroupId.FamilyFriendlyEntitlements);
            var nonNoaGeneralParam = new SqlParameter("@nonNoaGeneralParam", (int)NatureOfAdviceGroupId.General);
            var nonNoaYoungPersonWorkingParam = new SqlParameter("@nonNoaYoungPersonWorkingParam", (int)NatureOfAdviceGroupId.YoungPersonWorking);
            var nonNoaOtherParam = new SqlParameter("@nonNoaOtherParam", (int)NatureOfAdviceGroupId.Other);
            var nonNoaP00Param = new SqlParameter("@nonNoaP00Param", (int)NatureOfAdviceGroupId.P00);
            var nonNoaPxxParam = new SqlParameter("@nonNoaPxxParam", (int)NatureOfAdviceGroupId.PXX);
            var nonNoaRetirementParam = new SqlParameter("@nonNoaRetirementParam", (int)NatureOfAdviceGroupId.Retirement);
            var nonNoaTermsAndConditionsParam = new SqlParameter("@nonNoaTermsAndConditionsParam", (int)NatureOfAdviceGroupId.TermsAndConditions);
            var nonNoaTradeUnionParam = new SqlParameter("@nonNoaTradeUnionParam", (int)NatureOfAdviceGroupId.TradeUnion);
            var nonNoaCollectiveAgreementParam = new SqlParameter("@nonNoaCollectiveAgreementParam", (int)NatureOfAdviceGroupId.CollectiveAgreement);
            var nonNoaPayrollParam = new SqlParameter("@nonNoaPayrollParam", (int)NatureOfAdviceGroupId.Payroll);
            var nonNoaHrFace2FaceParam = new SqlParameter("@nonNoaHrFace2FaceParam", (int)NatureOfAdviceGroupId.Hrface2face);
            var nonNoaNonUtilisationParam = new SqlParameter("@nonNoaNonUtilisationParam", (int)NatureOfAdviceGroupId.NonUtilisation);
            var nonNoaProActiveCaseManagementParam = new SqlParameter("@nonNoaProActiveCaseManagementParam", (int)NatureOfAdviceGroupId.ProActiveCaseManagement);
            var nonNoaMiscGroupParam = new SqlParameter("@nonNoaMiscGroupParam", (int)NatureOfAdviceGroupId.MiscGroup);
            var nonNoaEarlyConciliationParam = new SqlParameter("@nonNoaEarlyConciliationParam", (int)NatureOfAdviceGroupId.EarlyConciliation);
            var nonNoaDocumentationGeneralParam = new SqlParameter("@nonNoaDocumentationGeneralParam", (int)NatureOfAdviceGroupId.DocumentationGeneral);
            var nonNoaDocumentationPiwParam = new SqlParameter("@nonNoaDocumentationPiwParam", (int)NatureOfAdviceGroupId.DocumentationPIW);

            return Context.Jobs.SqlQuery(sql, 
                lastActionDateParam
                ,nonNoaAbsenceParam
                ,nonNoaDiscriminationParam
                ,nonNoaFamilyFriendlyEntitlementsParam
                ,nonNoaGeneralParam
                ,nonNoaYoungPersonWorkingParam
                ,nonNoaOtherParam
                ,nonNoaP00Param
                ,nonNoaPxxParam
                ,nonNoaRetirementParam
                ,nonNoaTermsAndConditionsParam 
                ,nonNoaTradeUnionParam
                ,nonNoaCollectiveAgreementParam
                ,nonNoaPayrollParam
                ,nonNoaHrFace2FaceParam
                ,nonNoaNonUtilisationParam
                ,nonNoaProActiveCaseManagementParam
                ,nonNoaMiscGroupParam
                ,nonNoaEarlyConciliationParam
                ,nonNoaDocumentationGeneralParam
                ,nonNoaDocumentationPiwParam
                );
        }

        public IEnumerable<Job> GetOpenJobsForWorkingUserGroupByNonNatureOfAdviceGroupAndLastActionDate(DateTime lastActionDate)
        {           
            return Context.Jobs.Where(job => !job.Closed
                                             && !job.Deleted
                                             && job.LastActionDate < lastActionDate                                             
                                             && !job.NatureOfAdvice.NatureOfAdviceGroup.Deleted 
                                             && (job.ProActiveCallBackCreated == null || job.ProActiveCallBackCreated == false )
                                             && NatureOfAdviceHelper.NatureOfAdviceWorkingUserGroup.Contains(job.LastModifiedBy) 
                                             && NatureOfAdviceHelper.NonNatureOfAdviceGroups.Contains((NatureOfAdviceGroupId)job.NatureOfAdvice.NatureOfAdviceGroup.NatureOfAdviceGroupID))                                             
                                              .ToList();
        }

        public IEnumerable<Job> GetOpenConductSuspensionJobsByLastActionDate(DateTime lastActionDate)
        {
            //entity framework was generating duplicate joins so has to use raw query
            const string sql = "Select  j.* from [Advice].[dbo].[Job] j inner join NatureOfAdvice n on j.CurrentNatureOfAdviceID = n.NatureOfAdviceID inner join NatureOfAdviceGroup nn on n.NatureOfAdviceGroupID = nn.NatureOfAdviceGroupID where  j.LastactionDate < @lastActionDateConductSuspension and j.Closed = 0 and j.deleted = 0 and nn.Deleted = 0 and (j.ProActiveCallBackCreated = null or j.ProActiveCallBackCreated = 0) and n.NatureOfAdviceID = @natureofAdviceId";
            var lastActionDateConductSuspensionParam = new SqlParameter("@lastActionDateConductSuspension", lastActionDate);
            var noaConductSuspensionParam = new SqlParameter("@natureofAdviceId", (int)NatureOfAdvices.ConductSuspension);
            return Context.Jobs.SqlQuery(sql, lastActionDateConductSuspensionParam, noaConductSuspensionParam);

           
        }

        public IEnumerable<Job> GetOpenConductSuspensionJobsForWorkingUserGroupByLastActionDate(DateTime lastActionDate)
        {           
            return Context
                    .Jobs
                    .Where(job => job.Closed == false
                            && !job.Deleted
                            && job.LastActionDate < lastActionDate                           
                            && (job.ProActiveCallBackCreated == null || job.ProActiveCallBackCreated == false)
                            && NatureOfAdviceHelper.NatureOfAdviceWorkingUserGroup.Contains(job.LastModifiedBy) 
                            && (NatureOfAdvices)job.NatureOfAdvice.NatureOfAdviceID == NatureOfAdvices.ConductSuspension
                    )                  
                    .ToList();
        }
    }
}
