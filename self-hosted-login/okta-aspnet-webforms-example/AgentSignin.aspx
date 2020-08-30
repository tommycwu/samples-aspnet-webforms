<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgentSignin.aspx.cs" Inherits="okta_aspnet_webforms_example.WebForm2" %>
<!DOCTYPE html>
<script src="https://global.oktacdn.com/okta-signin-widget/3.2.1/js/okta-sign-in.min.js" type="text/javascript"></script>
<link href="https://global.oktacdn.com/okta-signin-widget/3.2.1/css/okta-sign-in.min.css" type="text/css" rel="stylesheet" />
<script src="Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="background2.css" />
</head>
<body>
<div id="okta-login-container"></div>
    <form id="form1" method="POST" action="AgentSignin.aspx" runat="server">
    <input type="hidden" name="sessionToken" id="hiddenSessionTokenField" />
        <div>
        </div>
    </form>
    <script type="text/javascript">

        var oktaDomain = '<%= System.Configuration.ConfigurationManager.AppSettings["okta:oktaDomain"].ToString() %>';

        var signIn = new OktaSignIn({
            baseUrl: oktaDomain
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
</body>
</html>
