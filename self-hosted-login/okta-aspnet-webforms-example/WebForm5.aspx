<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="WebForm5.aspx.cs" Inherits="okta_aspnet_webforms_example.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1">
        <div class="auto-style1">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Font-Size="Larger" Text="Scan your Ikon Pass to see how many days remain."></asp:Label>
            <br />
            <br />
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Img/19-20 season pass.png" OnClick="ImageButton1_Click" />
            <br />
            <br />
        </div>
        </div>
    </form>
</body>
</html>
