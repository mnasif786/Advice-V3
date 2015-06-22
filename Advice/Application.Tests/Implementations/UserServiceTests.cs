using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Advice.Application.Implementations;
using Advice.Common.Models;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace Advice.Application.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepository;
        private Mock<IPermissionRepository> _permissionRepository;
        private Mock<ITeamRepository> _teamRepository; 

        [SetUp]
        public void TestFixtureSetup()
        {
            _userRepository = new Mock<IUserRepository>();    
            _permissionRepository = new Mock<IPermissionRepository>();
            _teamRepository = new Mock<ITeamRepository>();
        }

        [Test]
        public void Given_valid_users_When_GetAllUsers_called_then_all_users_are_returned()
        {            
            var users = new List<User>()
            {
                new User() { UserID = 1, Username = "Mickey Mouse",     TeamID = 1},
                new User() { UserID = 2, Username = "Norman Normal",    TeamID = 1}                
            };

            _userRepository
                .Setup( x => x.GetAllUsers() )
                .Returns(users);

            var userService = GetTarget();

            IList<UserModel> userList = userService.GetAllUsers().ToList();

            Assert.That(userList, Is.Not.Null);
            Assert.AreEqual(userList.Count(), 2);

            Assert.AreEqual(userList[0].UserId,         users[0].UserID);
            Assert.AreEqual(userList[0].Username,       users[0].Username);
            Assert.AreEqual(userList[0].TeamId,         users[0].TeamID);         
        }


        [Test]
        public void Given_valid_users_When_GetUserByNameWithTeamAndPermissions_called_then_user_team_is_returned()
        {
            var team = new Team { TeamID = 1, Description = "Dev Team", DepartmentID = 1};
            var userName = "na.asif";
            var user = new User {UserID = 1, Username = "na.asif", TeamID = team.TeamID};

            _userRepository.Setup(x => x.GetUserByName(userName)).Returns(user);
            _teamRepository.Setup(x => x.GetTeamIdByTeamId(team.TeamID)).Returns(team);

            var userService = GetTarget();

            var userModel = userService.GetUserByNameWithTeamAndPermissions(userName);

            Assert.That(userModel.Team, Is.Not.Null);
            Assert.That(userModel.Team.TeamId, Is.EqualTo(team.TeamID));

        }

        
        private UserService GetTarget()
        {
            return new UserService(_userRepository.Object, _permissionRepository.Object, _teamRepository.Object);
        }
    }
}
