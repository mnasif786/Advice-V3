using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Contracts;
using Advice.Domain;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Advice.Domain.RepositoryContracts;
using StructureMap.Diagnostics;
using ActionType = Advice.Domain.Entities.Enums.ActionType;


namespace Advice.Data.Repository
{
    public class CustomEntityRepository : ICustomEntityRepository
    {
        private readonly AdviceEntities _context;

        public AdviceEntities Context { get { return _context; } }

        public CustomEntityRepository(IAdviceDbContextManager adviceDbContextManager)
        {
            _context = adviceDbContextManager.Context;
        }

        public IEnumerable<RecentCustomerAction> GetRecentCustomersAction(string userName)
        {
            var allowedActionTypes = new List<long>() { (long) ActionType.CaseReview, (long) ActionType.Critique, (long) ActionType.DocumentReview };
            var allowedActionGroups = new List<long>() { (long) ActionTypeGroupType.Call, (long) ActionTypeGroupType.Email };
            var customersAction = from action in Context.Actions.Where(act=>act.CreatedBy == userName)
                                   join job in Context.Jobs
                                    on action.JobID equals job.JobID
                                   join actionTypeGroup in
                                    Context.ActionTypeGroups
                                    on action.ActionTypeID equals actionTypeGroup.ActionTypeId
                                  where (allowedActionTypes.Contains(action.ActionTypeID) || allowedActionGroups.Contains(actionTypeGroup.ActionTypeGroupId))
                                        &&  action.CreatedDate >= DbFunctions.AddHours(DateTime.Now, -24)
                                  select new {job.ClientID, action.CreatedDate} into recentCustomerAction
                                  group recentCustomerAction by new { recentCustomerAction.ClientID } into recentCusotomerActionGroup
                                  select new RecentCustomerAction()
                                  {
                                      CustomerId = recentCusotomerActionGroup.Key.ClientID,
                                      ActionDate = recentCusotomerActionGroup.Max(m=>m.CreatedDate)
                                  };
            return customersAction;
        }

        ///  <summary>
        ///  Gets advisor with most actions on the job
        /// If more than one advisor have same no. of actions on the job, then gets the most recent advisor
        /// if the job has not got any action then it will return the user who created the job
        ///  </summary>
        ///  <param name="jobId">jobId</param>
        /// <param name="jobCreatedBy">Person who created the job</param>
        /// <returns>AdvisorName</returns>
        public string GetAdvisorNameWithMostRecentActionsByJobId(long jobId, string jobCreatedBy)
        {
            //Gets advisor with most actions on the job
            //If more than one advisor have same no. of actions on the job, then gets the most recent advisor
            //if the job has not got any action then it will return the user who created the job

            var assignedUser = Context.Actions.Where(j => j.JobID == jobId && !j.Deleted)

                               .GroupBy(p => p.LastModifiedBy)

                               .Select(g => new { LastModifiedBy = g.Key, ActionCount = g.Count(), LatestModifiedDate = g.Max(l => l.LastModifiedDate) })

                               .Where(a => a.ActionCount == Context.Actions.Where(j => j.JobID == jobId && !j.Deleted)
                                                            .GroupBy(p => p.LastModifiedBy)
                                                            .Select(g => new { LastModifiedBy = g.Key, ActionCount = g.Count() })
                                                            .Max(l => l.ActionCount))

                               .OrderByDescending(l => l.LatestModifiedDate).FirstOrDefault();

            if (assignedUser != null)
            {
                return assignedUser.LastModifiedBy;
            }

            return jobCreatedBy;            
        }
    }
}
