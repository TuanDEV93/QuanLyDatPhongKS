using QuanLyDatPhongKS.BUS;
using QuanLyDatPhongKS.DAO;
using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyDatPhongKS
{
    public partial class BookingConfirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DAO_Bill daoBill = new DAO_Bill();
                daoBill.AutoCancelLateBills();
                LoadBookingConfirmation();
            }
        }

        private void LoadBookingConfirmation()
        {
            try
            {
                
                string username = Session["Username"] as string;
                if (string.IsNullOrEmpty(username))
                {
                    Response.Write("Không tìm thấy Username trong Session.");
                    return;
                }

                
                DAO_BookingConfirmation daoBooking = new DAO_BookingConfirmation();
                string displayName = daoBooking.GetDisplayNameByUsername(username);
                if (string.IsNullOrEmpty(displayName))
                {
                    Response.Write("Không tìm thấy DisplayName tương ứng với Username: " + username);
                    return;
                }

                // 3. Lấy danh sách booking theo DisplayName
                List<BookingConfirmationDTO> bookingList = daoBooking.GetBookingByDisplayName(displayName);
                // Gộp các số phòng có cùng BillID
                var groupedBookings = bookingList
                    .GroupBy(b => new
                    {
                        b.BillID,
                        b.BookingDate,
                        b.CheckoutDate,
                        b.TotalAmount,
                        b.DepositPrice,
                        b.Status
                    })
                    .Select(g => new BookingConfirmationDTO
                    {
                        BillID = g.Key.BillID,
                        BookingDate = g.Key.BookingDate,
                        CheckoutDate = g.Key.CheckoutDate,
                        TotalAmount = g.Key.TotalAmount,
                        DepositPrice = g.Key.DepositPrice,
                        Status = g.Key.Status,
                        RoomNumber = string.Join(", ", g.Select(b => b.RoomNumber).Distinct())
                    }).ToList();
                // Chuyển mã trạng thái sang văn bản
                foreach (var booking in groupedBookings)
                {
                    switch (booking.Status)
                    {
                        case "0":
                            booking.Status = "Đã thanh toán";
                            break;
                        case "1":
                            booking.Status = "Đã đặt cọc";
                            break;
                        case "2":
                            booking.Status = "Đang chờ đặt cọc";
                            break;
                        case "3":
                            booking.Status = "Đã huỷ";
                            break;
                        case "4":
                            booking.Status = "Đang sử dụng";
                            break;
                        case "5":
                            booking.Status = "Trả phòng sớm";
                            break;
                        default:
                            booking.Status = "Không xác định";
                            break;
                    }
                }

                // Bind dữ liệu vào GridView
                if (bookingList.Count == 0)
                {
                    Response.Write("Không có lịch sử đặt sân.");
                }
                else
                {
                    GridView1.DataSource = groupedBookings;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Lỗi: " + ex.Message);
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ContinuePayment")
            {
                int billId = Convert.ToInt32(e.CommandArgument);

                // Truy cập BillHistory để lấy BillContent cũ (gần nhất)
                BUS_Bill busHistory = new BUS_Bill();
                string billContent = busHistory.GetLatestBillContentByBillID(billId);

                if (!string.IsNullOrEmpty(billContent))
                {
                    // Truy xuất số tiền cọc từ hóa đơn
                    BUS_Bill busBill = new BUS_Bill();
                    decimal deposit = busBill.GetDepositByBillID(billId);

                    // Chuyển sang QRCode với BillID, Deposit và BillContent
                    Response.Redirect($"QRCode.aspx?BillID={billId}&Deposit={deposit}&BillContent={billContent}");
                }
                else
                {
                    Response.Write("<script>alert('Không tìm thấy thông tin thanh toán trước đó.');</script>");
                }
            }
        }


    }
}
