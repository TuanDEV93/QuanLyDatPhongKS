using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyDatPhongKS.DTO
{

    public class RoomDTO
    {

        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
    
        public string TypeRoom { get; set; }
        public int PriceRoom { get; set; }

        public DateTime BookingDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string RoomContent {  get; set; }
        public string Status { get; set; }
        public bool IsMaintenance { get; set; }
    }
}