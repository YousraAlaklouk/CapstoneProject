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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            string SqlQuery = "SELECT * FROM Person WHERE Email=@Email AND Password=@Password AND Role ='Admin'";
            con.Open();
            SqlCommand cmd = new SqlCommand(SqlQuery, con); ;
            cmd.Parameters.AddWithValue("@Email", en.Email);
            cmd.Parameters.AddWithValue("@Password", en.Password);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Session["Email"] = en.Email.ToString();
                return RedirectToAction("AdminInterface", "Admin");
                en.Email = email;
                en.role = "Admin";

            }
            else
            {
                ViewData["Message"] = "User Login Details Failed!!";
            }
            if (en.Email.ToString() != null)
            {
                Session["Email"] = en.Email.ToString();
                status = "1";
            }
            else
            {
                status = "3";
            }

            con.Close();
            return RedirectToAction("AdminInterface", "Admin");

            return View();
        }


        }
}