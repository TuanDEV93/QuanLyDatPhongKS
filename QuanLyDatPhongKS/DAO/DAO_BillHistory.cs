using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using QuanLyDatPhongKS.DTO;
namespace QuanLyDatPhongKS.DAO
{
    public class DAO_BillHistory:DbConnection
    {
        private DbConnection dbConnection;
        public DAO_BillHistory()
        {
            dbConnection = new DbConnection();
        }
        public void InsertHistory(BillHistoryDTO history)
        {
            using (SqlConnection conn = dbConnection.cnn)
            {
                conn.Open();
                string query = "INSERT INTO BillHistory (BillID, BillContent) VALUES (@BillID, @BillContent)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BillID", history.BillID);
                    cmd.Parameters.AddWithValue("@BillContent", history.BillContent);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}