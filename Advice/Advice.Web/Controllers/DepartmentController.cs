using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Advice.Application.Contracts;
using Advice.Logging;
using Advice.Logging.Contracts;
using Advice.Logging.Models;
using Advice.Web.Helpers;

namespace Advice.Web.Controllers
{
    [RoutePrefix("api/departments")]
    public class DepartmentController : ApiController
    {
        private readonly IDepartmentService _departmentService;
        private readonly IUserIdentityFactory _userIdentityFactory;
        public static readonly IElkLog Log = ElkLogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private UserIdentity LoggedInUser
        {
            get { return _userIdentityFactory.GetUserIdentity(User); }
        }

        public DepartmentController(IDepartmentService departmentService, IUserIdentityFactory userIdentityFactory)
        {
            _departmentService = departmentService;
            _userIdentityFactory = userIdentityFactory;
        }

        [Route("")]
        public IHttpActionResult GetAllDepartments()
        {
            try
            {
                var departments = _departmentService.GetAllDepartments().OrderBy(d=>d.Description);
               
                return Ok(departments);
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = "Unable to get departments"
                });

                return InternalServerError(ex);
            }
        }
    }
}
