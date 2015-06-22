using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Common.Models;
using Advice.Common.Models.Maintenance;
using Advice.Common.Models.Request.CorporatePriority;

namespace Advice.Application.Contracts
{
    public  interface IMaintenanceService
    {
        IEnumerable<CorporatePriorityModel> GetAllCorporatePriorities();

        CorporatePriorityModel GetCorporatePriorityByCan(string can);
        void AddCorporatePriority(CorporatePriorityAddRequest corporatePriorityModel, string userName);
        void DeleteCorporatePriority(int corporatePriorityId, string userName);
        void EditCorporatePriority(CorporatePriorityEditRequest corporatePriorityRequest, string userName);
        MaintenanceUserModel GetMaintenanceUserPermissions(UserIdentityModel userIdentity);
        IEnumerable<CorporatePriorityUserModel> GetCorporatePriorityUsersExcludingThis(string userName);
        bool AddCorporatePriorityUser(long userId, string createdBy);
        void DeleteCorporatePriorityUser(long maintenanceUserPermissionId, string deletedBy);
    }
}
