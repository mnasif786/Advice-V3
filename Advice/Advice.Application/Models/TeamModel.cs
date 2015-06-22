using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;

namespace Advice.Application.Models
{
    public class TeamModel
    {
        public long? TeamId { get; set; }
        public string Description { get; set; }
        public long? DepartmentId { get; set; }
        public long? ManagerId { get; set; }

        public bool IsSpecial {
            get
            {
                return TeamId.HasValue &&
                       (
                           TeamId.Value == (long) SpecialServiceTeams.CorporateOutofHours ||
                           TeamId.Value == (long) SpecialServiceTeams.FreeAdviceSalesSupport ||
                           TeamId.Value == (long) SpecialServiceTeams.GbOutOfHours ||
                           TeamId.Value == (long) SpecialServiceTeams.HsOutOfHours ||
                           TeamId.Value == (long) SpecialServiceTeams.NiOutOfHours ||
                           TeamId.Value == (long) SpecialServiceTeams.RoiOutOfHours ||
                           TeamId.Value == (long) SpecialServiceTeams.TaxAdvice ||
                           TeamId.Value == (long) SpecialServiceTeams.VatAdvice);
                //return DepartmentId.HasValue && 
                //    (DepartmentId == (long)Departments.EmploymentServices || DepartmentId == (long)Departments.HealthAndSafety || DepartmentId == (long) Departments.Taxwise);

            }
        }


        public static TeamModel CreateFromTeam(Team team)
        {
            if (team == null) 
                return null;

            var teamModel = new TeamModel()
            {
                TeamId = team.TeamID,
                Description = team.Description,
                DepartmentId = team.DepartmentID,
                ManagerId = team.ManagerID,
                
            };
            
            return teamModel;
        }
    }
}
