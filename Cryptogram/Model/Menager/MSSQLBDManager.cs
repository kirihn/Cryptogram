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

namespace Cryptogram.Model.Menager
{
    internal class MSSQLBDManager
    {
        public SqlConnection Connection;
    
        private void GetSqlConnection(object sender, RoutedEventArgs e)
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

                MessageBox.Show(Connection.Database);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {

            }

        }
        private void CreateDB()
        {
            SqlCommand com = Connection.CreateCommand();

            com.CommandText = @"create database TestCryptogram;";

            com.ExecuteNonQuery();

            Connection.ChangeDatabase("TestCryptogram");

            StreamResourceInfo stream = Application.GetResourceStream(new Uri("Resources/DataBaseCreation.sql", UriKind.Relative));

            using (StreamReader sql = new StreamReader(stream.Stream))
            {
                com.CommandText = sql.ReadToEnd();

                com.ExecuteNonQuery();
            }

            com.Dispose();

            List<StreamResourceInfo> addRes = new List<StreamResourceInfo>()
            {
                Application.GetResourceStream(new Uri("Resources/SQLProcedures/AddSpeciality.sql", UriKind.Relative)),
                Application.GetResourceStream(new Uri("Resources/SQLProcedures/AddCourse.sql", UriKind.Relative)),
                Application.GetResourceStream(new Uri("Resources/SQLProcedures/AddGroup.sql", UriKind.Relative)),
                Application.GetResourceStream(new Uri("Resources/SQLProcedures/AddSubGroup.sql", UriKind.Relative)),
                Application.GetResourceStream(new Uri("Resources/SQLProcedures/AddPerson.sql", UriKind.Relative)),
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
    }
}
