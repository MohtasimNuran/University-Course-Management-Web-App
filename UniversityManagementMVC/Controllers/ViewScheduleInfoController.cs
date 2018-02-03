using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementMVC.Models;

namespace UniversityManagementMVC.Controllers
{
    public class ViewScheduleInfoController : Controller
    {
        private UniversityManagementDBEntities4 db = new UniversityManagementDBEntities4();

        public ActionResult ViewSchedule()
        {
            var allocateClass = GetDepartments();
            return View(allocateClass);
        }

        [HttpPost]
        public ActionResult ViewSchedule(int departmentId, int teacherId, int courseId)
        {
            var courseAssign = GetDepartments();
            return View();
        }

        private AllocateClass GetDepartments()
        {
            List<SelectListItem> departments = new List<SelectListItem>();

            AllocateClass allocateClass = new AllocateClass();

            List<Department> states = db.Departments.ToList();
            states.ForEach(
                x => { departments.Add(new SelectListItem { Text = x.Name, Value = x.DepartmentId.ToString() }); });
            allocateClass.Departments = departments;
            return allocateClass;
        }
        public JsonResult GetSchedule(int departmentId)
        {

            var viewAll = (db.Courses.Where(x => x.DepartmentId == departmentId).Select(x => new
            {
                x.Code,
                x.Name,
                Room = "Not Scheduled Yet",
                Day = "",
                Frm = "",
                Too = ""

            })).ToList();

            var viewWithList = (db.AllocateClassrooms.Where(x => x.DepartmentId == departmentId)).ToList();

            var viewStringList = viewWithList.Select(x => x.Too != null ? (x.Frm != null ? new
            {
                x.Course.Code,
                x.Course.Name,
                x.Room,
                x.Day,
                Frm = x.Frm.Value.ToString(),
                Too = x.Too.Value.ToString()

            } : null) : null).ToList();

            var viewDateFormatList = viewStringList.Select(x => new
            {
                x.Code,
                x.Name,
                x.Room,
                x.Day,
                Frm = Convert.ToDateTime(x.Frm).ToShortTimeString(),
                Too = Convert.ToDateTime(x.Too).ToShortTimeString()
            }).ToList();


            var result = viewAll.Where(x => viewDateFormatList.All(i => x.Code != i.Code)).ToList();
            var view = result.Union(viewDateFormatList).ToList();
            var viewOrdered = view.OrderBy(x => x.Code).ToList();

            //gobinda
            string code = string.Empty;

            List<SecondaryViewOrdered> secondaryViewOrdereds = new List<SecondaryViewOrdered>();

            int pos = -1;
            foreach (var x in viewOrdered)
            {
                if (code == x.Code)
                {
                    string nn = "</br>" +"R.No : "+ x.Room + ", " + x.Day + ", " + x.Frm + "-" + x.Too;
                    secondaryViewOrdereds[pos].ScheduleList.Add(nn);
                }
                else
                {
                    pos++;
                    code = x.Code;
                    string nn = "R.No : " + x.Room + ", " + x.Day + ", " + x.Frm + "-" + x.Too;
                    SecondaryViewOrdered svo = new SecondaryViewOrdered(x.Code, x.Name, nn);
                    secondaryViewOrdereds.Add(svo);
                }
            }
            

            //int count = 0;
            //for (int i = 0; i < viewOrdered.Count; i++)
            //{
            //    if (viewOrdered[i].Code == viewOrdered[i + 1].Code)
            //    {
            //        i++;
            //    }
            //    else
            //    {
            //        i++;
            //    }
            //}






            //var vvv = (viewOrdered.GroupBy(c => c.Code).Where(g => g.Count() > 1).Select(y => new
            //{
            //    Code = y.Key
            //})).ToList();

            //foreach (var v in )
            //{
            //    if (v.Code==viewOrdered.)
            //    {
            //        var xyz = viewOrdered.Select(x => new
            //        {
            //            x.Room,
            //            x.Day,
            //            x.Frm,
            //            x.Too
            //        }).ToList();
            //    }
            //}



            //foreach (var y in vvv)
            //{
            //    var xyz = viewOrdered.Where(x => x.Code == y.Code).Select(x => new
            //    {
            //        x.Room,
            //        x.Day,
            //        x.Frm,
            //        x.Too
            //    }).ToList();
            //}




            //var vv= viewOrdered.GroupBy(c => c.Code).Where(g => g.Skip(1).Any()).ToList();

            //var vvvv = vv.GroupBy(x => x.Code).Any(x => x.Any());
            //var vvvv = vv.GroupBy(x => x.Code).Where(x => x.Any()).ToList();

            //var query = lst.GroupBy(x => x)
            //  .Where(g => g.Count() > 1)
            //  .Select(y => y.Key)
            //  .ToList();

            return Json(secondaryViewOrdereds, JsonRequestBehavior.AllowGet);
        }

    }
}