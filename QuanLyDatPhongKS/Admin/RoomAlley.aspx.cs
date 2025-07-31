using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using QuanLyDatPhongKS.DAO;
using QuanLyDatPhongKS.DTO;

namespace QuanLyDatPhongKS.Admin
{
    public partial class RoomAlley : System.Web.UI.Page
    {
        DAO_Room dao = new DAO_Room();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCheckin.Text = DateTime.Today.ToString("yyyy-MM-dd");
                txtCheckout.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
            }
        }
       protected void FilterChanged(object sender, EventArgs e)
        {
           
           

            if (!DateTime.TryParse(txtCheckin.Text, out DateTime checkin) ||
                !DateTime.TryParse(txtCheckout.Text, out DateTime checkout))
            {
                return;
            }
          
            Session["SelectedCheckinDate"] = checkin;
            Session["SelectedCheckoutDate"] = checkout;
            var rooms = dao.GetAvailableRoomsWithStatus( checkin, checkout);
            rptRoom.DataSource = rooms;
            rptRoom.DataBind();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int roomId = int.Parse(btn.CommandArgument);
            Response.Redirect($"EditRoom.aspx?roomId={roomId}");
        }
        
    }
}
