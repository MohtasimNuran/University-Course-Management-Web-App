//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UniversityManagementMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Day
    {
        public Day()
        {
            this.AllocateClassrooms = new HashSet<AllocateClassroom>();
        }
    
        public string Day1 { get; set; }
    
        public virtual ICollection<AllocateClassroom> AllocateClassrooms { get; set; }
    }
}
