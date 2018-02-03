using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementMVC.Models;

namespace UniversityManagementMVC.Controllers
{
    public class StudentResultController : Controller
    {
        UniversityManagementDBEntities4 db = new UniversityManagementDBEntities4();
        public ActionResult SaveResult()
        {
            var saveStudentResult = GetStudents();
            ViewBag.Grades = GetGrades();
            return View(saveStudentResult);

        }
        [HttpPost]
        public ActionResult SaveResult(StudentResult studentResult)
        {
            var saveStudentResult = GetStudents();
            ViewBag.Grades = GetGrades();
            return View();

        }
        private SaveStudentResult GetStudents()
        {
            List<SelectListItem> students = new List<SelectListItem>();

            SaveStudentResult saveStudentResult = new SaveStudentResult();

            List<Student> states = db.Students.ToList();
            states.ForEach(x => { students.Add(new SelectListItem { Text = x.RegNo, Value = x.StudentId.ToString() }); });
            saveStudentResult.Students = students;
            return saveStudentResult;
        }
        private List<Grade> GetGrades()
        {
            List<Grade> grades = db.Grades.Select(x => x).ToList();
            return grades;
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
            return Json(name, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCourseByStudentId(int studentId)
        {

            var coursId = db.EnrollInACourses.Where(x => x.StudentId == studentId).ToList();

            var course = coursId.Select(c => new
            {
                CourseId = c.CourseId,
                Name = c.Course.Name,
            }).ToList();

            return Json(course, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveStudentResult(StudentResult studentResult)
        {

            if (db.StudentResults.Any(x => x.CourseId == studentResult.CourseId && x.StudentId == studentResult.StudentId))
            {
                var saved = "Already There";
                return Json(saved, JsonRequestBehavior.AllowGet);
            }
            else
            {
                db.StudentResults.Add(studentResult);
                db.SaveChanges();
                var saved = "Result Saved";
                return Json(saved, JsonRequestBehavior.AllowGet);
            }
        }



    }
}