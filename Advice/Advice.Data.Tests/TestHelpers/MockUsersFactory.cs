using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Entities;
using Moq;

namespace Advice.Data.Tests.TestHelpers
{
    internal static class MockUsersFactory
    {
        internal static Mock<System.Data.Entity.DbSet<User>> GetMockedUsers()
        {
           var userData = new List<User>()
            { //user: Muhammadnauman.Asif
                new User()
                {
                    UserID = 1,
                    Username = "Muhammadnauman.Asif",
                    CreatedBy = "Rana.Khan",
                    CreatedDate = DateTime.Now.AddDays(-7),
                    Deleted = false,
                    LastModifiedBy = "David.Boon",
                    LastModifiedDate = DateTime.Now.AddDays(-7),
                    RoleID = 1,
                    TeamID = 2
                },

                new User() //User: Rana.Khan
                {
                    UserID = 2,
                    Username = "Rana.Khan",
                    CreatedBy = "Nauman.Asif",
                    CreatedDate = DateTime.Now.AddDays(-7),
                    Deleted = false,
                    LastModifiedBy = "David.Boon",
                    LastModifiedDate = DateTime.Now.AddDays(-7),
                    RoleID = 1,
                    TeamID = 1
                },

                new User() //user:Andy.White
                {
                    UserID = 3,
                    Username = "Andy.White",
                    CreatedBy = "David.Boon",
                    CreatedDate = DateTime.Now.AddDays(-7),
                    Deleted = false,
                    LastModifiedBy = "David.Boon",
                    LastModifiedDate = DateTime.Now.AddDays(-7),
                    RoleID = 1,
                    TeamID = 2
                }
            };

            return DbSetInitialisedMockFactory<User>.CreateDbSetInitalisedMock(userData);
        }
    }
}
