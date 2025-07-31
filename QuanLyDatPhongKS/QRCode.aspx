<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QRCode.aspx.cs" Inherits="QuanLyDatPhongKS.QRCode" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Thanh toán đặt cọc</title>
    <style>
        .container {
            width: 500px;
            margin: 50px auto;
            border: 1px solid #ccc;
            padding: 30px;
            border-radius: 10px;
            font-family: Arial;
            text-align: center;
        }

        .qr-image {
            margin: 20px 0;
        }

        .info {
            font-size: 16px;
            margin-top: 10px;
        }

        .highlight {
            font-weight: bold;
            color: darkgreen;
        }
        .btn-back-home {
            margin-top: 20px;
            padding: 10px 20px;
            background-color: #007bff;
            color: white;
            border: none;
            text-decoration: none;
            display: inline-block;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn-back-home:hover {
            background-color: #0056b3;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Quét mã QR để thanh toán đặt cọc</h2>
            <h3><strong>Lưu ý: Bạn có thời gian là hết ngày hôm nay để thanh toán tiền cọc nếu không sẽ bị huỷ đơn!</strong></h3>
            <div class="qr-image">
                <asp:Image ID="imgQR" runat="server" Width="250px" Height="250px" />
            </div>

            <div class="info">
                <p>Nội dung chuyển khoản: <span class="highlight"><asp:Label ID="lblBillID" runat="server" /></span></p>
                <p>Số tiền cần thanh toán: <span class="highlight"><asp:Label ID="lblDeposit" runat="server" /></span></p>
                <p style="color:red;">*Vui lòng ghi đúng nội dung chuyển khoản </p>
            </div>

            <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick" />
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
        </div>
    </form>
    <a href="Home.aspx" class="btn-back-home">Trở về trang chủ</a>
</body>
</html>
