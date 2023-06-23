using CapstoneProject.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Windows.Forms;

namespace CapstoneProject.Controllers
{
    public class SupervisorController : Controller
    {

        SerialPort serialPort;
        string email = LoginController.email;
        ViewModel VR;
        string command;
        public ActionResult SupervisorInterface()
        {

            List <Models.Process> FriendList = new List<Models.Process>();
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
            {

                FriendList = db.Query<Models.Process>("SELECT * FROM MilkingProcess M INNER JOIN Person P ON P.PersonID= M.SupervisorID WHERE  P.Email = '" + email + "'").ToList();
                
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
            //MessageBox.Show(Session["Email"].ToString());
            return An;
        }
        string data;

        [HttpPost]
        [Obsolete]
        public ActionResult ProcessInsert(string txtanimal)
        {
            string s = txtanimal;

            //MessageBox.Show(s);

            try
            {
                if (Request.HttpMethod == "POST")
                {
                    DateTime date = DateTime.Now;
                    //VR = new ViewModel();
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                    {
                        //MessageBox.Show(date.ToString());
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO MilkingProcess (SupervisorID,AnimalID,DateTime ,State) VALUES ((SELECT PersonID FROM Person WHERE Email ='"+Session["Email"]+"'),@AnimalID,'"+date+"', 0)", con))
                        {
                          
                                cmd.Parameters.AddWithValue("@AnimalID", Convert.ToInt32(s));


                                con.Open();
                                ViewData["result"] = cmd.ExecuteNonQuery();
                            con.Close();


                        }


                        using (SqlCommand cmd2 = new SqlCommand("SELECT M.ProcessID FROM MilkingProcess M INNER JOIN Person P ON P.PersonID = M.SupervisorID WHERE M.DateTime = '" + date + "' AND P.Email= '" + Session["Email"] + "'", con))
                    {
                            con.Open();

                            data = cmd2.ExecuteScalar().ToString();


                            con.Close();

                        }




                    }

                    testArduino();

                }
                MessageBox.Show("Started Succssfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return RedirectToAction("Index", "Home");

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return View();

            }


          
        }
            public ActionResult ProcessInformation()
        {


            VR = new ViewModel

            {
/*                Process = ProcessInsert(),
*/                Animals = GetAnimalList(),
                Enroll = EnrollList()
            };
            return View(VR);
            

        }

        public void testArduino()
        {
            command = "1";
            serialPort = new SerialPort("COM3", 9600);

            try
            {
                serialPort.Open();
                serialPort.Write(command);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            while (serialPort.ReadLine().Contains("0"))
            {

            }
            //MessageBox.Show("outside");


            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE MilkingProcess SET State = 1 WHERE ProcessID =" + data + " ", con))
                {


                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("The Process Stopped Automaticly Because of Blood Detection");

                }
                con.Close();

            }
            command = "0";
            serialPort.Write(command);

        }

        public void getArduinoData()
        {
            serialPort = new SerialPort("COM3",9600);
            try
            {
                command = "1";


                string min;
                string ldr;
                string level;
                serialPort.Open();
                serialPort.Write(command);
                //MessageBox.Show("beginning of try");
                int i=0;
                
                while (true)
                {
                    
                    Thread.Sleep(5000);
                    //MessageBox.Show("beginning of while");
                    string a = serialPort.ReadExisting();
                    Console.WriteLine(a);
                    Thread.Sleep(1000);
                    string[] b = a.Split(',');
                    
                    MessageBox.Show(a);
                    //MessageBox.Show(c);
                    //ldr = b[0].ToString();
                    //Thread.Sleep(1000);

                    //level = b[2].ToString();
                    
                    //Thread.Sleep(1000);
                    //min = b[1].ToString();
                    //Thread.Sleep(1000);

                    //Thread.Sleep(1000);

                    //if (i > 1)
                    //{


                        




                    //    if (Convert.ToInt32(ldr) < Convert.ToInt32(min) - 5)
                    //    {

                    //        Thread.Sleep(1000);

                    //        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                    //        {
                    //            using (SqlCommand cmd = new SqlCommand("UPDATE MilkingProcess SET State = 1 WHERE ProcessID =" + data + " ", con))
                    //            {


                    //                con.Open();
                    //                cmd.ExecuteNonQuery();
                    //                MessageBox.Show("The Process Stopped Automaticly Because of Blood Detected and the milk level =" + b[2].ToString() + "%");

                    //            }
                    //            con.Close();

                    //        }
                            
                    //        break;


                    //    }
                    //}

                    
                    //i++;
                }
                
            }
            catch (Exception e)
            {
                command = "0";
                serialPort.Write(command);
                MessageBox.Show(e.ToString());
                //serialPort.Close();
            }
            command = "0" ;
            //serialPort.Close();
            serialPort.Write(command);

        }


    }
}