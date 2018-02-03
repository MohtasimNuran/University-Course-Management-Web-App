using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using UniversityManagementMVC.Models;

namespace UniversityManagementMVC.Controllers
{
    public class ViewCourseStaticsController : Controller
    {
        private UniversityManagementDBEntities4 db = new UniversityManagementDBEntities4();

        public ActionResult CourseStatics()
        {
            var courseAssign = GetDepartments();
            return View(courseAssign);
        }

        [HttpPost]
        public ActionResult CourseStatics(int departmentId, int teacherId, int courseId)
        {
            var courseAssign = GetDepartments();
            return View();
        }

        private CourseAssign GetDepartments()
        {
            List<SelectListItem> departments = new List<SelectListItem>();

            CourseAssign courseAssign = new CourseAssign();

            List<Department> states = db.Departments.ToList();
            states.ForEach(
                x => { departments.Add(new SelectListItem {Text = x.Name, Value = x.DepartmentId.ToString()}); });
            courseAssign.Departments = departments;
            return courseAssign;
        }

        public JsonResult GetCourseView(int departmentId)
        {
            //var view = (from course in db.Courses
            //            join courseAssign in db.CourseAssignTeachers
            //            on course.CourseId equals courseAssign.CourseId
            //            into assign
            //            from ass in assign.DefaultIfEmpty()
            //            join teacher in db.Teachers
            //            on ass.TeacherId equals teacher.TeacherId
            //            into courses
            //            from co in courses.DefaultIfEmpty()
            //            where course.DepartmentId == departmentId
            //            select new
            //            {
            //                course.Code,
            //                course.Name,
            //                course.Semester,
            //                teacherName = co.Name
            //            }).ToList();



            //if (db.CourseAssignTeachers.Any(x => x.DepartmentId == departmentId))
            //{
            //    var view = (from course in db.Courses
            //        join courseAssign in db.CourseAssignTeachers
            //            on course.CourseId equals courseAssign.CourseId
            //        join teacher in db.Teachers
            //            on courseAssign.TeacherId equals teacher.TeacherId
            //        where courseAssign.DepartmentId == departmentId
            //        select new
            //        {
            //            course.Code,
            //            course.Name,
            //            course.Semester,
            //            teacherName = teacher.Name
            //        }).ToList();
            //    return Json(view, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    var view = db.Courses.Where(x => x.DepartmentId == departmentId).Select(x => new
            //    {
            //        y = x.CourseId,
            //        x.Code,
            //        x.Name,
            //        x.Semester,
            //        teacherName="Not Assigned Yet"
            //    });

            //    return Json(view, JsonRequestBehavior.AllowGet);
            //}

            var viewAll = (db.Courses.Where(x => x.DepartmentId == departmentId).Select(x => new
            {
                x.Code,
                x.Name,
                x.Semester,
                //x.CourseAssignTeachers
                teacherName="Not Assigned Yet"

            })).ToList();

            var viewWithTeacher = (db.CourseAssignTeachers.Where(x => x.DepartmentId == departmentId).Select(x => new
            {
                x.Course.Code,
                x.Course.Name,
                x.Course.Semester,
                teacherName =x.Teacher.Name
            })).ToList();

            var result = viewAll.Where(x => viewWithTeacher.All(i => x.Code != i.Code)).ToList();
            var view = result.Union(viewWithTeacher).ToList();
            return Json(view, JsonRequestBehavior.AllowGet);
        }
    }
}