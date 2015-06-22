using System;
using System.Globalization;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Advice.Common.Extensions;


namespace Advice.Common.Models
{
    public class TeamModel
    {
        public long? TeamId { get; set; }
        public string Description { get; set; }
        public long? DepartmentId { get; set; }
        public long? ManagerId { get; set; }
        public string DepartmentDescription { get; set; }
        public int DivisionId { get; set; }
        public string DivisionDescription { get; set; }

        public bool Deleted { get; set; }

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
                           //TeamId.Value == (long) SpecialServiceTeams.RoiOutOfHours ||
                           TeamId.Value == (long) SpecialServiceTeams.TaxAdvice ||
                           TeamId.Value == (long) SpecialServiceTeams.VatAdvice);
                //return DepartmentId.HasValue && 
                //    (DepartmentId == (long)Departments.EmploymentServices || DepartmentId == (long)Departments.HealthAndSafety || DepartmentId == (long) Departments.Taxwise);

            }
        }

        private const long DEVELOPMENT_TEAM_ID = 1;
        public bool IsDevelopmentTeam()
        {
            return TeamId == DEVELOPMENT_TEAM_ID;
        }

        public static TeamModel CreateFromTeam(Team team)
        {
            if (team == null) 
                return null;
            
            

            var teamModel = new TeamModel()
            {
                TeamId = team.TeamID,
                DepartmentId = team.DepartmentID,
                ManagerId = team.ManagerID,
                DivisionId = team.DivisionId ?? 0,
                DivisionDescription = team.Division != null ? team.Division.Description : string.Empty,
                DepartmentDescription = team.Department != null? team.Department.Description: string.Empty,
                Deleted = team.Deleted
            };

            if (teamModel.IsSpecial)
            {
                var specialServiceTeam = (SpecialServiceTeams)Enum.Parse(typeof(SpecialServiceTeams), team.TeamID.ToString());
                teamModel.Description = specialServiceTeam.GetDescription();
            }
            else
            {
                teamModel.Description = team.Description;    
            }
            
            return teamModel;
        }
    }
}
