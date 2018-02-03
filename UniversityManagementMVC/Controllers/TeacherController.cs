using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityManagementMVC.Models;

namespace UniversityManagementMVC.Controllers
{
    public class TeacherController : Controller
    {
        private UniversityManagementDBEntities4 db = new UniversityManagementDBEntities4();

        

        // GET: /Teacher/Create
        public ActionResult SaveTeacher()
        {
            ModelState.Clear();
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            ViewBag.Designation = new SelectList(db.Designations, "Designation1", "Designation1");
            return View();
        }

        // POST: /Teacher/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTeacher([Bind(Include = "TeacherId,Name,Address,Email,ContactNo,Designation,DepartmentId,CreditToBeTaken")] Teacher teacher)
        {
            ModelState.Clear();
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", teacher.DepartmentId);
            ViewBag.Designation = new SelectList(db.Designations, "Designation1", "Designation1", teacher.Designation);

            if (db.Teachers.Any(x => x.Name == teacher.Name || x.Email == teacher.Email))
            {
                ViewBag.Msg = "Already exists";
            }
            else
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                ViewBag.Msg = "Saved";
            }
            return View(teacher);
        }

       
    }
}
