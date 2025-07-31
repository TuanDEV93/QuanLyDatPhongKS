<%@ Page Title="Thêm khách hàng" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddCustomer.aspx.cs" Inherits="QuanLyDatPhongKS.Admin.AddCustomer" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="card-box">
                <h4 class="header-title mt-0">Điền vào mẫu</h4>
                <div class="row">
                    <div class="col-lg-6">
                        <div class="p-4">
                            <div class="form-group">
                                <label for="txtDisplayName">Họ tên khách hàng</label>
                                <asp:TextBox ID="txtDisplayName1" runat="server" CssClass="form-control" Placeholder="Nguyễn Văn A ..."></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtCustomerName">Tên đăng nhập</label>
                                <asp:TextBox ID="txtCustomerName1" runat="server" CssClass="form-control" Placeholder="nguyenvana ..."></asp:TextBox>
                                 <asp:Label ID="lblMessage1" runat="server" CssClass="text-danger"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label for="txtPassword">Mật khẩu</label>
                                <asp:TextBox ID="txtPassword1" runat="server" CssClass="form-control" TextMode="Password" Placeholder="********"></asp:TextBox>
                                 <asp:Label ID="lblMessage2" runat="server" CssClass="text-danger"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="p-4">
                            <div class="form-group">
                                <label for="txtPhone">Email</label>
                                <asp:TextBox ID="txtEmail1" runat="server" CssClass="form-control" Placeholder="abc@gmail.com ..."></asp:TextBox>
                                <asp:Label ID="lblMessage4" runat="server" CssClass="text-danger"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label for="txtPhone">SĐT</label>
                                <asp:TextBox ID="txtPhone1" runat="server" CssClass="form-control" Placeholder="0123456789 ..."></asp:TextBox>
                                <asp:Label ID="lblMessage5" runat="server" CssClass="text-danger"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label for="txtAddress">Địa chỉ</label>
                                <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" Placeholder="Hanoi ..."></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="text-center">
                    <asp:Button ID="BtnSubmit1" runat="server" CssClass="btn btn-success waves-effect waves-light" Text="Xác nhận" OnClick="BtnSubmit1_Click" />
                    <asp:Button ID="BtnCancel1" runat="server" CssClass="btn btn-danger waves-effect waves-light" Text="Hủy" OnClick="BtnCancel1_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
