using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;

namespace Advice.Data.Repository
{
    public class PermissionRepository : AdviceRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(IAdviceDbContextManager adviceDbContextManager) : base(adviceDbContextManager)
        {
            
        }

        public IEnumerable<Permission> GetPermissionsByRoleId(int roleId)
        {
            var permissions = Context.PermissionRoles.Where(r=>r.RoleID == roleId && r.Deleted==false).Select(permission=> permission.Permission);
            return permissions;
        }
    }
}
