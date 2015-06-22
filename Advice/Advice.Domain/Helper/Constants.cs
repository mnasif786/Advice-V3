using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Entities.Enums;

namespace Advice.Domain.Helper
{
    public static class Constants
    {
        // TODO: Don't like hardcoded constants. This should be in a table somewhere. SGG
        public static readonly List<TaskTypeIds> ProActiveTaskTypes = new List<TaskTypeIds>() 
        {
            TaskTypeIds.FollowupTask,
            TaskTypeIds.ServiceReview,
            TaskTypeIds.ProActiveCallback
        };   
    }

    
}
