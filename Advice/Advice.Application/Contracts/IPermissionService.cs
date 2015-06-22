using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Common.Models;

namespace Advice.Application.Contracts
{
    public interface IPermissionService
    {
        IEnumerable<PermissionModel> GetPermissionsByRoleId(int roleId);
    }
}
