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
using System.Web.Helpers;
using System.Windows.Forms;
using System.Web.Mvc.Html;

namespace CapstoneProject.Controllers
{
    public class SupervisorController : Controller
    {
        string email = LoginController.email;
        public ActionResult SupervisorInterface()
        {

            List < Models.Process> FriendList = new List<Models.Process>();
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
            {

                FriendList = db.Query<Models.Process>("SELECT * FROM MilkingProcess M INNER JOIN Person P ON P.PersonID= M.SupervisorID WHERE  P.Email = '" + Session["Email"].ToString() + "'").ToList();

            }
            return View(FriendList);
        }
        /*        public ActionResult DropDownControl()

                {
                        string SqlQuery = "SELECT P.PersonID, A.AnimalID FROM Person P INNER JOIN MilkingProcess M ON M.SupervisorID=P.PersonID INNER JOIN Animal A ON M.AnimalID = A.AnimalID WHERE P.Email =@Email";
                        cmd.Parameters.AddWithValue("@Email", email);


                }*/
        public List<Animal> GetAnimalList()

        {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
                string SqlQuery = "SELECT AnimalID FROM Animal";
                con.Open();
                SqlCommand cmd = new SqlCommand(SqlQuery, con); ;
                SqlDataReader sdr = cmd.ExecuteReader();
                List<Animal> An = new List<Animal>();
                try
                {
                    while (sdr.Read())
                    {
                        An.Add(new Animal
                        {


                            ID = Convert.ToInt32(sdr["AnimalID"]),



                        });
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }


                con.Close();

                return An;
            }




        /*                {
                            return RedirectToAction("ProcessInformation", "Supervisor");
                            con.Close();

                        }
                        else
                        {
                            ViewData["Message"] = "Cannot get information";
                            con.Close();

                        }

                    }
                    catch (SqlException ee)
                    {

                        MessageBox.Show(ee.Message);
                        return View("ProcessInformation");

                    }
                    return View("ProcessInformation");


                    *//*            return RedirectToAction("ProcessInformation", "Supervisor");
                       *            , A.AnimalID FROM Person P INNER JOIN MilkingProcess M ON M.SupervisorID=P.PersonID INNER JOIN Animal A ON M.AnimalID = A.AnimalID 
                    *//*
                }*/

        public List<Enroll> EnrollList() {


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            string SqlQuery = "SELECT PersonID FROM Person WHERE Email ='"+Session["Email"]+"'";
            con.Open();
            SqlCommand cmd = new SqlCommand(SqlQuery, con); ;
            SqlDataReader sdr = cmd.ExecuteReader();
            List<Enroll> An = new List<Enroll>();
            try
            {
                while (sdr.Read())
                {
                    An.Add(new Enroll
                    {
                        PersonID = Convert.ToInt32(sdr["PersonID"]),
                    });
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }


            con.Close();

            return An;
        }
        [HttpPost]
        [Obsolete]
        public ActionResult ProcessInsert(string txtanimal)
        {
            string s = txtanimal;

            MessageBox.Show(s);

            try
            {
                if (Request.HttpMethod == "POST")
                {
                    ViewModel vm = new ViewModel();
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO MilkingProcess (SupervisorID,AnimalID,DateTime ,State) VALUES ((SELECT PersonID FROM Person WHERE Email ='"+Session["Email"]+"'),@AnimalID,(SELECT GETDATE()), 1)", con))
                        {
                            cmd.Parameters.AddWithValue("@AnimalID", Convert.ToInt32(s));


                            con.Open();


                            ViewData["result"] = cmd.ExecuteNonQuery();
                            con.Close();

                        }
                    }
                }
                MessageBox.Show("Started Succssfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return View();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return View();

            }


          
        }
            public ActionResult ProcessInformation()
        {

            ViewModel VR = new ViewModel

            {
/*                Process = ProcessInsert(),
*/                Animals = GetAnimalList(),
                Enroll = EnrollList()
            };


            
        




            return View(VR);

        }
    }
}