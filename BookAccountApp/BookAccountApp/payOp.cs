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
    
    public partial class payOp
    {
        public int payOpId { get; set; }
        public string code { get; set; }
        public Nullable<decimal> cash { get; set; }
        public string opType { get; set; }
        public string side { get; set; }
        public Nullable<int> serviceId { get; set; }
        public string opStatus { get; set; }
        public Nullable<System.DateTime> opDate { get; set; }
        public string notes { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> officeId { get; set; }
        public Nullable<int> passengerId { get; set; }
        public Nullable<int> userId { get; set; }
        public string recipient { get; set; }
        public string recivedFrom { get; set; }
        public Nullable<int> paysideId { get; set; }
        public Nullable<int> flightId { get; set; }
        public string opName { get; set; }
    
        public virtual office office { get; set; }
        public virtual passengers passengers { get; set; }
        public virtual serviceData serviceData { get; set; }
        public virtual users users { get; set; }
        public virtual users users1 { get; set; }
        public virtual users users2 { get; set; }
        public virtual paySides paySides { get; set; }
        public virtual flights flights { get; set; }
    }
}
