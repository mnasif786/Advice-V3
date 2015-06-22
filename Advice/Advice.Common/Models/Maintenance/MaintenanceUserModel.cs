using System.Collections.Generic;
using Advice.Domain.Entities;

namespace Advice.Common.Models.Maintenance
{
    public class MaintenanceUserModel
    {
        public UserIdentityModel Identity { get; private set; }
        public MaintenanceUserPermissionsModel MaintenancePermissions { get; set; }
        

        public static MaintenanceUserModel Create(IList<MaintenanceUserPermission> maintenanceUserPermissionList, UserIdentityModel identity)
        {
            return new MaintenanceUserModel()
            {
                Identity = identity,
                MaintenancePermissions = MaintenanceUserPermissionsModel.Create(maintenanceUserPermissionList)
                
            };
        }
    }

    
}
