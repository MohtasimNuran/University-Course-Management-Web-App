using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementMVC.Models
{
    public class SaveStudentResult
    {
        public IList<SelectListItem> Students { get; set; }
        public string Names { get; set; }
        public string Emails { get; set; }
        public string Departments { get; set; }
        public IList<SelectListItem> Courses { get; set; }
        public IList<SelectListItem> Grades { get; set; }
    }
}