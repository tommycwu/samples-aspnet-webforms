<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentSignin.aspx.cs" Inherits="okta_aspnet_webforms_example.WebForm2" %>
<!DOCTYPE html>
<script src="https://global.oktacdn.com/okta-signin-widget/3.2.1/js/okta-sign-in.min.js" type="text/javascript"></script>
<link href="https://global.oktacdn.com/okta-signin-widget/3.2.1/css/okta-sign-in.min.css" type="text/css" rel="stylesheet" />
<script src="Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="background3.css" />
</head>
<body>
<form id="form2" runat="server">
<div></div>
<div id="okta-login-container" style="float: left; margin-left: 30px; margin-top: 200px" ></div>
    <input type="hidden" name="sessionToken" id="hiddenSessionTokenField" />
        <div>
        </div>
    <script type="text/javascript">

        var signIn = new OktaSignIn({
            baseUrl: '<%= System.Configuration.ConfigurationManager.AppSettings["auth:oktaDomain"].ToString() %>'
        });

        signIn.renderEl({ el: '#okta-login-container' }, (res) => {
            var sessionTokenField = $("#hiddenSessionTokenField");
            sessionTokenField.val(res.session.token);
            var form = sessionTokenField.parent();
            form.submit();
        }, (err) => {
            console.error(err);
        });

    </script>
    </form>
</body>
</html>
