using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyDatPhongKS
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] != null)
                {
                    lblUserName.Text = "Hello, " + Session["Username"].ToString();
                    btnLogin.Visible = false;  // Ẩn nút đăng nhập
                    btnLogout.Visible = true;  // Hiển thị nút đăng xuất
                }
                else
                {
                    lblUserName.Text = "";
                    btnLogin.Visible = true;  // Hiển thị nút đăng nhập
                    btnLogout.Visible = false; // Ẩn nút đăng xuất
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Home.aspx");
        }
    }
}