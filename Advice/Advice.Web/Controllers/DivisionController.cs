using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Advice.Application.Contracts;
using Advice.Common.Models;
using Advice.Logging;
using Advice.Logging.Contracts;
using Advice.Logging.Models;
using Advice.Web.Helpers;

namespace Advice.Web.Controllers
{
    [RoutePrefix("api/divisions")]
    public class DivisionController : ApiController
    {
        private readonly IDivisionService _divisionService;
        private readonly IUserIdentityFactory _userIdentityFactory;
        public static readonly IElkLog Log = ElkLogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private UserIdentity LoggedInUser
        {
            get { return _userIdentityFactory.GetUserIdentity(User); }
        }

        public DivisionController(IDivisionService divisionService, IUserIdentityFactory userIdentityFactory)
        {
            _divisionService = divisionService;
            _userIdentityFactory = userIdentityFactory;
        }


        [Route("")]
        public IHttpActionResult GetAllDivisions()
        {
            try
            {
                var divisions = _divisionService.GetAllDivisions().OrderBy(div=> div.Description);
                return Ok(divisions);
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = "Unable to get Divisions"
                });

                return InternalServerError(ex);
            }
            
        }

        [HttpPost, Route("editDivision")]
        public IHttpActionResult EditDivision(DivisionModel divisionModel)
        {
            try
            {
                _divisionService.UpdateDivision(divisionModel);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = string.Format("Division [0] could not be edited", divisionModel.Description)
                });

                return InternalServerError(ex);
            }
        }


        [HttpPost, Route("addDivision")]
        public IHttpActionResult AddDivision(DivisionModel divisionModel)
        {
            try
            {
                _divisionService.AddDivision(divisionModel, LoggedInUser.Username);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = string.Format("Division {0} could not be added", divisionModel.Description)
                });

                return InternalServerError(ex);
            }
        }

    }
}