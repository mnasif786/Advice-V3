using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Domain;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;
using Task = Advice.Domain.Entities.Task;

namespace Advice.Data.Repository
{
    public class UserRepository : AdviceRepository<User>, IUserRepository
    {
        public UserRepository(IAdviceDbContextManager adviceDbContextManager)
            : base(adviceDbContextManager)
        {
            
        }

        public IEnumerable<User> GetAllUsers()
        {
            return Context.Users.Where(u => !u.Deleted).OrderBy(o => o.Username);            
        }

        public IEnumerable<User> GetUsersByTeamId(int teamId)
        {
            var users = Context.Users.Where(u => u.TeamID == teamId && !u.Deleted).OrderBy(o=>o.Username);
            return users;
        }

        public User GetUserByName(string userName)
        {
            return Context.Users.SingleOrDefault(u => u.Username == userName && u.Deleted==false);
        }
    }
}
