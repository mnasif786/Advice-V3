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
    
    public partial class TaskTypeSLA
    {
        public long TaskTypeSLAID { get; set; }
        public long TaskTypeID { get; set; }
        public long DepartmentID { get; set; }
        public int DefaultSLATime { get; set; }
        public int DefaultWarningWindow { get; set; }
        public Nullable<bool> UserCanRaise { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public int DefaultAcceptableWindow { get; set; }
    
        public virtual TaskType TaskType { get; set; }
        public virtual Department Department { get; set; }
    }
}