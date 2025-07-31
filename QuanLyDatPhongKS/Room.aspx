<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Room.aspx.cs" Inherits="QuanLyDatPhongKS.Room" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Banner thu gọn -->
    <div class="fh5co-parallax" style="background-image: url(images/slider1.jpg);">
        <div class="overlay"></div>
        <div class="container">
            <div class="row">
                <div class="col-md-12 text-center fh5co-table">
                    <div class="fh5co-intro fh5co-table-cell">
                        <p><span>Danh sách phòng còn trống</span></p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <style>
        .room-grid th, .room-grid td {
            padding: 10px 15px;
            text-align: left;
        }

        .room-grid th {
            background-color: #f2f2f2;
        }

        .room-grid td {
            border-bottom: 1px solid #ddd;
        }

    </style>
    <!-- Bộ lọc loại phòng -->
       <div style="margin-bottom: 20px;">
        <label for="ddlManualFilter"><strong>Lọc loại phòng:</strong></label>
        <asp:DropDownList ID="ddlManualFilter" runat="server" CssClass="form-control">
            <asp:ListItem Text="-- Tất cả --" Value="" />
            <asp:ListItem Text="Phòng thông thường " Value="Standard" />
            <asp:ListItem Text="Phòng cao cấp  " Value="Medium" />
            <asp:ListItem Text="Phòng hạng sang " Value="VIP" />
        </asp:DropDownList>

        <asp:Button ID="btnManualFilter" runat="server" Text="Lọc phòng" 
   OnClientClick="saveSelectedRoomsToHiddenField(); return true;" 
    OnClick="btnManualFilter_Click" />
    </div>

    <!-- Danh sách phòng -->
    <div style="max-width: 1200px; margin: 0 auto; padding: 20px;">
        
        <asp:GridView ID="gvRooms" runat="server" AutoGenerateColumns="false" CssClass="room-grid" style="width: 100%; table-layout: fixed;">
        <Columns>
            <asp:BoundField DataField="RoomNumber" HeaderText="Tên Phòng" />
            <asp:BoundField DataField="TypeRoom" HeaderText="Loại Phòng" />
            <asp:BoundField DataField="BookingDate" HeaderText="Thời gian đặt phòng(trước 14:00 p.m)" DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField DataField="CheckOutDate" HeaderText="Thời gian trả phòng(trước 12:00 p.m)" DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField DataField="PriceRoom" HeaderText="Giá phòng/Đêm" />
            <asp:BoundField DataField="RoomContent" HeaderText="Mô tả phòng" />
            <asp:TemplateField HeaderText="Chọn(Huỷ)">
                <ItemTemplate>
                    <button type="button" class="btn-add-room" onclick="addRoom(this)">Chọn/Huỷ </button>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    <!-- Panel hiện danh sách phòng đã chọn -->
    <div id="selectedRoomsPanel" style="display: none; margin-top: 20px; border: 1px solid #ccc; padding: 15px;">
        <h4>Phòng đã chọn:</h4>
        <ul id="selectedRoomsList"></ul>
        <asp:HiddenField ID="hfSelectedRooms" runat="server" />
        <asp:Button ID="btnProceed" runat="server" Text="Tiếp tục đặt phòng" OnClick="btnProceed_Click" CssClass="btn btn-primary" />
    </div>
    </div>

   <script>
       //tạo 1 biến toàn cục để lưu danh sách phòng đã chọn trước đó
       var selectedRooms = [];

       function addRoom(button) {
           var row = button.closest("tr");
           var cells = row.getElementsByTagName("td");

           var roomNumber = cells[0].innerText.trim();

          
           var existingIndex = selectedRooms.findIndex(r => r.RoomNumber === roomNumber);

           if (existingIndex !== -1) {
               // Đã có trong danh sách, tiến hành huỷ chọn
               selectedRooms.splice(existingIndex, 1);
               alert("Phòng đã được huỷ chọn!");

           } else {
               //  Thêm mới phòng
               var room = {
                   RoomNumber: roomNumber,
                   RoomType: cells[1].innerText.trim(),
                   BookingDate: parseDateFromDDMMYYYY(cells[2].innerText.trim()),
                   CheckoutDate: parseDateFromDDMMYYYY(cells[3].innerText.trim()),
                   RoomPrice: parseInt(cells[4].innerText.trim().replace(/[^0-9]/g, ''))
               };
               selectedRooms.push(room);
               alert("Đã chọn phòng!");
           }

           // Cập nhật lại danh sách hiển thị
           var ul = document.getElementById("selectedRoomsList");
           ul.innerHTML = "";
           selectedRooms.forEach(function (room) {
               const bookingDate = new Date(room.BookingDate);
               const checkoutDate = new Date(room.CheckoutDate);
               const format = d => d.toLocaleDateString('vi-VN');

               var li = document.createElement("li");
               li.innerText = `Phòng ${room.RoomNumber} (${room.RoomType}) - Giá: ${room.RoomPrice.toLocaleString()} - Đặt: ${format(bookingDate)} - Trả: ${format(checkoutDate)}`;
               ul.appendChild(li);
           });

       
           document.getElementById("selectedRoomsPanel").style.display = selectedRooms.length > 0 ? "block" : "none";

           
           document.getElementById("<%= hfSelectedRooms.ClientID %>").value = JSON.stringify(selectedRooms);
       }
       //Hàm chuyển đổi form ngày
       function parseDateFromDDMMYYYY(dateStr) {
           var parts = dateStr.split('/');
           var day = parts[0].padStart(2, '0');
           var month = parts[1].padStart(2, '0');
           var year = parts[2];
           return `${year}-${month}-${day}`; 
       }
       function saveSelectedRoomsToHiddenField() {
           document.getElementById("<%= hfSelectedRooms.ClientID %>").value = JSON.stringify(selectedRooms);
       }
       //window.onload sẽ chạy lại sau postback và đọc dữ liệu từ hfSelectedRooms.
       //khôi phục selectedRooms vào JavaScript và đồng thời render lại các < li > vào danh sách phòng đã chọn.
       window.onload = function () {
           const hfValue = document.getElementById("<%= hfSelectedRooms.ClientID %>").value;
           if (hfValue && hfValue.trim().length > 0) {
               try {
                   selectedRooms = JSON.parse(hfValue);

                   var ul = document.getElementById("selectedRoomsList");
                   ul.innerHTML = ""; // Xóa danh sách cũ trước khi render lại

                   selectedRooms.forEach(function (room) {
                       const bookingDate = new Date(room.BookingDate);
                       const checkoutDate = new Date(room.CheckoutDate);
                       const format = d => d.toLocaleDateString('vi-VN');

                       var li = document.createElement("li");
                       li.innerText = `Phòng ${room.RoomNumber} (${room.RoomType}) - Giá: ${room.RoomPrice.toLocaleString()} - Đặt: ${format(bookingDate)} - Trả: ${format(checkoutDate)}`;
                       ul.appendChild(li);
                   });

                   if (selectedRooms.length > 0) {
                       document.getElementById("selectedRoomsPanel").style.display = "block";
                   }
               } catch (err) {
                   console.warn("Không thể khôi phục danh sách phòng đã chọn: ", err);
               }
           }
       };
   </script>
</asp:Content>
