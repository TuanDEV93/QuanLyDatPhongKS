<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="QuanLyDatPhongKS.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="fh5co-parallax" style="background-image: url(images/slider1.jpg); min-height:200px;" data-stellar-background-ratio="0.5">
	<div class="overlay"></div>
	<div class="container">
		<div class="row">
			<div class="col-md-12 col-md-offset-0 col-sm-12 col-sm-offset-0 col-xs-12 col-xs-offset-0 text-center fh5co-table">
				<div class="fh5co-intro fh5co-table-cell">
					<h1 class="text-center">Đăng Ký</h1 >
				</div>
			</div>
		</div>
	</div>
</div>
    <head>
        <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet">
        <style>
    body {
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    }

    .form-container {
        max-width: 700px;
        margin: 40px auto;
        background: #fff;
        border-radius: 10px;
        box-shadow: 0 5px 15px rgba(0,0,0,0.1);
        padding: 30px;
    }

    .form-title {
        text-align: center;
        font-size: 32px;
        font-weight: bold;
        margin-bottom: 30px;
    }

    .form-group {
        display: flex;
        align-items: center;
        margin-bottom: 20px;
    }

    .form-group label {
        width: 150px;
        margin-right: 15px;
        font-weight: 500;
    }

    .form-group .input-container {
        position: relative;
        width: 100%;
    }

    .form-group i {
        position: absolute;
        left: 15px;
        top: 50%;
        transform: translateY(-50%);
        color: gray;
    }

    .form-group input {
        padding-left: 45px;
        height: 45px;
        width: 100%;
        border: 1px solid #ccc;
        border-radius: 25px;
        box-sizing: border-box;
    }

    .btn-register {
        display: block;
        width: 200px;
        margin: 30px auto 0;
        border-radius: 25px;
        background-color: #00c3ff;
        border: none;
        padding: 12px;
        font-size: 16px;
        color: white;
        font-weight: bold;
        cursor: pointer;
        transition: background-color 0.3s ease, transform 0.3s ease;
    }

    .btn-register:hover {
        background-color: #00a0cc;
        transform: scale(1.05);
    }

    .message-box {
        position: fixed;
        top: 180px;
        right: 20px;
        padding: 15px 20px;
        border-radius: 8px;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.2);
        font-size: 16px;
        display: flex;
        align-items: center;
        gap: 10px;
        opacity: 0;
        transform: translateX(100%);
        transition: opacity 0.3s ease, transform 0.3s ease;
        color: white;
        z-index: 1000;
    }

    .message-box.success {
        background-color: #4caf50;
    }

    .message-box.error {
        background-color: #f44336;
    }

    .message-box.show {
        opacity: 1;
        transform: translateX(0);
    }

    .message-box.hidden {
        display: none;
    }

    .icon {
        font-size: 23px;
    }
</style>
    </head>
   <div class="form-container">
    <asp:Label ID="Label1" runat="server" CssClass="form-title" Text="ĐĂNG KÝ TÀI KHOẢN"></asp:Label>

    <div class="form-group">
        <label for="txtUsername">Tên đăng nhập</label>
        <div class="input-container">
            <i class="fas fa-user"></i>
            <asp:TextBox ID="txtUsername" runat="server" Placeholder="Nhập tên đăng nhập..."></asp:TextBox>
        </div>
    </div>

    <div class="form-group">
        <label for="txtPassword">Mật khẩu</label>
        <div class="input-container">
            <i class="fas fa-lock"></i>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Placeholder="Nhập mật khẩu..."></asp:TextBox>
        </div>
    </div>

    <div class="form-group">
        <label for="txtFullname">Họ và tên</label>
        <div class="input-container">
            <i class="fas fa-user-circle"></i>
            <asp:TextBox ID="txtFullname" runat="server" Placeholder="Nhập họ và tên..."></asp:TextBox>
        </div>
    </div>

    <div class="form-group">
        <label for="txtAddress">Địa chỉ</label>
        <div class="input-container">
            <i class="fas fa-map-marker-alt"></i>
            <asp:TextBox ID="txtAddress" runat="server" Placeholder="Nhập địa chỉ..."></asp:TextBox>
        </div>
    </div>

    <div class="form-group">
        <label for="txtPhone">Số điện thoại</label>
        <div class="input-container">
            <i class="fas fa-phone"></i>
            <asp:TextBox ID="txtPhone" runat="server" Placeholder="Nhập số điện thoại..."></asp:TextBox>
        </div>
    </div>

    <div class="form-group">
        <label for="txtEmail">Email</label>
        <div class="input-container">
            <i class="fas fa-envelope"></i>
            <asp:TextBox ID="txtEmail" runat="server" Placeholder="Nhập email..."></asp:TextBox>
        </div>
    </div>

    <asp:Button ID="btnRegister" runat="server" CssClass="btn-register" Text="Đăng Ký" OnClick="btnRegister_Click" />

    <asp:Label ID="lblNotification" runat="server" Visible="False"></asp:Label>
</div>
    <div id="notificationMessage" class="message-box hidden">
    <span class="icon"></span>
    <span class="message"></span>
</div>
    <script>
        function showNotificationMessage(message, type) {
            const messageBox = document.getElementById('notificationMessage');
            const icon = messageBox.querySelector('.icon');
            const messageText = messageBox.querySelector('.message');

            messageText.textContent = message;

            if (type === 'success') {
                icon.innerHTML = '<i class="fas fa-check-circle"></i>';
            } else if (type === 'error') {
                icon.innerHTML = '<i class="fas fa-times-circle"></i>';
            }

            messageBox.className = `message-box ${type} show`;

            setTimeout(() => {
                messageBox.classList.remove('show');
                setTimeout(() => {
                    messageBox.classList.add('hidden');
                }, 300);
            }, 3000);
        }
    </script>
</asp:Content>
