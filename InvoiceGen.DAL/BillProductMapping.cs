//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InvoiceGen.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class BillProductMapping
    {
        public long ID { get; set; }
        public Nullable<long> BillID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public string RateCharged { get; set; }
        public string Qyantity { get; set; }
        public string TotalAmount { get; set; }
        public string DiscountPerc { get; set; }
        public string CessPerc { get; set; }
        public string IGST { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
    
        public virtual BillMaster BillMaster { get; set; }
        public virtual Product Product { get; set; }
    }
}
