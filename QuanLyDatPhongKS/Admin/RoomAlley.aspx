<%@ Page Title="Quản lý phòng" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="RoomAlley.aspx.cs" Inherits="QuanLyDatPhongKS.Admin.RoomAlley" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .room-card {
        border: 1px solid #ddd;
        border-radius: 10px;
        overflow: hidden;
        background-color: #fff;
        box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        margin-bottom: 20px;
    }

    .room-header {
        height: 60px;
        display: flex;
        justify-content: flex-end;
        align-items: center;
        padding: 0 10px;
        color: white;
        font-weight: bold;
        position: relative;
    }

    .room-header .status-label {
        background-color: #ffc107;
        color: #fff;
        font-size: 12px;
        padding: 3px 8px;
        border-radius: 3px;
        position: absolute;
        right: 10px;
        top: 10px;
    }

    .status-available {
        background-color: #28a745;
    }

    .status-occupied {
        background-color: #dc3545;
    }

    .status-maintenance {
        background-color: #ffc107;
        color: #000;
    }

    .room-body {
        padding: 20px;
        text-align: center;
    }

    .room-body .info-title {
        font-size: 22px;
        font-weight: bold;
        margin: 5px 0;
    }

    .room-body .info-label {
        font-size: 14px;
        color: #888;
        margin-bottom: 15px;
    }

    .room-body .room-status {
        margin-top: 10px;
        font-size: 16px;
    }

    .room-body .room-status span:first-child {
        font-weight: bold;
        margin-right: 5px;
    }

    .btn-sm {
        margin: 5px;
    }
</style>
    <div class="mb-3">
    <a href="AddRoom.aspx" class="btn btn-success">
         Thêm phòng
    </a>
</div>
<div class="row mb-4">
    <div class="col-md-3">
        <label>Ngày nhận</label>
        <asp:TextBox ID="txtCheckin" runat="server" CssClass="form-control" TextMode="Date" />
    </div>
    <div class="col-md-3">
        <label>Ngày trả</label>
        <asp:TextBox ID="txtCheckout" runat="server" CssClass="form-control" TextMode="Date" />
    </div>
    <div class="col-md-3 align-self-end">
        <asp:Button ID="btnFilter" runat="server" CssClass="btn btn-primary" Text="Lọc phòng" OnClick="FilterChanged" />
    </div>

</div>

<div class="row">
    <asp:Repeater ID="rptRoom" runat="server">
        <ItemTemplate>
            <div class="col-lg-4 col-sm-6">
                <div class="room-card">
                    <!-- Thanh trạng thái màu -->
                    <div class='room-header <%# Eval("Status").ToString() == "Trống" ? "status-available" : Eval("Status").ToString() == "Bảo trì" ? "status-maintenance" : "status-occupied" %>'>
                        <div class="status-label">TRẠNG THÁI</div>
                    </div>

                    <!-- Nội dung chính -->
                    <div class="room-body">
                        <div class="info-label">Mã phòng</div>
                        <div class="info-title"><%# Eval("RoomNumber") %></div>
                        <div class="room-status">
                            <span>Trạng thái:</span>
                            <span><%# Eval("Status") %></span>
                        </div>

                        <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-warning btn-sm"
                            Text="Xem" CommandArgument='<%# Eval("RoomID") %>'
                            OnClick="btnEdit_Click" />
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
</asp:Content>
