using System;
using System.Collections;
using System.Collections.Generic;
using Advice.Application.Contracts;
using Advice.Common.Models;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;
using System.Linq;

namespace Advice.Application.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
      
        public IEnumerable<TeamModel> GetTeamsByDepartmentId(long departmentId)
        {
            //NA: we do need to send two separate quries to get teams at the moment.
            //One to get special out of hour teams etc. These are marked as deleted in DB as Advice V2 consider Deleted flag as special service out of hour team.
            //This is not a correct solution in AV2. However, If we change the deleted flag to false then AdviceV2 will be broken.

            //Correct Solution: We need to have Special DB flag set in Team table in Advice database and un-delete(change the delete flag to false) for special services team in the database.
            //In this way we only need to send One query to database which would fetch the records by departmentId.However, we will have to wait until AV3 rolls out properly to Live. 

            //This Special team query will be removed in future according to the solution suggested above.
            var specialServiceTeams = _teamRepository
                        .GetSpecialServiceTeamsByDepartmentId(departmentId)
                        .Select(TeamModel.CreateFromTeam);

            var teams = _teamRepository
                        .GetTeamsByDepartmentId(departmentId)
                        .Select(TeamModel.CreateFromTeam);



            return specialServiceTeams.Concat(teams); 
        }

        public IEnumerable<TeamModel> GetAllTeamsWithDivisionAndDepartment()
        {
            return _teamRepository.GetAllTeamsWithDivisionAndDepartment().Select(TeamModel.CreateFromTeam);
        }

        public void UpdateTeam(AddEditTeamModel teamModel, string userName)
        {
            var team = _teamRepository.GetById(teamModel.TeamId);
            team.EditTeam(teamModel.Description, teamModel.DivisionId, teamModel.DepartmentId, userName);
            _teamRepository.Update(team);
            _teamRepository.SaveChanges();
        }

        public void AddTeam(AddEditTeamModel teamModel, string userName)
        {
            var team = Team.Create(teamModel.Description, teamModel.DivisionId, teamModel.DepartmentId, userName);
            _teamRepository.Insert(team);
            _teamRepository.SaveChanges();
        }

        public void DeleteTeam(int teamId, string userName)
        {
            var team = _teamRepository.GetById(teamId);
            team.DeleteTeam(userName);
            _teamRepository.Update(team);
            _teamRepository.SaveChanges();
        }

        public void ReinstateTeam(int teamId, string userName)
        {
            var team = _teamRepository.GetById(teamId);
            team.ReinstateTeam(userName);
            _teamRepository.Update(team);
            _teamRepository.SaveChanges();
        }

        public bool AnyUserAssociatedWithTeam(int teamId)
        {
            return _teamRepository.AnyUserAssociatedWithTeam(teamId);
        }

    }
}
