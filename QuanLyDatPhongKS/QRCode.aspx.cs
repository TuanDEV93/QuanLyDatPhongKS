using QuanLyDatPhongKS.BUS;
using QuanLyDatPhongKS.DTO;
using System;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.UI;

namespace QuanLyDatPhongKS
{
    public partial class QRCode : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string billId = Request.QueryString["BillID"];
                string deposit = Request.QueryString["Deposit"];
                string billContent = Request.QueryString["BillContent"]; 

                if (string.IsNullOrEmpty(billId) || string.IsNullOrEmpty(deposit))
                {
                    Response.Write("<script>alert('Thiếu thông tin BillID hoặc Deposit!');</script>");
                    return;
                }

                // Nếu không có BillContent thì tạo mới
                if (string.IsNullOrEmpty(billContent))
                {
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    billContent = $"{billId}{timestamp}";

                    // Lưu vào bảng BillHistory
                    BUS_BillHistory bus = new BUS_BillHistory();
                    bus.AddHistory(new BillHistoryDTO
                    {
                        BillID = Convert.ToInt32(billId),
                        BillContent = billContent
                    });
                }

                // Lưu vào ViewState để dùng khi check trạng thái
                ViewState["BillID"] = billId;
                ViewState["Deposit"] = deposit;
                ViewState["BillContent"] = billContent;

                // Gán vào giao diẹn
                lblBillID.Text = billContent;
                lblDeposit.Text = deposit + " VND";

                string bankId = ConfigurationManager.AppSettings["ID_Bank"];
                string stk = ConfigurationManager.AppSettings["STK"];
                string template = ConfigurationManager.AppSettings["Template"];
                string ctk = ConfigurationManager.AppSettings["CTK"];
                //chuỗi lấy từ web VietQR với các tham số tiền, mô tả , và chủ tk
                string qrUrl = $"https://img.vietqr.io/image/{bankId}-{stk}-{template}.png?amount={deposit}&addInfo={billContent}&accountName={ctk}";
                imgQR.ImageUrl = qrUrl;
            }
        }
        //Sử dụng hàm timer để kiểm tra tự động mỗi 1s
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            string billId = ViewState["BillID"]?.ToString();
            string deposit = ViewState["Deposit"]?.ToString();

            string billContent = ViewState["BillContent"]?.ToString();

            if (!string.IsNullOrEmpty(billId) && !string.IsNullOrEmpty(deposit) && !string.IsNullOrEmpty(billContent))
            {
                if (CheckPaymentStatus(billContent, deposit))  
                {
                    BUS_Bill busBill = new BUS_Bill();
                    busBill.UpdateStatus(Convert.ToInt32(billId), 1); // cập nhật trạng thái
                    Response.Write("<script>alert('Thanh toán thành công! Cảm ơn bạn đã đặt phòng tại khách sạn chúng tôi'); window.location='BookingConfirmation.aspx?BillID=" + billId + "';</script>");
                }
            }
        }

        private bool CheckPaymentStatus(string billContent, string deposit)
        {
            try
            {
                string urlAppScriptCheckPayment = $"https://script.google.com/macros/s/AKfycby7-FooKyZ9DEPjMj9DbbcpXX8V2KOyWgh9lVd6tGgfmnnulB-aFf_mZRW-NI-Ks1C9rw/exec?description={billContent}&value={deposit}";
                using (WebClient client = new WebClient())
                {
                    string response = client.DownloadString(urlAppScriptCheckPayment);
                    return response.Trim().ToLower() == "true";
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
