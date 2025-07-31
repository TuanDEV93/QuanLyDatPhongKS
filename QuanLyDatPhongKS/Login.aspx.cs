using QuanLyDatPhongKS.BUS;
using QuanLyDatPhongKS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyDatPhongKS
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Session["Username"] != null)
                {
                    Response.Redirect("Home.aspx");
                }
               
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            BUS_Account bus = new BUS_Account();

            int role = bus.DangNhapThanhCong(username, password);

            if (role == 1 || role == 0)
            {
                Session["Username"] = username;
                Session["Role"] = role;

                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "Swal.fire({ icon: 'success', title: 'Đăng nhập thành công!', showConfirmButton: false, timer: 1500 }).then(() => { window.location = 'Admin/RoomAlley.aspx'; });", true);
            }
            else if (role == 2)
            {
                Session["Username"] = username;
                Session["Role"] = role;

                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "Swal.fire({ icon: 'success', title: 'Đăng nhập thành công!', showConfirmButton: false, timer: 1500 }).then(() => { window.location = 'Home.aspx'; });", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "showMessage",
                    "Swal.fire({ icon: 'Failed', title: 'Đăng nhập thất bại! Vui lòng kiểm tra lại tài khoản hoặc mật khẩu', showConfirmButton: false, timer: 1500 }).then(() => { window.location = 'Login.aspx'; });", true);
            }
        }

    }
}