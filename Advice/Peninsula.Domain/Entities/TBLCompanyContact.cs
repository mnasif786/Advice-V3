//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Peninsula.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBLCompanyContact
    {
        public int CompanyContactID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public int SiteAddressID { get; set; }
        public string CustomerKey { get; set; }
        public Nullable<int> ContactTypeID { get; set; }
        public Nullable<int> PositionID { get; set; }
        public string Title { get; set; }
        public string Forename { get; set; }
        public string Initial { get; set; }
        public string Surname { get; set; }
        public string TelNoMain { get; set; }
        public string TelNoAlt { get; set; }
        public string EMail { get; set; }
        public string NameSoundX { get; set; }
        public Nullable<bool> flagHidden { get; set; }
        public Nullable<bool> PrivateAndConfidential { get; set; }
        public string FaxNumber { get; set; }
        public string ContactHrsSpanStart { get; set; }
        public string ContactHrsSpanEnd { get; set; }
        public Nullable<int> CompanyNo { get; set; }
        public Nullable<int> ContactNo { get; set; }
        public string Notes { get; set; }
    }
}