using QuanLyDatPhongKS.BUS;
using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace QuanLyDatPhongKS.Admin
{
    public partial class Customer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCustomerData();
            }
        }

        private BUS_Customer customerBUS = new BUS_Customer();
        private void LoadCustomerData()
        {
            BUS_Customer busCustomer = new BUS_Customer();
            gvCustomer1.DataSource = busCustomer.GetCustomer();
            gvCustomer1.DataBind();
        }
        protected void gvCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int customerId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditCustomer")
            {
                // Lưu CustomerID vào HiddenField để sửa
                hfEditCustomerIDPanel1.Value = customerId.ToString();

                // Lấy dữ liệu từ BUS và gán vào Panel
                BUS_Customer busCustomer = new BUS_Customer();
                CustomerDTO customer = busCustomer.GetCustomerById(customerId);

                if (customer != null)
                {
                    txtEditDisplayNamePanel1.Text = customer.displayName;
                    txtEditEmailPanel1.Text = customer.EMAIL;
                    txtEditAddressPanel1.Text = customer.ADDRESS;
                    txtEditPhonePanel1.Text = customer.PHONE;

                    // Hiển thị Panel
                    EditPanel1.Visible = true;
                }
            }
            else if (e.CommandName == "DeleteCustomer")
            {
                // Xử lý xóa khách hàng
                BUS_Customer busCustomer = new BUS_Customer();

                busCustomer.DeleteCustomer(customerId);
                LoadCustomerData(); // Load lại dữ liệu sau khi xóa
            }
        }
        protected void gvCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                e.Row.Cells[0].Text = "KH-" + e.Row.Cells[0].Text; 
            }
        }
        protected void gvCustomer1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomer1.PageIndex = e.NewPageIndex;
            LoadCustomerData();
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            BUS_Customer customerBus = new BUS_Customer();
            string displayName = SearchInput.Value; // Lấy giá trị từ input
            List<CustomerDTO> customer = customerBus.SearchCustomerByDisplayName(displayName);

            
            if (customer.Count > 0)
            {
                gvCustomer1.DataSource = customer;
                gvCustomer1.DataBind();
            }
            else
            {
                lblMessage.Text = "Không tìm thấy khách hàng nào!";
            }
        }
        protected void btnSavePanel1_Click(object sender, EventArgs e)
        {
            try
            {
                int customerId = Convert.ToInt32(hfEditCustomerIDPanel1.Value);

                // Lấy dữ liệu từ các TextBox
                string displayName = txtEditDisplayNamePanel1.Text;
                string email = txtEditEmailPanel1.Text;
                string address = txtEditAddressPanel1.Text;
                string phone = txtEditPhonePanel1.Text;

                // Tạo đối tượng DTO và gọi BUS để cập nhật
                CustomerDTO customer = new CustomerDTO
                {
                    customerID = customerId,
                    displayName = displayName,
                    EMAIL = email,
                    ADDRESS = address,
                    PHONE = phone
                };

                BUS_Customer busCustomer = new BUS_Customer();
                busCustomer.UpdateCustomer(customer);

                //Kiểm tra thông tin đầu vào
                if (string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(address))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Vui lòng nhập đầy đủ thông tin bắt buộc.')", true);
                    return;
                }

                if (!Regex.IsMatch(phone, @"^0\d{9,10}$"))
                {
                    lblMessage5.Text = "Số điện thoại không hợp lệ. Vui lòng nhập số điện thoại có 10-11 chữ số và bắt đầu bằng số 0.";
                    lblMessage5.ForeColor = System.Drawing.Color.Red;
                    lblMessage5.Visible = true;
                    return;
                }

                if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                {
                    lblMessage4.Text = "Email không hợp lệ. Vui lòng nhập email đúng định dạng.";
                    lblMessage4.ForeColor = System.Drawing.Color.Red;
                    lblMessage4.Visible = true; // Show the label with the message
                    return;
                }

                // Reload dữ liệu và ẩn Panel
                LoadCustomerData();
                EditPanel1.Visible = false;

                Response.Write("<script>alert('Cập nhật thành công!');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Có lỗi xảy ra: " + ex.Message + "');</script>");
            }
        }

        protected void btnCancelPanel1_Click(object sender, EventArgs e)
        {
            // Ẩn Panel khi nhấn Hủy
            EditPanel1.Visible = false;
        }
    }
}