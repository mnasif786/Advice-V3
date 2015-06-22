using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Common.Models
{
    public class AddEditTeamModel
    {
        public int TeamId { get; set; }
        public string Description { get; set; }
        public int DivisionId { get; set; }
        public int DepartmentId { get; set; }
    }
}
