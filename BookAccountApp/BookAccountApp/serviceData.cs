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
    
    public partial class serviceData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public serviceData()
        {
            this.payOp = new HashSet<payOp>();
        }
    
        public int serviceId { get; set; }
        public string serviceNum { get; set; }
        public string type { get; set; }
        public string passenger { get; set; }
        public string ticketNum { get; set; }
        public string airline { get; set; }
        public Nullable<int> officeId { get; set; }
        public Nullable<System.DateTime> serviceDate { get; set; }
        public string pnr { get; set; }
        public Nullable<decimal> ticketvalueSP { get; set; }
        public Nullable<decimal> ticketvalueDollar { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<decimal> priceBeforTax { get; set; }
        public Nullable<decimal> commitionRatio { get; set; }
        public Nullable<decimal> commitionValue { get; set; }
        public Nullable<decimal> cost { get; set; }
        public Nullable<decimal> saleValue { get; set; }
        public Nullable<decimal> paid { get; set; }
        public Nullable<decimal> profit { get; set; }
        public string notes { get; set; }
        public string state { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
    
        public virtual office office { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<payOp> payOp { get; set; }
        public virtual users users { get; set; }
        public virtual users users1 { get; set; }
    }
}
