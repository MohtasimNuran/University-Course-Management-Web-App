using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementMVC.Models;

namespace UniversityManagementMVC.Controllers
{
    public class EnrollInACourseController : Controller
    {
        UniversityManagementDBEntities4 db = new UniversityManagementDBEntities4();


        //public ActionResult EnrollStudentInACourse()
        //{
        //    ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegNo");
        //    ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name");
        //    //var courseAssign = GetDepartments();
        //    return View();

        //}

        //[HttpPost]
        //public ActionResult EnrollStudentInACourse(EnrollInACourse enrollInACourse)
        //{

        //    ModelState.Clear();

        //    if (db.EnrollInACourses.Any(x => x.CourseId == enrollInACourse.CourseId))
        //    {
        //        ViewBag.Msg = "Already Enrolled";
        //    }
        //    else
        //    {
        //        db.EnrollInACourses.Add(enrollInACourse);
        //        db.SaveChanges();
        //        ViewBag.Msg = "Enrolled Succesfully";
        //    }
        //    ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegNo");
        //    ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name");
        //    return View();
        //}






        public ActionResult EnrollCourse()
        {
            var courseAssign = GetStudents();
            return View(courseAssign);

        }
        [HttpPost]
        public ActionResult EnrollCourse(EnrollInACourse enrollInACourse)
        {
            var courseAssign = GetStudents();
            
            return View();

        }
        private EnrollCourse GetStudents()
        {
            List<SelectListItem> students = new List<SelectListItem>();

            EnrollCourse enrollCourse = new EnrollCourse();

            List<Student> states = db.Students.ToList();
            states.ForEach(x => { students.Add(new SelectListItem { Text = x.RegNo, Value = x.StudentId.ToString() }); });
            enrollCourse.Students = students;
            return enrollCourse;
        }

        public JsonResult EnrollStudent(EnrollInACourse enrollInACourse)
        {

            if (db.EnrollInACourses.Any(x => x.CourseId == enrollInACourse.CourseId && x.StudentId== enrollInACourse.StudentId))
            {
                var saved = "Already Enrolled";
                return Json(saved, JsonRequestBehavior.AllowGet);
            }
            else
            {
                db.EnrollInACourses.Add(enrollInACourse);
                db.SaveChanges();
                var saved = "Enrolled Successfully";
                return Json(saved, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetStudentName(int studentId)
        {
            var name = db.Students.Where(x => x.StudentId == studentId).Select(x => x.Name);
            return Json(name, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStudentEmail(int studentId)
        {
            var name = db.Students.Where(x => x.StudentId == studentId).Select(x => x.Email);
            return Json(name, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStudentDepartment(int studentId)
        {
            var name = db.Students.Where(x => x.StudentId == studentId).Select(x => x.Department.Code);

            //int nam = db.Students.Where(x => x.StudentId == studentId).Select(x => x.DepartmentId).ToList().LastOrDefault();
            //int dept = Convert.ToInt32(nam);
            //var name = db.Departments.Where(x => x.DepartmentId == dept).Select(x => x.Name);

            return Json(name, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseByStudentId(int studentId)
        {

            int depId = db.Students.Where(x => x.StudentId == studentId).Select(x => x.DepartmentId).ToList().LastOrDefault();

            var course = (db.Courses.Where(x => x.DepartmentId == depId).Select(x => new
                {
                    CourseId = x.CourseId,
                    Code = x.Code

                })).ToList();


            return Json(course, JsonRequestBehavior.AllowGet);
        }


    }
}