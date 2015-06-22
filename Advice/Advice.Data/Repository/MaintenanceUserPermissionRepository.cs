using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Advice.Data.Common;
using Advice.Data.Contracts;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;
using Advice.Domain.RepositoryContracts;

namespace Advice.Data.Repository
{
    public class MaintenanceUserPermissionRepository : AdviceRepository<MaintenanceUserPermission>, IMaintenanceUserPermissionRepository 
    {
        public MaintenanceUserPermissionRepository(IAdviceDbContextManager adviceDbContextManager)
            : base(adviceDbContextManager)
        {
            
        }

        public IEnumerable<MaintenanceUserPermission> GetMaintenanceUserPermissions(long userId)
        {
            return Context.MaintenanceUserPermissions.Where(p => p.UserId == userId && !p.Deleted);
        }

        public IEnumerable<MaintenanceUserPermission> GetCorporatePriorityUsers()
        {
            return Context.MaintenanceUserPermissions
                   .Where(p => p.MaintenancePermissionId == (int) MaintenancePermissions.CorporatePriority &&  !p.Deleted)
                   .Include(u=>u.User);
        }

        public MaintenanceUserPermission GetCorporatePriorityUserByUserId(long userId)
        {
            return Context.MaintenanceUserPermissions.SingleOrDefault(p => p.MaintenancePermissionId == (int) MaintenancePermissions.CorporatePriority 
                                                                      && p.UserId == userId && !p.Deleted);
        }
    }
}
