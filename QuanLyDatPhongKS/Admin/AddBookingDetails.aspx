<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="AddBookingDetails.aspx.cs" Inherits="QuanLyDatPhongKS.Admin.AddBookingDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4">Chi Tiết Đặt Phòng</h2>

        <asp:Panel ID="pnlBooking" runat="server" CssClass="card p-4 shadow-sm bg-white rounded">

            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mb-3"></asp:Label>

            <div class="mb-3">
                <asp:Label ID="lblCustomerName" runat="server" Text="Họ và Tên" AssociatedControlID="txtCustomerName" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" Placeholder="Nhập họ tên"></asp:TextBox>
            </div>

            <div class="mb-3">
                <asp:Label ID="lblPhone" runat="server" Text="Số điện thoại" AssociatedControlID="txtPhone" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Placeholder="Nhập số điện thoại"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblTotalAmount" runat="server" Text="Tổng tiền" AssociatedControlID="txtTotalAmount" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="form-control" Placeholder="Nhập số điện thoại"></asp:TextBox>
            </div>         
            <div class="mb-3">
                <asp:Label runat="server" Text="Ngày nhận phòng:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtCheckIn" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>

            <div class="mb-3">
                <asp:Label runat="server" Text="Ngày trả phòng:" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtCheckOut" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="Xác nhận đặt phòng" CssClass="btn btn-success" OnClick="btnSubmit_Click" />
        </asp:Panel>
    </div>
</asp:Content>
