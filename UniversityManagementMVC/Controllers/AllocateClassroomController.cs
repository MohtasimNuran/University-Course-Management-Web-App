using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using UniversityManagementMVC.Models;

namespace UniversityManagementMVC.Controllers
{
    public class AllocateClassroomController : Controller
    {
        private UniversityManagementDBEntities4 db = new UniversityManagementDBEntities4();

        public ActionResult AllocateClassroom()
        {
            var allocateClass = GetDepartments();
            ViewBag.Rooms = GetRooms();
            ViewBag.Days = GetDays();
            return View(allocateClass);
        }

        [HttpPost]
        public ActionResult AllocateClassroom(AllocateClassroom allocateClassroom)
        {
            var courseAssign = GetDepartments();
            ViewBag.Rooms = GetRooms();
            ViewBag.Days = GetDays();
            //db.AllocateClassrooms.Add(allocateClassroom);
            //db.SaveChanges();
            //ViewBag.Msg = "Saved";
            return View();
        }

        private AllocateClass GetDepartments()
        {
            List<SelectListItem> departments = new List<SelectListItem>();

            AllocateClass allocateClass = new AllocateClass();

            List<Department> states = db.Departments.ToList();
            states.ForEach(x =>
            {
                departments.Add(new
                    SelectListItem
                {
                    Text = x.Name,
                    Value = x.DepartmentId.ToString()
                });
            });
            allocateClass.Departments = departments;
            return allocateClass;
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
                    x =>
                    {
                        courses.Add(new
                            SelectListItem
                        {
                            Text = x.Code,
                            Value = x.CourseId.ToString()
                        });
                    });
            }
            return courses;
        }

        public JsonResult GetCourse(string departmentId)
        {
            var courses = GetCourses(departmentId);
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        private List<Room> GetRooms()
        {
            List<Room> rooms = db.Rooms.Select(x => x).ToList();
            return rooms;
        }

        private List<Day> GetDays()
        {
            List<Day> days = db.Days.Select(x => x).ToList();
            return days;
        }

        public JsonResult Allocate(AllocateClassroom allocateClassroom)
        {
            //var value = db.AllocateClassrooms.Where(
            //    x =>
            //        (
            //        x.Day == allocateClassroom.Day) || (x.Room == allocateClassroom.Room) ||
            //        (x.Frm > allocateClassroom.Frm && x.Too>allocateClassroom.Frm)||
            //        (x.Frm<allocateClassroom.Frm && x.Too<allocateClassroom.Frm)
            //        );


            //var classRoomAllocations = db.AllocateClassrooms.Where(
            //        t =>
            //            (
            //            t.Room == allocateClassroom.Room) && (t.Day == allocateClassroom.Day) &&
            //            (t.Frm < allocateClassroom.Frm && t.Too > allocateClassroom.Frm) ||
            //            (t.Frm < allocateClassroom.Too && t.Too > allocateClassroom.Too) ||
            //                (t.Frm > allocateClassroom.Frm && t.Frm < allocateClassroom.Too) ||
            //                (t.Too > allocateClassroom.Frm && t.Too < allocateClassroom.Too)
            //            ).ToList();

            //var dh =
            //    db.AllocateClassrooms.Where(x => x.Day == allocateClassroom.Day && x.Room == allocateClassroom.Room)
            //        .Select(c => new
            //{
            //    CourseId = c.AllocateClassroomId
            //}).ToList().LastOrDefault();

            //var view = db.AllocateClassrooms.Where(
            //            x =>
            //                (x.AllocateClassroomId == dh.CourseId) &&
            //                ((x.Frm > allocateClassroom.Frm && x.Frm > allocateClassroom.Too) ||
            //                (x.Too < allocateClassroom.Frm && x.Too < allocateClassroom.Too))
            //            ).ToList();



            //db.AllocateClassrooms.Add(allocateClassroom);
            //db.SaveChanges();
            //var saved = "Allocate successfully";
            //return Json(saved, JsonRequestBehavior.AllowGet);




            //if (db.AllocateClassrooms.Any(x => x.Day == allocateClassroom.Day))
            //{
            //    if (db.AllocateClassrooms.Any(x => x.Room == allocateClassroom.Room))
            //    {
            //        var view = db.AllocateClassrooms.Where(
            //            x =>
            //                ((x.Room == allocateClassroom.Room) || (x.Day == allocateClassroom.Day)) &&
            //                ((x.Frm > allocateClassroom.Frm && x.Frm > allocateClassroom.Too) ||
            //                (x.Too < allocateClassroom.Frm && x.Too < allocateClassroom.Too))
            //            ).ToList();


            //        if (view.Count == 0)
            //        {
            //            db.AllocateClassrooms.Add(allocateClassroom);
            //            db.SaveChanges();
            //            var saved = "Allocate successfully";
            //            return Json(saved, JsonRequestBehavior.AllowGet);
            //        }
            //        else
            //        {
            //            var saved = "Already allocated";
            //            return Json(saved, JsonRequestBehavior.AllowGet);
            //        }
            //    }
            //    else
            //    {
            //        db.AllocateClassrooms.Add(allocateClassroom);
            //        db.SaveChanges();
            //        var saved = "Allocate successfully";
            //        return Json(saved, JsonRequestBehavior.AllowGet);
            //    }
            //}
            //else
            //{
            //    db.AllocateClassrooms.Add(allocateClassroom);
            //    db.SaveChanges();
            //    var saved = "Allocate successfully";
            //    return Json(saved, JsonRequestBehavior.AllowGet);
            //}

            var listOfAllocated =
                db.AllocateClassrooms.Where(x => x.Day == allocateClassroom.Day && x.Room == allocateClassroom.Room)
                    .ToList();

            //var view = listOfAllocated.Any(
            //            x =>

            //                //(x.Frm>=allocateClassroom.Frm && x.Frm>=allocateClassroom.Too)
            //                ////(x.Frm == allocateClassroom.Frm && x.Too == allocateClassroom.Too) ||
            //                //((x.Frm > allocateClassroom.Frm && x.Frm > allocateClassroom.Too) ||
            //                //(x.Too < allocateClassroom.Frm && x.Too < allocateClassroom.Too))
            //            );


            int count = 0;

            foreach (var x in listOfAllocated)
            {

                if ((x.Frm == allocateClassroom.Frm) ||
                    (x.Frm == allocateClassroom.Too || x.Too == allocateClassroom.Frm) ||
                    (x.Too == allocateClassroom.Too))
                {
                    var ac = "no";
                    //break;
                }

                if ((x.Frm > allocateClassroom.Frm || x.Frm > allocateClassroom.Too))
                {
                    if ((x.Frm > allocateClassroom.Frm && x.Frm > allocateClassroom.Too))
                    {
                        var ac = "yes";
                        count++;
                        //break;
                    }
                    else
                    {
                        var ac = "no";
                        //break;
                    }
                }
                if (x.Too < allocateClassroom.Frm || x.Too < allocateClassroom.Too)
                {
                    if (x.Too < allocateClassroom.Frm && x.Too < allocateClassroom.Too)
                    {
                        var ac = "yes";
                        count++;
                        //break;
                    }
                    else
                    {
                        var ac = "no";
                        //break;
                    }
                }
            }

            if (listOfAllocated.Count == count)
            {
                db.AllocateClassrooms.Add(allocateClassroom);
                db.SaveChanges();
                var saved = "Allocate successfully";
                return Json(saved, JsonRequestBehavior.AllowGet);
            }

            else
            {
                var saved = "Already allocated";
                return Json(saved, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
