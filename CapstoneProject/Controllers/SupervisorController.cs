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
    public class SupervisorController : Controller
    {
        public ActionResult SupervisorInterface()
        {
            List < Models.Process> FriendList = new List<Models.Process>();
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
            {

                FriendList = db.Query<Models.Process>("SELECT * FROM MilkingProcess M INNER JOIN Person P ON P.PersonID= M.SupervisorID WHERE  P.Email = '" + Session["Email"].ToString() + "'").ToList();

            }
            return View(FriendList);
        }

        public ActionResult ProcessInformation()
        {
            return View();
        }


    }
}