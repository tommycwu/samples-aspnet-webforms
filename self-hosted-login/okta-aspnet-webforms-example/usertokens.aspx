<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usertokens.aspx.cs" Inherits="okta_aspnet_webforms_example.WebForm9" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="gridview.css" />
    <link rel="stylesheet" type="text/css" href="Content/bootstrap.css" />

    <title></title>
    <style type="text/css">

table {
  background-color: transparent;
}
table {
  border-collapse: collapse;
  border-spacing: 0;
}
* {
  -webkit-box-sizing: border-box;
  -moz-box-sizing: border-box;
  box-sizing: border-box;
}
  *,
  *:before,
  *:after {
    color: #000 !important;
    text-shadow: none !important;
    background: transparent !important;
    -webkit-box-shadow: none !important;
    box-shadow: none !important;
  }
  th {
  text-align: left;
}
td,
th {
  padding: 0;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <div>
            <a href="Welcome.aspx">Home</a>&nbsp;|&nbsp;<asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton1_Click">Logout</asp:LinkButton>
        </div>
        <br />
        <br />
        <div>ID Token</div>
        <div>
            <asp:GridView runat="server" ID="GridViewID" OnRowDataBound="GridViewID_RowDataBound" CssClass="mGrid"></asp:GridView>
        </div>
        <br />
        <div>Access Token</div>
        <asp:GridView runat="server" ID="GridViewAccess" OnRowDataBound="GridViewAccess_RowDataBound" CssClass="mGrid"></asp:GridView>
    </form>
    </body>
</html>
