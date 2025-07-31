using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace QuanLyDatPhongKS.DAO
{
    public class DAO_BookingConfirmation
    {
        private DbConnection dbConnection;

        public DAO_BookingConfirmation()
        {
            dbConnection = new DbConnection();
        }
        public string GetDisplayNameByUsername(string username)
        {
            string displayName = string.Empty;
            string query = "SELECT DisplayName FROM Customer WHERE CustomerName = @CustomerName";

            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@CustomerName", SqlDbType.NVarChar).Value = username;

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        displayName = result.ToString();
                    }
                }
            }

            return displayName;
        }


        // Lấy danh sách booking dựa trên tên
        public List<BookingConfirmationDTO> GetBookingByDisplayName(string displayName)
        {
            List<BookingConfirmationDTO> bookingList = new List<BookingConfirmationDTO>();

            string query = @"
                            SELECT 
                            BL.BillID,
                            BK.BookingDate,
                            BK.CheckoutDate,
                            R.RoomNumber,
                            BL.TotalAmount,
                            BL.Deposit AS DepositPrice,
                            BL.Status
                        FROM Bill BL
                        INNER JOIN Booking BK ON BL.BillID = BK.BillID
                        INNER JOIN Room R ON BK.RoomID = R.RoomID
                        WHERE BL.CustomerName = @DisplayName
                        ORDER BY BK.BookingID DESC";

            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@DisplayName", SqlDbType.NVarChar).Value = displayName;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BookingConfirmationDTO booking = new BookingConfirmationDTO
                            {
                                BillID = Convert.ToInt32(reader["BillID"]),
                                BookingDate = Convert.ToDateTime(reader["BookingDate"]),
                                CheckoutDate = Convert.ToDateTime(reader["CheckoutDate"]),
                                RoomNumber = reader["RoomNumber"].ToString(),
                                TotalAmount = Convert.ToInt32(reader["TotalAmount"]),
                                DepositPrice = reader["DepositPrice"] != DBNull.Value ? Convert.ToInt32(reader["DepositPrice"]) : 0,
                                Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "Chưa thanh toán"
                            };

                            bookingList.Add(booking);
                        }
                    }
                }
            }

            return bookingList;
        }



    }
}