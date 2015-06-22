using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Entities;
using Advice.Domain.Entities.Enums;

namespace Advice.Common.Models.Maintenance
{
    public class MaintenanceUserPermissionsModel
    {
        public bool HasTeamPermission { get; private set; }
        public bool HasDivisionPermission { get; private set; }
        public CorporatePriorityPermissionModel CorporatePriorityPermission { get; private set; }

        public static MaintenanceUserPermissionsModel Create(IList<MaintenanceUserPermission> maintenanceUserPermissionList)
        {
            return new MaintenanceUserPermissionsModel()
            {
                HasTeamPermission = maintenanceUserPermissionList.Any(p => p.MaintenancePermission.MaintenancePermissionId == (int) MaintenancePermissions.Teams),
                HasDivisionPermission = maintenanceUserPermissionList.Any(p => p.MaintenancePermission.MaintenancePermissionId == (int)MaintenancePermissions.Divisions),
                CorporatePriorityPermission = CorporatePriorityPermissionModel.Create(
                    maintenanceUserPermissionList.Any(p => p.MaintenancePermission.MaintenancePermissionId == (int)MaintenancePermissions.CorporatePriority),
                    maintenanceUserPermissionList.Any(p => p.MaintenancePermission.MaintenancePermissionId == (int)MaintenancePermissions.CorporatePriorityManageUsers) 
                )
               
            };
        }

    }

    public class CorporatePriorityPermissionModel
    {
        public bool HasCorporatePriorityPermission { get; private set; }
        public bool HasManageUserPermission { get; private set; }

        public static CorporatePriorityPermissionModel Create(bool hasPermission, bool hasManageUserPermission)
        {
            return new CorporatePriorityPermissionModel()
            {
                HasCorporatePriorityPermission = hasPermission,
                HasManageUserPermission = hasManageUserPermission
            };
        }
    }
}
