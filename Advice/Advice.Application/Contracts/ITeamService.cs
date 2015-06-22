using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Common.Models;

namespace Advice.Application.Contracts
{
    public interface ITeamService
    {      
        IEnumerable<TeamModel> GetTeamsByDepartmentId(long departmentId);
        IEnumerable<TeamModel> GetAllTeamsWithDivisionAndDepartment();
        void UpdateTeam(AddEditTeamModel teamModel, string userName);
        void AddTeam(AddEditTeamModel teamModel, string userName);
        void DeleteTeam(int teamId, string userName);
        void ReinstateTeam(int teamId, string userName);
        bool AnyUserAssociatedWithTeam(int teamId);
    }
}
