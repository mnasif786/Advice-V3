using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Data.CustomExceptions;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Advice.Domain.RepositoryContracts;

namespace Advice.Data.Repository
{
    public class TeamRepository : AdviceRepository<Team>, ITeamRepository{
       
        public TeamRepository(IAdviceDbContextManager adviceDbContextManager): base(adviceDbContextManager){
            
        }

        public IEnumerable<Team> GetTeamsByDepartmentId(long departmentId)
        {
            var teams = Context.Teams.Where(t => t.DepartmentID == departmentId && !t.Deleted).OrderBy(o=>o.Description);
            return teams;
        }

        public IEnumerable<Team> GetSpecialServiceTeamsByDepartmentId(long departmentId)
        {
            var specialServiceTeams = new List<long>
            {
                (long) SpecialServiceTeams.FreeAdviceSalesSupport, 
                (long) SpecialServiceTeams.CorporateOutofHours,
                (long) SpecialServiceTeams.GbOutOfHours,
                (long) SpecialServiceTeams.HsOutOfHours,
                (long) SpecialServiceTeams.NiOutOfHours,
                //(long) SpecialServiceTeams.RoiOutOfHours, Not required. trello ticket #336
                (long) SpecialServiceTeams.TaxAdvice,
                (long) SpecialServiceTeams.VatAdvice
            };

            var teams = Context.Teams
                .Where(t => t.DepartmentID == departmentId && specialServiceTeams.Contains(t.TeamID))
                .OrderBy(o => o.Description);

            return teams;
        }

        public long? GetTeamIdByTeamName(string teamName)
        {
            try
            {
                var team = Context.Teams.First(t => t.Description.ToLower() == teamName.ToLower());

                if (team == null)
                    return null;

                return team.TeamID;
            }
            catch (Exception ex)
            {
                throw new TeamNameNotFoundException(teamName, ex.Message);
            }
        }

        public Team GetTeamIdByTeamId(long teamId)
        {
            try
            {
                return Context.Teams.Single(t=>t.TeamID == teamId);
                
            }
            catch (Exception ex)
            {
                throw new TeamNameNotFoundException(teamId.ToString(), ex.Message);
            }
        }

        public IEnumerable<Team> GetAllTeamsWithDivisionAndDepartment()
        {
            return Context.Teams.Include(d => d.Division).Include(dep => dep.Department);
        }

        public bool AnyUserAssociatedWithTeam(int teamId)
        {
            var user = Context.Users.FirstOrDefault(u => u.TeamID == teamId && !u.Deleted);
            return user != null;
        }
    }
}
