using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Domain.Entities.Enums
{
    public enum TaskModifyingReasons : long
    {
        Delete = 1,
        Reinstate=2,
        Created = 15,
        Edit = 8,
        UpdatedByAction = 13,
        BulkReassigned = 16,
        AssociatedAndCompleted = 17,
        AssociatedAndCancelled = 20,
        Disassociated = 21,
        Read = 22,
        SentToOutlook = 26
    }
}
