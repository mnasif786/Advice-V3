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
    
    public partial class CorporatePriorityByCansQueryResult
    {
        public int CorporatePriorityId { get; set; }
        public string Can { get; set; }
        public Nullable<decimal> ContractValue { get; set; }
        public string ContractDetail { get; set; }
        public Nullable<System.DateTime> ContractEndDate { get; set; }
        public long UserId { get; set; }
        public bool Deleted { get; set; }
    }
}
