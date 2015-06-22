using System;
using System.Collections.Generic;
using Advice.Application.Contracts;
using Advice.Common.Models;
using Advice.Domain.RepositoryContracts;
using System.Linq;

namespace Advice.Application.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly ITeamRepository _teamRepository;

        public UserService(IUserRepository userRepository, IPermissionRepository permissionRepository, ITeamRepository teamRepository)
        {
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
            _teamRepository = teamRepository;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _userRepository
                        .GetAllUsers()
                        .Select(UserModel.CreateFromUser)
                        .ToList();
        }

        public UserModel GetUserByName(string usernName)
        {
           var user = _userRepository.GetUserByName(usernName);
           return new UserModel()
           {
               UserId = user.UserID,
               RoleId = user.RoleID,
               TeamId = user.TeamID,
               Username = user.Username
           };
        }

        public UserModel GetUserByNameWithTeamAndPermissions(string userName)
        {
            var user = _userRepository.GetUserByName(userName);
            
            if (user == null)
                    throw new Exception("User not found");

            var userModel = new UserModel
                {
                    UserId = user.UserID,
                    RoleId = user.RoleID,
                    TeamId = user.TeamID,
                    Username = user.Username
                };

            if (user.RoleID.HasValue)
            {
                var permissionList = _permissionRepository.GetPermissionsByRoleId((int) user.RoleID.Value).ToList();
                userModel.Permissions = UserPermissionModel.Create(permissionList);
            }


            if (user.TeamID.HasValue)
            {
                var team = _teamRepository.GetTeamIdByTeamId(user.TeamID.Value);   
                userModel.Team = TeamModel.CreateFromTeam(team);
            }

            return userModel;
           
        }

    }
}
