using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Common;
using Advice.Domain.Entities;

namespace Advice.Domain.RepositoryContracts
{
    public interface IPermissionRepository : IAdviceRepository<Permission>
    {
        IEnumerable<Permission> GetPermissionsByRoleId(int roleId);
    }
}
