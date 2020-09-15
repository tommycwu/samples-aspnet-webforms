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
            <a href="agentsignin.aspx">Home</a>
        </div>
        <br />
        <div class="text-center"></div>
        <br />
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Img/logo_GetMyHealthData2.png" OnClick="ImageButton1_ClickAsync" CssClass="center-block" BorderWidth="5px" Enabled="False" />
        <div class="text-center">
        <br />
            <asp:Label ID="Label1" runat="server" Font-Size="Larger" Text="Click on the button above to get your health data."></asp:Label>
        <br />
        </div>

        <br />
        <div>
            <asp:Label ID="Label4" runat="server" Text="Id Token" Visible="False"></asp:Label>
        </div>
        <div>
            <div><asp:GridView runat="server" ID="GridViewID" OnRowDataBound="GridViewID_OnRowDataBound" CssClass="mGrid"></asp:GridView></div>
        </div>
        <br />

        <div>
            <asp:Label ID="Label3" runat="server" Text="Access Token" Visible="False"></asp:Label>
        </div>
        <div>
            <div><asp:GridView runat="server" ID="GridViewAccess" CssClass="mGrid" OnRowDataBound="GridViewAccess_RowDataBound"></asp:GridView></div>
        </div>
    </form>
</body>
</html>
