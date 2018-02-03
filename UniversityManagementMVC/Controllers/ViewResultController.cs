using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Windows.Forms;
using Rotativa;
using UniversityManagementMVC.Models;

namespace UniversityManagementMVC.Controllers
{
    public class ViewResultController : Controller
    {
        UniversityManagementDBEntities4 db = new UniversityManagementDBEntities4();
        public ActionResult ViewResult()
        {
            var saveStudentResult = GetStudents();
            return View(saveStudentResult);

        }
        [HttpPost]
        public ActionResult ViewResult(int studentId)
        {
            var saveStudentResult = GetStudents();
            return View(saveStudentResult);

        }
        //public ActionResult GeneratePdf()
        //{
        //    return new Rotativa.ActionAsPdf("ViewResult");
        //}

        private SaveStudentResult GetStudents()
        {
            List<SelectListItem> students = new List<SelectListItem>();

            SaveStudentResult saveStudentResult = new SaveStudentResult();

            List<Student> states = db.Students.ToList();
            states.ForEach(x => { students.Add(new SelectListItem { Text = x.RegNo, Value = x.StudentId.ToString() }); });
            saveStudentResult.Students = students;
            return saveStudentResult;
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
        public JsonResult GetResultView(int studentId)
        {
            var viewWithGrade = db.StudentResults.Where(x => x.StudentId == studentId).Select(x => new
            {
                x.Course.Code,
                x.Course.Name,
                x.Grade
            }).ToList();


            var viewAll = db.EnrollInACourses.Where(x => x.StudentId == studentId).Select(x => new
            {
                x.Course.Code,
                x.Course.Name,
                Grade = "Not graded yet"
            }).ToList();

            var result = viewAll.Where(x => viewWithGrade.All(i => x.Code != i.Code)).ToList();
            var view = result.Union(viewWithGrade).ToList();

            return Json(view, JsonRequestBehavior.AllowGet);
        }

        private static void Capture(string capturedFilePath)
        {
            Bitmap bitmap = new Bitmap
          (Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //Rectangle rect = new Rectangle(800, 800, 800, 800);
            //Bitmap bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);


            Graphics graphics = Graphics.FromImage(bitmap as System.Drawing.Image);
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

            bitmap.Save(capturedFilePath, ImageFormat.Bmp);
        }
        public JsonResult GeneratePdf()
        {
            Capture("H:/C#/MVC/UniversityManagementMVC/Result/ScreenShot.bmp");
            var saved = "Result Saved";
            return Json(saved, JsonRequestBehavior.AllowGet);
        }
    }
}