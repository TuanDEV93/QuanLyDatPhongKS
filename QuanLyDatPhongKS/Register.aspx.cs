using QuanLyDatPhongKS.BUS;
using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyDatPhongKS
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                Response.Redirect("Home.aspx");
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BUS_Account bus = new BUS_Account();
                int verifyUsername = bus.TrungTenDangNhap (txtUsername.Text.Trim());
                int verifyPhone = bus.TrungSoDienThoai (txtPhone.Text.Trim());
                int verifyEmail = bus.TrungEmail (txtEmail.Text.Trim());

                if (verifyUsername == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "showMessage",
                    "showNotificationMessage('Tên đăng nhập này đã có người sử dụng !', 'error');", true);
                }
                else if (verifyPhone == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "showMessage",
                    "showNotificationMessage('Số điện thoại này đã có người sử dụng !', 'error');", true);
                }
                else if (verifyEmail == 1) 
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "showMessage",
                    "showNotificationMessage('Email này đã có người sử dụng !', 'error');", true);
                }  
                else
                {
                    CustomerDTO customer = new CustomerDTO();
                    customer.customerName = txtUsername.Text;
                    customer.passWord = txtPassword.Text;
                    customer.displayName = txtFullname.Text;
                    customer.EMAIL = txtEmail.Text;
                    customer.ADDRESS = txtAddress.Text;
                    customer.PHONE = txtPhone.Text;
                    bus.GhiThongTinKhachHang (customer);
                    ClientScript.RegisterStartupScript(this.GetType(), "showMessage",
                    "showNotificationMessage('Đăng kí thành công!', 'success');", true);
                    XoaThongTin();
                    Response.AddHeader("REFRESH", "1;URL=Login.aspx");
                }
            }
        }

        public void XoaThongTin()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtFullname.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
        }
    }
}