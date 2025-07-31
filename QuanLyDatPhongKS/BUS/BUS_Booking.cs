using QuanLyDatPhongKS.DAO;
using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyDatPhongKS.BUS
{
    public class BUS_Booking
    {
        private DAO_Booking daoBooking;

        public BUS_Booking()
        {
            daoBooking = new DAO_Booking();
        }

        public bool AddBooking(BookingDTO booking)
        {
            return daoBooking.InsertBooking(booking);
        }
        public int GetRoomIdByNumber(string roomNumber)
        {
            return daoBooking.GetRoomIdByNumber(roomNumber);
        }
        //public BillDTO GetBillByRoomID(int roomId)
        //{
        //    return daoBooking.GetBillByRoomID(roomId);
        //}
        public bool DeleteBookingByID(int bookingId)
        {
            return daoBooking.DeleteBookingByID(bookingId);
        }
        public List<BookingDTO> GetRoomDetailsByBillID(int billId)
        {
            return daoBooking.GetRoomDetailsByBillID(billId);
        }
        public int GetTotalAmountByBillID(int billID)
        {
            return daoBooking.GetTotalAmountByBillID(billID);
        }
        public void UpdateCheckoutDateByBillID(int billID, DateTime newCheckoutDate)
        {
            daoBooking.UpdateCheckoutDateByBillID(billID, newCheckoutDate);
        }

    }
}