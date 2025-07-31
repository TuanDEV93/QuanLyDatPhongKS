<%@ Page Title="Quản lý hóa đơn" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Bill.aspx.cs" Inherits="QuanLyDatPhongKS.Admin.Bill" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
       .btn-today {
           font-weight: bold; 
           border: 2px solid blue; 
           padding: 5px 10px; 
           border-radius: 4px; 
           background-color: white; 
           color: black; 
           cursor: pointer; 
       }

       .btn-today:hover {
           background-color: lightblue;
       }
    </style>
    <div id="filterMonthYear" runat="server" style="margin-top: 20px;">
        <fieldset style="border: 1px solid #ccc; padding: 10px; border-radius: 5px;">
            <legend style="font-weight: bold;">Lọc theo ngày, trạng thái, khách hàng</legend>
            <div class="form-group">
                <label for="txtFromDate">Từ ngày:</label>
                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date" />
            </div>

            <div class="form-group">
                <label for="txtToDate">Đến ngày:</label>
                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date" />
            </div>

            <div class="form-group">
                <label for="ddlStatusFilter">Trạng thái:</label>
                <asp:DropDownList ID="ddlStatusFilter" runat="server" CssClass="form-control">
                    <asp:ListItem Text="--Tất cả--" Value="" />
                    <asp:ListItem Text="Đã thanh toán" Value="0" />
                    <asp:ListItem Text="Đã đặt cọc" Value="1" />
                    <asp:ListItem Text="Đang chờ đặt cọc" Value="2" />
                    <asp:ListItem Text="Đã huỷ" Value="3" />
                    <asp:ListItem Text="Đang sử dụng" Value="4" />
                    <asp:ListItem Text="Trả phòng sớm" Value="5" />
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="txtCustomerName">Tên khách hàng:</label>
                <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="txtPhone">Số điện thoại:</label>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" />
            </div>

            <asp:Button ID="btnFilter" runat="server" Text="Duyệt" OnClick="btnFilter_Click" CssClass="btn btn-success" />
            <asp:Button ID="btnReset" runat="server" Text="Tải lại" OnClick="btnReset_Click" CssClass="btn btn-primary" />
        </fieldset>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div class="card-box">
                <div class="table-responsive">
                    <asp:GridView ID="gvBill" runat="server" CssClass="table table-hover agents-mails-checkbox m-0 table-centered table-actions-bar"
                        AutoGenerateColumns="False" OnRowDataBound="gvBill_RowDataBound" DataKeyNames="BillId" OnRowCommand="gvBill_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="BillID" HeaderText="Mã hóa đơn" />
                            <asp:BoundField DataField="CustomerName" HeaderText="Khách hàng" />
                            <asp:BoundField DataField="Phone" HeaderText="SĐT" />
                            <asp:BoundField DataField="BookingDate" HeaderText="Ngày nhận" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="CheckoutDate" HeaderText="Ngày trả" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Deposit" HeaderText="Đặt cọc (VNĐ)" DataFormatString="{0:N0}" />
                            <asp:BoundField DataField="TotalAmount" HeaderText="Tổng tiền (VNĐ)" DataFormatString="{0:N0}" />
                            <asp:BoundField DataField="RemainingAmount" HeaderText="Số tiền còn phải trả (VNĐ)" DataFormatString="{0:N0}" />
                            <asp:TemplateField HeaderText="Trạng thái">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" 
                                        OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                        <asp:ListItem Text="--Chọn--" Value="" />
                                        <asp:ListItem Text="Chờ đặt cọc" Value="2" />
                                        <asp:ListItem Text="Đã đặt cọc" Value="1" />
                                        <asp:ListItem Text="Đã thanh toán" Value="0" />
                                        <asp:ListItem Text="Đang sử dụng" Value="4" />
                                        <asp:ListItem Text="Trả phòng sớm" Value="5" />
                                        <asp:ListItem Text="Bị hủy" Value="3" />
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Chi tiết">
                                <ItemTemplate>
                                    <asp:Button ID="btnViewRooms" runat="server" Text="Xem" CommandName="ViewRooms" CommandArgument='<%# Eval("BillID") %>' CssClass="btn btn-info btn-sm" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <asp:GridView ID="gvRoomDetails" runat="server" CssClass="table table-bordered table-sm mt-2"
                    AutoGenerateColumns="false" OnRowCommand="gvRoomDetails_RowCommand" DataKeyNames="BookingID">
                    <Columns>
                        <asp:BoundField DataField="RoomNumber" HeaderText="Số phòng" />
                        <asp:BoundField DataField="RoomType" HeaderText="Loại phòng" />
                        <asp:BoundField DataField="RoomPrice" HeaderText="Giá" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="BookingDate" HeaderText="Ngày nhận" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="CheckoutDate" HeaderText="Ngày trả" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:TemplateField HeaderText="Hành động">
                            <ItemTemplate>
                                <asp:Button ID="btnCancelRoom" runat="server" Text="Huỷ phòng" CommandName="CancelRoom" 
                                            CommandArgument='<%# Eval("BookingID") %>' CssClass="btn btn-danger btn-sm" 
                                            OnClientClick="return confirm('Bạn có chắc chắn muốn hủy phòng này không?');"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
