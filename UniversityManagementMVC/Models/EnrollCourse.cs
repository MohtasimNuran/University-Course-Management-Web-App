using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace UniversityManagementMVC.Models
{
    public class EnrollCourse
    {
        //[Required(ErrorMessage = "Enter Student RegNo")]
        //[DisplayName("Student RegNo")]
        public IList<SelectListItem> Students { get; set; }
        //[Required]
        public string Names { get; set; }
       // [Required]
        public string Emails { get; set; }
        //[Required]
        public string Departments { get; set; }
       // [Required]
        public IList<SelectListItem> Courses { get; set; }
       // [Required]
        [DataType(DataType.Date)]
        public DateTime Dates { get; set; }
    }
}