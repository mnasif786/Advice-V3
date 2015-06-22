using Advice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

namespace Advice.Data.Repository
{
    public interface ITaskStoredProcRunner
    {
        IEnumerable<GetTasksByTeamIds_Type> ExecuteQuery(ObjectContext context, String storedProcName, long[] teamIds);
    }
}
