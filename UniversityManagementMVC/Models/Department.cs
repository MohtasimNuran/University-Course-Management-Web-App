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
    
    public partial class Department
    {
        public Department()
        {
            this.Courses = new HashSet<Course>();
            this.CourseAssignTeachers = new HashSet<CourseAssignTeacher>();
            this.Students = new HashSet<Student>();
            this.Teachers = new HashSet<Teacher>();
            this.AllocateClassrooms = new HashSet<AllocateClassroom>();
        }
    
        public int DepartmentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<CourseAssignTeacher> CourseAssignTeachers { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<AllocateClassroom> AllocateClassrooms { get; set; }
    }
}
