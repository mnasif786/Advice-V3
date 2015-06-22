using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Domain.Entities.Enums
{
    public enum TaskModifyingReasonGroups: long
    {
        Delete              = 1,
        Reinstate           = 2,
        Reassign            = 3,
        Reset               = 4,
        Edit                = 5,
        UpdatedByAction     = 6,
        Created             = 7,
        Read                = 8,
    }
}
