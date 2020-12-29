using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WMS.Class;
using WMS.Models;
using WMS.Modules;

namespace WMS.Controllers
{
    public class HomeController : Controller
    {
        private SqlDbConnection database = new SqlDbConnection();

        //private SqlConnection database = new SqlConnection();


        //
        // GET: /Home/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        // POST: Home/Index
        [HttpPost]
        //[NoDirectAccessTribute]
        public ActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Response.Write(System.Convert.ToString(database.DbConnect("new_cpdportal")));
                using (SqlConnection conn = new SqlConnection(System.Convert.ToString(database.DbConnect("new_cpdportal"))))
                {

                    var procedure = "sp_auth_sign_in";
                    int result = 0;

                    try
                    {
                        Response.Write("try open");
                        conn.Open();
                        Response.Write("true");
                        using (SqlCommand comm = new SqlCommand(procedure, conn))
                        {
                            comm.CommandType = CommandType.StoredProcedure;
                            //comm.Parameters.AddWithValue("@employeeid", Convert.ToString(model.EmployeeId));
                            // comm.Parameters.AddWithValue("@employeeid", model.EmployeeId(1));

                            comm.Parameters.AddWithValue("@employeeid", model.EmployeeId[0]);
                            comm.Parameters.AddWithValue("@password", model.Password);



                            comm.Parameters.Add("@emp_name", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                            comm.Parameters.Add("@role_group", SqlDbType.VarChar, 2).Direction = ParameterDirection.Output;
                            comm.Parameters.Add("@chg_pass", SqlDbType.Bit).Direction = ParameterDirection.Output;

                            comm.Parameters.Add("@img_path", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                            comm.Parameters.Add("@division_group", SqlDbType.VarChar, 3).Direction = ParameterDirection.Output;
                            comm.Parameters.Add("@department_group", SqlDbType.VarChar, 3).Direction = ParameterDirection.Output;
                            comm.Parameters.Add("@firstname", SqlDbType.VarChar, 25).Direction = ParameterDirection.Output;
                            comm.Parameters.Add("@logintries", SqlDbType.Int).Direction = ParameterDirection.Output;
                            comm.Parameters.Add("@uname_docutrack", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                            comm.Parameters.Add("@ugroup_docutrack", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                            comm.Parameters.Add("@ustatus_docutrack", SqlDbType.Bit).Direction = ParameterDirection.Output;
                            comm.Parameters.Add("@SignatoryInitial", SqlDbType.VarChar, 3).Direction = ParameterDirection.Output;
                            comm.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.Output;

                            //foreach (SqlParameter param in comm.Parameters)
                            //{

                            //    Debug.Print(param.ParameterName);
                            //    Debug.Print(param.Value);
                            //}

                            //conn.Open();
                            comm.ExecuteScalar();
                            //string getValue = comm.ExecuteScalar().ToString();  
                            //var count = Convert.ToInt32(comm.ExecuteScalar());
                            //string getValue = Convert.ToString(comm.ExecuteScalar());
                            //object o = comm.ExecuteScalar();
                            //comm.ExecuteNonQuery();

                            result = System.Convert.ToInt32(comm.Parameters["@result"].Value);

                            if (result == 0)
                            {

                                // Session["EmployeeID"] = model.EmployeeId[0].Replace(",", "").ToUpper();

                                string c = model.EmployeeId[0];
                                c = c.Replace(",", "");
                                c = c.ToUpper();

                                Session["EmployeeID"] = c;

                                Session["EmployeeName"] = comm.Parameters["@emp_name"].Value;
                                Session["RoleGroup"] = comm.Parameters["@role_group"].Value;
                                Session["BoolChangePassword"] = comm.Parameters["@chg_pass"].Value;

                                // Session["ImagePath"] = ModFunction.GetContextPath() + System.Convert.ToString(comm.Parameters["@img_path"].Value);
                                Session["ImagePath"] = ModFunction.GetContextPath_2() + System.Convert.ToString(comm.Parameters["@img_path"].Value);
                                Session["DivisionGroup"] = comm.Parameters["@division_group"].Value;
                                Session["DepartmentGroup"] = comm.Parameters["@department_group"].Value;
                                Session["FirstName"] = comm.Parameters["@firstname"].Value;
                                Session["DocuTrackUserName"] = comm.Parameters["@uname_docutrack"].Value;

                                Session["DocuTrackGroupName"] = comm.Parameters["@ugroup_docutrack"].Value;

                                Session["BoolUserStatusDocutrack"] = comm.Parameters["@ustatus_docutrack"].Value;
                                Session["SignatoryInitial"] = comm.Parameters["@SignatoryInitial"].Value;

                                string n = "";
                                if (!(ReferenceEquals(Request.Cookies["EmployeeID"], null)))
                                {
                                    //if (Request.Cookies["EmployeeID"].Value.Contains(System.Convert.ToString(model.EmployeeId[0])))
                                    if (Request.Cookies["EmployeeID"].Value.Contains(model.EmployeeId[0]))
                                    {
                                        goto x;
                                    }
                                    n = Request.Cookies["EmployeeID"].Value;
                                }

                                Response.Cookies["EmployeeID"].Value = n + "," + System.Convert.ToString(model.EmployeeId);
                                Response.Cookies["EmployeeID"].Expires = DateTime.Now.AddDays(60);

                            x:

                                //return RedirectToAction("Index", "Dashboard");
                                return RedirectToAction("Index", "MIS");
                            }
                            else
                            {
                                Session["Result"] = result;
                                Session["logintries"] = comm.Parameters["@logintries"].Value;
                                return RedirectToAction("Index", "Home");
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                        Response.Write(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

            }
            return View(model);
        }


        [DataContract]
        public class RecaptchaApiResponse
        {
            [DataMember(Name = "success")]
            public bool Success;

            [DataMember(Name = "error-codes")]
            public List<string> ErrorCodes;
        }

        //--- To get user IP(Optional)
        private string GetUserIp()
        {
            var visitorsIpAddr = string.Empty;

            if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                visitorsIpAddr = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else if (!string.IsNullOrEmpty(Request.UserHostAddress))
            {
                visitorsIpAddr = Request.UserHostAddress;
            }

            return visitorsIpAddr;
        }

        public void btnValidateReCaptcha_Click(object sender, EventArgs e)
        {
            //start building recaptch api call
            var sb = new StringBuilder();
            sb.Append("https://www.google.com/recaptcha/api/siteverify?secret=");

            //our secret key
            var secretKey = "6LeucXUUAAAAADgXqtw-q99qZbDPVsXbrEp7Es_n";
            sb.Append(secretKey);

            //response from recaptch control
            sb.Append("&");
            sb.Append("response=");
            var reCaptchaResponse = Request["g-recaptcha-response"];
            sb.Append(reCaptchaResponse);

            //client ip address
            //---- This Ip address part is optional. If you donot want to send IP address you can
            //---- Skip(Remove below 4 lines)
            sb.Append("&");
            sb.Append("remoteip=");
            var clientIpAddress = GetUserIp();
            sb.Append(clientIpAddress);

            //make the api call and determine validity
            using (var client = new WebClient())
            {
                var uri = sb.ToString();
                var json = client.DownloadString(uri);
                var serializer = new DataContractJsonSerializer(typeof(RecaptchaApiResponse));
                var ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
                var result = serializer.ReadObject(ms) as RecaptchaApiResponse;

                //--- Check if we are able to call api or not.
                if (result == null)
                {
                    //lblForMessage.Text = "Captcha was unable to make the api call";
                    ViewBag.Result = "Captcha was unable to make the api call";
                }
                else // If Yes
                {
                    //api call contains errors
                    if (result.ErrorCodes != null)
                    {
                        if (result.ErrorCodes.Count > 0)
                        {
                            foreach (var error in result.ErrorCodes)
                            {
                                //lblForMessage.Text = "reCAPTCHA Error: " + error;
                                ViewBag.Result = "reCAPTCHA Error: " + error;

                            }
                        }
                    }
                    else //api does not contain errors
                    {
                        if (!result.Success) //captcha was unsuccessful for some reason
                        {
                            //lblForMessage.Text = "Captcha did not pass, please try again.";
                            ViewBag.Result = "Captcha did not pass, please try again.";
                        }
                        else //---- If successfully verified. Do your rest of logic.
                        {
                            //lblForMessage.Text = "Captcha cleared ";
                            ViewBag.Result = "Captcha cleared ";
                        }
                    }

                }

            }
        }
    }
}