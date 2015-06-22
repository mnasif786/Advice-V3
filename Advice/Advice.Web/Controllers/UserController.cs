using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using Advice.Application.Contracts;
using Advice.Common.Models;
using Advice.Web.Helpers;

namespace Advice.Web.Controllers
{
   [RoutePrefix("api/users")]
    public class UserController : ApiController
    {        
        private readonly IUserService _userService;
        private readonly IUserIdentityFactory _userIdentityFactory;
        private UserIdentity _userIdentity;
        private UserIdentity LoggedInUser
        {
            get
            {
                if (_userIdentity == null)
                {
                    _userIdentity = _userIdentityFactory.GetUserIdentity(User);
                }

                return _userIdentity;
            }
        }

        public UserController(IUserService userService, IPermissionService permissionService, IUserIdentityFactory userIdentityFactory)
        {
            _userService = userService;
            _userIdentityFactory = userIdentityFactory;
        }

        [Route("")]
        public IHttpActionResult GetAllUsers()
        {            
            return Ok(_userService.GetAllUsers() );            
        }

        [HttpGet, Route("getLoggedInUser")]
        public IHttpActionResult GetLoggedInUser()
        {
            return Ok(LoggedInUser);
        }

        [Route("loggedin/permissions")]
        public IHttpActionResult GetUserWithPermissions()
        {
            var user = _userService.GetUserByNameWithTeamAndPermissions(LoggedInUser.Username);

            if (user.IsInDevelopmentTeam())
            {
                user.Permissions.Developer = true;
                user.MachineName = Environment.MachineName;
            }

            user.Identity = new UserIdentityModel(LoggedInUser.Name, LoggedInUser.Domain, LoggedInUser.Firstname, LoggedInUser.Surname, LoggedInUser.Username);

            return Ok(user);
        }

    }
}
