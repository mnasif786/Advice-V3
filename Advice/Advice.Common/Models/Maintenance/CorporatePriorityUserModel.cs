using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Common.Models.Maintenance
{
    public class CorporatePriorityUserModel
    {
        public long MaintenanceUserPermissionId { get; set; }
        public string UserName { get; set; }

        public static CorporatePriorityUserModel Create(long maintenancePermissionId, string userName)
        {
            return new CorporatePriorityUserModel()
            {
                MaintenanceUserPermissionId = maintenancePermissionId,
                UserName = userName
            };
        }
    }
}
