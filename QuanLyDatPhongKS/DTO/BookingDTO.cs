using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyDatPhongKS.DTO
{
    public class BookingDTO
    {
        public int BookingID { get; set; }
        public int BillID { get; set; }
        public int RoomID { get; set; }
        public string RoomNumber {  get; set; }
        public int RoomPrice { get; set; }
        public string RoomType { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime CheckoutDate { get; set; }
       
    }

}