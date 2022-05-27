using RentalHouseWebApp.Models;
using RentalHouseWebApp.Helpers;
using System.Data.SqlClient;



namespace RentalHouseWebApp.DataAccess
{
    public class OwnerData
    {
        public string ErrorMessage { get; private set; }

        //Get all Employees
        public List<OwnerModel> GetAll()
        {
            try
            {
                ErrorMessage = string.Empty;
                ErrorMessage = "";

                List<OwnerModel> Ownermodels = new List<OwnerModel>();

                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select Id, Name,  MobileNumber,ExpectedStatus,Rules,Password from [dbo].Owner";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                OwnerModel ownermodel = new OwnerModel();
                                ownermodel.Id = reader.GetInt32(0);
                                ownermodel.Name = reader.GetString(1);
                                ownermodel.MobileNumber = reader.GetString(2);
                                ownermodel.ExpectedStatus = reader.GetString(3);
                                ownermodel.Rules = reader.GetString(4);
                                ownermodel.Password = reader.GetString(5);

                                Ownermodels.Add(ownermodel);
                            }
                        }
                    }
                }

                return Ownermodels;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        //Get Employee by Name
        public List<OwnerModel> GetOwnerByName(string name)
        {
            try
            {
                ErrorMessage = "";

                OwnerModel ownermodel = null;
                List<OwnerModel> ownermodels = new List<OwnerModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select Id, Name,  MobileNumber,ExpectedStatus,Rules,Password from Owner where name like '%{name}%'";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ownermodel = new OwnerModel();
                                ownermodel.Id = reader.GetInt32(0);
                                ownermodel.Name = reader.GetString(1);
                                ownermodel.MobileNumber = reader.GetString(2);
                                ownermodel.ExpectedStatus = reader.GetString(3);
                                ownermodel.Rules = reader.GetString(4);
                                ownermodel.Password = reader.GetString(5);

                                ownermodels.Add(ownermodel);


                            }
                        }
                    }
                }
                return ownermodels;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }


        //Get Department By Id
        public OwnerModel GetOwnerById(int id)
        {
            try
            {
                OwnerModel ownermodel = null;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select Id, Name ,MobileNumber,ExpectedStatus,Rules,Password from Owner where Id = {id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                 ownermodel = new OwnerModel();
                                ownermodel.Id = reader.GetInt32(0);
                                ownermodel.Name = reader.GetString(1);
                                ownermodel.MobileNumber = reader.GetString(2);
                                ownermodel.ExpectedStatus = reader.GetString(3);
                                ownermodel.Rules = reader.GetString(4);
                                ownermodel.Password = reader.GetString(5);


                            }
                        }
                    }
                }

                return ownermodel;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public OwnerModel GetOwnerByName(string Name, string Password)
        {
            try
            {
                ErrorMessage = "";


                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select * from  Owner where Name = '{Name}' and Password= '{Password}'";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {



                                OwnerModel ownermodel = new OwnerModel();
                                ownermodel.Id = reader.GetInt32(0);
                                ownermodel.Name = reader.GetString(1);
                                ownermodel.MobileNumber = reader.GetString(2);
                                ownermodel.ExpectedStatus = reader.GetString(3);
                                ownermodel.Rules = reader.GetString(4);
                                ownermodel.Password = reader.GetString(5);





                                return ownermodel;
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


        public OwnerModel Insert(OwnerModel newOwnerModel)
        {
            try
            {
                ErrorMessage = string.Empty;

                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.Owner(Name,MobileNumber,ExpectedStatus,Rules,Password ) VALUES ('{newOwnerModel.Name}','{newOwnerModel.MobileNumber}','{newOwnerModel.ExpectedStatus}','{newOwnerModel.Rules}','{newOwnerModel.Password}'); SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newOwnerModel.Id = idInserted;
                            return newOwnerModel;
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
        public OwnerModel Update(OwnerModel updownermodel)
        {
            try
            {
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Owner SET Name = '{updownermodel.Name}', " +
                      
                        $"MobileNumber = '{updownermodel.MobileNumber}'," +
                        $"ExpectedStatus = '{updownermodel.ExpectedStatus}', " +
                        $"Rules = '{updownermodel.Rules}', " +
                          $"Password= '{updownermodel.Password}' " +

                        $"where Id = {updownermodel.Id}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();
                        if (numOfRows > 0)
                        {
                            return updownermodel;
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

