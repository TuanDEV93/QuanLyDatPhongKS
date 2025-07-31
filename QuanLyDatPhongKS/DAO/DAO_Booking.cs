using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QuanLyDatPhongKS.DAO
{
    public class DAO_Booking
    {
        private DbConnection dbConnection;

        public DAO_Booking()
        {
            dbConnection = new DbConnection();
        }
        public int GetRoomIdByNumber(string roomNumber)
        {
            using (SqlConnection conn = dbConnection.cnn)
            {
                string query = "SELECT RoomID FROM Room WHERE RoomNumber = @RoomNumber";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RoomNumber", roomNumber);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    conn.Close();

                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }
        public bool InsertBooking(BookingDTO booking)
        {
            using (SqlConnection conn = new SqlConnection(dbConnection.ConnectionString))
            {
                string query = @"
        INSERT INTO Booking (BillID, RoomID, BookingDate, CheckoutDate)
        VALUES (@BillID, @RoomID, @BookingDate, @CheckoutDate)";

                using (SqlCommand cmd = new SqlCommand(query, conn)) 
                {
                    cmd.Parameters.AddWithValue("@BillID", booking.BillID);
                    cmd.Parameters.AddWithValue("@RoomID", booking.RoomID);
                    cmd.Parameters.AddWithValue("@BookingDate", booking.BookingDate);
                    cmd.Parameters.AddWithValue("@CheckoutDate", booking.CheckoutDate);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    return rowsAffected > 0;
                }
            }
        }

        //public BillDTO GetBillByRoomID(int roomId)
        //    {
        //        BillDTO bill = null;
        //        using (SqlConnection conn = dbConnection.cnn)
        //        {
        //            string query = @"
        //        SELECT b.BillID, b.CustomerName, b.Phone, b.Deposit, b.TotalAmount, b.DateIssued, b.Status
        //        FROM Bill b
        //        INNER JOIN Booking bk ON b.BillID = bk.BillID
        //        WHERE bk.RoomID = @RoomID AND bk.CheckoutDate >= GETDATE()";

        //            SqlCommand cmd = new SqlCommand(query, conn);
        //            cmd.Parameters.AddWithValue("@RoomID", roomId);
        //            conn.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            if (reader.Read())
        //            {
        //                bill = new BillDTO
        //                {
        //                    BillID = Convert.ToInt32(reader["BillID"]),
        //                    CustomerName = reader["CustomerName"].ToString(),
        //                    Phone = reader["Phone"].ToString(),
        //                    Deposit = Convert.ToInt32(reader["Deposit"]),
        //                    TotalAmount = Convert.ToInt32(reader["TotalAmount"]),
        //                    DateIssued = Convert.ToDateTime(reader["DateIssued"]),
        //                    Status = Convert.ToInt32(reader["Status"])
        //                };
        //            }
        //        }
        //        return bill;
        //    }

        public bool DeleteBookingByID(int bookingId)
        {
            using (SqlConnection conn = dbConnection.cnn)
            {
                conn.Open();
                string query = "DELETE FROM Booking WHERE BookingID = @BookingID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BookingID", bookingId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public List<BookingDTO> GetRoomDetailsByBillID(int billId)
        {
            List<BookingDTO> list = new List<BookingDTO>();

            using (SqlConnection conn = new SqlConnection(dbConnection.ConnectionString))
            {
                conn.Open();
                string query = @"
                SELECT bk.BookingID, bk.RoomID, r.RoomNumber, rt.RoomType, rt.RoomPrice AS Price,
                       bk.BookingDate, bk.CheckoutDate, bk.BillID
                FROM Booking bk
                INNER JOIN Room r ON bk.RoomID = r.RoomID
                INNER JOIN RoomType rt ON r.RoomTypeID = rt.RoomTypeID
                WHERE bk.BillID = @BillID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BillID", billId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BookingDTO dto = new BookingDTO
                            {
                                BookingID = Convert.ToInt32(reader["BookingID"]),
                                RoomID = Convert.ToInt32(reader["RoomID"]),
                                RoomNumber = reader["RoomNumber"].ToString(),
                                RoomType = reader["RoomType"].ToString(),
                                RoomPrice = Convert.ToInt32(reader["Price"]),
                                BookingDate = Convert.ToDateTime(reader["BookingDate"]),
                                CheckoutDate = Convert.ToDateTime(reader["CheckoutDate"]),
                                BillID = Convert.ToInt32(reader["BillID"])
                            };
                            list.Add(dto);
                        }
                    }
                }
            }

            return list;
        }

        //Tính tổng tiền từ billid
        public int GetTotalAmountByBillID(int billId)
        {
            int total = 0;
            using (SqlConnection conn = new SqlConnection(dbConnection.ConnectionString))
            {    
            string query = @"
                            SELECT SUM(
                                DATEDIFF(DAY, b.BookingDate, b.CheckoutDate) * rt.RoomPrice
                            )
                            FROM Booking b
                            INNER JOIN Room r ON b.RoomID = r.RoomID
                            INNER JOIN RoomType rt ON r.RoomTypeID = rt.RoomTypeID
                            WHERE b.BillID = @BillID";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@BillID", billId);
                    conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                    total = Convert.ToInt32(result);
                    conn.Close();
            }
           
            return total;
        }
        }
        public void UpdateCheckoutDateByBillID(int billID, DateTime newCheckoutDate)
        {
            using (SqlConnection conn = new SqlConnection(dbConnection.ConnectionString))
            {
                string query = "UPDATE Booking SET CheckoutDate = @CheckoutDate WHERE BillID = @BillID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CheckoutDate", newCheckoutDate);
                cmd.Parameters.AddWithValue("@BillID", billID);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

    }

}