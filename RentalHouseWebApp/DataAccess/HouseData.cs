using RentalHouseWebApp.Models;
using RentalHouseWebApp.Helpers;
using System.Data.SqlClient;


namespace RentalHouseWebApp.DataAccess
{
    public class HouseData
    {
        public string ErrorMessage { get; private set; }

        //Get all Employees
        public List<HouseModel> GetAll()
        {
            try
            {
                ErrorMessage = string.Empty;
                ErrorMessage = "";

                List<HouseModel> housemodels = new List<HouseModel>();

                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select Id,Location,OwnerId,Picture,Facilities,Rent,DateofJoining,DateofEnd,Status,RentalCustomerId from [dbo].House";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                HouseModel housemodel = new HouseModel();
                                housemodel.Id = reader.GetInt32(0);
                                housemodel.Location = reader.GetString(1);
                                housemodel.OwnerId= reader.GetInt32(2);
                                housemodel.Picture= reader.GetString(3);
                                housemodel.Facilities = reader.GetString(4);
                                housemodel.Rent=  reader.GetString(5);
                                housemodel.DateofJoining = reader.GetDateTime(6);
                                housemodel.DateofEnd = reader.GetDateTime(7);
                                housemodel.Status = reader.GetString(8);
                                housemodel.RentalCustomerId= reader.GetInt32(9);



                                housemodels.Add(housemodel);
                            }
                        }
                    }
                }

                return housemodels;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        //Get Employee by Name
        public List<HouseModel> GetHouseByLocation(string location)
        {
            try
            {
                ErrorMessage = "";

                HouseModel housemodel = null;
                List<HouseModel> housemodels = new List<HouseModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"Select Id, Location,OwnerId,Picture,Facilities,Rent,DateofJoining,DateofEnd,Status ,RentalCustomerId from [dbo].House where location like '%{location}%'";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                housemodel = new HouseModel();
                                housemodel.Id = reader.GetInt32(0);
                                housemodel.Location = reader.GetString(1);
                                housemodel.OwnerId = reader.GetInt32(2);
                                housemodel.Picture = reader.GetString(3);
                                housemodel.Facilities = reader.GetString(4);
                                housemodel.Rent = reader.GetString(5);
                                housemodel.DateofJoining = reader.GetDateTime(6);
                                housemodel.DateofEnd =  reader.GetDateTime(7);
                                housemodel.Status = reader.GetString(8);

                                housemodels.Add(housemodel);


                            }
                        }
                    }
                }
                return housemodels;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }


        //Get Department By Id
        public HouseModel GetHouseById(int id)
        {
            try
            {
                HouseModel housemodel = null;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select Id,Location,OwnerId,Picture,Facilities,Rent,DateofJoining,DateofEnd,Status,RentalCustomerId from [dbo].House where Id = {id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                housemodel = new HouseModel();
                                housemodel.Id = reader.GetInt32(0);
                                housemodel.Location = reader.GetString(1);
                                housemodel.OwnerId = reader.GetInt32(2);
                                housemodel.Picture = reader.GetString(3);
                                housemodel.Facilities = reader.GetString(4);
                                housemodel.Rent =  reader.GetString(5);
                                housemodel.DateofJoining = reader.GetDateTime(6);
                                housemodel.DateofEnd =  reader.GetDateTime(7);
                                housemodel.Status = reader.GetString(8);
                                housemodel.RentalCustomerId = reader.GetInt32(9);



                            }
                        }
                    }
                }

                return housemodel;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
      
        
        

        public HouseModel Insert(HouseModel newHouseModel)
        {
            try
            {
                ErrorMessage = string.Empty;

                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.House(Location,OwnerId,Picture,Facilities,Rent,DateofJoining,DateofEnd,Status,RentalCustomerId) VALUES ('{newHouseModel.Location}','{newHouseModel.OwnerId}','{newHouseModel.Picture}','{newHouseModel.Facilities}','{newHouseModel.Rent}','{newHouseModel.DateofJoining.ToString("yyyy-MM-dd")}','{newHouseModel.DateofEnd?.ToString("yyyy-MM-dd")}','{newHouseModel.Status}','{newHouseModel.RentalCustomerId}') SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newHouseModel.Id = idInserted;
                            return newHouseModel;
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
        public HouseModel Update(HouseModel updHouseModel)
        {
            try
            {
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.House SET Location = '{updHouseModel.Location}', " +
                        $"OwnerId = '{updHouseModel.OwnerId}', " +
                      
                        $"Picture = '{updHouseModel.Picture}', " +
                        $"Facilities = '{updHouseModel.Facilities}', " +
                        $"Rent='{updHouseModel.Rent}', " +
                        $"DateofJoining='{updHouseModel.DateofJoining}', " +
                        $"DateofEnd='{updHouseModel.DateofEnd}', " +
                        $"Status='{updHouseModel.Status}', " +
                        $"RentalCustomerId='{updHouseModel.RentalCustomerId}' " + 
                        $"where Id = {updHouseModel.Id}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();
                        if (numOfRows > 0)
                        {
                            return updHouseModel;
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

        //Delete House
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

