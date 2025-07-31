using QuanLyDatPhongKS.BUS;
using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Optimization;

namespace QuanLyDatPhongKS
{
    public partial class Room : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                if (Request.QueryString["roomType"] != null)
                {
                    try
                    {
                        string roomType = Request.QueryString["roomType"];
                        DateTime checkin = DateTime.Parse(Request.QueryString["dateStart"]).Date;
                        DateTime checkout = DateTime.Parse(Request.QueryString["dateEnd"]).Date;

                        BUS_Room busRoom = new BUS_Room();
                        List<RoomDTO> availableRooms = busRoom.GetAvailableRooms(roomType, checkin, checkout);

                        foreach (var room in availableRooms)
                        {
                            room.BookingDate = checkin;
                            room.CheckOutDate = checkout;
                        }

                        gvRooms.DataSource = availableRooms;
                        gvRooms.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Lỗi: " + ex.Message + "');</script>");
                    }
                }
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(hfSelectedRooms.Value))
            {
                ViewState["SelectedRooms"] = hfSelectedRooms.Value;
            }

            // Nếu hf rỗng, thì mới khôi phục từ ViewState 
            if (string.IsNullOrEmpty(hfSelectedRooms.Value) && ViewState["SelectedRooms"] != null)
            {
                hfSelectedRooms.Value = ViewState["SelectedRooms"].ToString();
            }

            // Luôn đảm bảo panel hiển thị nếu có dữ liệu
            if (!string.IsNullOrEmpty(hfSelectedRooms.Value))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "restorePanel", "document.getElementById('selectedRoomsPanel').style.display = 'block';", true);
            }
        }
    

    protected void btnManualFilter_Click(object sender, EventArgs e)
        {
            string selectedRoomType = ddlManualFilter.SelectedValue;

            DateTime checkin = DateTime.Parse(Request.QueryString["dateStart"]).Date;
            DateTime checkout = DateTime.Parse(Request.QueryString["dateEnd"]).Date;

            BUS_Room busRoom = new BUS_Room();
            List<RoomDTO> filtered = busRoom.GetAvailableRooms(selectedRoomType, checkin, checkout);

            foreach (var room in filtered)
            {
                room.BookingDate = checkin;
                room.CheckOutDate = checkout;
            }

            //  Lấy lại danh sách đã chọn trước đó
            List<BookingDTO> selectedRooms = new List<BookingDTO>();
            if (!string.IsNullOrEmpty(hfSelectedRooms.Value))
            {
                selectedRooms = JsonConvert.DeserializeObject<List<BookingDTO>>(hfSelectedRooms.Value);
            }

            // ❗ Loại bỏ các phòng đã chọn trước đó ra khỏi filtered 
            var filteredExcludingSelected = filtered
                .Where(r => !selectedRooms.Any(s => s.RoomNumber == r.RoomNumber))
                .ToList();

            gvRooms.DataSource = filteredExcludingSelected;
            gvRooms.DataBind();

            // Khôi phục lại hidden field
            if (ViewState["SelectedRooms"] != null)
            {
                hfSelectedRooms.Value = ViewState["SelectedRooms"].ToString();
            }

            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "restorePanel", "document.getElementById('selectedRoomsPanel').style.display = 'block';", true);
        }


        //Lưu thông tin vào session
        protected void btnProceed_Click(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                
                string script = "alert('Bạn cần đăng nhập để tiếp tục!'); window.location='Login.aspx';";
                ClientScript.RegisterStartupScript(this.GetType(), "LoginRequiredAlert", script, true);
                return;
            }    
            try
            {
                var json = hfSelectedRooms.Value;
                if (!string.IsNullOrEmpty(json))
                {
                    var rooms = JsonConvert.DeserializeObject<List<BookingDTO>>(json);
                    Session["SelectedRooms"] = rooms;
                    Response.Redirect("Booking.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "noData", "console.warn('Không có dữ liệu để lưu');", true);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "jsonError", $"console.error('Lỗi khi xử lý JSON: {ex.Message}');", true);
            }
        }



    }
}
