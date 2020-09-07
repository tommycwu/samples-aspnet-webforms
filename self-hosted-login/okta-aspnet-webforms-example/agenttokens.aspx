<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agenttokens.aspx.cs" Inherits="okta_aspnet_webforms_example.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="Content/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="gridview.css" />
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
            <a href="AgentSignin.aspx">Agent Home</a>&nbsp;|&nbsp;<a href="UpdateUser.aspx">Update User</a>&nbsp;|&nbsp;<a href="oktaAddPerson.html">Create User</a>&nbsp;|&nbsp;<asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton1_Click">Logout</asp:LinkButton>
        </div>
        <br />
        <br />
        <div>ID Token</div>
        <div>
            <asp:GridView runat="server" ID="GridViewID" OnRowDataBound="GridViewID_OnRowDataBound" CssClass="mGrid"></asp:GridView>
        </div>
        <br />
        <div>Access Token</div>
        <div>
            <asp:GridView runat="server" ID="GridViewAccess" OnRowDataBound="GridViewAccess_RowDataBound" CssClass="mGrid"></asp:GridView>
        </div>
    </form>
</body>
</html>
