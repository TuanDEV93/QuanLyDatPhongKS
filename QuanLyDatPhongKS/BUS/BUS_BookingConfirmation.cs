using QuanLyDatPhongKS.DAO;
using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyDatPhongKS.BUS
{
    public class BUS_BookingConfirmation
    {
        public List<BookingConfirmationDTO> GetBookingByDisplayName(string displayName)
        {
            DAO_BookingConfirmation confirm = new DAO_BookingConfirmation();
            return confirm.GetBookingByDisplayName(displayName); 
        }
    }
}