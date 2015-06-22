using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Entities.Enums;

// ReSharper disable once CheckNamespace
namespace Advice.Domain.Entities
{
    public partial class MaintenanceUserPermission
    {
        public static MaintenanceUserPermission Create(long userId, CreatedUser createdUser)
        {
            return new MaintenanceUserPermission
            {
                UserId = userId,
                MaintenancePermissionId = (int) MaintenancePermissions.CorporatePriority,
                CreatedBy = createdUser.Name,
                CreatedDate = createdUser.CreatedDate,
                
            };
        }

        public void MarkAsDelete(ModifiedUser modifiedUser, DeletedUser deletedUser)
        {
            Deleted = true;
            DeletedBy = deletedUser.Name;
            DeletedDate = deletedUser.DeletedDate;
            LastModifiedBy = modifiedUser.Name;
            LastModifiedDate = modifiedUser.LastModifiedDate;
        }
    }
}
