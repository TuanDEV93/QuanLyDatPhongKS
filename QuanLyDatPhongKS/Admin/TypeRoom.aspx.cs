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
    public partial class TypeRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTypeRoomData();
            }
        }

        private void LoadTypeRoomData()
        {
            BUS_Room busRoom = new BUS_Room();
            gvTypeRoom.DataSource = busRoom.GetAllRoomTypes(); 
            gvTypeRoom.DataBind();
        }

        protected void gvTypeRoom_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int typeRoomId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditRoomType")
            {
                hfEditTypeRoomID.Value = typeRoomId.ToString();

                BUS_Room busRoom = new BUS_Room();
                RoomDTO room = busRoom.GetTypeRoomById(typeRoomId);

                if (room != null)
                {
                    txtEditTypeRoomPanel.Text = room.TypeRoom;
                    txtEditPriceRoomPanel.Text = room.PriceRoom.ToString();
                    txtEditRoomContentPanel.Text = room.RoomContent;
                    EditPanel.Visible = true;
                }
            }
            else if (e.CommandName == "DeleteRoomType")
            {
                BUS_Room busRoom = new BUS_Room();
                busRoom.DeleteRoomType(typeRoomId); 
                LoadTypeRoomData();
            }
        }
        protected void gvTypeRoom_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = "TR-" + e.Row.Cells[0].Text; // Thêm tiền tố "TR-" vào mã nhân viên
            }
        }
        protected void btnSavePanel_Click(object sender, EventArgs e)
        {
            try
            {
                int typeRoomId = Convert.ToInt32(hfEditTypeRoomID.Value);

                string typeName = txtEditTypeRoomPanel.Text;
                string priceText = txtEditPriceRoomPanel.Text;
                string content = txtEditRoomContentPanel.Text;

                if (string.IsNullOrEmpty(typeName) || string.IsNullOrEmpty(priceText) || string.IsNullOrEmpty(content))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Vui lòng nhập đầy đủ thông tin.');", true);
                    return;
                }

                RoomDTO room = new RoomDTO
                {
                    RoomTypeId = typeRoomId,
                    TypeRoom = typeName,
                    PriceRoom = int.Parse(priceText),
                    RoomContent = content
                };

                BUS_Room busRoom = new BUS_Room();
                busRoom.UpdateRoomType(room); 

                LoadTypeRoomData();
                EditPanel.Visible = false;

                Response.Write("<script>alert('Cập nhật thành công!');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Có lỗi xảy ra: " + ex.Message + "');</script>");
            }
        }


        protected void btnCancelPanel_Click(object sender, EventArgs e)
        {
            // Ẩn Panel khi nhấn Hủy
            EditPanel.Visible = false;
        }
    }
}