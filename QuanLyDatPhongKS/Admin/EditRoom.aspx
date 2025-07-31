<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="EditRoom.aspx.cs" Inherits="QuanLyDatPhongKS.Admin.EditRoom" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
    .form-control {
        width: 100%;
        margin-bottom: 10px;
        padding: 8px;
    }
    .btn {
        margin-right: 10px;
        margin-top: 10px;
    }
    .customer-panel {
        margin-top: 20px;
        border-top: 1px solid #ccc;
        padding-top: 15px;
    }
    </style>
    <h2>Chỉnh sửa thông tin phòng</h2>
    <asp:Panel ID="pnlRoomInfo" runat="server" CssClass="form-container">
        <asp:Label ID="lblRoomNumber" runat="server" Text="Số phòng:" AssociatedControlID="txtRoomNumber" />
        <asp:TextBox ID="txtRoomNumber" runat="server" CssClass="form-control" />

        <asp:Label ID="lblStatus" runat="server" Text="Trạng thái phòng:" AssociatedControlID="ddlStatus" />
        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
            <asp:ListItem Text="Trống" Value="Empty" />
            <asp:ListItem Text="Đã Đặt" Value="Booked" />
            <asp:ListItem Text="Bảo trì" Value="Maintenance" />
        </asp:DropDownList>
        <asp:Label ID="lblBookedDates" runat="server" Text="Các ngày đã được đặt:" />
        <br />
        <asp:Literal ID="litBookedDates" runat="server" />
        <br />
        <asp:Label ID="lblPrice" runat="server" Text="Giá phòng:" AssociatedControlID="txtRoomPrice" />
        <asp:TextBox ID="txtRoomPrice" runat="server" CssClass="form-control" ReadOnly="true"/>

        <asp:Label ID="lblRoomType" runat="server" Text="Loại phòng:" AssociatedControlID="txtRoomType" />
        <asp:TextBox ID="txtRoomType" runat="server" CssClass="form-control" ReadOnly="true"/>

        <asp:Label ID="lblRoomContent" runat="server" Text="Mô tả phòng:" AssociatedControlID="txtRoomContent" />
        <asp:TextBox ID="txtRoomContent" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" />

        <asp:Button ID="btnSave" runat="server" Text="Lưu" OnClick="btnSave_Click" CssClass="btn btn-primary" />
    </asp:Panel>

    
</asp:Content>
