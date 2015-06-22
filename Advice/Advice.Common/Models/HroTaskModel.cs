using System;
using Advice.Domain.Entities;

namespace Advice.Common.Models
{
    public class HroTaskModel
    {
        public long TaskId { get; set; }
        public long? DepartmentId { get; set; }
        public string DepartmentDescription { get; set; }
        public long? JurisdictionId { get; set; }
        public string JurisdictionDescription { get; set; }
        public string IndividualOrGroup { get; set; }
        public string IndividualForename { get; set; }
        public string IndividualSurname { get; set; }
        public string GroupName { get; set; }
        public string ResponseType { get; set; }
        public string ResponseDate { get; set; }
        public string ResponseTime { get; set; }
        public long? HroNatureOfAdviceGroupId { get; set; }
        public Guid? HroUserId { get; set; }
        public string ResponseEmail { get; set; }
        public int? HroEmployeeId { get; set; }

        public string HroNatureOfAdviceGroup { get; set; }

        public static HroTaskModel Create(HroTask hroTask)
        {
            if (hroTask == null)
                return null;

            var hroTaskModel = new HroTaskModel
            {
                TaskId = hroTask.TaskID,
                DepartmentId = hroTask.DepartmentID,
                GroupName = hroTask.GroupName,
                HroEmployeeId = hroTask.HroEmployeeId,
                HroNatureOfAdviceGroupId = hroTask.HroNatureOfAdviceGroupId,
                HroUserId = hroTask.HroUserID,
                IndividualForename = hroTask.IndividualForename,
                IndividualOrGroup = hroTask.IndividualOrGroup,
                IndividualSurname = hroTask.IndividualSurname,
                JurisdictionDescription = hroTask.JusrisdictionDescription,
                JurisdictionId = hroTask.JusrisdictionID,
                ResponseDate = hroTask.ResponseDate.HasValue ? hroTask.ResponseDate.Value.ToShortDateString() : string.Empty,
                ResponseEmail = hroTask.ResponseEmail,
                ResponseTime = hroTask.ResponseTime,
                ResponseType = hroTask.ResponseType,
                HroNatureOfAdviceGroup = hroTask.HroNatureOfAdviceGroup != null ? hroTask.HroNatureOfAdviceGroup.Description : string.Empty
            };

            return hroTaskModel;
        }
    }
}
