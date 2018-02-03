using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementMVC.Models;

namespace UniversityManagementMVC.Controllers
{
    public class RegisterStudentController : Controller
    {
        UniversityManagementDBEntities4 db = new UniversityManagementDBEntities4();


        public ActionResult RegisterStudent()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name");
            return View();

        }

        [HttpPost]
        public ActionResult RegisterStudent(Student student)
        {
            ModelState.Clear();
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name");
            string year = student.Date.Year.ToString();
            student.RegNo = GenerateRegNo(student.DepartmentId, year);
            if (db.Students.Any(x => x.Email == student.Email || x.Name == student.Name))
            {
                ViewBag.Msg = "Already exists";
            }
            else
            {
                db.Students.Add(student);
                db.SaveChanges();
                ViewBag.Msg = "Saved";
            }
            return View();
        }

        public string GenerateRegNo(int departmentId, string year)
        {
            string dept = db.Departments.Where(x => x.DepartmentId == departmentId).Select(x => x.Code).ToList().LastOrDefault();
            var count = db.Students.Count(x => x.DepartmentId == departmentId);
            string cou = string.Format("{0:D3}", count+1);
            string code = dept + "-" + year + "-" + cou;
            return code;
        }
    }
}