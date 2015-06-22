using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.ServiceModel.Security;
using System.Web;
using System.Web.Http;
using Advice.Application.Contracts;
using Advice.Common.Models;
using Advice.Application.Requests;
using Advice.Logging;
using Advice.Logging.Contracts;
using Advice.Logging.Models;
using Advice.Web.Helpers;
using Advice.Web.Models;

namespace Advice.Web.Controllers
{
    [RoutePrefix("api/tasks")]
    public class TaskController : ApiController
    {
        private readonly ITaskService _taskService;
        private readonly IUserIdentityFactory _userIdentityFactory;
        public static readonly IElkLog Log = ElkLogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private UserIdentity LoggedInUser
        {
            get { return _userIdentityFactory.GetUserIdentity(User); }
        }
        public TaskController(ITaskService taskService, IUserIdentityFactory userIdentityFactory)
        {
            _taskService = taskService;
            _userIdentityFactory = userIdentityFactory;
        }

        [Route("")]
        public IHttpActionResult GetAllTasks()
        {
            var name = _userIdentityFactory.GetUserIdentity(User).Name;

            return Ok(_taskService.GetAllTasksByUser(name));
        }

        [Route("{id:long}")]
        public IHttpActionResult GetTask(long id)
        {
           var task = _taskService.GetById(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }
        
        [Route("timeline/team/{teamId:int}")]
        public IHttpActionResult GetTimelineByTeam(int teamId)
        {
           return Ok(_taskService.GetTaskListTimelineByTeamId(teamId));
        }

        [Route("timeline/team/teamId")]
        public IHttpActionResult GetTimelineByTeamIds([FromUri]long[] id)
        {
            return Ok(_taskService.GetTaskListTimelineByTeamIds(id));
        }

        [Route("timeline/user/{username}")]
        public IHttpActionResult GetTimelineByUser(string username)
        {
            return Ok(_taskService.GetTaskListTimelineByUsername(username));
        }

        [Route("proactive/loggedinuser")]
        public IHttpActionResult GetProActiveTaskTimelineByLoggedInUser()
        {
            try
            {
                return Ok(_taskService.GetProActiveTasksByUserName(LoggedInUser.Username));
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = string.Format("Pro-Active tasks could not be loaded for user {0}", LoggedInUser.Username)
                });

                return InternalServerError(ex);
            }
        }

        [Route("loggedinuser")]
        public IHttpActionResult GetTasksTimelinesByLoggedInUser()
        {
            try
            {
                return Ok(_taskService.GetTasksByUser(LoggedInUser.Username));
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = string.Format("Pro-Active tasks could not be loaded for user {0}", LoggedInUser.Username)
                });

                return InternalServerError(ex);
            }
        }

        [Route("proactive/user/{username}")]
        public IHttpActionResult GetProActiveTasksAndTimelinesByUser(string username)
        {
            try
            {
                return Ok(_taskService.GetProActiveTasksByUserName(username));
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = string.Format("Pro-Active tasks could not be loaded for user {0}", username)
                });

                return InternalServerError(ex);
            }
        }

        [Route("proactive/teams")]
        public IHttpActionResult GetProActiveTasksAndTimelinesByTeams([FromUri]long[] id)
        {
            try
            {
                return Ok(_taskService.GetProActiveTasksByTeams(id));
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = string.Format("Pro-Active tasks could not be loaded for teams {0}", id)
                });

                return InternalServerError(ex);
            }
        }

        [Route("currentusertasks")]
        public IHttpActionResult GetTasksForCurrentUser()
        {
            try
            {
                var username = _userIdentityFactory.GetUserIdentity(User).Username;

                if (username == null)
                {
                    return NotFound();
                }

                return Ok(_taskService.GetAllTasksByUser(username));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("teamname/{teamname}")]
        public IHttpActionResult GetTasksByTeamName(string teamName)
        {
            try
            {
                return Ok(_taskService.GetAllTasksByTeamName(teamName));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("teamId")]
        public IHttpActionResult GetTasksByTeamId([FromUri]long[] id)
        {
            if (id == null || id.Length == 0)
                return BadRequest("No Team Id's requested");

            try
            {
                return Ok(_taskService.GetAllTasksByTeamId(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("teamuser/{username}")]
        public IHttpActionResult GetTasksByUserName(string userName)
        {
            try
            {
                return Ok(_taskService.GetAllTasksByUser(userName));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("taskDetails/{taskId:long}")]
        public IHttpActionResult GetTasksDetailsByTaskId(long taskId)
        {
            try
            {
                return Ok(_taskService.GetTaskWithDetailsByTaskId(taskId, LoggedInUser.Username));
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = string.Format("Task with TaskId {0} could not be loaded", taskId)
                });

                return InternalServerError(ex);
            }
        }

        [Route("taskHistory/{taskId:long}")]
        public IHttpActionResult GetTasksHistoryByTaskId(long taskId)
        {
            try
            {
                return Ok(_taskService.GetTaskHistoryByTaskId(taskId));                
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("deleted/{username}")]
        public IHttpActionResult GetDeletedTasksByUserName(string userName)
        {
            try
            {
                return Ok(_taskService.GetDeletedTasksByUserName(userName,7));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }        
        }


        [Route("deleted/teams")]
        public IHttpActionResult GetDeletedTasksByTeamIds([FromUri]long[] id)
        {
            try
            {
                var sinceLastDays = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ShowDeletedTasksSinceLastDays"]);
                return Ok(_taskService.GetDeletedTasksByTeamIds(id, sinceLastDays));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpPost, Route("restoreOutlookMessage/{taskId:long}/{messageId:long}")]
        public IHttpActionResult RestoreMessagesToOutlook(long taskId, long messageId)
        {
            return Ok(_taskService.RestoreEmailToOutlook(taskId, messageId, LoggedInUser.Username));
        }

        [HttpPost, Route("restoreBulkMessagesToOutlook")]
        public IHttpActionResult RestoreBulkMessagesToOutlook([FromUri]long[] id)
        {
            _taskService.RestoreBulkEmailsToOutlook(id, LoggedInUser.Username);
            return Ok();
        }


        [HttpDelete, Route("deleteTask/{taskId:long}")]
        public IHttpActionResult Delete(long taskId)
        {
            try
            {
                _taskService.Delete(taskId, LoggedInUser.Username);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest() { Exception = ex, 
                                                             ServerVariables = HttpContext.Current.Request.ServerVariables,
                                                             User = LoggedInUser.Username,
                                                             AdditionalMessage = string.Format("Task with TaskId {0} could not be deleted", taskId)});
                return InternalServerError();
            }
        }

        [Route("reinstate")]
        public IHttpActionResult ReinstateTask([FromBody]long taskId)
        {
           try
            {
                _taskService.ReinstateTask(taskId, LoggedInUser.Name);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Name,
                    AdditionalMessage = string.Format("Task with TaskId {0} could not be reinstated", taskId)
                });
                return InternalServerError();
            }
        }


        [HttpDelete, Route("bulkTasks")]
        public IHttpActionResult DeleteBulkTasks([FromUri]long[] id)
        {
            try
            {
                _taskService.DeleteBulkTasks(id, LoggedInUser.Username);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = string.Format("Tasks with TaskIds {0} could not be deleted", id)
                });
                
                return InternalServerError(ex);
            }
        }

        [HttpPost, Route("resetTaskSla")]
        public IHttpActionResult ResetTaskSla(ResetTaskSlaModel resetTaskSlaModel)
        {
            try
            {
                _taskService.ResetTaskSla(resetTaskSlaModel.TaskId, resetTaskSlaModel.DueDate, resetTaskSlaModel.Urgent, resetTaskSlaModel.TaskModifyingReasonId, resetTaskSlaModel.Comments, LoggedInUser.Username);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = string.Format("Tasks SLA for Task {0} could not be updated", resetTaskSlaModel.TaskId)
                });

                return InternalServerError(ex);
            }
        }

        [HttpPost, Route("reassign")]
        public IHttpActionResult ReassignTask(ReassignTaskModel reassignTaskModel)
        {
            try
            {
                reassignTaskModel.ReAssignedByUser = LoggedInUser.Username;
                _taskService.ReassignTask(reassignTaskModel);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = string.Format("Tasks with Task Id {0} could not be reassigned", reassignTaskModel.TaskId)
                });

                return InternalServerError(ex);
            }
        }

        [Route("user/{username}")]
        public IHttpActionResult GetTasksByUser(string userName)
        {
            try
            {
                return Ok(_taskService.GetTasksByUser(userName));
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = string.Format("Tasks could not be loaded for user {0}", userName)
                });
                
                return InternalServerError(ex);
            }
        }

        [Route("teams")]
        public IHttpActionResult GetTasksByTeams([FromUri]long[] id)
        {
            if (id == null || id.Length == 0)
                return BadRequest("No Team Id's requested");

            try
            {
                return Ok(_taskService.GetTasksByTeams(id));
            }
            catch (Exception ex)
            {
                Log.LogException(new ExceptionLogRequest()
                {
                    Exception = ex,
                    ServerVariables = HttpContext.Current.Request.ServerVariables,
                    User = LoggedInUser.Username,
                    AdditionalMessage = string.Format("Tasks could not be loaded for teams {0}", id)
                });
                
                return InternalServerError(ex);
            }
        }


    }
}
