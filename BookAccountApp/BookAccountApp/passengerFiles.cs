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
    
    public partial class passengerFiles
    {
        public int fileId { get; set; }
        public string fileName { get; set; }
        public string extention { get; set; }
        public string folderName { get; set; }
        public string notes { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<int> passengerId { get; set; }
    
        public virtual passengers passengers { get; set; }
    }
}
