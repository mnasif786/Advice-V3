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
    
    public partial class NatureOfAdvice
    {
        public NatureOfAdvice()
        {
            this.Jobs = new HashSet<Job>();
        }
    
        public long NatureOfAdviceID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public long NatureOfAdviceGroupID { get; set; }
        public Nullable<long> OrderBy { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public System.DateTime LastModifiedDate { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<bool> IsKeyNatureOfAdvice { get; set; }
    
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual NatureOfAdviceGroup NatureOfAdviceGroup { get; set; }
    }
}
