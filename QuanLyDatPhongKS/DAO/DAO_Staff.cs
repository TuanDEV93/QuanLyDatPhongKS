using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

namespace QuanLyDatPhongKS.DAO
{
    public class DAO_Staff
    {

        private DbConnection dbConnection;

        public DAO_Staff()
        {
            dbConnection = new DbConnection();
        }
        public List<StaffDTO> GetStaffByRole()
        {
            List<StaffDTO> StaffList = new List<StaffDTO>();
            string query = @"SELECT UserID, DisplayName, Address, Phone FROM Account where Account.Role = 0 ";

            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString)) 
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StaffDTO staff = new StaffDTO
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                DisplayName = reader["DisplayName"].ToString(),
                                Address = reader["Address"].ToString(),
                                Phone = reader["Phone"].ToString()
                            };
                            StaffList.Add(staff);

                        }
                    }
                }
            }
            return StaffList;
        }
        public void UpdateStaff(StaffDTO staff)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))//khởi tạo chuỗi kết nối mới :>
            {
                string query = @"UPDATE Account SET DisplayName = @DisplayName, Address = @Address, 
                                Phone = @Phone WHERE UserID = @UserID";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", staff.UserID);
                command.Parameters.AddWithValue("@DisplayName", staff.DisplayName);
                command.Parameters.AddWithValue("@Address", staff.Address);
                command.Parameters.AddWithValue("@Phone", staff.Phone);
                connection.Open();
                //trả về số dòng bị thay đổi
                command.ExecuteNonQuery();
            }
        }
        public StaffDTO GetStaffById(int userId)
        {
            StaffDTO staff = null;
            string query = "SELECT UserID, DisplayName, Address, Phone FROM Account WHERE UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UserID", userId);
                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        staff = new StaffDTO
                        {
                            UserID = (int)reader["UserID"],
                            DisplayName = reader["DisplayName"].ToString(),
                            Address = reader["Address"].ToString(),
                            Phone = reader["Phone"].ToString()
                        };
                    }
                }
            }
            return staff;
        }
        public void DeleteStaff(int userId)
        {
            string query = "DELETE FROM Account WHERE UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UserID", userId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public bool AddStaff(StaffDTO staff)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                string query = "INSERT INTO Account ( UserName,DisplayName, Password,Address, Phone) " +
                               "VALUES (@UserName,@DisplayName,  @Password, @Address, @Phone)";

                SqlCommand command = new SqlCommand(query, connection);
               
                command.Parameters.AddWithValue("@UserName", staff.UserName);
                command.Parameters.AddWithValue("@DisplayName", staff.DisplayName);
                command.Parameters.AddWithValue("@Password", staff.Password);
                command.Parameters.AddWithValue("@Address", staff.Address);
                command.Parameters.AddWithValue("@Phone", staff.Phone);
                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }
        //Hàm kt username đã tồn tại trong sql chưa
        public bool IsUserNameExists(string userName)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM Account WHERE UserName = @UserName";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UserName", userName);

                connection.Open();
                int count = (int)cmd.ExecuteScalar(); // Thực thi và trả về kết quả đếm
                connection.Close();

                return count > 0; // Trả về true nếu userName đã tồn tại
            }
        }
    }
}