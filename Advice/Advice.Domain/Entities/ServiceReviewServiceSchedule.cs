//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Advice.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ServiceReviewServiceSchedule
    {
        public short ServiceReviewServiceScheduleId { get; set; }
        public short ServiceReviewServiceCansGroupId { get; set; }
        public int DayToRun { get; set; }
        public int MonthToRun { get; set; }
        public bool Deleted { get; set; }
        public Nullable<System.DateTime> LastRun { get; set; }
    
        public virtual ServiceReviewServiceCanGroup ServiceReviewServiceCanGroup { get; set; }
    }
}