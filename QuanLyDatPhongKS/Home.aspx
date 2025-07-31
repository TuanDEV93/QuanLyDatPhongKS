<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="QuanLyDatPhongKS.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<style>
		/* Container chính */
	.booking-bar {
		background-color: #ff5722;
		display: flex;
		flex-wrap: wrap;
		gap: 100px;
		align-items: flex-end;
		justify-content: center;
		border-radius: 16px;
		max-width: 1100px;
		margin: 0 auto;
	}

	/* Dropdown & textbox */
	.booking-bar .form-control {
		padding: 10px;
		border-radius: 8px;
		border: none;
		font-size: 16px;
		width: 100%;
	}

	/* Label */
	.booking-bar label {
		color: white;
		font-weight: 600;
		margin-bottom: 5px;
		display: block;
	}

	/* Nhóm form (label + input) */
	.booking-bar .form-group {
		display: flex;
		flex-direction: column;
		width: 180px;
	}

	/* Nút kiểm tra phòng */
	.btn-check-room {
		background-color: white;
		color: #ff5722;
		border: none;
		padding: 12px 24px;
		margin-bottom: 30px;
		border-radius: 30px;
		font-weight: bold;
		cursor: pointer;
		transition: background 0.3s;
	}

	.btn-check-room:hover {
		background-color: #ffe0cc;
	}


	</style>
<aside id="fh5co-hero" class="js-fullheight">
	<div class="flexslider js-fullheight">
		<ul class="slides">
	   	<li style="background-image: url(images/slider1.jpg);">
	   		<div class="overlay-gradient"></div>
	   		<div class="container">
	   			<div class="col-md-12 col-md-offset-0 text-center slider-text">
	   				<div class="slider-text-inner js-fullheight">
	   					<div class="desc">
	   						<p><span>Khách sạn Hương Ngọc điểm đến tuyệt vời</span></p>
	   				
		   					<p>
		   						<a href="Home.aspx" class="btn btn-primary btn-lg">Đặt phòng ngay</a>
		   					</p>
	   					</div>
	   				</div>
	   			</div>
	   		</div>
	   	</li>
	   	<li style="background-image: url(images/slider2.jpg);">
	   		<div class="overlay-gradient"></div>
	   		<div class="container">
	   			<div class="col-md-12 col-md-offset-0 text-center slider-text">
	   				<div class="slider-text-inner js-fullheight">
	   					<div class="desc">
	   						<p><span>Khách sạn Hương Ngọc nơi nghỉ dưỡng </span></p>
	   						<h2>Thoải mái và tiện lợi</h2>
		   					<p>
		   						<a href="Home.aspx" class="btn btn-primary btn-lg">Đặt phòng ngay</a>
		   					</p>
	   					</div>
	   				</div>
	   			</div>
	   		</div>
	   	</li>
	   	<li style="background-image: url(images/slider3.jpg);">
	   		<div class="overlay-gradient"></div>
	   		<div class="container">
	   			<div class="col-md-12 col-md-offset-0 text-center slider-text">
	   				<div class="slider-text-inner js-fullheight">
	   					<div class="desc">
	   						<p><span>Khách sạn Hương Ngọc</span></p>
	   						<h2>Nhiều ưu đãi lớn đang chờ đợi bạn</h2>
		   					<p>
		   						<a href="Home.aspx" class="btn btn-primary btn-lg">Đặt ngay</a>
		   					</p>
	   					</div>
	   				</div>
	   			</div>
	   		</div>
	   	</li>
	   	
	  	</ul>
  	</div>
</aside>
<div class="wrap">
	<div class="container">
		<div class="row">
			<div id="availability">
				<div class="booking-bar">
				<!-- Chọn loại phòng -->
				<div class="form-group">
					<label for="ddlRoomType">Chọn loại phòng</label>
					<asp:DropDownList ID="ddlRoomType" runat="server" CssClass="form-control">
						<asp:ListItem Text="Chọn loại phòng" Value="" />
						<asp:ListItem Text="Phòng thông thường " Value="Standard" />
						<asp:ListItem Text="Phòng cao cấp" Value="Medium" />
						<asp:ListItem Text="Phòng hạng sang " Value="VIP" />
					</asp:DropDownList>
				</div>

				<!-- Ngày đặt -->
				<div class="form-group">
					<label for="txtBookingDate">NGÀY ĐẶT</label>
					<asp:TextBox ID="txtBookingDate" runat="server" CssClass="form-control" TextMode="Date" />
				</div>

				<!-- Ngày ra -->
				<div class="form-group">
					<label for="txtCheckoutDate">NGÀY RA</label>
					<asp:TextBox ID="txtCheckoutDate" runat="server" CssClass="form-control" TextMode="Date" />
				</div>

				<!-- Nút kiểm tra -->
				<asp:Button ID="btnCheckRoom" runat="server" Text="Kiểm tra phòng" CssClass="btn-check-room" OnClick="btnCheckRoom_Click" />
			</div>
			</div>
		</div>
	</div>
</div>

<div id="fh5co-counter-section" class="fh5co-counters">
	<div class="container">
		<div class="row">
			<div class="col-md-3 text-center">
				<span class="fh5co-counter js-counter" data-from="0" data-to="20356" data-speed="5000" data-refresh-interval="50"></span>
				<span class="fh5co-counter-label">Người dùng truy cập</span>
			</div>
			<div class="col-md-3 text-center">
				<span class="fh5co-counter js-counter" data-from="0" data-to="15501" data-speed="5000" data-refresh-interval="50"></span>
				<span class="fh5co-counter-label">khách sạn</span>
			</div>
			<div class="col-md-3 text-center">
				<span class="fh5co-counter js-counter" data-from="0" data-to="8200" data-speed="5000" data-refresh-interval="50"></span>
				<span class="fh5co-counter-label">giao dịch</span>
			</div>
			<div class="col-md-3 text-center">
				<span class="fh5co-counter js-counter" data-from="0" data-to="8763" data-speed="5000" data-refresh-interval="50"></span>
				<span class="fh5co-counter-label">lượt  &amp; đánh giá</span>
			</div>
		</div>
	</div>
</div>

<div id="featured-hotel" class="fh5co-bg-color">
	<div class="container">
		
		<div class="row">
			<div class="col-md-12">
				<div class="section-title text-center">
					<h2>Phòng Thường</h2>
				</div>
			</div>
		</div>

		<div class="row">
			<div class="feature-full-1col">
				<div class="image" style="background-image: url(images/Standard.jpg);">
					<div class="descrip text-center">
						<p><small>Chỉ từ</small><span>250.000 VNĐ/đêm</span></p>
					</div>
				</div>
				<div class="desc">
					<h3>Khách sạn Hương Ngọc - Phòng đơn</h3>
					<p>Bao gồm các tiện ích như 1 giường đơn, 1 máy lạnh 1 ti vi, ...</p>
					<p>Chúng tôi cung cấp các dịch vụ dành riêng cho khách hàng chọn phòng đơn đầy đủ tiện nghi.</p>
					<p><a href="Home.aspx" class="btn btn-primary btn-luxe-primary">Đặt phòng ngay <i class="ti-angle-right"></i></a></p>
				</div>
			</div>
		</div>

	</div>
</div>
<div id="featured-hotel1" class="fh5co-bg-color">
	<div class="container">
		
		<div class="row">
			<div class="col-md-12">
				<div class="section-title text-center">
					<h2>Phòng Cao cấp</h2>
				</div>
			</div>
		</div>

		<div class="row">
			<div class="feature-full-1col">
				<div class="image" style="background-image: url(images/medium.jpg);">
					<div class="descrip text-center">
						<p><small>Chỉ từ</small><span>350.000 VNĐ/đêm</span></p>
					</div>
				</div>
				<div class="desc">
					<h3>Khách sạn Hương Ngọc - Phòng cao cấp</h3>
					<p>Bao gồm các tiện ích như 2 giường đơn, 1 máy lạnh, 1 ti vi 56" full HD đầy đủ ứng dụng xem phim,bồn tắm ...</p>
					<p>Chúng tôi cung cấp các dịch vụ dành riêng cho khách hàng chọn phòng cao cấp đầy đủ tiện nghi.</p>
					<p><a href="Home.aspx" class="btn btn-primary btn-luxe-primary">Đặt phòng ngay <i class="ti-angle-right"></i></a></p>
				</div>
			</div>
		</div>

	</div>
</div>
	<div id="featured-hotel2" class="fh5co-bg-color">
	<div class="container">
		
		<div class="row">
			<div class="col-md-12">
				<div class="section-title text-center">
					<h2>Phòng hạng sang</h2>
				</div>
			</div>
		</div>

		<div class="row">
			<div class="feature-full-1col">
				<div class="image" style="background-image: url(images/vip.jpg);">
					<div class="descrip text-center">
						<p><small>Chỉ từ</small><span>450.000 VNĐ/đêm</span></p>
					</div>
				</div>
				<div class="desc">
					<h3>Khách sạn Hương Ngọc - Phòng hạng sang</h3>
					<p>Bao gồm các tiện ích như 2 giường đôi, 2 máy lạnh 1 ti vi full hd full phụ kiện máy chơi game, View phố siêu đẹp ...</p>
					<p>Chúng tôi cung cấp các dịch vụ dành riêng cho khách hàng chọn phòng hạng sang đầy đủ tiện nghi.</p>
					<p><a href="Home.aspx" class="btn btn-primary btn-luxe-primary">Đặt phòng ngay <i class="ti-angle-right"></i></a></p>
				</div>
			</div>
		</div>

	</div>
</div>
	<script>
	document.addEventListener("DOMContentLoaded", function () {
		const btn = document.getElementById("<%= btnCheckRoom.ClientID %>");
		const startInput = document.getElementById("<%= txtBookingDate.ClientID %>");
		const endInput = document.getElementById("<%= txtCheckoutDate.ClientID %>");

		btn.addEventListener("click", function (e) {
			const startDate = new Date(startInput.value);
			const endDate = new Date(endInput.value);

			if (!startInput.value || !endInput.value || endDate <= startDate) {
				e.preventDefault(); // chặn submit
				alert("Ngày ra phải sau ngày đặt. Vui lòng chọn lại.");
			}
		});
	});
    </script>

</asp:Content>