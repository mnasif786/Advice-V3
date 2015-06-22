using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advice.Domain.Entities.Enums;

namespace Advice.Data.Helpers
{
    public static class NatureOfAdviceHelper
    {
        public static IEnumerable<NatureOfAdviceGroupId> NonNatureOfAdviceGroups
        {
            get
            {
                return new List<NatureOfAdviceGroupId>()
                {
                    NatureOfAdviceGroupId.Absence,
                    NatureOfAdviceGroupId.Discrimination,
                    NatureOfAdviceGroupId.FamilyFriendlyEntitlements,
                    NatureOfAdviceGroupId.General,
                    NatureOfAdviceGroupId.YoungPersonWorking,
                    NatureOfAdviceGroupId.Other,
                    NatureOfAdviceGroupId.P00,
                    NatureOfAdviceGroupId.PXX,
                    NatureOfAdviceGroupId.Retirement,
                    NatureOfAdviceGroupId.TermsAndConditions,
                    NatureOfAdviceGroupId.TradeUnion,
                    NatureOfAdviceGroupId.CollectiveAgreement,
                    NatureOfAdviceGroupId.Payroll,
                    NatureOfAdviceGroupId.Hrface2face,
                    NatureOfAdviceGroupId.NonUtilisation,
                    NatureOfAdviceGroupId.ProActiveCaseManagement,
                    NatureOfAdviceGroupId.MiscGroup,
                    NatureOfAdviceGroupId.EarlyConciliation,
                    NatureOfAdviceGroupId.DocumentationGeneral,
                    NatureOfAdviceGroupId.DocumentationPIW,
                };
            }
        }

        public static IEnumerable<NatureOfAdviceGroupId> NatureOfAdviceGroups
        {
            get
            {
                return new List<NatureOfAdviceGroupId>()
                {
                    NatureOfAdviceGroupId.Capability,
                    NatureOfAdviceGroupId.Grievance,
                    NatureOfAdviceGroupId.Redundancy,
                    NatureOfAdviceGroupId.SOSR,
                    NatureOfAdviceGroupId.Conduct
                };
            }
        }

        public static IEnumerable<string> NatureOfAdviceWorkingUserGroup
        {
            get
            {
                return new List<string>()
                {
                    "Hazel.Metcalf"
                    ,"Brinsley.Wilkinson"
                    ,"Patrick.Carroll-Fogg"
                    ,"Paul.Higgins"
                    ,"Rebecca.Farrell"
                    ,"Antonia.McCormack"
                    ,"Sarah.Plimley"
                    ,"Angela.Watson"
                    ,"David.Goodrich"
                    ,"Jamie.Okeeffe"
                    ,"Hannah.Williamson"
                    ,"Andrew.Burgess"
                    ,"Kate.Palmer"
                    ,"Bertrand.SternGillet"
                    ,"Michelle.Clifford"
                    ,"Kirsty.Hudson"
                    ,"Richard.Prior"
                    ,"Charlotte.Harris"
                    ,"Gemma.Burns"
                    ,"Paul.Holcroft"
                    ,"Tina.Ayres"
                    ,"Melanie.Darlington"
                    ,"Vanessa.Monroy"
                    ,"Kelly.Powell"
                    ,"Theresa.Stirton"
                    ,"Alan.Hickey"
                    ,"Marc.Ramsbottom"
                    ,"Dominique.Coulthard"
                    ,"Diyana.Bell"
                    ,"David.Donnelly"
                    ,"Alison.Kirk"
                    ,"Sue.Hargreaves"
                    ,"Linda.Howe"
                };
            }
        }
    }
}
