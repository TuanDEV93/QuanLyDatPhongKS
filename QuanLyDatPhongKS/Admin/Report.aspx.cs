using QuanLyDatPhongKS.BUS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLyDatPhongKS.Admin
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            //ngăn cuộn lên đầu trang
            Page.MaintainScrollPositionOnPostBack = true;
        }

        public void LoadTopKhachHang()
        {
            int selectedMonth = int.Parse(ddlMonthCustomer.SelectedValue);
            int selectedYear = int.Parse(ddlYearCustomer.SelectedValue);

            BUS_Bill busBill = new BUS_Bill();
            DataTable dt = busBill.LoadTopKhachHang(selectedMonth, selectedYear);

            if (dt != null && dt.Rows.Count > 0)
            {
                gvTop.DataSource = dt;
                gvTop.DataBind();
            }
            else
            {
                gvTop.DataSource = null;
                gvTop.DataBind();

            }
        }

        protected void btnDuyet_Click(object sender, EventArgs e)
        {
            LoadTopKhachHang();
        }


        public void LoadTopSan()
        {
            int selectedMonth = int.Parse(ddlMonthLane.SelectedValue);
            int selectedYear = int.Parse(ddlYearLane.SelectedValue);

            BUS_Bill busBill = new BUS_Bill();
            DataTable dt = busBill.LoadTopRoom(selectedMonth, selectedYear);

            if (dt != null && dt.Rows.Count > 0)
            {
                gvLane.DataSource = dt;
                gvLane.DataBind();
            }
            else
            {
                gvLane.DataSource = null;
                gvLane.DataBind();

            }
        }
        protected void btnDuyetSan_Click(object sender, EventArgs e)
        {
            LoadTopSan();
        }

        public void LoadDoanhThuTheoKhoangThoiGian()
        {
            DateTime startDate = DateTime.Parse(txtNgayBatDau.Value);
            DateTime endDate = DateTime.Parse(txtNgayKetThuc.Value);

            BUS_Bill busBill = new BUS_Bill();
            DataTable dt = busBill.LoadDoanhThuTheoKhoangThoiGian(startDate, endDate);

            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("TimeRange", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    row["TimeRange"] = $"{startDate:dd/MM/yyyy} - {endDate:dd/MM/yyyy}";
                }

                GridViewRevenue.DataSource = dt;
                GridViewRevenue.DataBind();
            }
            else
            {
                GridViewRevenue.DataSource = null;
                GridViewRevenue.DataBind();
            }
        }

        protected void btnThongKe_Click (object sender, EventArgs e)
        {
            LoadDoanhThuTheoKhoangThoiGian();
        }
        public void LoadTopCanceller()
        {
            int selectedMonth = int.Parse(ddlMonthCancel.SelectedValue);
            int selectedYear = int.Parse(ddlYearCancel.SelectedValue);

            BUS_Bill busBill = new BUS_Bill();
            DataTable topCancellers = busBill.LoadTopCanceller(selectedMonth, selectedYear);

            if (topCancellers != null && topCancellers.Rows.Count > 0)
            {
                gvHuySan.DataSource = topCancellers;
                gvHuySan.DataBind();
            }
            else
            {
                gvHuySan.DataSource = null;
                gvHuySan.DataBind();

            }

        }
        protected void btnDuyetHuy_Click(object sender, EventArgs e)
        {
            LoadTopCanceller();
        }
    }
}