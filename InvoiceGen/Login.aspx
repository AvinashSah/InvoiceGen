<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="InvoiceGen.Login" %>

<!--A Design by W3layouts
Author: W3layouts
Author URL: http://w3layouts.com
License: Creative Commons Attribution 3.0 Unported
License URL: http://creativecommons.org/licenses/by/3.0/
-->
<!DOCTYPE HTML>
<html>

<head runat="server">
    <title>Invoice Gen</title>
    <!-- Meta Tags -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="Login PPMS" />
    <script type="application/x-javascript">
		addEventListener("load", function () {
			setTimeout(hideURLbar, 0);
		}, false);

		function hideURLbar() {
			window.scrollTo(0, 1);
		}
    </script>
    <!-- //Meta Tags -->
    <!-- Font-Awesome-CSS -->
    <link href="Contents/css/font-awesome-login.css" rel="stylesheet">
    <!--// Font-Awesome-CSS -->
    <!-- Required Css -->
    <link href="Contents/css/login.css" rel='stylesheet' type='text/css' />
    <!--// Required Css -->
    <!--fonts-->
    <link href="//fonts.googleapis.com/css?family=Montserrat:300,400,500,600" rel="stylesheet">
    <!--//fonts-->
</head>

<body>
    <!--background-->
    <h1>Invoice Generator</h1>
    <!-- Main-Content -->
    <div class="main-w3layouts-form">
        <h2>
            <img src="" alt="logo"></h2>
        <!-- main-w3layouts-form -->
        <form id="loginForm" runat="server">
            <div class="fields-w3-agileits">
                <span class="fa fa-user" aria-hidden="true"></span>
                <asp:TextBox ID="txtUserName" runat="server" type="text" name="Username" required="" placeholder="Username" />
                <%--<asp:RequiredFieldValidator ID="rfvUser" ErrorMessage="Please enter Username" ControlToValidate="txtUserName" runat="server" />--%>
            </div>
            <div class="fields-w3-agileits">
                <span class="fa fa-key" aria-hidden="true"></span>
                <asp:TextBox ID="txtPassword" runat="server" type="password" name="Password" placeholder="Password" />
                <%--<asp:RequiredFieldValidator ID="rfvPWD" runat="server" ControlToValidate="txtPWD" ErrorMessage="Please enter Password" />--%>
            </div>
            <div class="remember-section-wthree">
                <ul>
                    <li>
                        <label class="anim">
                            <asp:CheckBox ID="chkRememberMe" class="checkbox" runat="server" type="checkbox" />
                            <span>Remember me ?</span>
                        </label>
                    </li>
                    <%--<li><a href="#" onserverclick="GuestLoginClick" runat="server">Login as guest</a> </li>--%>
                </ul>
                <div class="clear"></div>
            </div>
            <input id="login" type="submit" value="Admin" runat="server" onserverclick="adminLoginClick" form="loginForm" />
            <asp:Button ID="guestLogin" OnClick="GuestLoginClick" runat="server" Text="Guest" />
        </form>
        <!--// main-w3layouts-form -->
    </div>
    <!--// Main-Content-->
    <!-- copyright -->
    <div class="copyright-w3-agile">
        <p>&copy; 2017 Invoice Gen Login All Rights Reserved | Design by <a href="http://w3layouts.com/" target="_blank">W3layouts</a>			</p>
    </div>
    <!--// copyright -->
    <!--//background-->
</body>

</html>

