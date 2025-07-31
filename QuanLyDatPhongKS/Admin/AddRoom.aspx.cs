using QuanLyDatPhongKS.BUS;
using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyDatPhongKS.Admin
{
    public partial class AddRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlRoomType.Items.Add(new ListItem("Standard", "1"));
                ddlRoomType.Items.Add(new ListItem("Medium", "2"));
                ddlRoomType.Items.Add(new ListItem("Vip", "3"));
            }
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                RoomDTO newRoom = new RoomDTO();
                newRoom.RoomNumber = txtRoomNumber.Text.Trim();
                newRoom.RoomTypeId = int.Parse(ddlRoomType.SelectedValue);
                newRoom.IsMaintenance = false;

                BUS_Room bus = new BUS_Room();

                // Kiểm tra trùng số phòng trước khi thêm

                bool result = bus.AddRoom(newRoom);
                if (result)
                {
                    Response.Write("<script>alert('Thêm phòng thành công!');</script>");
                    ClearForm();
                }
                else
                {
                    Response.Write("<script>alert('Thêm phòng thất bại!');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Lỗi: " + ex.Message + "');</script>");
            }
        }
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtRoomNumber.Text = "";
            ddlRoomType.SelectedIndex = 0;
        }

    }
}