using QuanLyDatPhongKS.BUS;
using QuanLyDatPhongKS.DAO;
using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyDatPhongKS
{
    public partial class Booking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                var rooms = Session["SelectedRooms"] as List<BookingDTO>;
                if (rooms != null && rooms.Any())
                {
                    var uniqueBookingDates = rooms.Select(r => r.BookingDate.Date.ToString("dd/MM/yyyy")).Distinct().ToList();
                    var uniqueCheckoutDates = rooms.Select(r => r.CheckoutDate.Date.ToString("dd/MM/yyyy")).Distinct().ToList();

                    txtBookingDate.Text = string.Join(", ", uniqueBookingDates);
                    txtCheckoutDate.Text = string.Join(", ", uniqueCheckoutDates);

                    txtRoomDetails.Text = string.Join(Environment.NewLine, rooms.Select(r =>
                    $"Phòng: {r.RoomNumber} - Loại phòng: {TranslateRoomType(r.RoomType)} - Đơn giá: {r.RoomPrice:N0}"));



                    decimal total = 0;

                    foreach (var room in rooms)
                    {
                        int stayDays = (room.CheckoutDate - room.BookingDate).Days;
                        if (stayDays == 0) stayDays = 1; // Trường hợp chỉ ở 1 ngày

                        total += room.RoomPrice * stayDays;
                    }
                    txtTotalAmount.Text = total.ToString("0");  // Định dạng số nguyên

                    
                    decimal depositAmount = total * 0.2m;
                    txtDepositAmount.Text = depositAmount.ToString("0");  
                }


            }
        }
        private string TranslateRoomType(string roomType)
        {
            switch (roomType.ToLower())
            {
                case "standard":
                    return "Thông thường";
                case "medium":
                    return "Cao cấp";
                case "vip":
                    return "Hạng sang";
                default:
                    return roomType;
            }
        }

        protected void btnConfirmBooking_Click(object sender, EventArgs e)
        {
            
            List<BookingDTO> selectedRooms = Session["SelectedRooms"] as List<BookingDTO>;
            if (selectedRooms == null || selectedRooms.Count == 0)
            {
                string script = "alert('Không có phòng nào được chọn!');";
                ClientScript.RegisterStartupScript(this.GetType(), "RoomSelectionAlert", script, true);

                return;
            }
            else
            {
                DateTime bookingDate = selectedRooms[0].BookingDate;
                DateTime checkoutDate = selectedRooms[0].CheckoutDate;
                int totalAmount;
                if (!int.TryParse(txtTotalAmount.Text, out totalAmount))
                {
                    string script = "alert('Số tiền tổng không hợp lệ!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "TotalAmountAlert", script, true);
                    return;
                }

                int depositAmount;
                if (!int.TryParse(txtDepositAmount.Text, out depositAmount))
                {
                    string script = "alert('Số tiền đặt cọc không hợp lệ!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "DepositAmountAlert", script, true);
                    return;
                }
                // 1. Tạo hóa đơn (Bill) và lấy BillID
                BillDTO bill = new BillDTO
                {

                    CustomerName = HttpContext.Current.Session["DisplayName"]?.ToString(),
                    Phone = HttpContext.Current.Session["Phone"]?.ToString(),
                    TotalAmount = totalAmount,
                    Deposit = depositAmount,
                    Status = 2
                };

                BUS_Bill busBill = new BUS_Bill();
                int billId = busBill.AddBill(bill);
                if (billId <= 0)
                {
                    string script = "alert('Không thể tạo hóa đơn!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "RoomSelectionAlert", script, true);

                    return;
                }

                // 2. Duyệt từng phòng, lấy RoomID từ RoomNumber rồi insert vào Booking
                foreach (BookingDTO bookingDto in selectedRooms)
                {
                    BUS_Booking busBooking = new BUS_Booking();
                    string roomNumber = bookingDto.RoomNumber;
                    int roomId = busBooking.GetRoomIdByNumber(bookingDto.RoomNumber);
                    if (roomId > 0)
                    {
                       
                        busBooking.AddBooking(new BookingDTO
                        {
                            BillID = billId,
                            RoomID = roomId,
                            BookingDate = bookingDto.BookingDate,
                            CheckoutDate = bookingDto.CheckoutDate,
                        });
                    }
                }

                // 3. Chuyển trang hoặc báo thành công
                Response.Redirect($"QRCode.aspx?BillID={billId}&Deposit={depositAmount}");
            }

        }
    }
}