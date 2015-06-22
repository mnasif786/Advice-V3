using System.ComponentModel;

namespace Advice.Domain.Entities.Enums
{
    public enum DerivedTaskStatusForDisplay
    {
        [Description("Outstanding")]
        Outstanding,

        [Description("OverdueTask")]
        Red,

        [Description("ApproachingSLATask")]
        Amber,

        [Description("WithinSLATask")]
        Green,

        [Description("StartedTask")]
        Platinum
    }
}
