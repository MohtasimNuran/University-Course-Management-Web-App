using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementMVC.Models
{
    public class CourseAssign
    {
        public IList<SelectListItem> Departments { get; set; }
        public IList<SelectListItem> Teachers { get; set; }
        public float CreditToBeTaken { get; set; }
        public float RemainingCredit { get; set; }
        public IList<SelectListItem> Courses { get; set; }
        public string CourseName { get; set; }
        public float CourseCredit { get; set; }
        
    }
}