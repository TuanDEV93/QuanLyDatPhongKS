<%@ Page Title="Thêm nhân viên" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddStaff.aspx.cs" Inherits="QuanLyDatPhongKS.Admin.AddStaff" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <h4 class="header-title mt-0">Điền vào mẫu</h4>
                <div class="row">
                    <div class="col-lg-6">
                        <div class="p-4">
                            <div class="form-group">
                                <label for="txtDisplayName">Họ tên nhân viên</label>
                                <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control" Placeholder="Nguyễn Văn A"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtUserName">Tên đăng nhập</label>
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" Placeholder="nguyenvana"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtPassword">Mật khẩu</label>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="********"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="p-4">
                            <div class="form-group">
                                <label for="txtPhone">SĐT</label>
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Placeholder="0123456789"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtAddress">Địa chỉ</label>
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" Placeholder="Hà Nội"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="text-center">
                    <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-success waves-effect waves-light" Text="Xác nhận" OnClick="BtnSubmit_Click" />
                    <asp:Button ID="BtnCancel" runat="server" CssClass="btn btn-danger waves-effect waves-light" Text="Hủy" OnClick="BtnCancel_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
