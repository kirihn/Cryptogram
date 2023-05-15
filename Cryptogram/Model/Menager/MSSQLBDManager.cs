using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Resources;
using System.Windows;
using System.Data.SqlClient;
using System.Data.Common;

namespace Cryptogram.Model.Menager
{
    public class MSSQLBDManager
    {
        public static MSSQLBDManager Instance { get; set; }
        public SqlConnection Connection;
        public bool IsConnected { get => Connection.State == System.Data.ConnectionState.Open; }

        static MSSQLBDManager()
        {
            Instance = new MSSQLBDManager();
        }

        public MSSQLBDManager()
        {
            GetSqlConnection();
        }
        private  void GetSqlConnection()
        {

            try
            {
                Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
                Connection.Open();

                SqlCommand com = Connection.CreateCommand();

                com.CommandText = @"select case ISNULL(DB_ID('TestCryptogram'), -1) when -1 then 'N' else 'Y' end [IS HAVE];";

                SqlDataReader reader = com.ExecuteReader();

                bool isHaveDB = false;

                while (reader.Read())
                {
                    isHaveDB = reader.GetString(0) == "Y";
                }

                reader.Close();
                reader.Dispose();
                com.Dispose();

                if (!isHaveDB)
                    CreateDB();
                else
                    Connection.ChangeDatabase("TestCryptogram");

                //MessageBox.Show(Connection.Database);
            }
            catch (Exception exception)
            {
                MessageBox.Show("--- MSSQL MENAGER ---\n " + exception.Message);
            }
        }
        private void CreateDB()
        {
            SqlCommand com = Connection.CreateCommand();

            com.CommandText = @"create database TestCryptogram;";

            com.ExecuteNonQuery();

            Connection.ChangeDatabase("TestCryptogram");

            StreamResourceInfo stream = Application.GetResourceStream(new Uri("SQLADO/DBCreation.sql", UriKind.Relative));

            StreamReader sql = new StreamReader(stream.Stream);

            com.CommandText = sql.ReadToEnd();

            com.ExecuteNonQuery();

            sql.Dispose();


            com.Dispose();

            List<StreamResourceInfo> addRes = new List<StreamResourceInfo>()
            {
                Application.GetResourceStream(new Uri("SQLADO/AddNewUser.sql", UriKind.Relative)),
                Application.GetResourceStream(new Uri("SQLADO/AddNewMessage.sql", UriKind.Relative)),

                Application.GetResourceStream(new Uri("SQLADO/DelMessage.sql", UriKind.Relative)),
                Application.GetResourceStream(new Uri("SQLADO/DelUser.sql", UriKind.Relative)),
            };

            foreach (var item in addRes)
            {
                using (StreamReader reader = new StreamReader(item.Stream))
                {
                    SqlCommand proc = Connection.CreateCommand();

                    proc.CommandText = reader.ReadToEnd();

                    proc.ExecuteNonQuery();
                }
            }
        }
        public void RestartConnection()
        {
            try
            {
                Connection.Close();
                Connection.Dispose();
                
                Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);
                Connection.Open();
                
                Connection.ChangeDatabase("UniversityLog");
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message, "Restart sql server error!", MessageBoxButton.OK, MessageBoxImage.Stop);

                Application.Current.Shutdown();
            }
        }

        public SqlTransaction CreateTransaction()
        {
            try
            {
                return Connection.BeginTransaction();
            }
            catch
            {
                RestartConnection();

                return Connection.BeginTransaction();
            }
        }
        public SqlCommand CreateCommand() => Connection.CreateCommand();


        public void AddUser()
        {

        }
    }
}
