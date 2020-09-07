<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="IkonPass.aspx.cs" Inherits="okta_aspnet_webforms_example.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="gridview.css" />
    <link rel="stylesheet" type="text/css" href="Content/bootstrap.css" />
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <div>
            &nbsp;<a href="Welcome.aspx">Home</a>
        </div>
        <br />
        <div class="text-center"><asp:Label ID="Label1" runat="server" Font-Size="Larger" Text="Scan your Ikon Pass to see how many days remain."></asp:Label></div>
        <br />
        <br />
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Img/19-20 season pass.png" OnClick="ImageButton1_ClickAsync" CssClass="center-block" />
        <br />
        <br />
        <div>
            <asp:Label ID="Label3" runat="server" Text="Access Token" Visible="False"></asp:Label>
        </div>
        <div>
            <asp:GridView runat="server" ID="GridViewAccess" CssClass="mGrid"></asp:GridView>
        </div>
    </form>
</body>
</html>
