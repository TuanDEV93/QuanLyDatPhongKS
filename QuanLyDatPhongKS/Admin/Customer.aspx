<%@ Page Title="Danh sách khách hàng" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="QuanLyDatPhongKS.Admin.Customer" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="card-box">
                <div class="row">
                    <div class="col-sm-8">
                        <form>
                            <div class="form-group search-box">
                                <input type="text" id="SearchInput" runat="server" class="form-control product-search" placeholder="Nhập tên khách hàng..." />
                                <asp:Button ID="BtnSearch" runat="server" Text="Tìm kiếm" CssClass="btn btn-search" OnClick="BtnSearch_Click" />
                            </div>
                            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
                    </div>
                    </form>
                </div>
                <div class="col-sm-4">
                    <a href="AddCustomer.aspx" class="btn btn-success btn-rounded btn-md waves-effect waves-light mb-4">
                        <i class="md md-add"></i>Thêm khách hàng
                    </a>
                </div>
            </div>

            <div class="table-responsive">
                <asp:GridView ID="gvCustomer1" runat="server" CssClass="table table-hover agents-mails-checkbox m-0 table-centered table-actions-bar"
                    AutoGenerateColumns="False" AllowPaging="True" OnRowCommand="gvCustomer_RowCommand" OnRowDataBound="gvCustomer_RowDataBound" OnPageIndexChanging="gvCustomer1_PageIndexChanging" PageSize="5">
                    <Columns>
                        <asp:BoundField DataField="CustomerID" HeaderText="Mã khách hàng" />
                        <asp:BoundField DataField="DisplayName" HeaderText="Họ tên" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Address" HeaderText="Địa chỉ" />
                        <asp:BoundField DataField="Phone" HeaderText="Số điện thoại" />

                        <asp:TemplateField HeaderText="Tác vụ">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit1" runat="server" CommandName="EditCustomer" CommandArgument='<%# Eval("CustomerID") %>'>
                                <i class="mdi mdi-pencil-box-outline text-success"></i>
                            </asp:LinkButton>
                                <asp:LinkButton ID="btnDelete1" runat="server" CommandName="DeleteCustomer" CommandArgument='<%# Eval("CustomerID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa khách hàng này không?');">
                                <i class="mdi mdi-close-box-outline text-danger"></i>
                            </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:HiddenField ID="hfEditCustomerID" runat="server" />
                <asp:HiddenField ID="hfDeleteCustomerID" runat="server" />
            </div>
            <!-- Modal chỉnh sửa thông tin khách hàng -->
            <asp:Panel ID="EditPanel1" runat="server" Visible="false" CssClass="border p-3 mt-3">
                <h5>Chỉnh sửa thông tin khách hàng</h5>
                <div class="form-group">
                    <label for="txtEditFullNamePanel">Họ tên</label>
                    <asp:TextBox ID="txtEditDisplayNamePanel1" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEditEmailPanel">Email</label>
                    <asp:TextBox ID="txtEditEmailPanel1" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="lblMessage4" runat="server" CssClass="text-danger"></asp:Label>
                </div>
                <div class="form-group">
                    <label for="txtEditAddressPanel">Địa chỉ</label>
                    <asp:TextBox ID="txtEditAddressPanel1" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEditPhonePanel">Số điện thoại</label>
                    <asp:TextBox ID="txtEditPhonePanel1" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="lblMessage5" runat="server" CssClass="text-danger"></asp:Label>
                </div>
                <asp:HiddenField ID="hfEditCustomerIDPanel1" runat="server" />
                <div>
                    <asp:Button ID="btnSavePanel1" runat="server" Text="Lưu thay đổi" CssClass="btn btn-primary" OnClick="btnSavePanel1_Click" />
                    <asp:Button ID="btnCancelPanel1" runat="server" Text="Hủy" CssClass="btn btn-secondary" OnClick="btnCancelPanel1_Click" />
                </div>
            </asp:Panel>
            <!-- end table responsive -->
        </div>
        <!-- end card-box -->
    </div>
    <!-- end col -->
    </div>
</asp:Content>
