using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyDatPhongKS.DTO
{
    public class StaffDTO
    {
        public int UserID { get; set; }
        public string DisplayName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

    }
}