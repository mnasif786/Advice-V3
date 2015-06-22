using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Domain.Entities.Enums
{
    public enum SpecialServiceTeams : long
    {
        //RoiOutOfHours = 17,

        [Description("NI Out Of Hours")]
        NiOutOfHours = 32,
        [Description("GB Out Of Hours")]
        GbOutOfHours = 33,
        [Description("Free Advice")]
        FreeAdviceSalesSupport = 37,
        [Description("Priority Out Of Hours")]
        CorporateOutofHours = 38,
        [Description("Health & Safety Out Of Hours")]
        HsOutOfHours = 39,
        [Description("Tax Advice")]
        TaxAdvice = 41,
        [Description("Vat Advice")]
        VatAdvice = 40
    }
}
