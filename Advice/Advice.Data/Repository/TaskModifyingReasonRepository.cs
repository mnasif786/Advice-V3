using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Data.CustomExceptions;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Advice.Domain.RepositoryContracts;

namespace Advice.Data.Repository
{
    public class TaskModifyingReasonRepository : AdviceRepository<Team>, ITaskModifyingReasonRepository
    {

        public TaskModifyingReasonRepository(IAdviceDbContextManager adviceDbContextManager)
            : base(adviceDbContextManager)
        {
            
        }


        public IEnumerable<TaskModifyingReason> GetTaskModifyingReasonsByGroupId(TaskModifyingReasonGroups taskModifyingReasonGroupId)
        {
            return
                Context.TaskModifyingReasons.Where(
                    group => group.TaskModifyingReasonGroupID == (long)taskModifyingReasonGroupId && !group.Deleted);
        }
    }
}
