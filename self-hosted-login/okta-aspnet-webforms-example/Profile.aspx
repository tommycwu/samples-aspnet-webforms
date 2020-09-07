<%@ Page Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="okta_aspnet_webforms_example.Profile" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>&nbsp;<br />
        <a href="AgentSignin.aspx">Agent Home</a>&nbsp;|&nbsp;<a href="WebForm7.aspx">Update User</a>&nbsp;|&nbsp;<a href="oktaAddPerson.html">Create User</a> |
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Logout</asp:LinkButton>
        
    </div>
    <h2>View Claims</h2>
    <asp:GridView runat="server" ID="GridViewClaims" OnRowDataBound="GridViewClaims_OnRowDataBound"></asp:GridView>
</asp:Content>
