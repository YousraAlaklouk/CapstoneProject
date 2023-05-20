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
using CapstoneProject.Controllers;
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            string SqlQuery = "SELECT P.PersonID, A.AnimalID FROM Person P INNER JOIN MilkingProcess M ON M.SupervisorID=P.PersonID INNER JOIN Animal A ON M.AnimalID = A.AnimalID WHERE P.Email =@Email";
            con.Open();
            SqlCommand cmd = new SqlCommand(SqlQuery, con); ;
            cmd.Parameters.AddWithValue("@Email", Session["Email"]);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                return RedirectToAction("ProcessInformation", "Supervisor");


            }
            else
            {
                ViewData["Message"] = "Cannot get information";
            }


            con.Close();
            return RedirectToAction("ProcessInformation", "Supervisor");

        }


    }
}