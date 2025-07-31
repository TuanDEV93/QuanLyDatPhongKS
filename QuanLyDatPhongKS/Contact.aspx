<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="QuanLyDatPhongKS.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div class="fh5co-parallax" style="background-image: url(images/slider1.jpg);" data-stellar-background-ratio="0.5">
		<div class="overlay"></div>
		<div class="container">
			<div class="row">
				<div class="col-md-12 col-md-offset-0 col-sm-12 col-sm-offset-0 col-xs-12 col-xs-offset-0 text-center fh5co-table">
					<div class="fh5co-intro fh5co-table-cell">
						<h1 class="text-center">Liên hệ với chúng tôi</h1>
						
					</div>
				</div>
			</div>
		</div>
	</div>

	<div id="fh5co-contact-section">
		<div class="row">
			<div class="col-md-6">
				<div id="map" class="fh5co-map">
			<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3720.9575825295715!2d105.89279011038003!3d21.154086283381933!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x313507889f511ad9%3A0x22a4a54af65d3ac5!2zVsaw4budbiBob2Ega2h1IMSRw7QgdGjhu4sgSMOgIGjGsMahbmcgTGnDqm4gaMOg!5e0!3m2!1svi!2s!4v1747723330043!5m2!1svi!2s" width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
				</div>

			</div>
			<div class="col-md-6">
				<div class="col-md-12">
					<h3>Địa chỉ của chúng tôi</h3>
						<ul class="contact-info">
						<li><i class="ti-map"></i>khu đô thị liên hà Đông anh Hà nội</li>
						<li><i class="ti-mobile"></i>0373597532</li>
						
						<li><i class="ti-home"></i><a href="khachsanhuongngoc.ddns.net:9999/Home">www.khachsanhuongngoc.ddns.net:9999/Home</a></li>
					</ul>
				</div>
				<div class="col-md-12">
					<div class="row">
						<div class="col-md-6">
							<div class="form-group">
								<input type="text" class="form-control" placeholder="Họ tên">
							</div>
						</div>
						<div class="col-md-6">
							<div class="form-group">
								<input type="text" class="form-control" placeholder="Email">
							</div>
						</div>
						<div class="col-md-12">
							<div class="form-group">
								<textarea name="" class="form-control" id="" cols="30" rows="7" placeholder="Tin nhắn"></textarea>
							</div>
						</div>
						<div class="col-md-12">
							<div class="form-group">
								<input type="submit" value="Send Message" class="btn btn-primary">
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
