using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Application.Contracts;
using Advice.Common.Models;
using Advice.Common.Models.Maintenance;
using Advice.Common.Models.Request.CorporatePriority;
using Advice.Domain.Entities;
using Advice.Domain.RepositoryContracts;
using Peninsula.Domain.Entities;
using Peninsula.Domain.RepositoryContracts;

namespace Advice.Application.Implementations
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly ICorporatePriorityRepository _corporatePriorityRepository;
        private readonly ITblCustomerRepository _tblCustomerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMaintenanceUserPermissionRepository _maintenanceUserPermissionRepository;
        public MaintenanceService(ICorporatePriorityRepository corporatePriorityRepository,
                                  ITblCustomerRepository tblCustomerRepository,
                                  IUserRepository userRepository,
                                  IMaintenanceUserPermissionRepository maintenanceUserPermissionRepository)
        {
            _corporatePriorityRepository = corporatePriorityRepository;
            _tblCustomerRepository = tblCustomerRepository;
            _userRepository = userRepository;
            _maintenanceUserPermissionRepository = maintenanceUserPermissionRepository;
        }
        
        public IEnumerable<CorporatePriorityModel> GetAllCorporatePriorities()
        {
            var corporatePriorities = _corporatePriorityRepository.GetAllCorporatePriorities();
            var corporatePrioritiesList = corporatePriorities as IList<CorporatePriority> ?? corporatePriorities.ToList();
            if (corporatePrioritiesList.Count > 0)
            {
                var cans = corporatePrioritiesList.Select(cl => cl.Can).ToList();
                var customers = _tblCustomerRepository.GetCustomersByCustomerKeys(cans);
                return corporatePrioritiesList
                    .Select(cp =>CorporatePriorityModel.Create(cp, GetClientNameByCustomerKey(customers, cp.Can), cp.User.Username))
                    .OrderBy(o=> o.Can);
            }

            return new List<CorporatePriorityModel>();
        }

        public CorporatePriorityModel GetCorporatePriorityByCan(string can)
        {
            var corporatePriority = _corporatePriorityRepository.GetCorporatePriorityByCan(can);
            return corporatePriority != null ?  CorporatePriorityModel.Create(corporatePriority): null;
        }

        public void AddCorporatePriority(CorporatePriorityAddRequest request, string userName)
        {
           var corporatePriority =  CorporatePriority.Create(
               request.Can,
               request.ContractDetail,
               request.ContractEndDate,
               request.ContractValue,
               request.UserId,
               CreatedUser.Create(userName));
           
            _corporatePriorityRepository.Insert(corporatePriority);
            _corporatePriorityRepository.SaveChanges();
        }

        public void DeleteCorporatePriority(int corporatePriorityId, string userName)
        {
            var corporatePriority = _corporatePriorityRepository.GetById(corporatePriorityId);
            corporatePriority.MarkAsDelete(ModifiedUser.Create(userName), 
                                           DeletedUser.Create(userName));
            _corporatePriorityRepository.Update(corporatePriority);
            _corporatePriorityRepository.SaveChanges();
        }

        public void EditCorporatePriority(CorporatePriorityEditRequest corporatePriorityRequest, string userName)
        {
            var corporatePriority = _corporatePriorityRepository.GetById(corporatePriorityRequest.CorporatePriorityId);
            corporatePriority.Edit(corporatePriorityRequest.ContractValue, 
                                   corporatePriorityRequest.ContractDetail, 
                                   corporatePriorityRequest.ContractEndDate, 
                                   corporatePriorityRequest.UserId, 
                                   ModifiedUser.Create(userName));

            _corporatePriorityRepository.Update(corporatePriority);
            _corporatePriorityRepository.SaveChanges();
        }

        public MaintenanceUserModel GetMaintenanceUserPermissions(UserIdentityModel userIdentity)
        {
            var user = _userRepository.GetUserByName(userIdentity.UserName);
            if (user == null)
                    throw new Exception("User not found");
         
            var maintenanceUserPermissions = _maintenanceUserPermissionRepository.GetMaintenanceUserPermissions(user.UserID);
            return MaintenanceUserModel.Create(maintenanceUserPermissions.ToList(), userIdentity);
        }

        public IEnumerable<CorporatePriorityUserModel> GetCorporatePriorityUsersExcludingThis(string userName)
        {
            var user = _userRepository.GetUserByName(userName);
            var corporatePriorityUsers = _maintenanceUserPermissionRepository.GetCorporatePriorityUsers();
            var corporatePriorityUsersExludeThisUser = corporatePriorityUsers
                                                       .Where(u => u.UserId != user.UserID)
                                                       .Select(x=> CorporatePriorityUserModel.Create(x.MaintenanceUserPermissionId, x.User.Username))
                                                       .OrderBy(o=> o.UserName);
            return corporatePriorityUsersExludeThisUser;
        }

        /// <summary>
        /// Adds corporate priority user
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="createdBy">createdBy</param>
        /// <returns>false if user already exists otherwise true when a user is added</returns>
        public bool AddCorporatePriorityUser(long userId, string createdBy)
        {
            var existingMaintenanceUserPermission =_maintenanceUserPermissionRepository.GetCorporatePriorityUserByUserId(userId);
            if (existingMaintenanceUserPermission != null)
            {
                return false;
            }

            var maintenanceUserPermission = MaintenanceUserPermission.Create(userId, CreatedUser.Create(createdBy));
            _maintenanceUserPermissionRepository.Insert(maintenanceUserPermission);
            _maintenanceUserPermissionRepository.SaveChanges();
            
            return true;
        }

        public void DeleteCorporatePriorityUser(long maintenanceUserPermissionId, string deletedBy)
        {
            var maintenanceUserPermission = _maintenanceUserPermissionRepository.GetById(maintenanceUserPermissionId);
            maintenanceUserPermission.MarkAsDelete(ModifiedUser.Create(deletedBy), DeletedUser.Create(deletedBy));
            _maintenanceUserPermissionRepository.Update(maintenanceUserPermission);
            _maintenanceUserPermissionRepository.SaveChanges();
        }

        private string GetClientNameByCustomerKey(IEnumerable<TBLCustomer> customers, string customerKey)
        {

            var customer = customers.FirstOrDefault(cust => cust.CustomerKey == customerKey);
            if (customer != null)
            {
                return customer.CompanyName;
            }

            return string.Empty;
        }
    }
}
