using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using System.Security.Policy;

using System.Net;

namespace QuanLyDatPhongKS.DAO
{
    public class DAO_Customer
    {
        private DbConnection dbConnection;

        public DAO_Customer()
        {
            dbConnection = new DbConnection();
        }
        public List<CustomerDTO> GetCustomer()
        {
            List<CustomerDTO> CustomerList = new List<CustomerDTO>();
            string query = @"SELECT CustomerID, DisplayName, Email, Phone, Address FROM Customer ";

            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString)) // Sử dụng lk SqlConnection mới cho mỗi truy vấn
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CustomerDTO customer = new CustomerDTO
                            {
                                customerID = Convert.ToInt32(reader["CustomerID"]),
                                displayName = reader["DisplayName"].ToString(),
                                EMAIL = reader["Email"].ToString(),
                                PHONE = reader["Phone"].ToString(),
                                ADDRESS = reader["Address"].ToString()
                            };
                            CustomerList.Add(customer);
                        }
                    }
                }
            }
            return CustomerList;
        }
        public void UpdateCustomer(CustomerDTO customer)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))//khởi tạo chuỗi kết nối mới :>
            {
                string query = @"UPDATE Customer SET DisplayName = @DisplayName,Email = @Email, Phone = @Phone, Address = @Address
                                 WHERE CustomerID = @CustomerID";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", customer.customerID);
                command.Parameters.AddWithValue("@DisplayName", customer.displayName);
                command.Parameters.AddWithValue("@Email", customer.EMAIL);
                command.Parameters.AddWithValue("@Phone", customer.PHONE);
                command.Parameters.AddWithValue("@Address", customer.ADDRESS);
                connection.Open();
                //trả về số dòng bị thay đổi
                command.ExecuteNonQuery();
            }
        }
        public CustomerDTO GetCustomerById(int customerId)
        {
            CustomerDTO customer = null;
            string query = "SELECT CustomerID, CustomerName, Email, Phone, Address FROM Customer WHERE CustomerID = @CustomerID";

            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@CustomerID", customerId);
                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        customer = new CustomerDTO
                        {
                            customerID = (int)reader["CustomerID"],
                            displayName = reader["CustomerName"].ToString(),
                            EMAIL = reader["Email"].ToString(),
                            PHONE = reader["Phone"].ToString(),
                            ADDRESS = reader["Address"].ToString()
                        };
                    }
                }
            }
            return customer;
        }
        public void DeleteCustomer(int customerId)
        {
            string query = "DELETE FROM Customer WHERE CustomerID = @CustomerID";

            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public bool AddCustomer(CustomerDTO customer)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                string query = "INSERT INTO Customer ( CustomerName, Password, DisplayName, Email, Phone, Address) " +
                               "VALUES (@CustomerName, @Password, @DisplayName, @Email, @Phone, @Address)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@CustomerName", customer.customerName);
                command.Parameters.AddWithValue("@Password", customer.passWord);
                command.Parameters.AddWithValue("@DisplayName", customer.displayName);
                command.Parameters.AddWithValue("Email", customer.EMAIL);
                command.Parameters.AddWithValue("@Phone", customer.PHONE);
                command.Parameters.AddWithValue("@Address", customer.ADDRESS);
                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }
        public List<CustomerDTO> SearchCustomerByDisplayName(string displayName)
        {
            List<CustomerDTO> CustomerList = new List<CustomerDTO>();

            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                string query = "SELECT * FROM Customer WHERE DisplayName COLLATE Latin1_General_CI_AI LIKE @DisplayName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DisplayName", "%" + displayName + "%");
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CustomerDTO customer = new CustomerDTO
                    {
                        customerID = (int)reader["CustomerID"],
                        displayName = reader["DisplayName"].ToString(),
                        EMAIL = reader["Email"].ToString(),
                        PHONE = reader["Phone"].ToString(),
                        ADDRESS = reader["Address"].ToString()
                    };
                    CustomerList.Add(customer);
                }
            }

            return CustomerList;
        }
        //Hàm kt customername đã tồn tại trong sql chưa
        public bool IsCustomerNameExists(string customerName)
        {
            using (SqlConnection connection = new SqlConnection(dbConnection.cnn.ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM Customer WHERE CustomerName = @CustomerName";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@CustomerName", customerName);

                connection.Open();
                int count = (int)cmd.ExecuteScalar(); // Thực thi và trả về kết quả đếm
                connection.Close();

                return count > 0; // Trả về true nếu customerName đã tồn tại
            }
        }
    }
}
