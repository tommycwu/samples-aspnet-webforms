<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="okta_aspnet_webforms_example.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="background1.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Img/menu.png" OnClick="ImageButton4_Click"/>
            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Img/ikon.png" OnClick="ImageButton5_Click"/>
            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Img/search.png" ImageAlign="Right" OnClick="ImageButton2_Click" /> 
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Img/login.png" ImageAlign="Right" OnClick="ImageButton1_Click" /> 
            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Img/usd.png" ImageAlign="Right"/>
        </div>
        <div>
            <asp:ImageButton ID="ImageButton12" runat="server" Width="317px" Height="490px" ImageUrl="~/Img/agent.png" OnClick="ImageButton12_Click" Visible="False" />
        </div>
    </form>
</body>
</html>
