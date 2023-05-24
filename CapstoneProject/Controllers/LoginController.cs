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
using System.Threading;

using CapstoneProject.Models;
using System.Net.Mail;
using System.Net;

namespace CapstoneProject.Controllers
{
    public class LoginController : Controller
    {

        public static string email;

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


        public ActionResult ForgotPassword()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(Enroll e)
        {
            Random random = new System.Random();
            var value = "Aq" + random.Next(9999, 1000000) + "rs";

            using (MailMessage mail = new MailMessage())
            {

                mail.From = new MailAddress("samaalfares565@gmail.com");
                mail.To.Add(e.Email);
                mail.Subject = "Reset Password";
                mail.Body = "Dear Customer, <br/><br/>Yor new Password is as follows:<br/></br><h2> " + value + "</h2><br/><br/>Thanks";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("samaalfares565@gmail.com", "rleelwpahxpekdlw");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }

            try
            {
                if (Request.HttpMethod == "POST")
                {
                    Enroll er = new Enroll();
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("update Person set Password = '" + value + "' where Email = '" + e.Email + "'", con))
                        {
                            con.Open();
                            ViewData["result"] = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                return View("AdminLogin");
            }
            catch (SqlException ee)
            {
                MessageBox.Show(ee.Message);
                return View();
            }
        }

        [HttpPost]
        public ActionResult Supervisorlogin(Enroll e)
        {
            try
            {

                //String SqlCon = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
                string SqlQuery = "SELECT Email,Password FROM Person WHERE Email=@Email AND Password=@Password AND Role = 'Supervisor'";
                con.Open();
                SqlCommand cmd = new SqlCommand(SqlQuery, con); ;
                cmd.Parameters.AddWithValue("@Email", e.Email);
                cmd.Parameters.AddWithValue("@Password", e.Password);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    Session["Email"] = e.Email.ToString();
                    email = e.Email.ToString();
                    e.role = "Supervisor";
                    MessageBox.Show(" the email is " + email);
                    Thread.Sleep(1000);
                    return RedirectToAction("SupervisorInterface", "Supervisor");


                }
                else
                {

                    MessageBox.Show("Wrong Username Or Password  ");

                    ViewData["Message"] = "User Login Details Failed!!";

                }
                if (email != null)
                {
                    Session["Email"] = e.Email.ToString();
                    status = "1";
                    return RedirectToAction("Index", "Home");


                }
                else
                {
                    Session["Email"] = "";

                    status = "3";
                    return RedirectToAction("Index", "Home");

                }
                con.Close();

            }
            catch (Exception er)
            {

                MessageBox.Show("Error", er.Message);
                Thread.Sleep(30);
                return RedirectToAction("Index", "Home");

            }


            //return new JsonResult { Data = new { status = status } };  
        }
        [HttpPost]
        public ActionResult Adminlogin(Enroll en)
        {
            try
            {

                //String SqlCon = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
                string SqlQuery = "SELECT Email,Password FROM Person WHERE Email=@Email AND Password=@Password AND Role ='Admin'";
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
                    MessageBox.Show("Wrong Username Or Password  ");

                    ViewData["Message"] = "User Login Details Failed!!";
                }
                if (email != null)
                {
                    Session["Email"] = en.Email.ToString();
                    status = "1";
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    status = "3";
                    Session["Email"] = "";
                    return RedirectToAction("Index", "Home");

                }

                con.Close();

            }
            catch (Exception er)
            {

                MessageBox.Show("Error", er.Message);
                Thread.Sleep(30);
                return RedirectToAction("Index", "Home");

            }
        }
    }
}