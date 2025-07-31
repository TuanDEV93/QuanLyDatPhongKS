using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyDatPhongKS.DTO
{
    public class BookingConfirmationDTO
    {
        public  int CustomerID { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public string RoomNumber { get; set; }
        public int TotalAmount { get; set; }
        public int DepositPrice { get; set; }
        public string Status { get; set; }
        public int BillID { get; set; } 
    }
}