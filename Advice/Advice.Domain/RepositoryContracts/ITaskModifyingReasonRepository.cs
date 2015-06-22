using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;

namespace Advice.Domain.RepositoryContracts
{
    public interface ITaskModifyingReasonRepository
    {
        IEnumerable<TaskModifyingReason> GetTaskModifyingReasonsByGroupId(TaskModifyingReasonGroups taskModifyingReasonGroupId);
    }
}
