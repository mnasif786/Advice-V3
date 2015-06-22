using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Entities;

namespace Advice.Domain.RepositoryContracts
{
    public interface ICustomEntityRepository
    {
        IEnumerable<RecentCustomerAction> GetRecentCustomersAction(string userName);
        string GetAdvisorNameWithMostRecentActionsByJobId(long jobId, string jobCreatedBy);
    }
}
