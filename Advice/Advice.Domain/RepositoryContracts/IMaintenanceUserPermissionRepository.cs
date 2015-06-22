using System.Collections;
using System.Collections.Generic;
using Advice.Domain.Common;
using Advice.Domain.Entities;

namespace Advice.Domain.RepositoryContracts
{
    public interface IMaintenanceUserPermissionRepository : IAdviceRepository<MaintenanceUserPermission>
    {
        IEnumerable<MaintenanceUserPermission> GetMaintenanceUserPermissions(long userId);
        IEnumerable<MaintenanceUserPermission> GetCorporatePriorityUsers();
        MaintenanceUserPermission GetCorporatePriorityUserByUserId(long userId);
    }
}
