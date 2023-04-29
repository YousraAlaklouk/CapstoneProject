using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace CapstoneProject.Models
{
    public class Process
    {

        public int ProcessID { get; set; }

        public int SupervisorID { get; set; }


        public int AnimalID { get; set; }
        public DateTime DateTime { get; set; }
        public string State { get; set; }




    }

}