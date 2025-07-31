using QuanLyDatPhongKS.BUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyDatPhongKS.Admin
{
    public partial class AddBooking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                //Ẩn ngày đã qua 
                txtCheckin.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
                txtCheckout.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
                txtCheckin.Text = DateTime.Today.ToString("yyyy-MM-dd");
                txtCheckout.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
            }
        }

        protected string ConvertToVietnamese(string type)
        {
            switch (type.ToLower())
            {
                case "standard":
                    return "Phòng Thường";
                case "medium":
                    return "Cao cấp";
                case "vip":
                    return "Hạng sang";
                default:
                    return type;
            }
        }
        protected void FilterChanged(object sender, EventArgs e)
        {
            DateTime checkin, checkout;
            if (DateTime.TryParse(txtCheckin.Text, out checkin) && DateTime.TryParse(txtCheckout.Text, out checkout) && checkin < checkout)
            {
                
                BUS_Room room = new BUS_Room();
                var rooms = room.GetRooms(checkin, checkout);
                gvRooms.DataSource = rooms;
                gvRooms.DataBind();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "InvalidDate",
                    "alert('Vui lòng chọn ngày nhận và ngày trả hợp lệ. Ngày trả phải sau ngày nhận');", true);
            }
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            List<int> selectedIds = new List<int>();

            foreach (GridViewRow row in gvRooms.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                if (chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gvRooms.DataKeys[row.RowIndex].Value);
                    selectedIds.Add(id);
                }
            }

            if (selectedIds.Count == 0)
            {
                string script = "alert('Vui lòng chọn ít nhất 1 phòng');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }

            // Chuyển tiếp với danh sách ID và ngày
            string ids = string.Join(",", selectedIds);
            string url = $"AddBookingDetails.aspx?rooms={ids}&checkin={txtCheckin.Text}&checkout={txtCheckout.Text}";
            Response.Redirect(url, false);
        }

    }
}