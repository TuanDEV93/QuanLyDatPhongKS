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
    public partial class Staff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStaffData();
            }
        }

        private void LoadStaffData()
        {
            BUS_Staff busStaff = new BUS_Staff();
            gvStaff.DataSource = busStaff.GetStaffByRole();
            gvStaff.DataBind();
        }
        protected void gvStaff_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int userId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditStaff")
            {
                // Lưu UserID vào HiddenField để sửa
                hfEditUserIDPanel.Value = userId.ToString();

                // Lấy dữ liệu từ BUS và gán vào Panel
                BUS_Staff busStaff = new BUS_Staff();
                StaffDTO staff = busStaff.GetStaffById(userId);

                if (staff != null)
                {
                    txtEditDisplayNamePanel.Text = staff.DisplayName;
                    txtEditAddressPanel.Text = staff.Address;
                    txtEditPhonePanel.Text = staff.Phone;

                  
                    EditPanel.Visible = true;
                }
            }
            else if (e.CommandName == "DeleteStaff")
            {
                
                BUS_Staff busStaff = new BUS_Staff();
                
                busStaff.DeleteStaff(userId);
                LoadStaffData(); 
            }
        }
        protected void gvStaff_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {    
                    e.Row.Cells[0].Text = "NV-" + e.Row.Cells[0].Text; 
                }
            }
        protected void btnSavePanel_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = Convert.ToInt32(hfEditUserIDPanel.Value);

                // Lấy dữ liệu từ các TextBox
                string displayName = txtEditDisplayNamePanel.Text;
                string address = txtEditAddressPanel.Text;
                string phone = txtEditPhonePanel.Text;

                // Tạo đối tượng DTO và gọi BUS để cập nhật
                StaffDTO staff = new StaffDTO
                {
                    UserID = userId,
                    DisplayName = displayName,
                    Address = address,
                    Phone = phone
                };
                if (string.IsNullOrEmpty(displayName) ||  string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(address))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Vui lòng nhập đầy đủ thông tin bắt buộc.');", true);
                    return;
                }
                BUS_Staff busStaff = new BUS_Staff();
                busStaff.UpdateStaff(staff);

                // Reload dữ liệu và ẩn Panel
                LoadStaffData();
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