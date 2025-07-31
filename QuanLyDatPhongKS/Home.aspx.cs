using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyDatPhongKS
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Ẩn ngày đã qua 
            txtBookingDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
            txtCheckoutDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
            txtBookingDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtCheckoutDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
        }
        protected void btnCheckRoom_Click(object sender, EventArgs e)
        {
         
            if (string.IsNullOrWhiteSpace(ddlRoomType.SelectedValue) || ddlRoomType.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "RoomTypeAlert",
                    "alert('Vui lòng chọn loại phòng!');", true);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtBookingDate.Text) || string.IsNullOrWhiteSpace(txtCheckoutDate.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "DateAlert",
                    "alert('Vui lòng nhập đầy đủ ngày nhận và ngày trả phòng!');", true);
                return;
            }
            string roomType = ddlRoomType.SelectedValue;
            DateTime bookingDate = DateTime.Parse(txtBookingDate.Text);
            DateTime checkoutDate = DateTime.Parse(txtCheckoutDate.Text);

         
            string url = $"Room.aspx?roomType={roomType}&dateStart={bookingDate:yyyy-MM-dd}&dateEnd={checkoutDate:yyyy-MM-dd}";
            Response.Redirect(url);
        }

    }
}