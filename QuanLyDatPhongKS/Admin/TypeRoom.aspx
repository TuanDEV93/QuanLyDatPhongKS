<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="TypeRoom.aspx.cs" Inherits="QuanLyDatPhongKS.Admin.TypeRoom" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="card-box">
                <div class="row">
                    <div class="col-sm-4">
                        <a href="AddTypeRoom.aspx" class="btn btn-success btn-rounded btn-md waves-effect waves-light mb-4">
                            <i class="md md-add"></i> Thêm Loại phòng
                        </a>
                    </div>
                </div>

                <div class="table-responsive">
                    <asp:GridView ID="gvTypeRoom" runat="server" CssClass="table table-hover agents-mails-checkbox m-0 table-centered table-actions-bar"
                    AutoGenerateColumns="False" OnRowCommand="gvTypeRoom_RowCommand" OnRowDataBound="gvTypeRoom_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="RoomTypeID" HeaderText="Mã loại phòng" />
                        <asp:BoundField DataField="TypeRoom" HeaderText="Loại phòng" />
                        <asp:BoundField DataField="PriceRoom" HeaderText="Giá phòng" />
                        <asp:BoundField DataField="RoomContent" HeaderText="Mô tả phòng" />
                        <asp:TemplateField HeaderText="Tác vụ">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditRoomType" CommandArgument='<%# Eval("RoomTypeID") %>'>
                                    <i class="mdi mdi-pencil-box-outline text-success"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnDelete"  runat="server" CommandName="DeleteRoomType" CommandArgument='<%# Eval("RoomTypeID") %>' OnClientClick="return confirm('Bạn có chắc chắn muốn xóa nhân viên này không?');">
                                    <i class="mdi mdi-close-box-outline text-danger"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                    <asp:HiddenField ID="hfEditTypeRoomID" runat="server" />
                    <asp:HiddenField ID="hfDeleteTypeRoomID" runat="server" />
                </div>
                <!-- Modal chỉnh sửa thông tin nhân viên -->
            <asp:Panel ID="EditPanel" runat="server" Visible="false" CssClass="border p-3 mt-3">
                <h5>Chỉnh sửa thông tin loại phòng</h5>
                <div class="form-group">
                    <label for="txtEditTypeRoomPanel">Loại phòng</label>
                    <asp:TextBox ID="txtEditTypeRoomPanel" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEditPriceRoomPanel">Giá phòng</label>
                    <asp:TextBox ID="txtEditPriceRoomPanel" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtEditRoomContentPanel">Mô tả phòng</label>
                    <asp:TextBox ID="txtEditRoomContentPanel" runat="server" CssClass="form-control"></asp:TextBox>
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
