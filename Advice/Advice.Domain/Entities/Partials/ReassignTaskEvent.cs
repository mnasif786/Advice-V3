using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Domain.Entities
{
    public partial class ReassignTaskEvent
    {
        public static ReassignTaskEvent Create(Task task, long? previousTeamId, string previousUser)
        {
            return new ReassignTaskEvent
            {
                TaskId = task.TaskID,
                PreviousTeamId = previousTeamId,
                NewTeamId =  task.AssignedTeamID,
                PreviousUser = previousUser,
                NewUser = task.AssignedUser,
                Comment = task.LastModifiedComment,
                ReasonId = task.LastModifyingReasonID,
                ReassignedBy = task.LastModifiedBy,
                ReassignedDate = task.LastModifiedDate
            };
        }

    }
}
