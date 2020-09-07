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
<div><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Img/alterra.png" OnClick="ImageButton1_Click" /></div>
<div id="okta-login-container" style="float: left; margin-left: 100px" ></div>
    <input type="hidden" name="sessionToken" id="hiddenSessionTokenField" />
        <div>
        </div>
    <script type="text/javascript">
        var oktaDomain = '<%= System.Configuration.ConfigurationManager.AppSettings["okta:oktaDomain"].ToString() %>';
        var agentIssuer = '<%= System.Configuration.ConfigurationManager.AppSettings["alterra:Issuer"].ToString() %>';
        var cId = '<%= System.Configuration.ConfigurationManager.AppSettings["alterra:ClientId"].ToString() %>';
        var redirUri = '<%= System.Configuration.ConfigurationManager.AppSettings["okta:RedirectUri"].ToString() %>';

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

        ////var signIn = new OktaSignIn({
        ////    baseUrl: oktaDomain,
        ////    el: '#okta-login-container',
        ////    authParams: {
        ////        issuer: agentIssuer
        ////    }
        ////});

        ////signIn.showSignInToGetTokens({
        ////    clientId: cId,

        ////    redirectUri: redirUri,
        ////    getAccessToken: true,
        ////    getIdToken: true,
        ////    scope: 'openid profile'
        ////});

    </script>
    </form>
</body>
</html>
