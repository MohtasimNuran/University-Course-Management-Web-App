using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementMVC.Models;

namespace UniversityManagementMVC.Controllers
{
    public class CourseAssignController : Controller
    {
        UniversityManagementDBEntities4 db = new UniversityManagementDBEntities4();

        public ActionResult Index()
        {
            var courseAssign = GetDepartments();
            return View(courseAssign);
        }
        [HttpPost]
        public ActionResult Index(int departmentId, int teacherId, int courseId)
        {
            var courseAssign = GetDepartments();
            return View();
        }

        public JsonResult GetTeacher(string departmentId)
        {
            var teachers = GetTeachers(departmentId);
            return Json(teachers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourse(string departmentId)
        {
            var courses = GetCourses(departmentId);
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        private CourseAssign GetDepartments()
        {
            List<SelectListItem> departments = new List<SelectListItem>();

            CourseAssign courseAssign = new CourseAssign();

            List<Department> states = db.Departments.ToList();
            states.ForEach(x => { departments.Add(new SelectListItem { Text = x.Name, Value = x.DepartmentId.ToString() }); });
            courseAssign.Departments = departments;
            return courseAssign;
        }

        private List<SelectListItem> GetTeachers(string departmentId)
        {
            int departmntId;
            List<SelectListItem> teachers = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(departmentId))
            {
                departmntId = Convert.ToInt32(departmentId);
                List<Teacher> teacher = db.Teachers.Where(x => x.DepartmentId == departmntId).ToList();
                teacher.ForEach(
                    x => { teachers.Add(new SelectListItem { Text = x.Name, Value = x.TeacherId.ToString() }); });
            }
            return teachers;
        }

        private List<SelectListItem> GetCourses(string departmentId)
        {
            int departmntId;
            List<SelectListItem> courses = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(departmentId))
            {
                departmntId = Convert.ToInt32(departmentId);
                List<Course> course = db.Courses.Where(x => x.DepartmentId == departmntId).ToList();
                course.ForEach(
                    x => { courses.Add(new SelectListItem { Text = x.Code, Value = x.CourseId.ToString() }); });
            }
            return courses;
        }

        public JsonResult GetCreditByteacherId(int teacherId)
        {
            var getCredit = db.Teachers.Where(m => m.TeacherId == teacherId).Select(m => m.CreditToBeTaken);

            return Json(getCredit, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCourseNameByCourseId(int courseId)
        {
            var getCourseName = db.Courses.Where(m => m.CourseId == courseId).Select(m => m.Name);

            return Json(getCourseName, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCourseCreditByCourseId(int courseId)
        {
            var getCourseCredit = db.Courses.Where(m => m.CourseId == courseId).Select(m => m.Credit);

            return Json(getCourseCredit, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRemainingCredit(double teacherCreditId, double courseCreditId)
        {
            var getRemainCredit = teacherCreditId - courseCreditId;

            return Json(getRemainCredit, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveCourseAssignToTeacher(CourseAssignTeacher course)
        {

            if (db.CourseAssignTeachers.Any(x => x.CourseId == course.CourseId && x.TeacherId== course.TeacherId))
            {
                var saved = "Already exists";
                return Json(saved, JsonRequestBehavior.AllowGet);
            }
            else
            {
                db.CourseAssignTeachers.Add(course);
                db.SaveChanges();
                var saved = "Saved";
                return Json(saved, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetRemainingCreditFromCousreAssign(int teacherId)
        {
            if (db.CourseAssignTeachers.Any(x => x.TeacherId == teacherId))
            {

                var remain = db.CourseAssignTeachers.Where(x => x.TeacherId == teacherId).Select(x => x.RemainingCredit).Sum();

                return Json(remain, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var remain = db.Teachers.Where(x => x.TeacherId == teacherId).Select(x => x.CreditToBeTaken);

                return Json(remain, JsonRequestBehavior.AllowGet);
            }
        }

    }
}