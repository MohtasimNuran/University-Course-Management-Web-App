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
    
    public partial class StudentResult
    {
        public int StudentResultId { get; set; }
        public Nullable<int> StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public Nullable<int> CourseId { get; set; }
        public string Grade { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual Grade Grade1 { get; set; }
        public virtual Student Student { get; set; }
    }
}
