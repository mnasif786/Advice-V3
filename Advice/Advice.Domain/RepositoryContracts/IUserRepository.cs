using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Common;
using Advice.Domain.Entities;
using Task = Advice.Domain.Entities.Task;

namespace Advice.Domain.RepositoryContracts
{
    public interface IUserRepository : IAdviceRepository<User>
    {
        IEnumerable<User> GetUsersByTeamId(int teamId);
        IEnumerable<User> GetAllUsers();
        User GetUserByName(string userName);
    }
}
