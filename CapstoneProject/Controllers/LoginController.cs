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

        public static string email = "";

        // GET: Login
        public string status;
        public ActionResult AdminLogin()
        {
            return View();
        }

        public ActionResult SupervisorLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Supervisorlog(Enroll e)
        {
            //String SqlCon = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-CNJT2HB\\SQLEXPRESS;Initial Catalog= MilkingSystem;Integrated Security=True");
            string SqlQuery = "SELECT Email,Password FROM Person WHERE Email=@Email AND Password=@Password AND Role = Supervisor";
            con.Open();
            SqlCommand cmd = new SqlCommand(SqlQuery, con); ;
            cmd.Parameters.AddWithValue("@Email", e.Email);
            cmd.Parameters.AddWithValue("@Password", e.Password);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Session["Email"] = e.Email.ToString();
                return View("Home");
                e.Email = email;

            }
            else
            {
                ViewData["Message"] = "User Login Details Failed!!";
            }
            if (e.Email.ToString() != null)
            {
                Session["Email"] = e.Email.ToString();
                status = "1";
            }
            else
            {
                status = "3";
            }

            con.Close();
            return View("Home");
            //return new JsonResult { Data = new { status = status } };  
        }
        [HttpPost]
        public ActionResult Adminlog(Enroll en)
        {
            //String SqlCon = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-CNJT2HB\\SQLEXPRESS;Initial Catalog= MilkingSystem;Integrated Security=True");
            string SqlQuery = "SELECT Email,Password FROM Person WHERE Email=@Email AND Password=@Password AND Role ='Admin'";
            con.Open();
            SqlCommand cmd = new SqlCommand(SqlQuery, con); ;
            cmd.Parameters.AddWithValue("@Email", en.Email);
            cmd.Parameters.AddWithValue("@Password", en.Password);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Session["Email"] = en.Email.ToString();
                return View("Home");
                en.Email = email;

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
            return View("Home");
            //return new JsonResult { Data = new { status = status } };  
        }

    }
}