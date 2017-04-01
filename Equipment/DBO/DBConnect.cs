using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
//Add MySql Library
using MySql.Data.MySqlClient;
using System.Data;
using Common;

namespace DBO
{
    public class DBConnect
    {
        public static MySqlConnection connection;
        //private static  string server = "localhost";
        //private static string database= "parking";
        //private static string uid = "root";
        //private static string password = "2ifdDKF_395bmw#";
        private static string connectionString;
        //private static string port = "3308";

        static DBConnect()
        {
            connectionString = System.Configuration.ConfigurationManager.AppSettings["MySqlConnectionString"];
            //connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";charset=utf8";
        }
        //open connection to database
        public static bool OpenConnection()
        {            
            connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    //case 0:
                    //    MessageBox.Show("Cannot connect to server.  Contact administrator");
                    //    break;

                    //case 1045:
                    //    MessageBox.Show("Invalid username/password, please try again");
                    //    break;
                }
                return false;
            }
        }

        //Close connection
        public static bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        
        

        //Update statement
        public void Update()
        {
            string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }
        }

        //Delete statement
        public void Delete()
        {
            string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }

        //Select statement
        public static ReturnValue Select(string sql)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (MySqlException ex)
                {
                    UtilityLog.WriteErrLogWithLog4Net(ex);
                    return new ReturnValue(false, "E11001");//数据库服务器链接错误！
                }
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    try
                    {
                        DataSet dsQuery = new DataSet();
                        da.Fill(dsQuery, "Result");
                        return new ReturnValue(true, "", dsQuery, dsQuery.Tables[0].Rows.Count);
                    }
                    catch (MySqlException ex)
                    {
                        UtilityLog.WriteErrLogWithLog4Net(ex);
                        return new ReturnValue(false, "E11002");//操作数据库时发生错误！.
                    }
                    catch (Exception ex)
                    {
                        UtilityLog.WriteErrLogWithLog4Net(ex);
                        return new ReturnValue(false, "E11002");//操作数据库时发生错误！.
                    }
                }
            }
        }

        public static string ExecuteSql(string strSql)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(strSql, connection))
                {
                    try
                    {
                        connection.Open();
                    }
                    catch (MySqlException ex)
                    {
                        UtilityLog.WriteErrLogWithLog4Net(ex);
                        return "E11001";//数据库服务器链接错误！
                    }
                    try
                    {
                        int intRows = cmd.ExecuteNonQuery();
                        if (intRows > 0)
                        {
                            return "0";
                        }
                        return "W11003";//操作成功，但沒有任何数据被更新。
                    }
                    catch (MySqlException ex)
                    {
                        UtilityLog.WriteErrLogWithLog4Net(ex);
                        return "E11002";//操作数据库时发生错误！
                    }
                    catch (Exception ex)
                    {
                        UtilityLog.WriteErrLogWithLog4Net(ex);
                        return "E11002";//操作数据库时发生错误！
                    }
                }
            }
        }

        public static object GetSingle(string strSql)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(strSql, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (MySqlException e)
                    {
                        connection.Close();
                        UtilityLog.WriteErrLogWithLog4Net(e);
                        throw e;
                    }
                }
            }
        }

        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM tableinfo";
            int Count = -1;

            //Open Connection
            if (OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar()+"");
                
                //close Connection
                CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Backup
        //public void Backup()
        //{
        //    try
        //    {
        //        DateTime Time = DateTime.Now;
        //        int year = Time.Year;
        //        int month = Time.Month;
        //        int day = Time.Day;
        //        int hour = Time.Hour;
        //        int minute = Time.Minute;
        //        int second = Time.Second;
        //        int millisecond = Time.Millisecond;

        //        //Save file to C:\ with the current date as a filename
        //        string path;
        //        path = "C:\\" + year + "-" + month + "-" + day + "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
        //        StreamWriter file = new StreamWriter(path);

                
        //        ProcessStartInfo psi = new ProcessStartInfo();
        //        psi.FileName = "mysqldump";
        //        psi.RedirectStandardInput = false;
        //        psi.RedirectStandardOutput = true;
        //        psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
        //        psi.UseShellExecute = false;

        //        Process process = Process.Start(psi);

        //        string output;
        //        output = process.StandardOutput.ReadToEnd();
        //        file.WriteLine(output);
        //        process.WaitForExit();
        //        file.Close();
        //        process.Close();
        //    }
        //    catch (IOException ex)
        //    {
        //        //MessageBox.Show("Error , unable to backup!");
        //    }
        //}

        //Restore
        //public void Restore()
        //{
        //    try
        //    {
        //        //Read file from C:\
        //        string path;
        //        path = "C:\\MySqlBackup.sql";
        //        StreamReader file = new StreamReader(path);
        //        string input = file.ReadToEnd();
        //        file.Close();


        //        ProcessStartInfo psi = new ProcessStartInfo();
        //        psi.FileName = "mysql";
        //        psi.RedirectStandardInput = true;
        //        psi.RedirectStandardOutput = false;
        //        psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
        //        psi.UseShellExecute = false;

                
        //        Process process = Process.Start(psi);
        //        process.StandardInput.WriteLine(input);
        //        process.StandardInput.Close();
        //        process.WaitForExit();
        //        process.Close();
        //    }
        //    catch (IOException ex)
        //    {
        //        //MessageBox.Show("Error , unable to Restore!");
        //    }
        //}
    }
}
