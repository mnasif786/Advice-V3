using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;

namespace Advice.Data.Repository
{
    public class CorporatePriorityRepository : AdviceRepository<CorporatePriority> , ICorporatePriorityRepository
    {
        public CorporatePriorityRepository(IAdviceDbContextManager adviceDbContextManager) 
            : base(adviceDbContextManager)
        {
        }

        public IEnumerable<CorporatePriority> GetAllCorporatePriorities()
        {
            return Context.CorporatePriorities.Where(cp => !cp.Deleted).Include(u => u.User);
        }

        public CorporatePriority GetCorporatePriorityByCan(string can)
        {
            return Context.CorporatePriorities.SingleOrDefault(c => c.Can.Equals(can) && c.Deleted==false);
        }

        public IEnumerable<CorporatePriorityByCansQueryResult> GetCorporatePriorityByCans(string regex)
        {
            //This is a storeprocedure call "CorporatePriorityByCansQuery"
            return Context.CorporatePriorityByCansQuery(regex);
        }

        //public IEnumerable<CorporatePriority> GetCorporatePriorityByCaNsStartWithAlphabets_AtoI()
        //{
        //    //return Context.CorporatePriorities.SqlQuery(
        //    //    "Select CorporatePriorityId, Can, ContractValue, ContractDetail, ContractEndDate, UserId, CreatedBy,CreatedDate,LastModifiedBy,LastModifiedDate,Deleted, DeletedBy,DeletedDate  from CorporatePriority Where Deleted=0 AND Can Like '[A-I]%'")
        //    //    .AsEnumerable();

        //    // We could use above mentioned query but that could break in a case of any column added or deleted as it is not strongly typed.

        //    return
        //        Context.CorporatePriorities.Where(c => c.Deleted == false
        //            && (c.Can.StartsWith("A") ||
        //                c.Can.StartsWith("B") ||
        //                c.Can.StartsWith("C") ||
        //                c.Can.StartsWith("D") ||
        //                c.Can.StartsWith("E") ||
        //                c.Can.StartsWith("F") ||
        //                c.Can.StartsWith("G") ||
        //                c.Can.StartsWith("H") ||
        //                c.Can.StartsWith("I")
        //            )).Include(u => u.User);
        //}

      
    }
}
