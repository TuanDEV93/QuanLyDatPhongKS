using QuanLyDatPhongKS.BUS;
using QuanLyDatPhongKS.DAO;
using QuanLyDatPhongKS.DTO;
using System;
using System.Security.Policy;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyDatPhongKS.Admin
{
    public partial class Bill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DAO_Bill daoBill = new DAO_Bill();
                daoBill.AutoCancelLateBills();
                LoadBillData();
            }
        }

        private void LoadBillData()
        {
            BUS_Bill billBus = new BUS_Bill();
            gvBill.DataSource = billBus.GetBills();
            gvBill.DataBind();
            gvRoomDetails.Visible = false;
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlStatus = (DropDownList)sender;
            //Namingcontainer sẽ giúp cho biết đòng trạng thái dó được chọn là ở dòng nào trong gv
            GridViewRow row = (GridViewRow)ddlStatus.NamingContainer;

            int billID = Convert.ToInt32(gvBill.DataKeys[row.RowIndex].Value);
            string selectedStatus = ddlStatus.SelectedValue;

            BUS_Bill billBus = new BUS_Bill();
            BUS_Booking bookingBus = new BUS_Booking();

            bool result = billBus.UpdateBillStatus(billID, selectedStatus);

            if (result)
            {
                if (selectedStatus == "5") 
                {
                    DateTime today = DateTime.Today;

                    // 1. Cập nhật ngày trả mới
                    bookingBus.UpdateCheckoutDateByBillID(billID, today);

                    // 2. Tính lại tổng tiền từ danh sách Booking
                    int newTotal = bookingBus.GetTotalAmountByBillID(billID);

                    // 3. Cập nhật tổng tiền vào bảng Bill
                    billBus.UpdateTotalAmount(billID, newTotal);
                }

                LoadBillData();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "UpdateError", "alert('Cập nhật trạng thái thất bại!');", true);
            }
        }


        protected void gvBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlStatus = e.Row.FindControl("ddlStatus") as DropDownList;
                if (ddlStatus != null)
                {
                    string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
                    ddlStatus.SelectedValue = status;
                }
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(txtFromDate.Text, out DateTime fromDate) ||
                !DateTime.TryParse(txtToDate.Text, out DateTime toDate))
            {
                return;
            }
            string customerName = txtCustomerName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            int? status = null;
            if (!string.IsNullOrEmpty(ddlStatusFilter.SelectedValue))
            {
                status = int.Parse(ddlStatusFilter.SelectedValue);
            }

            BUS_Bill billBus = new BUS_Bill();
            var filteredBills = billBus.GetBillsByFilter(fromDate, toDate, status, customerName, phone);
            gvBill.DataSource = filteredBills;
            gvBill.DataBind();
            gvRoomDetails.Visible = false;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtCustomerName.Text = "";
            txtPhone.Text = "";
            ddlStatusFilter.SelectedIndex = 0;
            LoadBillData();
        }

        protected void gvBill_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewRooms")
            {
                int billId = Convert.ToInt32(e.CommandArgument);
                ViewState["CurrentBillID"] = billId;

                BUS_Booking bus = new BUS_Booking();
                var list = bus.GetRoomDetailsByBillID(billId);
                gvRoomDetails.DataSource = list;
                gvRoomDetails.DataBind();
                gvRoomDetails.Visible = true;
            }
        }

        protected void gvRoomDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CancelRoom")
            {
                int bookingId = Convert.ToInt32(e.CommandArgument);
                BUS_Booking bookingBus = new BUS_Booking();

                bool result = bookingBus.DeleteBookingByID(bookingId);

                if (result)
                {
                    int billId = Convert.ToInt32(ViewState["CurrentBillID"]);

                    // Tính lại tổng tiền sau khi xoá booking
                    int newTotal = bookingBus.GetTotalAmountByBillID(billId);
                    new BUS_Bill().UpdateTotalAmount(billId, newTotal);

                    // Load lại chi tiết 
                    gvRoomDetails.DataSource = bookingBus.GetRoomDetailsByBillID(billId);
                    gvRoomDetails.DataBind();
                    LoadBillData();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "DeleteError", "alert('Hủy phòng thất bại!');", true);
                }
            }
        }
    }
}
