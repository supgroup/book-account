//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookAccountApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class office
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public office()
        {
            this.payOp = new HashSet<payOp>();
            this.serviceData = new HashSet<serviceData>();
            this.officeFiles = new HashSet<officeFiles>();
            this.officeSystem = new HashSet<officeSystem>();
        }
    
        public int officeId { get; set; }
        public string name { get; set; }
        public string agentName { get; set; }
        public Nullable<System.DateTime> joinDate { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string userName { get; set; }
        public string passwordSY { get; set; }
        public string PasswordSoto { get; set; }
        public string notes { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<decimal> balance { get; set; }
        public Nullable<decimal> commission_ratio { get; set; }
        public string code { get; set; }
    
        public virtual users users { get; set; }
        public virtual users users1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<payOp> payOp { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<serviceData> serviceData { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<officeFiles> officeFiles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<officeSystem> officeSystem { get; set; }
    }
}
