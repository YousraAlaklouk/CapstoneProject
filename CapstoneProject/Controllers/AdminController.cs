using CapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Data;
using Dapper;
using System.Configuration;

namespace CapstoneProject.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult AdminInterface()
        {
            return View();
        }

        public ActionResult SupervisorList()
        {
            List<Models.Enroll> FriendList = new List<Models.Enroll>();
            using (IDbConnection db = new SqlConnection("Data Source=DESKTOP-CNJT2HB\\SQLEXPRESS;Initial Catalog= MilkingSystem;Integrated Security=True"))
            {

                FriendList = db.Query<Models.Enroll>("SELECT * FROM Person WHERE Role != 'Admin'").ToList();

            }
            return View(FriendList);
     }
        public ActionResult ProcessHistory()
        {
            List<Models.Process> FriendList = new List<Models.Process>();
            using (IDbConnection db = new SqlConnection("Data Source=DESKTOP-CNJT2HB\\SQLEXPRESS;Initial Catalog= MilkingSystem;Integrated Security=True"))
            {

                FriendList = db.Query<Models.Process>("SELECT * FROM MilkingProcess").ToList();

            }
            return View(FriendList);
        }




    }
}