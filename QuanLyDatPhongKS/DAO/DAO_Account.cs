using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Ajax.Utilities;
using QuanLyDatPhongKS.DTO;

namespace QuanLyDatPhongKS.DAO
{

    public class DAO_Account:DbConnection
    {
        public void GhiThongTinKhachHang(CustomerDTO customer)
        {
            DAO_Account dao = new DAO_Account();
            dao.Open();
            string query = "INSERT INTO Customer (CustomerName,PassWord,DisplayName,Email,Phone,Address) values ('" + customer.customerName + "','" + customer.passWord + "','" + customer.displayName + "','" + customer.EMAIL + "','" + customer.PHONE + "','" + customer.ADDRESS + "') ";
            SqlCommand cmd = new SqlCommand(query, dao.cnn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            dao.Close();
        }

        public int TrungTenDangNhap(string customerName)
        {
            DAO_Account dao = new DAO_Account();
            dao.Open();
            string query = "SELECT * FROM Customer WHERE CustomerName = '" + customerName + "'";
            SqlCommand cmd = new SqlCommand(query, dao.cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                return 1;
            }
            return 0;

        }


        public int TrungSoDienThoai (string Phone)
        {
            DAO_Account dao = new DAO_Account();
            dao.Open();
            string query = "SELECT * FROM Customer WHERE PHONE = '" + Phone + "'";
            SqlCommand cmd = new SqlCommand(query, dao.cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                return 1;
            }
            return 0;
        }

        public int TrungEmail(string Email)
        {
            DAO_Account dao = new DAO_Account();
            dao.Open();
            string query = "SELECT * FROM Customer WHERE EMAIL = '" + Email + "'";
            SqlCommand cmd = new SqlCommand(query, dao.cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                return 1;
            }
            return 0;
        }

        public int DangNhapThanhCong(string Username, string Password)
        {
            int role = -1;
            DAO_Account dao = new DAO_Account();
            dao.Open();
            string queryAccount = "SELECT Role FROM Account WHERE Username = @Username AND Password = @Password";
            using (SqlCommand cmdAccount = new SqlCommand(queryAccount, dao.cnn))
            {
                cmdAccount.Parameters.AddWithValue("@Username", Username);
                cmdAccount.Parameters.AddWithValue("@Password", Password);

                using (SqlDataReader readerAccount = cmdAccount.ExecuteReader())
                {
                    if (readerAccount.HasRows)
                    {
                        readerAccount.Read();
                        role = readerAccount.GetInt32(0);
                        readerAccount.Close();
                        return role;
                    }
                }
            }

            string queryCustomer = "SELECT CustomerID FROM Customer WHERE CustomerName = @CustomerName AND Password = @Password";
            using (SqlCommand cmdCustomer = new SqlCommand(queryCustomer, dao.cnn))
            {
                cmdCustomer.Parameters.AddWithValue("@CustomerName", Username);
                cmdCustomer.Parameters.AddWithValue("@Password", Password);

                using (SqlDataReader readerCustomer = cmdCustomer.ExecuteReader())
                {
                    if (readerCustomer.HasRows)
                    {
                        role = 2;
                    }
                }
            }

            dao.Close();
            return role;
        }

    }
}
