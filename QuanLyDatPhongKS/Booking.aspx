<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="QuanLyDatPhongKS.Booking" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="fh5co-parallax" style="background-image: url(images/slider1.jpg);">
    <div class="overlay"></div>
    <div class="container">
        <div class="row">
            <div class="col-md-12 text-center fh5co-table">
                <div class="fh5co-intro fh5co-table-cell">
                </div>
            </div>
        </div>
    </div>
</div>
<style>
    .form-container {
        max-width: 960px;
        margin: 40px auto;
        padding: 30px;
        background: #fff;
        border-radius: 10px;
        box-shadow: 0 0 10px rgba(0,0,0,0.1);
    }

    .form-container h2 {
        text-align: center;
        margin-bottom: 30px;
    }

    .form-grid {
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 20px 40px;
    width: 100%;
}

    .form-group {
        display: flex;
        flex-direction: column;
    }

    label {
        font-weight: bold;
        margin-bottom: 5px;
    }

       .form-control {
        padding: 10px;
        border: 1px solid #ccc;
        border-radius: 6px;
        width: 100%;
        box-sizing: border-box;
    }
       
        .full-width {
            grid-column: 1 / -1;
        }

      
        .full-textbox {
            resize: none;
            min-height: 120px;
        }
    .form-control[readonly] {
        background-color: #f0f0f0;
    }

    .btn-submit {
        grid-column: span 2;
        justify-self: center; 
        padding: 12px 24px;
        background-color: #28a745;
        border: none;
        color: white;
        font-size: 16px;
        border-radius: 6px;
        cursor: pointer;
        margin-right: 80px;
    }


    .btn-submit:hover {
        background-color: #218838;
    }

    @media (max-width: 768px) {
        .form-grid {
            grid-template-columns: 1fr;
        }

        .btn-submit {
            grid-column: span 1;
        }
    }
</style>

<div class="form-container">
    <h2>Xác Nhận Đơn Đặt Phòng</h2>
    <h3>Lưu ý: Sau khi xác nhận đơn. Bạn có thời gian là hết ngày hôm nay để thanh toán tiền đặt cọc nếu không sẽ bị huỷ đơn</h3>
    <div class="form-grid">
        <div class="form-group">
            <label for="txtBookingDate">Ngày đặt(Trước 14:00 pm):</label>
            <asp:TextBox ID="txtBookingDate" runat="server" CssClass="form-control"  />
        </div>

        <div class="form-group">
            <label for="txtCheckoutDate">Ngày trả(Trước 12:00 pm):</label>
            <asp:TextBox ID="txtCheckoutDate" runat="server" CssClass="form-control"  />
        </div>

        <div class="form-group full-width">
            <label for="txtRoomDetails">Chi tiết phòng đặt:</label>
            <asp:TextBox ID="txtRoomDetails" runat="server" CssClass="form-control full-textbox"
                TextMode="MultiLine" Rows="5" ReadOnly="true" />
        </div>
        <div class="form-group">
            <label for="txtTotalAmount">Tổng tiền thanh toán:</label>
            <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="form-control" ReadOnly="true" />
        </div>

        <div class="form-group">
            <label for="txtDepositAmount">Số tiền cọc (20%):</label>
            <asp:TextBox ID="txtDepositAmount" runat="server" CssClass="form-control" ReadOnly="true" />
        </div>

        <asp:Button ID="btnConfirmBooking" runat="server" Text="Xác nhận đặt phòng" CssClass="btn-submit" OnClick="btnConfirmBooking_Click" />
    </div>
</div>


</asp:Content>