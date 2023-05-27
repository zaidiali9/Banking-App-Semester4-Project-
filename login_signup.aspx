﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login_signup.aspx.cs" Inherits="Lab_Project_Final.login_signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log In Page</title>
    <link href="Login.css" rel="stylesheet" />
    <link href="Lib/bootstrap.min.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="container" class="container">
		<!-- FORM SECTION -->
		<div class="row">
			<!-- SIGN UP -->
			<div class="col align-items-center flex-col sign-up">
				<div class="form-wrapper align-items-center">
					<div class="form sign-up">
						<div class="flex-row d-flex">
                        <div class="input-group">
                            <i class='bx bxs-user'></i>
                            <asp:TextBox ID="FullName" placeholder="Full Name" runat="server"></asp:TextBox>
                        </div>
                        <div class="input-group">
							<i class='bx bxs-user'></i>
                            <asp:TextBox ID="Username_signup" placeholder="Username" runat="server"></asp:TextBox>
						</div>
                        </div>
						<div class="flex-row d-flex">
						<div class="input-group">
							<i class='bx bx-mail-send'></i>
							<asp:TextBox ID="Email_signup" placeholder="Email" runat="server"></asp:TextBox>
                        </div>
						<div class="input-group">
							<i class='bx bxs-lock-alt'></i>
                            <asp:TextBox ID="Password_signup1" type="password" placeholder="Password" runat="server"></asp:TextBox>
						</div>

						</div>
                        <div class="flex-row d-flex">
						<div class="input-group">
							<i class='bx bxs-lock-alt'></i>
                            <asp:TextBox ID="Address" placeholder="Address" runat="server"></asp:TextBox>
						</div>
                        <div class="input-group">
                            <i class='bx bxs-lock-alt'></i>
                            <asp:TextBox ID="CNIC" placeholder="CNIC" runat="server"></asp:TextBox>
                        </div>
                        </div>
                        <div class="flex-row d-flex">
                        <div class="input-group">
                            <i class='bx bxs-lock-alt'></i>
                            <asp:TextBox ID="Ph_no" placeholder="Phone #" runat="server"></asp:TextBox>
                        </div>
                            <div class="input-group">
                                <i class='bx bxs-lock-alt'></i>
                                <asp:TextBox ID="sex_g" placeholder="Gender" runat="server"></asp:TextBox>
                            </div>
							</div>
                        <div class="d-flex mb-3">
                            <asp:TextBox runat="server" ID="calender" type="date" Width="100%"></asp:TextBox>
                        </div>
                        <asp:Button ID="Signup" runat="server" Text="Sign Up" CssClass="btn px-5 btn-success" Width="100%" OnClick="Signup_Click" />
                        <p>
                            
							<span>
								Already have an account?
							</span>
							<b onclick="toggle()" class="pointer">
								Sign in here
							</b>
						</p>
					</div>
				</div>
			
			</div>
			<!-- END SIGN UP -->
			<!-- SIGN IN -->
			<div class="col align-items-center flex-col sign-in">
				<div class="form-wrapper align-items-center">
					<div class="form sign-in">
						<div class="input-group">
							<i class='bx bxs-user'></i>
							<asp:TextBox ID="Username_signin" placeholder="Username" runat="server"></asp:TextBox>
                        </div>
						<div class="input-group">
							<i class='bx bxs-lock-alt'></i>
							<asp:TextBox ID="Password_Signin" placeholder="Password" type="password" runat="server"></asp:TextBox>
							
						</div>
						<asp:Button ID="Signin" runat="server" Text="Sign In" CssClass="btn btn-success px-5" width="100%" OnClick="Signin_Click"  />
						<p>
							<b>
								Forgot password?
							</b>
						</p>
						<p>
							<span>
								Don't have an account?
							</span>
							<b onclick="toggle()" class="pointer">
								Sign up here
							</b>
						</p>
					</div>
				</div>
				<div class="form-wrapper">
		
				</div>
			</div>
			<!-- END SIGN IN -->
		</div>
		<!-- END FORM SECTION -->
		<!-- CONTENT SECTION -->
		<div class="row content-row">
			<!-- SIGN IN CONTENT -->
			<div class="col align-items-center flex-col">
				<div class="text sign-in">
					<h2>
						Welcome
					</h2>
	
				</div>
				<div class="img sign-in">
		
				</div>
			</div>
			<!-- END SIGN IN CONTENT -->
			<!-- SIGN UP CONTENT -->
			<div class="col align-items-center flex-col">
				<div class="img sign-up">
				
				</div>
				<div class="text sign-up">
					<h2>
						Join with us
					</h2>
	
				</div>
			</div>
			<!-- END SIGN UP CONTENT -->
		</div>
		<!-- END CONTENT SECTION -->
	</div>
        </div>
    </form>
    <script src="Login.js"></script>
    <script src="Lib/popper.min.js"></script>
    <script src="Lib/bootstrap.bundle.min.js"></script>
</body>
</html>
