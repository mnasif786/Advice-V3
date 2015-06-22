using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Data.Repository;
using Advice.Data.Tests.TestHelpers;
using Advice.Domain;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace Advice.Data.Tests.Repository.UserTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private IAdviceDbContextManager _adviceDbContextManager;
        private Mock<AdviceEntities> _adviceEntities;
        private IUserRepository _userRepository;
        private List<User> _userData;

        [SetUp]
        public void TestFixtureSetup()
        {
            _adviceEntities = new Mock<AdviceEntities>();
            _adviceDbContextManager = new AdviceDbContextManager(_adviceEntities.Object);

            //Setting up data for task
            _userData = new List<User>()
            {
                new User() { UserID = 1, Username = "Mickey Mouse",     TeamID = 1, RoleID = 1, Deleted = false  },
                new User() { UserID = 2, Username = "Donald Duck",      TeamID = 1, RoleID = 1, Deleted = false  },
                new User() { UserID = 3, Username = "Fred Flintstone",  TeamID = 1, RoleID = 1, Deleted = false  },
                new User() { UserID = 4, Username = "Scooby Doo",       TeamID = 1, RoleID = 1, Deleted = false  },               
            };

            var dbSetMockTask = DbSetInitialisedMockFactory<User>.CreateDbSetInitalisedMock( _userData );

            _adviceEntities
                .Setup(x => x.Users)
                .Returns(dbSetMockTask.Object);

            _adviceEntities
                .Setup(x => x.Set<User>())
                .Returns(dbSetMockTask.Object);

            _userRepository = new UserRepository(_adviceDbContextManager);            
        }


        [Test]
        public void Given_users_exists_When_Get_all_user_is_called_then_all_Users_returned()
        {                       
            var users = _userRepository.GetAllUsers();

            Assert.AreEqual(_userData.Count(), users.Count() );
        }
    }
}
