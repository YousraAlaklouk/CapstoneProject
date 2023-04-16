using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Windows;
using System.Windows.Forms;
using System.Web.Mvc.Html;

using CapstoneProject.Models;   


namespace CapstoneProject.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult AdminLogin()
        {
            return View();
        }

        public ActionResult SupervisorLogin()
        {
            return View();
        }

        

    }
}