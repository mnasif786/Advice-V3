using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Advice.Application.Contracts;
using Advice.Common.Models;
using Advice.Common.Models.Request.CorporatePriority;
using Advice.Logging;
using Advice.Logging.Contracts;
using Advice.Logging.Models;
using Advice.Web.Helpers;
using Advice.Web.Models;
using Advice.Web.Models.Maintenance;

namespace Advice.Web.Controllers
{
     [RoutePrefix("api/maintenance")]
    public class MaintenanceController : ApiController
    {
        private readonly IMaintenanceService _maintenanceService;
        private readonly IUserIdentityFactory _userIdentityFactory;
        private readonly IClientService _clientService;
        public static readonly IElkLog Log = ElkLogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private UserIdentity LoggedInUser
        {
            get { return _userIdentityFactory.GetUserIdentity(User); }
        }


        public MaintenanceController(IMaintenanceService maintenanceService, IUserIdentityFactory userIdentityFactory, IClientService clientService)
        {
            _maintenanceService = maintenanceService;
            _userIdentityFactory = userIdentityFactory;
            _clientService = clientService;
        }

        [Route("corporatePriorities")]
        public IHttpActionResult GetAllCorporatePriorities()
        {
            try
            {
                var corporatePriorities = _maintenanceService.GetAllCorporatePriorities();
                return Ok(corporatePriorities);

            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = string.Format("Corporate Priorities could not be retrieved")
                });

                return InternalServerError(ex);
            }

        }

        [HttpPost, Route("corporatepriority/add")]
        public IHttpActionResult AddCorporatePriority(CorporatePriorityAddRequest corporatePriorityAddRequest)
        {
            try
            {
                _maintenanceService.AddCorporatePriority(corporatePriorityAddRequest, LoggedInUser.Username);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = string.Format("CorporatePriority could not be added for CAN {0}", corporatePriorityAddRequest.Can)
                });

                return InternalServerError(ex);
            }
        }

         [HttpPost, Route("corporatepriority/delete/{corporatePriorityId:int}")]
         public IHttpActionResult DeleteCorporatePriority(int corporatePriorityId)
         {
             try
             {
                 _maintenanceService.DeleteCorporatePriority(corporatePriorityId, LoggedInUser.Username);
                 return Ok();
             }
             catch (Exception ex)
             {
                 Log.LogException(new ExceptionLogRequest()
                 {
                     Exception = ex,
                     ServerVariables = HttpContext.Current.Request.ServerVariables,
                     User = LoggedInUser.Username,
                     AdditionalMessage = string.Format("CorporatePriority with Id {0} could not be deleted", corporatePriorityId)
                 });

                 return InternalServerError(ex);
             }
         }

         [HttpPost, Route("corporatepriority/edit")]
         public IHttpActionResult EditCorporatePriority(CorporatePriorityEditRequest corporatePriorityModel)
         {
             try
             {
                 _maintenanceService.EditCorporatePriority(corporatePriorityModel, LoggedInUser.Username);
                 return Ok();
             }
             catch (Exception ex)
             {
                 Log.LogException(new ExceptionLogRequest()
                 {
                     Exception = ex,
                     ServerVariables = HttpContext.Current.Request.ServerVariables,
                     User = LoggedInUser.Username,
                     AdditionalMessage = string.Format("CorporatePriority with Id {0} could not be edited", corporatePriorityModel.CorporatePriorityId)
                 });

                 return InternalServerError(ex);
             }
         }

         [Route("cansStartWith/{can}")]
         public IHttpActionResult GetCansStartWith(string can)
         {
             var clientNames = _clientService.GetCansStartWith(can,20);
             var clientNamesList = clientNames as IList<string> ?? clientNames.ToList();
             return Ok(clientNamesList);
         }


         [Route("corporatepriority/{can}")]
         public IHttpActionResult GetCorporatePriorityByCan(string can)
         {
             try
             {
                 var corporatePriority = _maintenanceService.GetCorporatePriorityByCan(can);

                 if (corporatePriority == null)
                     return NotFound();

                 return Ok(corporatePriority);
             }
             catch (Exception ex)
             {
                 Log.LogException(new ExceptionLogRequest()
                 {
                     Exception = ex,
                     ServerVariables = HttpContext.Current.Request.ServerVariables,
                     User = LoggedInUser.Username,
                     AdditionalMessage =
                         string.Format("CorporatePriority could not be returned for CAN {0}", can)
                 });

                 return InternalServerError(ex);
             }
         }

         [Route("user/loggedin/permissions")]
         public IHttpActionResult GetUserWithPermissions()
         {
             try
             {
                 var userIdentity = new UserIdentityModel(LoggedInUser.Name, LoggedInUser.Domain, LoggedInUser.Firstname, LoggedInUser.Surname, LoggedInUser.Username);
                 var maintenanceUser = _maintenanceService.GetMaintenanceUserPermissions(userIdentity);

                 return Ok(maintenanceUser);
             }
             catch (Exception ex)
             {
                 Log.LogException(new ExceptionLogRequest()
                 {
                     Exception = ex,
                     ServerVariables = HttpContext.Current.Request.ServerVariables,
                     User = LoggedInUser.Username,
                     AdditionalMessage = "Maintenance users with permissions could not be retrieved"
                 });

                 return InternalServerError(ex);
             }
             
         }

         [Route("corporatepriority/users")]
         public IHttpActionResult GetCorporatePriorityUsers()
         {
             try
             {
                 var corporatePriorityUsers = _maintenanceService.GetCorporatePriorityUsersExcludingThis(LoggedInUser.Username);

                 return Ok(corporatePriorityUsers);
             }
             catch (Exception ex)
             {
                 Log.LogException(new ExceptionLogRequest()
                 {
                     Exception = ex,
                     ServerVariables = HttpContext.Current.Request.ServerVariables,
                     User = LoggedInUser.Username,
                     AdditionalMessage = "corporate priority users could not be retrieved"
                 });

                 return InternalServerError(ex);
             }
         }

         [HttpPost, Route("corporatepriority/user/add/{userId:long}")]
         public IHttpActionResult AddCorporatePriorityUser(long userId)
         {
             try
             {
                 var isUserAdded = _maintenanceService.AddCorporatePriorityUser(userId, LoggedInUser.Username);
                 var addUserStatus = new { UserAdded = isUserAdded, UserAlreadyExist = !isUserAdded };
                 return Ok(addUserStatus);
             }
             catch (Exception ex)
             {
                 Log.LogException(new ExceptionLogRequest()
                 {
                     Exception = ex,
                     ServerVariables = HttpContext.Current.Request.ServerVariables,
                     User = LoggedInUser.Username,
                     AdditionalMessage = "CorporatePriority user could not be added"
                 });

                 return InternalServerError(ex);
             }
         }

         [HttpPost, Route("corporatepriority/user/delete/{maintenanceUserPermissionId:long}")]
         public IHttpActionResult DeleteCorporatePriorityUser(long maintenanceUserPermissionId)
         {
             try
             {
                 _maintenanceService.DeleteCorporatePriorityUser(maintenanceUserPermissionId, LoggedInUser.Username);
                 return Ok();
             }
             catch (Exception ex)
             {
                 Log.LogException(new ExceptionLogRequest()
                 {
                     Exception = ex,
                     ServerVariables = HttpContext.Current.Request.ServerVariables,
                     User = LoggedInUser.Username,
                     AdditionalMessage = "Corporate Priority user could not be deleted"
                 });

                 return InternalServerError(ex);
             }
         }
    }
}

