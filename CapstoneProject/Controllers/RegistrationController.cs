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
    public class RegistrationController : Controller
    {



        public ActionResult AnimalRegistration()
        {
            return View();
        }

        public ActionResult SupervisorRegistration()
        {
            return View();
        }

        public string value = "";

        [HttpPost]
        [Obsolete]
        public ActionResult SupEn(Enroll en)
        {
            try
            {
                if (Request.HttpMethod == "POST")
                {
                    String Sup = "Supervisor";
                    Enroll er = new Enroll();
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Person (Role,Name,Surname ,Gender, Email, PhoneNumber,Password) VALUES (@Role,@Name,@Surname,@Gender, @Email,@PhoneNumber,@Password)", con))
                        {
                            cmd.Parameters.AddWithValue("@Role", Sup);
                            cmd.Parameters.AddWithValue("@Name", en.Name);
                            cmd.Parameters.AddWithValue("@Surname", en.Surname);
                            cmd.Parameters.AddWithValue("@Password", en.Password);
                            cmd.Parameters.AddWithValue("@Gender", en.Gender);
                            cmd.Parameters.AddWithValue("@Email", en.Email);
                            cmd.Parameters.AddWithValue("@PhoneNumber", en.PhoneNumber);
                            con.Open();
                            ViewData["result"] = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                MessageBox.Show("Registered Succssfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return RedirectToAction("AdminInterface", "Admin");
            }
            catch (SqlException ee)
            {
                MessageBox.Show(ee.Message);
                return View("SupervisorRegistration");
            }

        }

        [HttpPost]
        [Obsolete]
        public ActionResult AnimalEn(Animal an)
        {
            try
            {
                if (Request.HttpMethod == "POST")
                {
                    Animal a = new Animal();
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Animal (Type,Age,Gender) VALUES (@type,@age,@gen)", con))
                        {
                            cmd.Parameters.AddWithValue("@type", an.Type);
                            cmd.Parameters.AddWithValue("@age", an.Age);
                            cmd.Parameters.AddWithValue("@gen", an.Gender);

                            con.Open();
                            ViewData["result"] = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                MessageBox.Show("Registered Succssfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return RedirectToAction("AdminInterface", "Admin");
            }
            catch (SqlException ee)
            {
                MessageBox.Show(ee.Message);
                return View("AnimalRegistration");

            }


        }


    }
}