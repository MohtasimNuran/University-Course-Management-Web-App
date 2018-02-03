using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementMVC.Models
{
    public class AllocateClass
    {
        public IList<SelectListItem> Departments { get; set; }
        public IList<SelectListItem> Courses { get; set; }
        public IList<SelectListItem> Rooms { get; set; }
        public IList<SelectListItem> Days { get; set; }

        [DataType(DataType.Time)]
        public DateTime Frms { get; set; }
        [DataType(DataType.Time)]
        public DateTime Toos { get; set; }
    }
}