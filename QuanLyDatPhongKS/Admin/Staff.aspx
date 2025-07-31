<%@ Page Title="Quản lý nhân viên" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="QuanLyDatPhongKS.Admin.Staff" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="card-box">
                <div class="row">
                    <div class="col-sm-4">
                        <a href="AddStaff.aspx" class="btn btn-success btn-rounded btn-md waves-effect waves-light mb-4">
                            <i class="md md-add"></i> Thêm nhân viên
                        </a>
                    </div>
                </div>

                <div class="table-responsive">
                    <asp:GridView ID="gvStaff" runat="server" CssClass="table table-hover agents-mails-checkbox m-0 table-centered table-actions-bar"
                    AutoGenerateColumns="False" OnRowCommand="gvStaff_RowCommand" OnRowDataBound="gvStaff_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="UserID" HeaderText="Mã nhân viên" />
                        <asp:BoundField DataField="DisplayName" HeaderText="Họ tên" />
                        <asp:BoundField DataField="Address" HeaderText="Địa chỉ" />
                        <asp:BoundField DataField="Phone" HeaderText="Số điện thoại" />
                     
                        <asp:TemplateField HeaderText="Tác vụ">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditStaff" CommandArgument='<%# Eval("UserID") %>'>
                                    <i class="mdi mdi-pencil-box-outline text-success"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnDelete"  runat="server" CommandName="DeleteStaff" CommandArgument='<%# Eval("UserID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa nhân viên này không?');">
                                    <i class="mdi mdi-close-box-outline text-danger"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                    <asp:HiddenField ID="hfEditUserID" runat="server" />
                    <asp:HiddenField ID="hfDeleteUserID" runat="server" />
                </div>
                <!-- Modal chỉnh sửa thông tin nhân viên -->
            <asp:Panel ID="EditPanel" runat="server" Visible="false" CssClass="border p-3 mt-3">
                <h5>Chỉnh sửa thông tin nhân viên</h5>
                <div class="form-group">
                    <label for="txtEditFullNamePanel">Họ tên</label>
                    <asp:TextBox ID="txtEditDisplayNamePanel" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEditAddressPanel">Địa chỉ</label>
                    <asp:TextBox ID="txtEditAddressPanel" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEditPhonePanel">Số điện thoại</label>
                    <asp:TextBox ID="txtEditPhonePanel" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:HiddenField ID="hfEditUserIDPanel" runat="server" />
                <div>
                    <asp:Button ID="btnSavePanel" runat="server" Text="Lưu thay đổi" CssClass="btn btn-primary" OnClick="btnSavePanel_Click" />
                    <asp:Button ID="btnCancelPanel" runat="server" Text="Hủy" CssClass="btn btn-secondary" OnClick="btnCancelPanel_Click" />
                </div>
            </asp:Panel>
                <!-- end table responsive -->
            </div>
            <!-- end card-box -->
        </div>
        <!-- end col -->
    </div>
</asp:Content>
