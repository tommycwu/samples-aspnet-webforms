<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Callback.aspx.cs" Inherits="okta_aspnet_webforms_example.authorize.Callback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
.mGrid {
    width: 100%;
    background-color: #fff;
    margin: 5px 0 10px 0;
    border: solid 1px #525252;
    border-collapse: collapse;
    font-family: Calibri;
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
        .auto-style1 {
            border-collapse: collapse;
            background-color: transparent;
        }
        .auto-style2 {
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <script type="text/javascript">
            function JavaScriptFunction() {
                document.getElementById('<%= hdnResultValue.ClientID %>').value = document.URL;
            }
        </script>
        <asp:HiddenField ID="hdnResultValue" Value="0" runat="server" />
        <asp:Button ID="Button_Get" runat="server" Text="Show Token ID" OnClick="Button_Get_Click" OnClientClick="JavaScriptFunction();" Visible="True" />
        </div>
        <asp:GridView runat="server" ID="GridViewID" OnRowDataBound="GridViewID_OnRowDataBound" CssClass="mGrid"></asp:GridView>
    </form>
</body>
</html>
