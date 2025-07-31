<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="AddBooking.aspx.cs" Inherits="QuanLyDatPhongKS.Admin.AddBooking" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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

    <asp:GridView ID="gvRooms" runat="server"
                  CssClass="table table-bordered table-hover"
                  AutoGenerateColumns="False"
                  DataKeyNames="RoomID"
                  EmptyDataText="Không có phòng trống!">
        <Columns>
            
            <asp:TemplateField HeaderStyle-Width="60px" HeaderText="Chọn">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="RoomID"     HeaderText="ID" />
            <asp:BoundField DataField="RoomNumber" HeaderText="Số phòng" />

            <asp:TemplateField HeaderText="Loại phòng">
                <ItemTemplate>
                    <%# ConvertToVietnamese(Eval("TypeRoom").ToString()) %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="PriceRoom"  HeaderText="Giá phòng/Đêm (VNĐ)"
                            DataFormatString="{0:N0}" HtmlEncode="false" />

            <asp:BoundField DataField="RoomContent" HeaderText="Mô tả" />
        </Columns>
    </asp:GridView>
  
    <asp:Button ID="btnConfirm" runat="server"
                CssClass="btn btn-success mt-3"
                Text="Đặt các phòng đã chọn"
                OnClick="btnConfirm_Click" />
</asp:Content>
