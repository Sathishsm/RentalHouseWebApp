
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalHouseWebApp.Helpers;
using RentalHouseWebApp.Models;

namespace RentalHouseWebApp.DataAccess
{
    internal class TransectionData
    {


        public string ErrorMessage { get; private set; }
        public List<TransectionModel> GetAll()
        {
            try
            {
                ErrorMessage = "";
                List<TransectionModel> transectionmodels = new List<TransectionModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "select RC.Id as RentalCustomerId ,H.Id as HouseId,OW.Id as OwnerId ,H.DateofJoining,H.DateofEnd,H.Rent,H.Status " +
                                  "from[dbo].[House] AS H " +
                                    "INNER JOIN[dbo].Owner AS OW on OW.Id = H.OwnerId " +
                                   
                                    "INNER JOIN[dbo].RentalCustomer AS RC on RC.Id = H.RentalCustomerid ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TransectionModel transectionmodel = new TransectionModel();

                                
                                transectionmodel.RentalCustomerId = reader.GetInt32(0);
                                transectionmodel.HouseId = reader.GetInt32(1);
                                transectionmodel.OwnerId = reader.GetInt32(2);
                                transectionmodel.DateofJoining = reader.GetDateTime(3);
                                transectionmodel.DateofEnd = reader.GetDateTime(4);
                                transectionmodel.Rent = reader.GetString(5);
                                transectionmodel.Status = reader.GetString(6);





                                transectionmodels.Add(transectionmodel);
                            }
                        }
                    }
                }
                return transectionmodels;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }


        //public TransectionModel GetTransectionById(int id)

        //{
        //    try
        //    {

        //        ErrorMessage = "";

        //        TransectionModel transectionmodel = null;

        //        using (SqlConnection conn = DataBase.GetConnection())
        //        {
        //            conn.Open();
        //            var sqlStmt = $"Select Id , RentalCustomerId, HouseId, OwnerId, DateofJoing, DateofEnd, Rent from Transection where Id={id}";
        //            using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
        //            {
        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        transectionmodel = new TransectionModel();

        //                        transectionmodel.Id = reader.GetInt32(0);
        //                        transectionmodel.RentalCustomerId = reader.GetInt32(1);
        //                        transectionmodel.HouseId = reader.GetInt32(2);
        //                        transectionmodel.OwnerId = reader.GetInt32(3);
        //                        transectionmodel.DateofJoining = reader.GetDateTime(4);
        //                        transectionmodel.DateofEnd = reader.GetDateTime(5);
        //                        transectionmodel.FinalRent = reader.GetInt32(6);


        //                    }
        //                }
        //            }
        //        }
        //        return transectionmodel;

        //    }
        //    catch (Exception ex)
        //    {

        //        ErrorMessage = ex.Message;
        //        return null;
        //    }
        //}











        //public TransectionModel Insert(TransectionModel newTransectionModel)
        //{
        //    try
        //    {
        //        ErrorMessage = string.Empty;
        //        ErrorMessage = String.Empty;
        //        using (SqlConnection conn = DataBase.GetConnection())
        //        {
        //            conn.Open();
        //            var sqlStmt = $"Insert into Transection (RentalCustomerId,HouseId,OwnerId,DateofJoining,DateofEnd,FinalRent) values ('{newTransectionModel.RentalCustomerId}','{newTransectionModel.HouseId}','{newTransectionModel.OwnerId}','{newTransectionModel.DateofJoining.ToString("yyyy-MM-dd")}','{newTransectionModel.DateofEnd.ToString("yyyy-MM-dd")}','{newTransectionModel.FinalRent}'); select scope_identity()";

        //            using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
        //            {
        //                int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
        //                if (idInserted > 0)
        //                {
        //                    newTransectionModel.Id = idInserted;
        //                    return newTransectionModel;
        //                }
        //            }
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ErrorMessage = ex.Message;
        //        return null;
        //    }
        //}





        //public TransectionModel Update(TransectionModel updTransectionModel)
        //{
        //    try
        //    {
        //        ErrorMessage = string.Empty;
        //        using (SqlConnection conn = DataBase.GetConnection())
        //        {
        //            conn.Open();
        //            string sqlStmt = $"UPDATE dbo.training SET RentalCustomerId = {updTransectionModel.RentalCustomerId}, " +
        //                $"HouseId = {updTransectionModel.HouseId}," +
        //                $"OwnerId = {updTransectionModel.OwnerId}," +
        //                $"DateofJoning='{updTransectionModel.DateofJoining.ToString("yyyy-MM-dd")}'," +
        //                $"DateofEnd='{updTransectionModel.DateofEnd.ToString("yyyy-MM-dd")}'," +
        //                $"FinalRent='{updTransectionModel.FinalRent}'," +
        //                $"where trainingid = {updTransectionModel.Id}";

        //            using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
        //            {
        //                int numOfRows = cmd.ExecuteNonQuery();

        //                if (numOfRows > 0)
        //                {
        //                    return updTransectionModel;
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMessage = ex.Message;

        //    }
        //    return null;
        //}



        //public int Delete(int id)
        //{
        //    try
        //    {
        //        ErrorMessage = string.Empty;
        //        int numOfRows = 0;
        //        using (SqlConnection conn = DataBase.GetConnection())
        //        {
        //            conn.Open();
        //            string sqlStmt = $"DELETE FROM dbo.Transection Where Id={id}";
        //            using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
        //            {
        //                numOfRows = cmd.ExecuteNonQuery();
        //            }
        //        }
        //        return numOfRows;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMessage = ex.Message;
        //        return 0;
        //    }



    }
}


