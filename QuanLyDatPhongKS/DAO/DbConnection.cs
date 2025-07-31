using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QuanLyDatPhongKS.DAO
{
    public class DbConnection
    {
        public SqlConnection cnn;
        private string _strCnn ;
        public string ConnectionString => _strCnn;
        public DbConnection()
        {
            _strCnn = ConfigurationManager.ConnectionStrings["QLKSConnectionString"].ConnectionString;

            cnn = new SqlConnection(_strCnn);
        }

        public DbConnection(string strCnn)
        {
            this._strCnn = strCnn;
            cnn= new SqlConnection(_strCnn);
        }
        public void Open()
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
        }
        public void Close()
        {
            if (cnn.State == ConnectionState.Open)
                cnn.Close();
        }
        // Phương thức thực thi truy vấn trả về DataTable
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            try
            {
                Open();
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            finally
            {
                Close();
            }
        }

        // Phương thức thực thi lệnh trả về số dòng bị ảnh hưởng
        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            try
            {
                Open();
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    return cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                Close();
            }
        }

        // Phương thức thực thi lệnh trả về giá trị đơn lẻ
        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            try
            {
                Open();
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    return cmd.ExecuteScalar();
                }
            }
            finally
            {
                Close();
            }
        }

    }

}