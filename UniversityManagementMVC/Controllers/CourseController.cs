using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementMVC.Models;

namespace UniversityManagementMVC.Controllers
{
    public class CourseController : Controller
    {
        UniversityManagementDBEntities4 db = new UniversityManagementDBEntities4();


        public ActionResult SaveCourse()
        {
            ViewBag.Semester = new SelectList(db.Semesters, "Semester1", "Semester1");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            return View();

        }

        [HttpPost]
        public ActionResult SaveCourse(Course course)
        {
            ModelState.Clear();
            ViewBag.Semester = new SelectList(db.Semesters, "Semester1", "Semester1");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");

            if (db.Courses.Any(x => x.Code == course.Code || x.Name == course.Name))
            {
                ViewBag.Msg = "Already exists";
            }
            else
            {
                db.Courses.Add(course);
                db.SaveChanges();
                ViewBag.Msg = "Saved";
            }


            return View();
        }



    }
}