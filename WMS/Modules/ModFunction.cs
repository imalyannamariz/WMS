using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows;
using WMS.Class;
using WMS.Models;

namespace WMS.Modules
{
    
        public sealed class ModFunction
        {
            private static SqlDbConnection database = new SqlDbConnection();
            private static dataHelper datahelper = new dataHelper();

            public static IEnumerable<OnlineUsersDepartmentModel> GetOnlineUsersDepartment(dynamic EmployeeID)
            {
                List<OnlineUsersDepartmentModel> list = new List<OnlineUsersDepartmentModel>();
                using (SqlConnection conn = new SqlConnection(System.Convert.ToString(database.DbConnect("new_cpdportal"))))
                {
                    var procedure = "sp_list_users_online_dept";
                    try
                    {
                        conn.Open();
                        using (SqlCommand comm = new SqlCommand(procedure, conn))
                        {
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Parameters.AddWithValue("@employeeid", EmployeeID);
                            using (SqlDataReader rdr = comm.ExecuteReader())
                            {
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        OnlineUsersDepartmentModel records = new OnlineUsersDepartmentModel();
                                        records.DepartmentName = rdr.GetString(0);
                                        records.DepartmentID = rdr.GetString(1);
                                        list.Add(records);
                                    }
                                }
                            }

                        }

                    }
                    catch (Exception)
                    {
                        //error page here.
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

                return list;
            }

            public static IEnumerable<OnlineUsersNameModel> GetOnlineUsersName(dynamic DepartmentID, dynamic EmployeeID)
            {
                List<OnlineUsersNameModel> list = new List<OnlineUsersNameModel>();
                using (SqlConnection conn = new SqlConnection(System.Convert.ToString(database.DbConnect("new_cpdportal"))))
                {
                    var procedure = "sp_list_users_online";
                    try
                    {
                        conn.Open();
                        using (SqlCommand comm = new SqlCommand(procedure, conn))
                        {
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Parameters.AddWithValue("@department_group", DepartmentID);
                            comm.Parameters.AddWithValue("@employeeid", EmployeeID);
                            using (SqlDataReader rdr = comm.ExecuteReader())
                            {
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        OnlineUsersNameModel records = new OnlineUsersNameModel();
                                        records.Name = rdr.GetString(0);
                                        records.Avatar = GetContextPath() + rdr.GetString(1);
                                        list.Add(records);
                                    }
                                }
                            }

                        }

                    }
                    catch (Exception)
                    {
                        //error page here.
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

                return list;
            }

            public static dynamic GetContextPath()
            {
                var url = HttpContext.Current.Request.Url.ToString();
                var context = string.Empty;


                if (url.Contains("megaworldcpd"))
                {
                    context = "/megaworldcpd";
                }

                //lcl 10.22.2018    -   this is stored in custom.js
                //if (url.Contains("/MIS"))
                //{
                //    context = "/megaworldcpd";
                //}


                return context;
            }

            public static dynamic GetContextPath_2()
            {
                var url = HttpContext.Current.Request.Url.ToString();
                var context = string.Empty;

                if (url.Contains("megaworldcpd"))
                {
                    context = "/megaworldcpd";
                }

                return url;
            }

            public static dynamic GetContextPath_3()
            {
                //updated 10.29.2018
                var url = HttpContext.Current.Request.Url.ToString();
                var context = string.Empty;

                string baseUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority +
                                 HttpContext.Current.Request.ApplicationPath.TrimEnd('/');
                //HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";


                //if (url.Contains("megaworldcpd"))
                //{
                //    context = "/megaworldcpd";
                //}

                if (baseUrl.Contains("megaworldcpd"))
                {
                    context = "/megaworldcpd";
                    baseUrl = baseUrl.Replace("/megaworldcpd", "");
                }


                return baseUrl;
            }



            public static IEnumerable<ProjectsModel> GetListOfProjects(dynamic RoleGroup, dynamic DivisionGroup, dynamic DepartmentGroup)
            {
                List<ProjectsModel> list = new List<ProjectsModel>();
                using (SqlConnection conn = new SqlConnection(System.Convert.ToString(database.DbConnect("new_cpdportal"))))
                {
                    var procedure = "sp_list_of_projects";
                    try
                    {
                        conn.Open();
                        using (SqlCommand comm = new SqlCommand(procedure, conn))
                        {
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Parameters.AddWithValue("@role_group", RoleGroup);
                            comm.Parameters.AddWithValue("@division_group", DivisionGroup);
                            comm.Parameters.AddWithValue("@department_group", DepartmentGroup);
                            using (SqlDataReader rdr = comm.ExecuteReader())
                            {
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        ProjectsModel records = new ProjectsModel();
                                        records.ProjectSite = rdr.GetString(0);
                                        list.Add(records);
                                    }
                                }
                            }

                        }

                    }
                    catch (Exception)
                    {
                        // error page here
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

                return list;
            }

            public static IEnumerable<RecipientsRetagModel> GetAllRecipientsRetag(dynamic ID, dynamic EID)
            {
                List<RecipientsRetagModel> list = new List<RecipientsRetagModel>();
                using (SqlConnection conn = new SqlConnection(System.Convert.ToString(database.DbConnect("new_cpdportal"))))
                {
                    var procedure = "sp_list_of_recipients_retag";
                    try
                    {
                        conn.Open();
                        using (SqlCommand comm = new SqlCommand(procedure, conn))
                        {
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Parameters.AddWithValue("@shoutbox_id", ID);
                            comm.Parameters.AddWithValue("@employeeid", EID);
                            using (SqlDataReader rdr = comm.ExecuteReader())
                            {
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        RecipientsRetagModel records = new RecipientsRetagModel();
                                        records.EID = rdr.GetString(0);
                                        records.EName = rdr.GetString(1);
                                        list.Add(records);
                                    }
                                }
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

                return list;
            }

            public static string EncodeID(dynamic ID)
            {
                //byte[] encoded = System.Text.Encoding.UTF8.GetBytes((char[])ID);

                byte[] encoded = System.Text.Encoding.UTF8.GetBytes((string)ID);


                return Convert.ToBase64String(encoded);
            }

            public static string DecodeID(dynamic ID)
            {
                byte[] encoded = Convert.FromBase64String(System.Convert.ToString(ID));
                return System.Text.Encoding.UTF8.GetString(encoded);
            }

            public static string ProjectName(dynamic ID)
            {
                var name = string.Empty;
                using (SqlConnection conn = new SqlConnection(System.Convert.ToString(database.DbConnect("new_cpdportal"))))
                {
                    var procedure = "sp_name_project";
                    try
                    {
                        conn.Open();
                        using (SqlCommand comm = new SqlCommand(procedure, conn))
                        {
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Parameters.AddWithValue("@project_id", ID);
                            using (SqlDataReader rdr = comm.ExecuteReader())
                            {
                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        name = rdr.GetString(0);
                                    }
                                }
                            }

                        }

                    }
                    catch (Exception)
                    {
                        // error message here.
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

                return name;
            }


            public static AdministrationModel GetUserAccounts()
            {
                var records = new AdministrationModel();

                string iNewAccounts = "";
                string iDisabledAccounts = "";
                string iAllAccounts = "";

                using (SqlConnection conn = new SqlConnection(database.DbConnect("new_cpdportal")))
                {
                    var procedure = "sp_count_users";
                    try
                    {
                        conn.Open();
                        using (SqlCommand comm = new SqlCommand(procedure, conn))
                        {
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Parameters.Add("@new_accounts", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                            comm.Parameters.Add("@disabled_accounts", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                            comm.Parameters.Add("@total_accounts", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                            comm.ExecuteScalar();

                            iNewAccounts = comm.Parameters["@new_accounts"].Value.ToString();
                            iDisabledAccounts = comm.Parameters["@disabled_accounts"].Value.ToString();
                            iAllAccounts = comm.Parameters["@total_accounts"].Value.ToString();

                            records.NewAccounts = (string)iNewAccounts.ToString();
                            records.DisabledAccounts = (string)iDisabledAccounts.ToString();
                            records.AllAccounts = (string)iAllAccounts.ToString();

                            //records.NewAccounts = (string)comm.Parameters["@new_accounts"].Value;
                            //records.DisabledAccounts = (string)comm.Parameters["@disabled_accounts"].Value;
                            //records.AllAccounts = (string)comm.Parameters["@total_accounts"].Value;
                        }

                    }
                    catch (Exception ex)
                    {
                        //error here
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }

                }

                return records;
            }
        }
    
}