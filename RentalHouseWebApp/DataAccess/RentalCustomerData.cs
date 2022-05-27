using RentalHouseWebApp.Models;
using RentalHouseWebApp.Helpers;
using System.Data.SqlClient;



namespace RentalHouseWebApp.DataAccess
{
    public class RentalCustomerData
    {
        public string ErrorMessage { get; private set; }

        //Get all Employees
        public List<RentalCustomerModel> GetAll()
        {
            try
            {
                ErrorMessage = string.Empty;
                ErrorMessage = "";

                List<RentalCustomerModel> rentalcustomermodels = new List<RentalCustomerModel>();

                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select Id, Name, Gender, DOB, MobileNumber,PermanentAddress,PreferedLocation,Status,Password from [dbo].RentalCustomer e";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                RentalCustomerModel rentalcustomermodel = new RentalCustomerModel();
                                rentalcustomermodel.Id = reader.GetInt32(0);
                                rentalcustomermodel.Name = reader.GetString(1);
                                rentalcustomermodel.Gender = reader.GetString(2);
                                rentalcustomermodel.DOB = reader.GetDateTime(3);
                                rentalcustomermodel.MobileNumber = reader.GetString(4);
                                rentalcustomermodel.PermanentAddress = reader.GetString(5);
                                rentalcustomermodel.PreferedLocation = reader.GetString(6);
                                rentalcustomermodel.Status = reader.GetString(7);
                                rentalcustomermodel.Password = reader.GetString(8);
                                rentalcustomermodels.Add(rentalcustomermodel);
                            }
                        }
                    }
                }

                return rentalcustomermodels;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        //Get Employee by Name
        public List<RentalCustomerModel> GetRentalCustomerByName(string name)
        {
            try
            {
                ErrorMessage = "";

                RentalCustomerModel rentalcustomermodel = null;
                List<RentalCustomerModel> rentalcustomermodels = new List<RentalCustomerModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select Id, Name, Gender, DOB,  MobileNumber,PermanentAddress,PreferedLocation,Status,Password from RentalCustomer where name like '%{name}%'";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                rentalcustomermodel = new RentalCustomerModel();
                                rentalcustomermodel.Id = reader.GetInt32(0);
                                rentalcustomermodel.Name = reader.GetString(1);
                                rentalcustomermodel.Gender = reader.GetString(2);
                                rentalcustomermodel.DOB = reader.GetDateTime(3);
                                rentalcustomermodel.MobileNumber = reader.GetString(4);
                                rentalcustomermodel.PermanentAddress = reader.GetString(5);
                                rentalcustomermodel.PreferedLocation = reader.GetString(6);
                                rentalcustomermodel.Status = reader.GetString(7);
                                rentalcustomermodel.Password = reader.GetString(8);
                                rentalcustomermodels.Add(rentalcustomermodel);


                            }
                        }
                    }
                }
                return rentalcustomermodels;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }


        //Get Department By Id
        public RentalCustomerModel GetRentalCustomerById(int id)
        {
            try
            {
                RentalCustomerModel rentalcustomermodel = null;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select Id, Name, Gender, DOB, MobileNumber,PermanentAddress,PreferedLocation,Status,Password from RentalCustomer where Id = {id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                rentalcustomermodel = new RentalCustomerModel();
                                rentalcustomermodel.Id = reader.GetInt32(0);
                                rentalcustomermodel.Name = reader.GetString(1);
                                rentalcustomermodel.Gender = reader.GetString(2);
                                rentalcustomermodel.DOB = reader.GetDateTime(3);
                                rentalcustomermodel.MobileNumber = reader.GetString(4);
                                rentalcustomermodel.PermanentAddress = reader.GetString(5);
                                rentalcustomermodel.PreferedLocation = reader.GetString(6);
                                rentalcustomermodel.Status = reader.GetString(7);
                                rentalcustomermodel.Password = reader.GetString(8);
                               
                            }
                        }
                    }
                }

                return rentalcustomermodel;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public RentalCustomerModel GetCustomerByName(string Name, string Password)
        {
            try
            {
                ErrorMessage = "";


                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select * from  RentalCustomer where Name = '{Name}' and Password= '{Password}'";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {


                                RentalCustomerModel rentalcustomermodel = new RentalCustomerModel();
                                rentalcustomermodel.Id = reader.GetInt32(0);
                                rentalcustomermodel.Name = reader.GetString(1);
                                rentalcustomermodel.Gender = reader.GetString(2);
                                rentalcustomermodel.DOB = reader.GetDateTime(3);
                                rentalcustomermodel.MobileNumber = reader.GetString(4);
                                rentalcustomermodel.PermanentAddress = reader.GetString(5);
                                rentalcustomermodel.PreferedLocation = reader.GetString(6);
                                rentalcustomermodel.Status = reader.GetString(7);
                                rentalcustomermodel.Password = reader.GetString(8);


                                return rentalcustomermodel;


                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

            public RentalCustomerModel Insert(RentalCustomerModel newRentalCustomerModel)
        {
            try
            {
                ErrorMessage = string.Empty;

                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.RentalCustomer(Name, Gender, DOB,  MobileNumber,PermanentAddress,PreferedLocation,Status,Password) VALUES ('{newRentalCustomerModel.Name}','{newRentalCustomerModel.Gender}','{newRentalCustomerModel.DOB.ToString("yyyy-MM-dd")}','{newRentalCustomerModel.MobileNumber}','{newRentalCustomerModel.PermanentAddress}','{newRentalCustomerModel.PreferedLocation}','{newRentalCustomerModel.Status}','{newRentalCustomerModel.Password}'); SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newRentalCustomerModel.Id = idInserted;
                            return newRentalCustomerModel;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
                return null;
            }
        }
        public RentalCustomerModel Update(RentalCustomerModel updRentalCustomerModel)
        {
            try
            {
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.RentalCustomer SET Name = '{updRentalCustomerModel.Name}', " +
                        $"Gender = '{updRentalCustomerModel.Gender}', " +
                        $"DOB = '{updRentalCustomerModel.DOB.ToString("yyyy-MM-dd")}' ," +
                        $"MobileNumber = '{updRentalCustomerModel.MobileNumber}'," +
                        $"PermanentAddress = '{updRentalCustomerModel.PermanentAddress}', " +
                        $"PreferedLocation = '{updRentalCustomerModel.PreferedLocation}', " +
                        $"Status='{updRentalCustomerModel.Status}', " +
                        $"Password='{updRentalCustomerModel.Password}' " +
                        $"where Id = {updRentalCustomerModel.Id}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();
                        if (numOfRows > 0)
                        {
                            return updRentalCustomerModel;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return null;
        }

        //Delete Employee
        public int Delete(int id)
        {
            try
            {
                ErrorMessage = String.Empty;
                int numOfRows = 0;

                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"DELETE FROM House Where Id = {id}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        numOfRows = cmd.ExecuteNonQuery();
                    }
                }
                return numOfRows;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return 0;
            }
        }
    }
}
    
