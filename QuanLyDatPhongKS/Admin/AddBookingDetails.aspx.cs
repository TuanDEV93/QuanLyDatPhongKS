using QuanLyDatPhongKS.BUS;
using QuanLyDatPhongKS.DAO;
using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyDatPhongKS.Admin
{
    public partial class AddBookingDetails : System.Web.UI.Page
    {
        protected List<int> selectedRoomIds = new List<int>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string roomsParam = Request.QueryString["rooms"];
                string checkinParam = Request.QueryString["checkin"];
                string checkoutParam = Request.QueryString["checkout"];

                if (string.IsNullOrEmpty(roomsParam) || string.IsNullOrEmpty(checkinParam) || string.IsNullOrEmpty(checkoutParam))
                {
                    lblMessage.Text = "Dữ liệu phòng hoặc ngày không hợp lệ.";
                    btnSubmit.Enabled = false;
                    return;
                }

                DateTime checkin, checkout;
                if (!DateTime.TryParse(checkinParam, out checkin) || !DateTime.TryParse(checkoutParam, out checkout))
                {
                    lblMessage.Text = "Ngày nhận/trả phòng không hợp lệ.";
                    btnSubmit.Enabled = false;
                    return;
                }

                txtCheckIn.Text = checkin.ToString("yyyy-MM-dd");
                txtCheckOut.Text = checkout.ToString("yyyy-MM-dd");

                // Tách danh sách phòng
                string[] roomIdsStr = roomsParam.Split(',');
                foreach (var idStr in roomIdsStr)
                {
                    if (int.TryParse(idStr, out int id))
                        selectedRoomIds.Add(id);
                }

                if (selectedRoomIds.Count == 0)
                {
                    lblMessage.Text = "Không có phòng nào được chọn.";
                    btnSubmit.Enabled = false;
                    return;
                }

                // Tính tổng tiền
                int totalAmount = 0;
                BUS_Room busRoom = new BUS_Room();

                foreach (int roomId in selectedRoomIds)
                {
                    RoomDTO room = busRoom.GetRoomById(roomId);
                    if (room != null)
                    {
                        int stayDays = (checkout - checkin).Days;
                        if (stayDays == 0) stayDays = 1;

                        totalAmount += (int)(room.PriceRoom * stayDays);
                    }
                }

                
                txtTotalAmount.Text = totalAmount.ToString("N0");
                ViewState["TotalAmount"] = totalAmount;
                ViewState["CheckIn"] = checkin;
                ViewState["CheckOut"] = checkout;
                ViewState["SelectedRoomIds"] = selectedRoomIds;
            }
            else
            {
                selectedRoomIds = ViewState["SelectedRoomIds"] as List<int>;
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string CustomerName  = txtCustomerName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            int totalAmount = (int)ViewState["TotalAmount"];
            DateTime checkin = (DateTime)ViewState["CheckIn"];
            DateTime checkout = (DateTime)ViewState["CheckOut"];
            
            BillDTO bill = new BillDTO
            {
                CustomerName = CustomerName,
                Phone = phone,
                TotalAmount = totalAmount,
                Status = 0
            };

            BUS_Bill busBill = new BUS_Bill();
            int billId = busBill.AddBill(bill);
            if (billId <= 0)
            {
                string script = "alert('Không thể tạo hóa đơn!');";
                ClientScript.RegisterStartupScript(this.GetType(), "RoomSelectionAlert", script, true);

                return;
            }
            BUS_Booking busBooking = new BUS_Booking();
            foreach (int roomId in selectedRoomIds)
            {
                BookingDTO booking = new BookingDTO
                {
                    RoomID = roomId,
                    BillID = billId,
                    BookingDate = checkin,
                    CheckoutDate = checkout,
                };

                bool success = busBooking.AddBooking(booking);
                if (!success)
                {
                    lblMessage.Text = $"Lỗi khi thêm đặt phòng cho phòng ID {roomId}";
                    return;
                }
                string script = "alert('Đặt phòng thành công!'); window.location='RoomAlley.aspx';";
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", script, true);
            }
        }
    }
}
