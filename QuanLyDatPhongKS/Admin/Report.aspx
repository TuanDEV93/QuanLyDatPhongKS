<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="QuanLyDatPhongKS.Admin.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <head>
        <style>
            .btn-doanhthu {
                margin-top: 20px;
            }
        </style>
    </head>
    <div class="row mt-4">
        <div class="col-lg-12">
            <div class="card-box">
                <h2 class="mb-4">Danh sách khách hàng đặt phòng nhiều nhất</h2>

                <div class="row mb-4 align-items-center">
                    <div class="col-sm-4">
                        <asp:Label ID="lblChonThangCustomer" runat="server" Text="Chọn tháng:" CssClass="font-weight-bold" />
                        <asp:DropDownList ID="ddlMonthCustomer" runat="server" CssClass="form-control">
                            <asp:ListItem Text="1" Value="1" />
                            <asp:ListItem Text="2" Value="2" />
                            <asp:ListItem Text="3" Value="3" />
                            <asp:ListItem Text="4" Value="4" />
                            <asp:ListItem Text="5" Value="5" />
                            <asp:ListItem Text="6" Value="6" />
                            <asp:ListItem Text="7" Value="7" />
                            <asp:ListItem Text="8" Value="8" />
                            <asp:ListItem Text="9" Value="9" />
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="11" Value="11" />
                            <asp:ListItem Text="12" Value="12" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-4">
                        <asp:Label ID="lblChonNamCustomer" runat="server" Text="Chọn năm:" CssClass="font-weight-bold" />
                        <asp:DropDownList ID="ddlYearCustomer" runat="server" CssClass="form-control">
                            <asp:ListItem Text="2024" Value="2024" />
                            <asp:ListItem Text="2025" Value="2025" />
                            <asp:ListItem Text="2026" Value="2026" />
                            <asp:ListItem Text="2027" Value="2027" />
                            <asp:ListItem Text="2028" Value="2028" />
                            <asp:ListItem Text="2029" Value="2029" />
                            <asp:ListItem Text="2030" Value="2030" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-4">
                        <asp:Button ID="btnDuyetCustomer" runat="server" Text="Duyệt" Width="89px" CssClass="btn btn-success btn-rounded btn-doanhthu" OnClick="btnDuyet_Click" />
                    </div>
                </div>

                <div class="table-responsive">
                    <asp:GridView ID="gvTop" runat="server"
                        CssClass="table table-striped table-bordered table-hover"
                        AutoGenerateColumns="False"
                        EmptyDataText="Không có dữ liệu.">
                        <Columns>
                            
                            <asp:BoundField DataField="CustomerName" HeaderText="Họ và tên" SortExpression="UserBooking" />
                           
                            <asp:BoundField DataField="Phone" HeaderText="Số điện thoại" SortExpression="Phone" />
                            <asp:BoundField DataField="TotalBookings" HeaderText="Tổng số lần đặt" SortExpression="TotalBookings" />
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lblMessageCustomer" runat="server" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>
    </div>


    <div class="row mt-4">
        <div class="col-lg-12">
            <div class="card-box">
                <h2 class="mb-4">Danh sách phòng được đặt nhiều nhất</h2>

                <div class="row mb-4 align-items-center">
                    <div class="col-sm-4">
                        <asp:Label ID="lblChonThangLane" runat="server" Text="Chọn tháng:" CssClass="font-weight-bold" />
                        <asp:DropDownList ID="ddlMonthLane" runat="server" CssClass="form-control">
                            <asp:ListItem Text="1" Value="1" />
                            <asp:ListItem Text="2" Value="2" />
                            <asp:ListItem Text="3" Value="3" />
                            <asp:ListItem Text="4" Value="4" />
                            <asp:ListItem Text="5" Value="5" />
                            <asp:ListItem Text="6" Value="6" />
                            <asp:ListItem Text="7" Value="7" />
                            <asp:ListItem Text="8" Value="8" />
                            <asp:ListItem Text="9" Value="9" />
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="11" Value="11" />
                            <asp:ListItem Text="12" Value="12" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-4">
                        <asp:Label ID="lblChonNamLane" runat="server" Text="Chọn năm:" CssClass="font-weight-bold" />
                        <asp:DropDownList ID="ddlYearLane" runat="server" CssClass="form-control">
                            <asp:ListItem Text="2024" Value="2024" />
                            <asp:ListItem Text="2025" Value="2025" />
                            <asp:ListItem Text="2026" Value="2026" />
                            <asp:ListItem Text="2027" Value="2027" />
                            <asp:ListItem Text="2028" Value="2028" />
                            <asp:ListItem Text="2029" Value="2029" />
                            <asp:ListItem Text="2030" Value="2030" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-4">
                        <asp:Button ID="btnDuyetSan" runat="server" Text="Duyệt" Width="89px" CssClass="btn btn-success btn-rounded btn-doanhthu"  OnClick="btnDuyetSan_Click" />
                    </div>
                </div>

                <div class="table-responsive">
                    <asp:GridView ID="gvLane" runat="server"
                        CssClass="table table-striped table-bordered table-hover"
                        AutoGenerateColumns="False"
                        EmptyDataText="Không có dữ liệu.">
                        <Columns>
                            <asp:BoundField DataField="RoomNumber" HeaderText="Mã Phòng" />
                            <asp:BoundField DataField="TotalBookings" HeaderText="Tổng số lượt đặt" />
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
        </div>
    </div>


    <div class="row mt-4">
        <div class="col-lg-12">
            <div class="card-box">
                <h2 class="mb-4">Danh sách khách hàng hủy đặt phòng nhiều nhất</h2>

                <div class="row mb-4 align-items-center">
                    <div class="col-sm-4">
                        <asp:Label ID="lblChonThangHuy" runat="server" Text="Chọn tháng:" CssClass="font-weight-bold" />
                        <asp:DropDownList ID="ddlMonthCancel" runat="server" CssClass="form-control">
                            <asp:ListItem Text="1" Value="1" />
                            <asp:ListItem Text="2" Value="2" />
                            <asp:ListItem Text="3" Value="3" />
                            <asp:ListItem Text="4" Value="4" />
                            <asp:ListItem Text="5" Value="5" />
                            <asp:ListItem Text="6" Value="6" />
                            <asp:ListItem Text="7" Value="7" />
                            <asp:ListItem Text="8" Value="8" />
                            <asp:ListItem Text="9" Value="9" />
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="11" Value="11" />
                            <asp:ListItem Text="12" Value="12" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-4">
                        <asp:Label ID="lblChonNamHuy" runat="server" Text="Chọn năm:" CssClass="font-weight-bold" />
                        <asp:DropDownList ID="ddlYearCancel" runat="server" CssClass="form-control">
                            <asp:ListItem Text="2024" Value="2024" />
                            <asp:ListItem Text="2025" Value="2025" />
                            <asp:ListItem Text="2026" Value="2026" />
                            <asp:ListItem Text="2027" Value="2027" />
                            <asp:ListItem Text="2028" Value="2028" />
                            <asp:ListItem Text="2029" Value="2029" />
                            <asp:ListItem Text="2030" Value="2030" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-4">
                        <asp:Button ID="btnDuyetHuy" runat="server" Text="Duyệt" Width="89px" CssClass="btn btn-success btn-rounded btn-doanhthu" OnClick="btnDuyetHuy_Click" />
                    </div>
                </div>

                <div class="table-responsive">
                    <asp:GridView ID="gvHuySan" runat="server"
                        CssClass="table table-striped table-bordered table-hover"
                        AutoGenerateColumns="False"
                        EmptyDataText="Không có dữ liệu.">
                        <Columns>
                            <asp:BoundField DataField="CustomerName" HeaderText="Họ và tên" />
                            <asp:BoundField DataField="Phone" HeaderText="Số điện thoại" />
                            <asp:BoundField DataField="CancelCount" HeaderText="Tổng số lượt hủy" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>



    <div class="row mt-4">
        <div class="col-lg-12">
            <div class="card-box">
                <h2 class="mb-4">Tổng doanh thu</h2>

                <div class="row mb-4 align-items-center">
                    <div class="col-sm-4">
                        <label for="txtNgayBatDau" class="font-weight-bold">Chọn ngày bắt đầu:</label>
                        <input type="date" id="txtNgayBatDau" runat="server" class="form-control" />
                    </div>

                    <div class="col-sm-4">
                        <label for="txtNgayKetThuc" class="font-weight-bold">Chọn ngày kết thúc:</label>
                        <input type="date" id="txtNgayKetThuc" runat="server" class="form-control" />
                    </div>

                    <div class="col-sm-4">
                        <asp:Button ID="btnThongKe" runat="server" Text="Duyệt" Width="89px" CssClass="btn btn-success btn-rounded btn-doanhthu" OnClick="btnThongKe_Click" />
                    </div>
                </div>


                <div class="table-responsive">
                    <asp:GridView ID="GridViewRevenue" runat="server"
                        CssClass="table table-striped table-bordered table-hover"
                        AutoGenerateColumns="False"
                        EmptyDataText="Không có dữ liệu.">
                        <Columns>
                            <asp:BoundField DataField="TimeRange" HeaderText="Khoảng thời gian" />
                            <asp:BoundField DataField="TotalRevenue" HeaderText="Tổng doanh thu" DataFormatString="{0:N0}" />
                        </Columns>
                    </asp:GridView>

                </div>

            </div>
        </div>
    </div>

</asp:Content>
