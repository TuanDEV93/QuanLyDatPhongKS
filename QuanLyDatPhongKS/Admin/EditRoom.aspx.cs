using QuanLyDatPhongKS.BUS;
using QuanLyDatPhongKS.DAO;
using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyDatPhongKS.Admin
{
    public partial class EditRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int roomId = Convert.ToInt32(Request.QueryString["RoomID"]);
                DateTime checkin = (DateTime)Session["SelectedCheckinDate"];
                DateTime checkout = (DateTime)Session["SelectedCheckoutDate"];
                ViewState["RoomID"] = roomId;
                BUS_Room busRoom = new BUS_Room();
                var room = busRoom.GetRoomById(roomId);
                if (room != null)
                {
                    txtRoomNumber.Text = room.RoomNumber;
                    ddlStatus.SelectedValue = room.Status;
                    txtRoomPrice.Text = room.PriceRoom.ToString("0.##");
                    txtRoomType.Text = room.TypeRoom;
                    txtRoomContent.Text = room.RoomContent;
                    //sẽ so ngày trả với ngày hiện tại nếu ngày trả lớn hơn ngày hiện tại sẽ hiện đã đặt.
                    if (room.Status == "Booked")
                    {
                        ddlStatus.Enabled = false;
                    }
                }
                LoadBookedDates(roomId);
                ddlStatus_SelectedIndexChanged(null, null);
            }
        }
        private void LoadBookedDates(int roomId)
        {
            var dao = new DAO_Room();
            DateTime checkin = (DateTime)Session["SelectedCheckinDate"];
            DateTime checkout = (DateTime)Session["SelectedCheckoutDate"];
            var dateRanges = dao.GetBookedDateRangesForRoom(roomId, checkin, checkout);
            //Hiển thị ngày đặt và ngày trả
            var formatted = dateRanges.Select(r =>
                $"{r.BookingDate:dd/MM/yyyy} ➜ {r.CheckOutDate:dd/MM/yyyy}"
            );

            litBookedDates.Text = string.Join("<br/>", formatted);
        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStatus = ddlStatus.SelectedValue;

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            RoomDTO room = new RoomDTO
            {
                RoomID = Convert.ToInt32(Request.QueryString["RoomID"]), 
                RoomNumber = txtRoomNumber.Text,
                IsMaintenance = ddlStatus.SelectedValue == "Maintenance",
                RoomContent = txtRoomContent.Text
                
            };

            BUS_Room roomBUS = new BUS_Room();
            roomBUS.UpdateRoom(room);
            string script = "alert('Cập nhật thành công!'); window.location='RoomAlley.aspx';";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
        }
       
    }
}