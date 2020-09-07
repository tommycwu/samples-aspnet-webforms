<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shoppertokens.aspx.cs" Inherits="okta_aspnet_webforms_example.WebForm6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="background4.css" />
    <link rel="stylesheet" type="text/css" href="gridview.css" />
    <link rel="stylesheet" type="text/css" href="Content/bootstrap.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
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
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <div>
                ID Token
            </div>
            <div>
                <asp:GridView runat="server" ID="GridViewID" OnRowDataBound="GridViewID_OnRowDataBound" CssClass="mGrid">
                </asp:GridView>
            </div>
            <div>
                Access Token
            </div>
            <div>
                <asp:GridView runat="server" ID="GridViewAccess" CssClass="mGrid">
                </asp:GridView>
            </div>

        </div>
    </form>
    </body>
</html>
