//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InvoiceGen.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserRoleMapping
    {
        public int Id { get; set; }
        public Nullable<long> UserId { get; set; }
        public Nullable<long> RoleId { get; set; }
    
        public virtual RoleMaster RoleMaster { get; set; }
        public virtual UserMaster UserMaster { get; set; }
    }
}