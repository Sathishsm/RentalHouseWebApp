using System.Data.SqlClient;
using RentalHouseWebApp.Helpers;
using RentalHouseWebApp.Models;

namespace RentalHouseWebApp.DataAccess
{
    public class DashBoardData
    {
        public string ErrorMessage { get; private set; }
        public DashBoardModel GetAll()
        {
            try
            {

                ErrorMessage = String.Empty;
                ErrorMessage = "";
                var d = new DashBoardModel();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "select count(*) as RentalCustomerCount from RentalCustomer";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.RentalCustomerCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    sqlStmt = "select count(*) as OwnerCount from Owner";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.OwnerCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    sqlStmt = "select count(*) as HouseCount from House";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.HouseCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                  //  sqlStmt = "select count(*) as CompletedTrainingCount from EmployeeCourse where Grade is  Not Null";
                   // using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    //{
                    //    d.CompletedTrainingCount = Convert.ToInt32(cmd.ExecuteScalar());
                   // }

                }

                return d;


            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }

    }
}