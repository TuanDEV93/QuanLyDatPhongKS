<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="AddTypeRoom.aspx.cs" Inherits="QuanLyDatPhongKS.Admin.AddTypeRoom" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <h4 class="header-title mt-0">Thêm Loại Phòng Mới</h4>
                <div class="row">
                    <div class="col-lg-6">
                        <div class="p-4">
                            <div class="form-group">
                                <label for="txtRoomType">Loại phòng</label>
                                <asp:TextBox ID="txtRoomType" runat="server" CssClass="form-control" Placeholder="vip,manunual,..."></asp:TextBox>
                                <asp:Label ID="lblRoomType" runat="server" CssClass="text-danger"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label for="txtRoomType">Giá phòng</label>
                                <asp:TextBox ID="txtPriceRoom" runat="server" CssClass="form-control" Placeholder="200000,..."></asp:TextBox>
                                <asp:Label ID="lblPriceRoom" runat="server" CssClass="text-danger"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label for="txtRoomContent">Mô tả phòng</label>
                                <asp:TextBox ID="txtRoomContent" runat="server" CssClass="form-control" Placeholder="1 giường,..."></asp:TextBox>
                                <asp:Label ID="lblRoomContent" runat="server" CssClass="text-danger"></asp:Label>
                            </div>
                            <div class="text-center mt-3">
                                <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-success" Text="Thêm loại phòng" OnClick="BtnSubmit_Click" />
                                <asp:Button ID="BtnCancel" runat="server" CssClass="btn btn-danger" Text="Hủy" OnClick="BtnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
