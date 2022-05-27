using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace RentalHouseWebApp.Helpers
{
    public class DataBase
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=ENWIN-530\\SQLEXPRESS;Initial Catalog=RentalHouse;Integrated Security=True;";

            return new SqlConnection(connectionString);
        }
    }
}

