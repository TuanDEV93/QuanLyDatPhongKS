using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyDatPhongKS.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Role"] != null && Convert.ToInt32(Session["Role"]) == 0)
                {
                    DisableAdminButtons();
                 
                }
            }
        }

        private void DisableAdminButtons()
        {
            
            aStaff.Attributes["class"] += " disabled";
            aSubMenuStaff.Attributes["class"] += "disabled";
            aAddSubMenuStaff.Attributes["class"] += "disabled";
            aCustomer.Attributes["class"] += " disabled";
            aReport.Attributes["class"] += " disabled";
            aSubMenuReport.Attributes["class"] += " disabled";
            aBill.Attributes["class"] += "disabled";
        }
        
    }
}