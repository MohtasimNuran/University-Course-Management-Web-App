using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementMVC.Models;

namespace UniversityManagementMVC.Controllers
{
    public class DepartmentController : Controller
    {
        UniversityManagementDBEntities4 db = new UniversityManagementDBEntities4();
        
        //
        // GET: /Department/SaveDepartment
        public ActionResult SaveDepartment()
        {
            ModelState.Clear();
            return View();
        }

        //
        // POST: /Department/SaveDepartment
        [HttpPost]
        public ActionResult SaveDepartment(Department department)
        {
            ModelState.Clear();
            if (db.Departments.Any(x => x.Code == department.Code || x.Name == department.Name))
                {
                    ViewBag.Msg = "Already exists";
                }
                else
                {
                    db.Departments.Add(department);
                    db.SaveChanges();
                    ViewBag.Msg = "Saved";
                } 
            
            
            return View();
        }


        public ActionResult ViewDepartment()
        {
            return View(GetStudents());
        }

        private List<Department> GetStudents()
        {
            List<Department> students = db.Departments.Select(s => s).ToList();
            return students;
        }

    }
}
