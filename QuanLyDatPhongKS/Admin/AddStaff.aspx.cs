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
    public partial class AddStaff : System.Web.UI.Page
    {
        private BUS_Staff staffBUS = new BUS_Staff();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các trường nhập liệu
            string displayName = txtDisplayName.Text.Trim();
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(address))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Vui lòng nhập đầy đủ thông tin bắt buộc.');", true);
                return;
            }
            if (staffBUS.IsUserNameExists(userName))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác!');", true);
                return;
            }
           
            StaffDTO staff = new StaffDTO
            {
                DisplayName = displayName,
                UserName = userName,
                Password = password,
                Phone = phone,
                Address = address
            };

            
            bool isAdded = staffBUS.AddStaff(staff);

            if (isAdded)
            {
                // Thông báo thành công
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Thêm nhân viên thành công.'); window.location='Staff.aspx';", true);
            }
            else
            {
                // Thông báo thất bại
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Thêm nhân viên thất bại. Vui lòng thử lại.');", true);
            }
        }
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            // Chuyển hướng về trang danh sách nhân viên
            Response.Redirect("Staff.aspx");
        }
    }
}