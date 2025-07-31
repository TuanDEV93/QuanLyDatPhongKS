<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="BookingConfirmation.aspx.cs" Inherits="QuanLyDatPhongKS.BookingConfirmation" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <div class="fh5co-parallax" style="background-image: url(images/slider1.jpg);">
    <div class="overlay"></div>
    <div class="container">
        <div class="row">
            <div class="col-md-12 text-center fh5co-table">
                <div class="fh5co-intro fh5co-table-cell">
                    <p><span>Lịch sử đặt phòng </span></p>
                </div>
            </div>
        </div>
    </div>
</div>
    <style>
        .bookingconfirm-grid {
            width: 100%;
            border-collapse: collapse;
        }

        .bookingconfirm-grid th,
        .bookingconfirm-grid td {
            padding: 10px 15px;
            text-align: left;
        }

        .bookingconfirm-grid th {
            background-color: #f2f2f2;
        }

        .bookingconfirm-grid td {
            border-bottom: 1px solid #ddd;
        }

    </style>
    <div style="width: 100%; overflow-x: auto;">
    <asp:GridView ID="GridView1" runat="server" CssClass="bookingconfirm-grid" 
    AutoGenerateColumns="false" EmptyDataText="Không có lịch sử đặt phòng."
    Width="100%" OnRowCommand="GridView1_RowCommand">
        <Columns>
            <asp:BoundField DataField="BillID" HeaderText="Mã hoá đơn" />
            <asp:BoundField DataField="BookingDate" HeaderText="Ngày đặt" DataFormatString="{0:MM/dd/yyyy}" />
            <asp:BoundField DataField="CheckoutDate" HeaderText="Ngày trả" DataFormatString="{0:MM/dd/yyyy}" />
            <asp:BoundField DataField="RoomNumber" HeaderText="Số phòng" />
            <asp:BoundField DataField="TotalAmount" HeaderText="Tổng tiền thanh toán" />
            <asp:BoundField DataField="DepositPrice" HeaderText="Tiền cọc" />
            <asp:BoundField DataField="Status" HeaderText="Trạng thái" />
             <asp:TemplateField HeaderText="">
            <ItemTemplate>
                <asp:Button ID="btnContinuePayment" runat="server" Text="Tiếp tục thanh toán" 
                    CommandName="ContinuePayment" 
                    CommandArgument='<%# Eval("BillID") %>' 
                    Visible='<%# Eval("Status").ToString() == "Đang chờ đặt cọc" %>' 
                    CssClass="btn btn-primary btn-sm" />
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

</asp:Content>
