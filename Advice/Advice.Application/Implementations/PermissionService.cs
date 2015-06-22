using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Application.Contracts;
using Advice.Common.Models;
using Advice.Domain.RepositoryContracts;

namespace Advice.Application.Implementations
{
    public class PermissionService: IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public IEnumerable<PermissionModel> GetPermissionsByRoleId(int roleId)
        {
            return _permissionRepository.GetPermissionsByRoleId(roleId).Select(p=> new PermissionModel((int)p.PermissionID, p.Description));
        }
    }
}
