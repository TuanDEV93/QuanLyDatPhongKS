using QuanLyDatPhongKS.BUS;
using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace QuanLyDatPhongKS.Admin
{
    public partial class AddCustomer : System.Web.UI.Page
    {
        private BUS_Customer customerBUS = new BUS_Customer();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnSubmit1_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các trường nhập liệu
            string displayName = txtDisplayName1.Text.Trim();
            string customerName = txtCustomerName1.Text.Trim();
            string password = txtPassword1.Text.Trim();
            string email = txtEmail1.Text.Trim();
            string phone = txtPhone1.Text.Trim();
            string address = txtAddress1.Text.Trim();

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(customerName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(address))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Vui lòng nhập đầy đủ thông tin bắt buộc.')", true);
                return;
            }

            if (customerBUS.IsCustomerNameExists(customerName))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác!');", true);
                return;
            }

            //Kiểm tra rằng buộc về thông tin khách hàng
            //^: Đánh dấu bắt đầu của chuỗi.
            //0: Ký tự 0 phải xuất hiện ở đầu chuỗi.
            //\d: Đại diện cho một chữ số từ 0 đến 9.
            //{ 9,10}: Xác định số lượng chữ số tiếp theo là từ 9 đến 10.Như vậy, tổng cộng sẽ có từ 10 đến 11 chữ số bao gồm chữ số 0 ở đầu.
            //$: Đánh dấu kết thúc của chuỗi.

            //Kiểm tra tên đăng nhập
            if (!Regex.IsMatch(customerName, @"^[a-zA-Z0-9]{5,18}[a-zA-Z0-9]$"))
            {
                lblMessage1.Text = "Tên đăng nhập không hợp lệ. Vui lòng nhập đúng định dạng (chứa chữ, số , độ dài từ 6-20 ký tự).";
                lblMessage1.ForeColor = System.Drawing.Color.Red;
                lblMessage1.Visible = true;
                return;
            }

            // Kiểm tra mật khẩu
            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"))
            {
                lblMessage2.Text = "Mật khẩu không hợp lệ. Mật khẩu phải chứa ít nhất 8 ký tự, bao gồm chữ cái in hoa, chữ thường, số và ký tự đặc biệt.";
                lblMessage2.ForeColor = System.Drawing.Color.Red;
                lblMessage2.Visible = true;
                return;
            }

            //Kiểm tra sđt
            if (!Regex.IsMatch(phone, @"^0\d{9,10}$"))
            {
                lblMessage5.Text = "Số điện thoại không hợp lệ. Vui lòng nhập số điện thoại có 10-11 chữ số và bắt đầu bằng số 0.";
                lblMessage5.ForeColor = System.Drawing.Color.Red;
                lblMessage5.Visible = true;
                return;
            }
            //Kiểm tra email
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                lblMessage4.Text = "Email không hợp lệ. Vui lòng nhập email đúng định dạng.";
                lblMessage4.ForeColor = System.Drawing.Color.Red;
                lblMessage4.Visible = true; // Show the label with the message
                return;
            }

            // Tạo đối tượng Customer
            CustomerDTO customer = new CustomerDTO
            {
                displayName = displayName,
                customerName = customerName,
                passWord = password,
                EMAIL = email,
                PHONE = phone,
                ADDRESS = address
            };

           
            bool isAdded = customerBUS.AddCustomer(customer);

            if (isAdded)
            {
                
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Thêm khách hàng thành công.'); window.location='Customer.aspx';", true);
            }
            else
            {
               
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Thêm khách hàng thất bại. Vui lòng thử lại.');", true);
            }
        }
        protected void BtnCancel1_Click(object sender, EventArgs e)
        {
            // Chuyển hướng về trang danh sách khách hàng
            Response.Redirect("Customer.aspx");
        }
    }
}
