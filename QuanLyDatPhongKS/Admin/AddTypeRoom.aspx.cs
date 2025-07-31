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
    public partial class AddTypeRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int price;
                if (!int.TryParse(txtPriceRoom.Text.Trim(), out price))
                {
                    Response.Write("<script>alert('Giá phòng phải là số nguyên hợp lệ!');</script>");
                    return;
                }
                RoomDTO newRoomType = new RoomDTO();
                newRoomType.TypeRoom = txtRoomType.Text.Trim();
                newRoomType.PriceRoom = price;
                newRoomType.RoomContent = txtRoomContent.Text.Trim();
                

                BUS_Room bus = new BUS_Room();

                // Kiểm tra trùng số phòng trước khi thêm

                bool result = bus.AddRoomType(newRoomType);
                if (result)
                {
                    Response.Write("<script>alert('Thêm loại phòng thành công!');</script>");
                    ClearForm();
                }
                else
                {
                    Response.Write("<script>alert('Thêm loại phòng thất bại!');</script>");
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
            txtRoomType.Text = "";
            txtRoomContent.Text = "";
            txtPriceRoom.Text = "";
            
        }
    }
}