using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace QuanLyDatPhongKS.DAO
{
    public class DAO_Room : DbConnection
    {
        private DbConnection dbConnection;
        public DAO_Room()
        {
            dbConnection = new DbConnection();
        }
        public List<RoomDTO> GetAvailableRooms(string roomType, DateTime checkin, DateTime checkout)
        {
            List<RoomDTO> rooms = new List<RoomDTO>();

            try
            {
                this.Open(); 

                string query = @"
                                SELECT 
                                    r.RoomID, 
                                    r.RoomNumber, 
                                    r.RoomTypeID, 
                                    rt.RoomType, 
                                    rt.RoomPrice,
                                    rt.RoomContent
                                FROM 
                                    Room r
                                INNER JOIN 
                                    RoomType rt ON r.RoomTypeID = rt.RoomTypeID
                                WHERE 
                                    rt.RoomType = @RoomType
                                    AND r.IsMaintenance = 0
                                    
                                    AND NOT EXISTS (
                                        SELECT 1
                                        FROM Booking b
                                        INNER JOIN Bill bill ON b.BillID = bill.BillID
                                        WHERE b.RoomID = r.RoomID
                                          AND b.BookingDate <= @CheckoutDate
                                          AND b.CheckoutDate >= @BookingDate
                                          AND bill.Status NOT IN (3, 5)
                                    );";

                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@RoomType", roomType);
                cmd.Parameters.AddWithValue("@BookingDate", checkin);

                cmd.Parameters.AddWithValue("@CheckoutDate", checkout);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RoomDTO room = new RoomDTO
                    {
                        RoomID = Convert.ToInt32(reader["RoomID"]),
                        RoomNumber = reader["RoomNumber"].ToString(),
                        RoomTypeId = Convert.ToInt32(reader["RoomTypeID"]),
                        TypeRoom = reader["RoomType"].ToString(),
                        PriceRoom = Convert.ToInt32(reader["RoomPrice"]),
                        RoomContent = reader["RoomContent"].ToString()
                    };
                    rooms.Add(room);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách phòng trống: " + ex.Message);
            }
            finally
            {
                this.Close(); // Đóng kết nối
            }

            return rooms;
        }

        public List<RoomDTO> GetAvailableRoomsWithStatus(DateTime checkin, DateTime checkout)
        {
            List<RoomDTO> rooms = new List<RoomDTO>();

            try
            {
                this.Open();

                string query = @"
            SELECT 
                r.RoomID, 
                r.RoomNumber, 
                r.RoomTypeID, 
                rt.RoomType, 
                rt.RoomPrice,
                rt.RoomContent,
                r.IsMaintenance,
                CASE 
                    WHEN EXISTS (
                        SELECT 1
                        FROM Booking b
                        INNER JOIN Bill bill ON b.BillID = bill.BillID
                        WHERE b.RoomID = r.RoomID
                          AND b.BookingDate <= @CheckoutDate
                          AND b.CheckoutDate >= @BookingDate
                          AND bill.Status NOT IN (3, 5)
                    ) THEN N'Đã đặt'
                    WHEN r.IsMaintenance = 1 THEN N'Bảo trì'
                    ELSE N'Trống'
                END AS Status
            FROM 
                Room r
            INNER JOIN 
                RoomType rt ON r.RoomTypeID = rt.RoomTypeID;
        ";

                SqlCommand cmd = new SqlCommand(query, cnn);

                cmd.Parameters.AddWithValue("@BookingDate", checkin);
                cmd.Parameters.AddWithValue("@CheckoutDate", checkout);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RoomDTO room = new RoomDTO
                    {
                        RoomID = Convert.ToInt32(reader["RoomID"]),
                        RoomNumber = reader["RoomNumber"].ToString(),
                        RoomTypeId = Convert.ToInt32(reader["RoomTypeID"]),
                        PriceRoom = Convert.ToInt32(reader["RoomPrice"]),
                        RoomContent = reader["RoomContent"].ToString(),
                        Status = reader["Status"].ToString()
                    };
                    rooms.Add(room);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách phòng: " + ex.Message);
            }
            finally
            {
                this.Close();
            }

            return rooms;
        }

        public List<RoomDTO> GetBookedDateRangesForRoom(int roomId, DateTime checkin, DateTime checkout)
        {
            List<RoomDTO> result = new List<RoomDTO>();

            try
            {
                this.Open();

                string query = @"
            SELECT RoomID, BookingDate, CheckoutDate
            FROM Booking
            WHERE RoomID = @RoomID
              AND (BookingDate <= @Checkout AND CheckoutDate >= @Checkin)";

                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@RoomID", roomId);
                cmd.Parameters.AddWithValue("@Checkin", checkin);
                cmd.Parameters.AddWithValue("@Checkout", checkout);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    RoomDTO room = new RoomDTO
                    {
                        RoomID = Convert.ToInt32(reader["RoomID"]),
                        BookingDate = Convert.ToDateTime(reader["BookingDate"]),
                        CheckOutDate = Convert.ToDateTime(reader["CheckoutDate"])
                    };

                    result.Add(room);
                }

                reader.Close();
            }
            finally
            {
                this.Close();
            }

            return result;
        }



        public RoomDTO GetRoomById(int roomId)
        {
            RoomDTO room = null;
            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                string query = @"
        SELECT 
            r.RoomID,
            r.RoomNumber,
            r.IsMaintenance,
            rt.RoomPrice,
            rt.RoomType,
            rt.RoomContent,
            (
                SELECT TOP 1 1 
                FROM Booking b 
                WHERE b.RoomID = r.RoomID 
                AND b.CheckOutDate >= GETDATE()
            ) AS IsBooked
        FROM Room r
        JOIN RoomType rt ON r.RoomTypeID = rt.RoomTypeID
        WHERE r.RoomID = @RoomID";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@RoomID", roomId);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string status;

                    if ((bool)reader["IsMaintenance"])
                    {
                        status = "Maintenance";
                    }
                    else if (reader["IsBooked"] != DBNull.Value)
                    {
                        status = "Booked";
                    }
                    else
                    {
                        status = "Empty";
                    }

                    room = new RoomDTO
                    {
                        RoomID = roomId,
                        RoomNumber = reader["RoomNumber"].ToString(),
                        Status = status,
                        PriceRoom = Convert.ToInt32(reader["RoomPrice"]),
                        TypeRoom = reader["RoomType"].ToString(),
                        RoomContent = reader["RoomContent"].ToString()
                    };
                }
            }
            return room;
        }
        //Admin với RoomType
        public RoomDTO GetTypeRoomById(int typeRoomId)
        {
            RoomDTO room = null;
            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                string query = @"
            SELECT * FROM RoomType WHERE RoomTypeID = @RoomTypeID";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@RoomTypeID", typeRoomId);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    room = new RoomDTO
                    {
                        RoomTypeId = typeRoomId,
                        TypeRoom = reader["RoomType"].ToString(),
                        PriceRoom = Convert.ToInt32(reader["RoomPrice"]),
                        RoomContent = reader["RoomContent"].ToString()
                    };
                }
            }
            return room;
        }
        public List<RoomDTO> GetAllRoomTypes()
        {
            List<RoomDTO> roomTypes = new List<RoomDTO>();
            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                string query = "SELECT * FROM RoomType";
                SqlCommand cmd = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RoomDTO room = new RoomDTO
                    {
                        RoomTypeId = Convert.ToInt32(reader["RoomTypeID"]),
                        TypeRoom = reader["RoomType"].ToString(),
                        PriceRoom = Convert.ToInt32(reader["RoomPrice"]),
                        RoomContent = reader["RoomContent"].ToString()
                    };
                    roomTypes.Add(room);
                }
            }
            return roomTypes;
        }
        public void DeleteRoomType(int typeRoomId)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                string query = "DELETE FROM RoomType WHERE RoomTypeID = @RoomTypeID";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@RoomTypeID", typeRoomId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateRoomType(RoomDTO room)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                string query = @"
            UPDATE RoomType
            SET RoomType = @RoomType,
                RoomPrice = @RoomPrice,
                RoomContent = @RoomContent
            WHERE RoomTypeID = @RoomTypeID";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@RoomTypeID", room.RoomTypeId);
                cmd.Parameters.AddWithValue("@RoomType", room.TypeRoom);
                cmd.Parameters.AddWithValue("@RoomPrice", room.PriceRoom);
                cmd.Parameters.AddWithValue("@RoomContent", room.RoomContent);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void UpdateRoom(RoomDTO room)
        {
            using (SqlConnection conn = dbConnection.cnn)
            {
                conn.Open();

                // 1. Cập nhật IsMaintenance trong bảng Room
                string updateRoomQuery = @"
            UPDATE Room 
            SET RoomNumber = @RoomNumber, 
                IsMaintenance = @IsMaintenance 
            WHERE RoomID = @RoomID";

                using (SqlCommand cmd = new SqlCommand(updateRoomQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@RoomID", room.RoomID);
                    cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                    cmd.Parameters.AddWithValue("@IsMaintenance", room.IsMaintenance ? 1 : 0);
                    cmd.ExecuteNonQuery();
                }

                // 2. Cập nhật RoomContent trong bảng RoomType thông qua RoomTypeID
                string updateRoomTypeQuery = @"
            UPDATE RoomType 
            SET RoomContent = @RoomContent 
            WHERE RoomTypeID = (
                SELECT RoomTypeID FROM Room WHERE RoomID = @RoomID
            )";

                using (SqlCommand cmd = new SqlCommand(updateRoomTypeQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@RoomID", room.RoomID);
                    cmd.Parameters.AddWithValue("@RoomContent", room.RoomContent ?? string.Empty);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public bool InsertRoom(RoomDTO room)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.ConnectionString))
            {
                string query = "INSERT INTO Room (RoomNumber, RoomTypeID, IsMaintenance) VALUES (@RoomNumber, @RoomTypeID, @IsMaintenance)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                cmd.Parameters.AddWithValue("@RoomTypeID", room.RoomTypeId);
                cmd.Parameters.AddWithValue("@IsMaintenance", room.IsMaintenance);

                connection.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
        public bool InsertTypeRoom(RoomDTO roomType)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.ConnectionString))
            {
                string query = "INSERT INTO RoomType (RoomType, RoomPrice, RoomContent) VALUES (@RoomType, @RoomPrice, @RoomContent)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@RoomType", roomType.TypeRoom);
                cmd.Parameters.AddWithValue("@RoomPrice", roomType.PriceRoom);
                cmd.Parameters.AddWithValue("@RoomContent", roomType.RoomContent);

                connection.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
        }
        //Hàm kiểm tra phòng không dùng
        public bool IsRoomNumberExists(string roomNumber)
        {
            using (SqlConnection conn = dbConnection.cnn)
            {
                string query = "SELECT COUNT(*) FROM Room WHERE RoomNumber = @RoomNumber";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RoomNumber", roomNumber);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        
        public List<RoomDTO> GetRooms(DateTime checkin, DateTime checkout)
        {
            List<RoomDTO> rooms = new List<RoomDTO>();

            try
            {
                this.Open();

                string query = @"
                                SELECT r.RoomID, r.RoomNumber, rt.RoomType, rt.RoomPrice, rt.RoomContent
                                FROM Room r
                                JOIN RoomType rt ON r.RoomTypeID = rt.RoomTypeID
                                WHERE r.IsMaintenance = 0
                                AND NOT EXISTS (
                                    SELECT 1
                                    FROM Booking b
                                    INNER JOIN Bill bill ON b.BillID = bill.BillID
                                    WHERE b.RoomID = r.RoomID
                                      AND b.BookingDate <= @CheckoutDate
                                      AND b.CheckoutDate >= @BookingDate
                                      AND bill.Status NOT IN (3, 5)
                                );";

                SqlCommand cmd = new SqlCommand(query, cnn);
                
                cmd.Parameters.AddWithValue("@BookingDate", checkin);

                cmd.Parameters.AddWithValue("@CheckoutDate", checkout);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RoomDTO room = new RoomDTO
                    {
                        RoomID = Convert.ToInt32(reader["RoomID"]),
                        RoomNumber = reader["RoomNumber"].ToString(),
                        TypeRoom = reader["RoomType"].ToString(),
                        PriceRoom = Convert.ToInt32(reader["RoomPrice"]),
                        RoomContent = reader["RoomContent"].ToString()
                    };
                    rooms.Add(room);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách phòng trống: " + ex.Message);
            }
            finally
            {
                this.Close(); // Đóng kết nối
            }

            return rooms;
        }
    }
}
