using System;
using System.Web.Http;
using Advice.Application;
using Advice.Application.Contracts;
using Advice.Web.Helpers;

namespace Advice.Web.Controllers
{
    [RoutePrefix("api/taskModifyingReasons")]
    public class TaskModifyingReasonController : ApiController
    {
        private readonly ITaskModifyingReasonService _taskModifyingReasonService;

        public TaskModifyingReasonController(ITaskModifyingReasonService taskModifyingReasonService)
        {
            _taskModifyingReasonService = taskModifyingReasonService;
        }

        [Route("getTaskModifyingReasonsForResetGroup")]
        public IHttpActionResult GetTaskModifyingReasonsForResetGroup()
        {
            var resetGroupTaskModifyingReasons = _taskModifyingReasonService.GetTaskModifyingReasonsForResetGroup();

            return Ok(resetGroupTaskModifyingReasons);            
        }

        [Route("Reassign")]
        public IHttpActionResult GetTaskModifyingReasonsForReassignGroup()
        {
            var reassignGroupTaskModifyingReasons = _taskModifyingReasonService.GetTaskModifyingReasonsForReassignGroup();

            return Ok(reassignGroupTaskModifyingReasons);
        }
    }
}
 