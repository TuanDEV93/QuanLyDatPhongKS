<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QuanLyDatPhongKS.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <div class="fh5co-parallax" style="background-image: url(images/slider1.jpg); min-height:80px; background-size:cover; background-position:center;" data-stellar-background-ratio="0.3">
    <div class="overlay"></div>
    <div class="container">
        <div class="row">
            <div class="col-md-12 text-center fh5co-table">
                <div class="fh5co-intro fh5co-table-cell">
                    <!-- Nội dung ở đây nếu có -->
                </div>
            </div>
        </div>
    </div>
</div>


    <head>
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
        
    <style>
        .login-container {
            max-width: 400px;
            margin: 50px auto;
            padding: 30px;
            background: white;
            border-radius: 20px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        }

        .login-container h2 {
            text-align: center;
            margin-bottom: 30px;
        }

        .input-wrapper {
            display: flex;
            align-items: center;
            background: #f9f9f9;
            border-radius: 30px;
            padding: 10px 20px;
            margin-bottom: 20px;
            border: 1px solid #ddd;
        }

        .input-wrapper i {
            margin-right: 10px;
            color: #666;
        }

        .input-wrapper input {
            border: none;
            background: transparent;
            width: 100%;
            outline: none;
            font-size: 16px;
        }

        .btn-login {
            background-color: #99ffff;
            border: none;
            width: 100%;
            padding: 10px;
            border-radius: 30px;
            font-size: 16px;
            cursor: pointer;
            transition: background 0.3s;
        }

        .btn-login:hover {
            background-color: #66e0e0;
        }

        .register-link {
            text-align: center;
            margin-top: 15px;
        }
    </style>
    </head>
     <div class="login-container">
        <h2>ĐĂNG NHẬP</h2>

        <div class="input-wrapper">
            <i class="fa fa-user"></i>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="Nhập tên đăng nhập..." />
        </div>

        <div class="input-wrapper">
            <i class="fa fa-lock"></i>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Nhập mật khẩu..." />
        </div>

        <asp:Button ID="btnLogin" runat="server" CssClass="btn-login" Text="Đăng Nhập" OnClick="btnLogin_Click" />

        <div class="register-link">
            <span>Bạn chưa có tài khoản? <a href="Register.aspx">Đăng Kí</a></span>
        </div>
    </div>
</asp:Content>
