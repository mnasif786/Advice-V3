using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Advice.Common.Models;
using Advice.Web.Controllers;
using Advice.WebAPI.Tests.Common;
using NUnit.Framework;

namespace Advice.WebAPI.Tests.TaskModifyingReasonTests
{
    [TestFixture]
    public class GetTaskModifyingReasonsForResetGroupTests : BaseTaskModifyingReasonControllerTests
    {
        private TaskModifyingReasonController _taskModifyingReasonController;

        [SetUp]
        public void SetUp()
        {
            var taskModifyingReasons = new List<TaskModifyingReasonModel>()
            {
                new TaskModifyingReasonModel(6, "Client request"),
                new TaskModifyingReasonModel(7, "Management decision")
                
            };

            TaskModifyingReasonServiceMock.Setup(x => x.GetTaskModifyingReasonsForResetGroup())
               .Returns(taskModifyingReasons);

            _taskModifyingReasonController = GetTarget();
        }

        [Test]
        public void Given_TaskModifyingReasons_Get_ModifyingReasons_For_ResetGroup_Returns_All_For_This_Group()
        {
            var actionResult = _taskModifyingReasonController.GetTaskModifyingReasonsForResetGroup();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<TaskModifyingReasonModel>>;

            Assert.That(contentResult.Content.ToList().Count, Is.EqualTo(2));
        }
    }
}
