using QuanLyDatPhongKS.DAO;
using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace QuanLyDatPhongKS.BUS
{
    public class BUS_Bill
    {
        private DAO_Bill daoBill;

        public BUS_Bill()
        {
            daoBill = new DAO_Bill();

        }
        public int AddBill(BillDTO dtoBill)
        {
            return daoBill.InsertBill(dtoBill);
        }

        public bool UpdateStatus(int billId, int status)
        {
            DAO_Bill daoBill = new DAO_Bill();
            return daoBill.UpdateStatusBill(billId, status);
        }
        public int GetDepositByBillID(int billId)
        {
            DAO_Bill dao = new DAO_Bill();
            return dao.GetDepositByBillID(billId);
        }
        public string GetLatestBillContentByBillID(int billId)
            {
                // Gọi DAO để lấy BillContent theo BillID mới nhất
                DAO_Bill dao = new DAO_Bill();
                return dao.GetLatestBillContent(billId);
            }

        public DataTable LoadDoanhThuTheoKhoangThoiGian(DateTime startDate, DateTime endDate)
        {
            DAO_Bill daoBill = new DAO_Bill();
            return daoBill.LoadDoanhThuTheoKhoangThoiGian(startDate, endDate);
        }

        public DataTable LoadTopCanceller(int month, int year)
        {
            DAO_Bill daoBill = new DAO_Bill();
            return daoBill.LoadTopCanceller(month, year);
        }

        public List<BillDTO> GetBills()
        {
         
            return daoBill.GetBills();
        }
        public bool UpdateBillStatus(int billID, string status)
        {
            return daoBill.UpdateBillStatus(billID, status);
        }
        public List<BillDTO> GetBillsByDate(DateTime date)
        {
            return daoBill.GetBillsByDate(date);
        }
        public List<BillDTO> GetBillsByFilter(DateTime? fromDate, DateTime? toDate, int? status, string customerName, string phone)
        {
            return daoBill.GetBillsByFilter(fromDate, toDate, status, customerName, phone);
        }

        public DataTable LoadTopRoom(int month, int year)
        {
            return daoBill.LoadTopRoom(month, year);
        }
        public DataTable LoadTopKhachHang(int month, int year)
        {
            return daoBill.LoadTopKhachHang(month, year);
        }
        public void UpdateTotalAmount(int billId, int newTotal)
        {
            daoBill.UpdateTotalAmount(billId, newTotal);
        }
    }
}