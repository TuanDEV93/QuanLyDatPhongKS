using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using QuanLyDatPhongKS.DTO;
using System.Data.Common;
using QuanLyDatPhongKS.Admin;

namespace QuanLyDatPhongKS.DAO
{
    public class DAO_Bill
    {

        private DbConnection dbConnection;
        public DAO_Bill()
        {
            dbConnection = new DbConnection();
        }
        //Thêm hoá đơn QLKS
        public int InsertBill(BillDTO dtoBill)
        {
            using (SqlConnection conn = dbConnection.cnn)
            {
                conn.Open();

                string username = HttpContext.Current.Session["Username"]?.ToString();
                string roleStr = HttpContext.Current.Session["Role"]?.ToString();

                if (string.IsNullOrEmpty(username))
                    throw new Exception("Không tìm thấy Username trong Session");

                int role = -1;
                if (!int.TryParse(roleStr, out role))
                    throw new Exception("Không xác định được quyền của người dùng.");

                string displayName = dtoBill.CustomerName;
                string phone = dtoBill.Phone;

                int customerId = -1;
                int userId = -1;

                if (role == 2)
                {
                    // Trường hợp khách hàng
                    string getCustomerInfoQuery = @"
                SELECT CustomerID, DisplayName, Phone 
                FROM Customer 
                WHERE CustomerName = @CustomerName";

                    using (SqlCommand getCmd = new SqlCommand(getCustomerInfoQuery, conn))
                    {
                        getCmd.Parameters.AddWithValue("@CustomerName", username);

                        using (SqlDataReader reader = getCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customerId = Convert.ToInt32(reader["CustomerID"]);
                                displayName = reader["DisplayName"].ToString();
                                phone = reader["Phone"].ToString();
                            }
                            else
                            {
                                throw new Exception("Không tìm thấy thông tin khách hàng.");
                            }
                        }
                    }
                }
                else if (role == 0 || role == 1)
                {
                    // Trường hợp admin/nhân viên
                    string getUserInfoQuery = @"
                SELECT UserID 
                FROM Account 
                WHERE Username = @Username";

                    using (SqlCommand getCmd = new SqlCommand(getUserInfoQuery, conn))
                    {
                        getCmd.Parameters.AddWithValue("@Username", username);

                        using (SqlDataReader reader = getCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userId = Convert.ToInt32(reader["UserID"]);
                               
                            }
                            else
                            {
                                throw new Exception("Không tìm thấy thông tin người dùng.");
                            }
                        }
                    }
                }

                if (dtoBill.DateIssued == DateTime.MinValue)
                {
                    dtoBill.DateIssued = DateTime.Now;
                }

                string insertQuery = "";
                SqlCommand cmd;

                if (role == 2)
                {
                    insertQuery = @"
                INSERT INTO Bill (CustomerID, CustomerName, Phone, TotalAmount, Deposit, Status, DateIssued)
                OUTPUT INSERTED.BillID
                VALUES (@CustomerID, @CustomerName, @Phone, @TotalAmount, @Deposit, @Status, @DateIssued)";

                    cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                }
                else
                {
                    insertQuery = @"
                INSERT INTO Bill (UserID, CustomerName, Phone, TotalAmount, Deposit, Status, DateIssued)
                OUTPUT INSERTED.BillID
                VALUES (@UserID, @CustomerName, @Phone, @TotalAmount, @Deposit, @Status, @DateIssued)";

                    cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                }

                cmd.Parameters.AddWithValue("@CustomerName", displayName);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@TotalAmount", dtoBill.TotalAmount);
                cmd.Parameters.AddWithValue("@Deposit", dtoBill.Deposit);
                cmd.Parameters.AddWithValue("@Status", dtoBill.Status);
                cmd.Parameters.AddWithValue("@DateIssued", dtoBill.DateIssued);

                int billId = (int)cmd.ExecuteScalar();
                return billId;
            }
        }

        public bool UpdateStatusBill(int billId, int newStatus)
        {
            using (SqlConnection conn = dbConnection.cnn)
            {
                conn.Open();
                string query = "UPDATE Bill SET Status = @Status WHERE BillID = @BillID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", newStatus);
                    cmd.Parameters.AddWithValue("@BillID", billId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        public string GetLatestBillContent(int billId)
        {
            using (SqlConnection conn = dbConnection.cnn)
            {
                conn.Open();
                string query = "SELECT TOP 1 BillContent FROM BillHistory WHERE BillID = @BillID ORDER BY HistoryId DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BillID", billId);
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : null;

            }
        }

        public int GetDepositByBillID(int billId)
        {
            using (SqlConnection conn = dbConnection.cnn)
            {
                conn.Open();
                string query = "SELECT Deposit FROM Bill WHERE BillID = @BillID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@BillID", billId);

                    object result = cmd.ExecuteScalar();
                    conn.Close();

                    if (result != null && int.TryParse(result.ToString(), out int deposit))
                    {
                        return deposit;
                    }

                    return 0; 
                }
            }
        }
        //Admin
        public List<BillDTO> GetBills()
        {
            List<BillDTO> billList = new List<BillDTO>();

            string query = @"
                            SELECT 
                                b.BillID, 
                                b.CustomerName, 
                                b.Phone, 
                                b.Deposit, 
                                b.TotalAmount, 
                                b.Status,
                                MIN(bk.BookingDate) AS BookingDate,
                                MAX(bk.CheckoutDate) AS CheckoutDate
                            FROM Bill b
                            INNER JOIN Booking bk ON b.BillID = bk.BillID
                            GROUP BY b.BillID, b.CustomerName, b.Phone, b.Deposit, b.TotalAmount, b.Status
                            ORDER BY MIN(bk.BookingDate) DESC";

            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BillDTO bill = new BillDTO
                            {
                                BillID = Convert.ToInt32(reader["BillID"]),
                                CustomerName = reader["CustomerName"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Deposit = Convert.ToInt32(reader["Deposit"]),
                                TotalAmount = Convert.ToInt32(reader["TotalAmount"]),
                                Status = Convert.ToInt32(reader["Status"]),
                                BookingDate = Convert.ToDateTime(reader["BookingDate"]),
                                CheckoutDate = Convert.ToDateTime(reader["CheckoutDate"])
                            };
                            billList.Add(bill);
                        }
                    }
                }
            }

            return billList;
        }

        private string MapStatusToText(string statusValue)
        {
            switch (statusValue)
            {
                case "1":
                    return "Đã đặt cọc";
                case "2":
                    return "Chờ đặt cọc";
                case "3":
                    return "Bị Huỷ";
                case "4":
                    return "Đang sử dụng";
                default:
                    return "Đã thanh toán";
            }
        }
        public bool UpdateBillStatus(int billID, string status)
        {
            string query = "UPDATE dbo.Bill SET Status = @Status WHERE BillID = @BillID";

            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@BillID", billID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public List<BillDTO> GetBillsByDate(DateTime date)
        {
            List<BillDTO> billList = new List<BillDTO>();
            string query = @"
                            SELECT 
                                b.BillID,
                                b.CustomerName,
                                b.Phone,
                                b.Deposit,
                                b.TotalAmount,
                               
                                b.Status,
                                bk.BookingDate,
                                bk.CheckoutDate,
                                r.RoomNumber,
                                rt.RoomType,
                                rt.RoomPrice
                            FROM Bill b
                            INNER JOIN Booking bk ON b.BillID = bk.BillID
                            INNER JOIN Room r ON bk.RoomID = r.RoomID
                            INNER JOIN RoomType rt ON r.RoomTypeID = rt.RoomTypeID
                            WHERE CAST(bk.BookingDate AS DATE) = @Date";

            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Date", date.Date);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BillDTO bill = new BillDTO
                            {
                                BillID = Convert.ToInt32(reader["BillId"]),
                                CustomerName = reader["CustomerName"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Deposit = Convert.ToInt32(reader["Deposit"]),
                                TotalAmount = Convert.ToInt32(reader["TotalAmount"]),
                               
                                Status = Convert.ToInt32(reader["Status"]),
                                BookingDate = Convert.ToDateTime(reader["BookingDate"]),
                                CheckoutDate = Convert.ToDateTime(reader["CheckoutDate"]),
                                RoomNumber = reader["RoomNumber"].ToString(),
                                RoomType = reader["RoomType"].ToString(),
                                RoomPrice = Convert.ToInt32(reader["RoomPrice"])
                            };
                            billList.Add(bill);
                        }
                    }
                }
            }

            return billList;
        }
        //Hàm lọc ngày tháng và trạng thái tên người đặt và số điện thoại
       public List<BillDTO> GetBillsByFilter(DateTime? fromDate, DateTime? toDate, int? status, string customerName, string phone)
{
            List<BillDTO> billList = new List<BillDTO>();

            string query = @"
                            SELECT 
                                b.BillID, 
                                b.CustomerName, 
                                b.Phone, 
                                b.Deposit, 
                                b.TotalAmount, 
                                b.Status,
                                MIN(bk.BookingDate) AS BookingDate,
                                MAX(bk.CheckoutDate) AS CheckoutDate
                            FROM Bill b
                            INNER JOIN Booking bk ON b.BillID = bk.BillID
                            WHERE 1 = 1";

            //Điều kiện lọc
            if (fromDate.HasValue)
                query += " AND bk.BookingDate >= @FromDate";
            if (toDate.HasValue)
                query += " AND bk.CheckoutDate <= @ToDate";
            if (status.HasValue)
                query += " AND b.Status = @Status";
            if (!string.IsNullOrEmpty(customerName))
                query += " AND b.CustomerName LIKE @CustomerName";
            if (!string.IsNullOrEmpty(phone))
                query += " AND b.Phone LIKE @Phone";

            // nếu có cùng billid thì gộp thành 1 dòng
                    query += @"
                GROUP BY b.BillID, b.CustomerName, b.Phone, b.Deposit, b.TotalAmount, b.Status
                ORDER BY MIN(bk.BookingDate) DESC";

            using (SqlConnection conn = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                // Gán giá trị tham số
                if (fromDate.HasValue)
                    cmd.Parameters.AddWithValue("@FromDate", fromDate.Value);
                if (toDate.HasValue)
                    cmd.Parameters.AddWithValue("@ToDate", toDate.Value);
                if (status.HasValue)
                    cmd.Parameters.AddWithValue("@Status", status.Value);
                if (!string.IsNullOrEmpty(customerName))
                    cmd.Parameters.AddWithValue("@CustomerName", "%" + customerName + "%");
                if (!string.IsNullOrEmpty(phone))
                    cmd.Parameters.AddWithValue("@Phone", "%" + phone + "%");

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    billList.Add(new BillDTO
                    {
                        BillID = Convert.ToInt32(reader["BillID"]),
                        CustomerName = reader["CustomerName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Deposit = Convert.ToInt32(reader["Deposit"]),
                        TotalAmount = Convert.ToInt32(reader["TotalAmount"]),
                        Status = Convert.ToInt32(reader["Status"]),
                        BookingDate = Convert.ToDateTime(reader["BookingDate"]),
                        CheckoutDate = Convert.ToDateTime(reader["CheckoutDate"])
                    });
                }
            }

            return billList;
        }



        public DataTable LoadDoanhThuTheoKhoangThoiGian(DateTime startDate, DateTime endDate)
        {
            using (SqlConnection connection = dbConnection.cnn)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = @"
                                    SELECT 
                                    @StartDate AS StartDate,  
                                    @EndDate AS EndDate,      
                                    SUM(CASE 
                                        WHEN Status = 0 THEN TotalAmount      
                                        WHEN Status = 1 THEN Deposit         
                                        WHEN Status = 3 THEN Deposit         
                                        WHEN Status = 4 THEN TotalAmount      
                                        ELSE 0                                
                                    END) AS TotalRevenue
                                FROM Bill
                                WHERE DateIssued >= @StartDate AND DateIssued <= @EndDate;";

                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);

                            transaction.Commit();
                            return dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
        }
        public DataTable LoadTopCanceller(int month, int year)
        {
            using (SqlConnection connection = dbConnection.cnn)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = @"
                            SELECT TOP 1 
                                B.CustomerName, 
                                B.Phone, 
                                COUNT(*) AS CancelCount
                            FROM Bill B
                            WHERE B.Status = 3
                              AND MONTH(B.DateIssued) = @Month
                              AND YEAR(B.DateIssued) = @Year
                            GROUP BY B.CustomerName, B.Phone
                            ORDER BY CancelCount DESC;";

                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@Month", month);
                        command.Parameters.AddWithValue("@Year", year);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);

                            transaction.Commit();
                            return dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
        }
        public DataTable LoadTopRoom(int month, int year)
        {
            using (SqlConnection connection = dbConnection.cnn)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = @"
                            SELECT TOP 1 WITH TIES 
                                   r.RoomNumber,            
                                   COUNT(*) AS TotalBookings
                            FROM   Booking bk
                            INNER JOIN Bill b ON bk.BillID = b.BillID
                            INNER JOIN Room r ON bk.RoomID = r.RoomID
                            WHERE  MONTH(bk.BookingDate) = @Month
                              AND  YEAR(bk.BookingDate) = @Year
                            GROUP BY r.RoomNumber
                            ORDER BY COUNT(*) DESC;";

                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@Month", month);
                        command.Parameters.AddWithValue("@Year", year);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);

                            transaction.Commit();

                            return dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
        }
        public DataTable LoadTopKhachHang(int month, int year)
        {
            using (SqlConnection connection = dbConnection.cnn)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string query = @"
                    SELECT TOP 1 WITH TIES --lấy cùng bản ghi có số lượt đặt nhiều nhất
                       
                       b.CustomerName, 
                       b.Phone, 
                       COUNT(*) AS TotalBookings
                FROM Booking bk
                JOIN Bill b ON bk.BillID = b.BillID
                WHERE MONTH(bk.BookingDate) = @Month 
                  AND YEAR(bk.BookingDate) = @Year
                  AND b.Status IN (0, 1, 4,5)
                GROUP BY b.CustomerName, b.Phone
                ORDER BY COUNT(*) DESC;";

                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@Month", month);
                        command.Parameters.AddWithValue("@Year", year);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);

                            transaction.Commit();

                            return dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
        }
        //Hàm cập nhật tổng tiền mới khi bấm xoá 1 hoá đơn phòng
        public void UpdateTotalAmount(int billId, int newTotal)
        {
            using (SqlConnection connection = dbConnection.cnn)
            {
                string query = "UPDATE Bill SET TotalAmount = @TotalAmount WHERE BillID = @BillID";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TotalAmount", newTotal);
                    cmd.Parameters.AddWithValue("@BillID", billId);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        public void AutoCancelLateBills()
        {
            string query = @"
         UPDATE Bill
        SET Status = 3
        WHERE Status = 2
          AND DATEDIFF(HOUR, DateIssued, GETDATE()) >= 24"; 

            using (SqlConnection conn = new SqlConnection(dbConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }


}

