using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WpfApp2
{
    internal class DataBase
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-F7J2KE8\SQLEXPRESS;Initial Catalog=School;Integrated Security=True");

        public void OpenConnection() 
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed) 
            {
                sqlConnection.Open();
            }
        }

        public void ClosedConnection() 
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open) 
            {
                sqlConnection.Close();
            }
        }

        public SqlConnection GetConnection() 
        {
            return sqlConnection;
        }
    }
}
