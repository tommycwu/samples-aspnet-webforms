<%@ Page Title="Legacy Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="okta_aspnet_webforms_example.About"  Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>


    <table class="nav-justified">
        <tr>
            <td style="width: 109px">


    <asp:Label ID="Label1" runat="server" Text="Email: "></asp:Label>


            </td>
            <td>


    <asp:TextBox ID="TextBox1" runat="server" Height="21px"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td style="width: 109px">


    <asp:Label ID="Label3" runat="server" Text="Password: "></asp:Label>


            </td>
            <td>
    <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 109px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_ClickAsync" Text="Login" />
    <br />

    </asp:Content>
