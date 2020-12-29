using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows;

namespace WMS.Class
{
    public class dataHelper
    {
        private SqlDbConnection database = new SqlDbConnection();

        public DataTable GetRecordsV2(dynamic Constr, dynamic storedProcedure, string[] @params, string[] values)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Convert.ToString(Constr)))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(System.Convert.ToString(storedProcedure), conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        double cnt = 0;
                        while (@params.Length > cnt)
                        {
                            var xparam = @params[(int)cnt];
                            var xvalue = values[(int)cnt];
                            if (!string.IsNullOrEmpty(xparam))
                            {
                                cmd.Parameters.AddWithValue(xparam, xvalue);
                            }
                            cnt++;
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            da.SelectCommand = cmd;
                            da.Fill(dt);
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
            return dt;
        }

        //lcl 4/2/2018 -  @params = parameter of the SP while values = values of the parameter.
        public string GetSingleRecord(dynamic Constr, dynamic storedProcedure, string[] @params, string[] values)
        {
            string str_return = "";

            using (SqlConnection conn = new SqlConnection(System.Convert.ToString(Constr)))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(System.Convert.ToString(storedProcedure), conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        double cnt = 0;
                        while (@params.Length > cnt)
                        {
                            var xparam = @params[(int)cnt];
                            var xvalue = values[(int)cnt];

                            if (!string.IsNullOrEmpty(xparam))
                            {
                                cmd.Parameters.AddWithValue(xparam, xvalue);
                            }
                            cnt++;
                        }

                        //using (SqlDataAdapter da = new SqlDataAdapter())
                        //{
                        //    da.SelectCommand = cmd;
                        //    da.Fill(str_return);
                        //}

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    //str_return = rdr["EventId"].ToString();
                                    str_return = rdr["return_id"].ToString();

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
            return str_return;
        }



        public DataSet GetRecordsDS(dynamic Constr, dynamic storedProcedure, string[] @params, string[] values)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(System.Convert.ToString(Constr)))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(System.Convert.ToString(storedProcedure), conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        double cnt = 0;
                        while (@params.Length > cnt)
                        {
                            var xparam = @params[(int)cnt];
                            var xvalue = values[(int)cnt];
                            if (!string.IsNullOrEmpty(xparam))
                            {
                                cmd.Parameters.AddWithValue(xparam, xvalue);
                            }
                            cnt++;
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            da.SelectCommand = cmd;
                            da.Fill(ds);
                        }

                    }

                }
                catch (Exception)
                {

                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            return ds;
        }


        public ResultModel saveData(dynamic ConStr, dynamic procedure, string[] @params, string[] values, string[] direction)
        {
            var output = "";
            ResultModel r = new ResultModel();

            using (SqlConnection conn = new SqlConnection(System.Convert.ToString(ConStr)))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(System.Convert.ToString(procedure), conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int cnt = 0;
                        while (direction.Length > cnt)
                        {
                            if (direction[cnt] == "IN")
                            {
                                var _param = @params[cnt];
                                var _value = values[cnt];
                                cmd.Parameters.AddWithValue(_param, _value);
                            }
                            else
                            {
                                output = @params[cnt];
                                cmd.Parameters.Add(output, SqlDbType.Int).Direction = ParameterDirection.Output;
                            }
                            cnt++;
                        }
                        cmd.ExecuteScalar();
                        //r.result = cmd.Parameters[output].Value.ToString();

                        r.result = (int)cmd.Parameters[output].Value;

                    }

                }
                catch (Exception ex)
                {
                    r.result = 9;
                    r.msg = ex.Message;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            return r;
        }


        public List<string> saveData_new(dynamic dbName, dynamic procedure, string[] @params, string[] values, string[] direction)
        //public dynamic saveData_new(dynamic dbName, dynamic procedure, string[] @params, string[] values, string[] direction)
        {
            //int res = 0;
            List<string> result = new List<string>();
            //int res1 = 0;
            //int res2 = 0;
            //var allResult = "";
            List<string> output = new List<string>();
            //var output = "";

            //var i_catId = "";
            //var i_subcatId = "";

            using (SqlConnection conn = new SqlConnection(System.Convert.ToString(database.DbConnect(dbName))))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(System.Convert.ToString(procedure), conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int cnt = 0;
                        while (direction.Length > cnt)
                        {
                            if (direction[cnt] == "IN")
                            {
                                var _param = @params[cnt];
                                var _value = values[cnt];
                                cmd.Parameters.AddWithValue(_param, _value);
                            }

                            else
                            {
                                //if (cnt == 12)
                                //{
                                //output = @params[cnt];
                                output.Add(@params[cnt]);
                                cmd.Parameters.Add(@params[cnt], SqlDbType.VarChar, 8000).Direction = ParameterDirection.Output;
                                //cmd.Parameters.Add(output, SqlDbType.Int).Direction = ParameterDirection.Output;
                                //cmd.Parameters.Add(output, SqlDbType.VarChar, 8000).Direction = ParameterDirection.Output;



                                //cmd.Parameters.Add(output, SqlDbType.VarChar).Direction = ParameterDirection.Output;
                                //}

                                //if (cnt == 13)
                                //{
                                //    i_catId = @params[cnt];
                                //    cmd.Parameters.Add(i_catId, SqlDbType.Int).Direction = ParameterDirection.Output;
                                //}

                                //if (cnt == 14)
                                //{
                                //    i_subcatId = @params[cnt];
                                //    cmd.Parameters.Add(i_subcatId, SqlDbType.Int).Direction = ParameterDirection.Output;
                                //}

                            }


                            cnt++;
                        }
                        cmd.ExecuteScalar();
                        //res = System.Convert.ToInt32(cmd.Parameters[output].Value.ToString());
                        //allResult = cmd.Parameters[output].Value.ToString();

                        //i_catId = cmd.Parameters[output[i]].Value.ToString();
                        //var i_subcatId = "";



                        for (var i = 0; i <= output.Count - 1; i++)
                        {
                            result.Add(cmd.Parameters[output[i]].Value.ToString());
                        }

                        result.Add("No Message");

                        //res1 = System.Convert.ToInt32(cmd.Parameters[i_catId].Value.ToString());
                        //res2 = System.Convert.ToInt32(cmd.Parameters[i_subcatId].Value.ToString());
                        //allResult = res + "*" + res1 + "*" + res2 + "*";
                    }

                }
                catch (Exception ex)
                {
                    //res = 9;
                    result.Add("999999");
                    result.Add(ex.Message);
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                    //conn.Dispose()
                }
            }

            return result;
            //return res;
            // return allResult;
        }


    }
}