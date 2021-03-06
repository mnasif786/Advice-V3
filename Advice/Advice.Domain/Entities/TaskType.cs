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
    
    public partial class TaskType
    {
        public TaskType()
        {
            this.Tasks = new HashSet<Task>();
            this.TaskTypeSLAs = new HashSet<TaskTypeSLA>();
        }
    
        public long TaskTypeID { get; set; }
        public string Description { get; set; }
        public Nullable<bool> ExternalTask { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<TaskTypeSLA> TaskTypeSLAs { get; set; }
    }
}
