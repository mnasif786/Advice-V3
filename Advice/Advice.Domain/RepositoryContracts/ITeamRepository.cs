using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Common;
using Advice.Domain.Entities;

namespace Advice.Domain.RepositoryContracts
{
    public interface ITeamRepository : IAdviceRepository<Team>
    {
        IEnumerable<Team> GetTeamsByDepartmentId(long departmentId);
        long? GetTeamIdByTeamName(string teamName);

        Team GetTeamIdByTeamId(long teamId);

        IEnumerable<Team> GetSpecialServiceTeamsByDepartmentId(long departmentId);

        IEnumerable<Team> GetAllTeamsWithDivisionAndDepartment();

        bool AnyUserAssociatedWithTeam(int teamId);
    }
}
