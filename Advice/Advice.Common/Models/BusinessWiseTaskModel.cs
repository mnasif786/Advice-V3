using Advice.Domain.Entities;

namespace Advice.Common.Models
{
    public class BusinessWiseTaskModel
    {
        public long TaskId { get; set; }
        public long? DepartmentId { get; set; }
        public string DepartmentDescription { get; set; }
        public long? JusrisdictionId { get; set; }
        public string JurisdictionDescription { get; set; }
        public string IndividualOrGroup { get; set; }
        public string IndividualForename { get; set; }
        public string IndividualSurname { get; set; }
        public string GroupName { get; set; }
        public string BusinessWiseOrTelephone { get; set; }
        public string PreferredDateOfContact { get; set; }
        public string PreferredTimeOfContact { get; set; }
        public long? BusinessWiseNatureOfAdviceGroupId { get; set; }
        public long? AuthorisedUserId { get; set; }

        public string BusinessWiseNatureOfAdviceGroup { get; set; }


        public static BusinessWiseTaskModel Create(BusinessWiseTask businessWiseTask)
        {
            if (businessWiseTask == null)
                return null;

            var busniessWiseTaskModel = new BusinessWiseTaskModel
            {
                TaskId = businessWiseTask.TaskID,
                DepartmentId = businessWiseTask.DepartmentID,
                GroupName = businessWiseTask.GroupName,
                IndividualForename = businessWiseTask.IndividualForename,
                IndividualOrGroup = businessWiseTask.IndividualOrGroup,
                IndividualSurname = businessWiseTask.IndividualSurname,
                JurisdictionDescription = businessWiseTask.JusrisdictionDescription,
                JusrisdictionId = businessWiseTask.JusrisdictionID,
                AuthorisedUserId = businessWiseTask.AuthorisedUserID,
                BusinessWiseNatureOfAdviceGroupId = businessWiseTask.BusinessWiseNatureOfAdviceGroupId,
                BusinessWiseOrTelephone = businessWiseTask.BusinessWiseOrTelephone,
                PreferredDateOfContact = businessWiseTask.PreferedDateOfContact.HasValue? businessWiseTask.PreferedDateOfContact.Value.ToShortDateString():string.Empty,
                PreferredTimeOfContact = businessWiseTask.PreferedTimeOfContact,
                BusinessWiseNatureOfAdviceGroup = businessWiseTask.BusinessWiseNatureOfAdviceGroup != null ? businessWiseTask.BusinessWiseNatureOfAdviceGroup.Description : string.Empty
            };

            return busniessWiseTaskModel;
        }

        
    }
}
