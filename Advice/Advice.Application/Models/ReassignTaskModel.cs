using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Application.Models
{
    public class ReassignTaskModel
    {
        public long TaskId { get; set; }
        public string AssignedUser { get; set; }
        public long? AssignedTeamId { get; set; }
        public bool Urgent { get; set; }
        public string Comments { get; set; }
        public string ReAssignedByUser { get; set; }
        public long? ClientId { get; set; }
        public DateTime? DueDate { get; set; }
        public long ReasonId { get; set; }
    }
}
