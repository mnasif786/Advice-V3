using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advice.Domain.Entities;


namespace Advice.Common.Models
{
    public class TaskSearchResultModel
    {
        public TaskTimelineModel Timeline { get; set; }
        public IEnumerable<TaskModel> Tasks { get; set; }

        public static TaskSearchResultModel Create(IEnumerable<TaskModel> tasks, TaskTimelineModel timeline)
        {
            return new TaskSearchResultModel
            {
                Tasks = tasks,
                Timeline = timeline
            };
        }

    }
}
