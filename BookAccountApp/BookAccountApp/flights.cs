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
    
    public partial class flights
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public flights()
        {
            this.serviceData = new HashSet<serviceData>();
        }
    
        public int flightId { get; set; }
        public string airline { get; set; }
        public string notes { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public Nullable<int> flightTableId { get; set; }
        public Nullable<int> fromTableId { get; set; }
        public Nullable<int> toTableId { get; set; }
        public Nullable<bool> isActive { get; set; }
    
        public virtual flightTable flightTable { get; set; }
        public virtual fromTable fromTable { get; set; }
        public virtual toTable toTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<serviceData> serviceData { get; set; }
    }
}
