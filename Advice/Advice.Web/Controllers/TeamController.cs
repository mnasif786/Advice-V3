using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Advice.Application;
using Advice.Application.Contracts;
using Advice.Application.Implementations;
using Advice.Common.Models;
using Advice.Logging;
using Advice.Logging.Contracts;
using Advice.Logging.Models;
using Advice.Web.Helpers;

namespace Advice.Web.Controllers
{
    [RoutePrefix("api/teams")]
    public class TeamController : ApiController
    {
        private readonly ITeamService _teamService;
        private readonly IUserIdentityFactory _userIdentityFactory;
        private readonly IUserService _userService;
        public static readonly IElkLog Log = ElkLogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private UserIdentity LoggedInUser
        {
            get { return _userIdentityFactory.GetUserIdentity(User); }
        }

        public TeamController(ITeamService teamService, IUserIdentityFactory userIdentityFactory, IUserService userService)
        {
            _teamService = teamService;
            _userIdentityFactory = userIdentityFactory;
            _userService = userService;
        }

        [Route("")]
        public IHttpActionResult GetAllTeams()
        {
            try
            {
                var user = _userService.GetUserByNameWithTeamAndPermissions(LoggedInUser.Username);

                if (user.Team.DepartmentId == null)
                    return NotFound();
                    
                
                var teams = _teamService.GetTeamsByDepartmentId(user.Team.DepartmentId.Value);

                return Ok(teams);

            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = string.Format("Not able to get teams for user: {0}", LoggedInUser.Username)
                });

                return InternalServerError(ex);
            }
           
        }

        [Route("allTeamsWithDivisionAndDepartment")]
        public IHttpActionResult GetAllTeamsWithDivisionAndDepartment()
        {
            try
            {
                var teams = _teamService.GetAllTeamsWithDivisionAndDepartment()
                            .OrderBy(t=>t.Description)
                            .ThenBy(div=> div.DivisionDescription)
                            .ThenBy(dep=> dep.DepartmentDescription);

                return Ok(teams);

            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = "Teams With Division And Department could not be retrieved"
                });

                return InternalServerError(ex);
            }
        }

        [HttpPost, Route("addTeam")]
        public IHttpActionResult AddTeam(AddEditTeamModel teamModel)
        {
            try
            {
                _teamService.AddTeam(teamModel, LoggedInUser.Username);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = string.Format("Team [0] could not be added", teamModel.Description)
                });

                return InternalServerError(ex);
            }
        }

        [HttpPost, Route("editTeam")]
        public IHttpActionResult EditTeam(AddEditTeamModel teamModel)
        {
            try
            {
                _teamService.UpdateTeam(teamModel, LoggedInUser.Username);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = string.Format("Team [0] could not be edited", teamModel.Description)
                });

                return InternalServerError(ex);
            }
        }

        [HttpPost, Route("deleteTeam/{teamId:int}")]
        public IHttpActionResult Delete(int teamId)
        {
            try
            {
                _teamService.DeleteTeam(teamId, LoggedInUser.Username);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = "Team could not be deleted"
                });

                return InternalServerError(ex);
            }
        }

        [HttpPost, Route("reinstateTeam/{teamId:int}")]
        public IHttpActionResult Reinstate(int teamId)
        {
            try
            {
                _teamService.ReinstateTeam(teamId, LoggedInUser.Username);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = "Team could not be deleted"
                });

                return InternalServerError(ex);
            }
        }

        [HttpGet, Route("anyUserAssociatedWithTeam/{teamId:int}")]
        public IHttpActionResult AnyUserAssociatedWithTeam(int teamId)
        {
            try
            {
                return Ok(_teamService.AnyUserAssociatedWithTeam(teamId));
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = "retrieving any user associated with Team failed"
                });

                return InternalServerError(ex);
            }
        }
    }
}
 