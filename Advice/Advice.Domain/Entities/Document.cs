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
    
    public partial class Document
    {
        public Document()
        {
            this.TaskDocuments = new HashSet<TaskDocument>();
        }
    
        public long DocumentID { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }
        public Nullable<long> ScannedDocumentsID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<long> DocumentLibraryId { get; set; }
        public Nullable<long> DocumentTypeId { get; set; }
    
        public virtual ICollection<TaskDocument> TaskDocuments { get; set; }
    }
}
