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
    
    public partial class flightTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public flightTable()
        {
            this.flights = new HashSet<flights>();
        }
    
        public int flightTableId { get; set; }
        public string name { get; set; }
        public Nullable<bool> isActive { get; set; }
        public string notes { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<flights> flights { get; set; }
    }
}
