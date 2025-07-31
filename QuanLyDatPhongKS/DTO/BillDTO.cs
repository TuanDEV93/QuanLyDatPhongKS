using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyDatPhongKS.DTO
{
    public class BillDTO
    {
        public int CustomerID {get; set;}
        public int UserID { get; set;}
        public int BillID { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public int TotalAmount { get; set; }
        public int Deposit { get; set; }
        public DateTime DateIssued { get; set; }
        public int Status { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public int RoomPrice { get; set; }
        //Tính tiền còn lại
        public int RemainingAmount => TotalAmount - Deposit;
    }
}