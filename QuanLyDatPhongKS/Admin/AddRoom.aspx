<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="AddRoom.aspx.cs" Inherits="QuanLyDatPhongKS.Admin.AddRoom" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <h4 class="header-title mt-0">Thêm Phòng Mới</h4>
                <div class="row">
                    <div class="col-lg-6">
                        <div class="p-4">
                            <div class="form-group">
                                <label for="txtRoomNumber">Số phòng</label>
                                <asp:TextBox ID="txtRoomNumber" runat="server" CssClass="form-control" Placeholder="107,109..."></asp:TextBox>
                                <asp:Label ID="lblRoomNumber" runat="server" CssClass="text-danger"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label for="ddlRoomType">Loại phòng</label>
                                <asp:DropDownList ID="ddlRoomType" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:Label ID="lblRoomType" runat="server" CssClass="text-danger"></asp:Label>
                            </div>
                            <div class="form-group form-check">
                                <asp:CheckBox ID="chkIsMaintenance" runat="server" CssClass="form-check-input" />
                                <label class="form-check-label" for="chkIsMaintenance">Phòng đang bảo trì</label>
                            </div>
                            <div class="text-center mt-3">
                                <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-success" Text="Thêm phòng" OnClick="BtnSubmit_Click" />
                                <asp:Button ID="BtnCancel" runat="server" CssClass="btn btn-danger" Text="Hủy" OnClick="BtnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
