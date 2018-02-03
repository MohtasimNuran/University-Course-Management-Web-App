using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementMVC.Models
{

    public class SecondaryViewOrdered
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public List<string> ScheduleList { get; set; }

        public SecondaryViewOrdered(string Code, string Name, String schedule)
        {
            this.Code = Code;
            this.Name = Name;
            ScheduleList = new List<string>();
            ScheduleList.Add(schedule);
        }


    }
}