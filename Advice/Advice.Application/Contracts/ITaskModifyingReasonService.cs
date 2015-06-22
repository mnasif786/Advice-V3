using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Common.Models;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;

namespace Advice.Application.Contracts
{
    public interface ITaskModifyingReasonService
    {
        IEnumerable<TaskModifyingReasonModel> GetTaskModifyingReasonsForResetGroup();
        IEnumerable<TaskModifyingReasonModel> GetTaskModifyingReasonsForReassignGroup();
    }
}
