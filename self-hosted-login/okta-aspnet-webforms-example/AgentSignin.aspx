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
            baseUrl: '<%= System.Configuration.ConfigurationManager.AppSettings["auth:oktaDomain"].ToString() %>',
            customButtons: [{
                title: 'Sign in with Google',
                className: 'social-auth-button social-auth-google-button link-button',
                click: function () {
                    window.location.href = 'https://twu.oktapreview.com/oauth2/v1/authorize?idp=0oau0jrldpmnd6z2s0h7&client_id=0oatxinjpsnnXRTia0h7&response_type=id_token&response_mode=fragment&scope=openid%20email&redirect_uri=http%3A%2F%2Flocalhost%3A8080%2Fauthorize%2Fcallback&state=state&nonce=nonce';
                }
            }]
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
