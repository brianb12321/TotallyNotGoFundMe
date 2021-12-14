<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="TotallyNotGuFundMe.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Contact Us!</h3>
    <address>
        133th Acme St.<br />
        Duluth, MN 55013-4924<br />
        <abbr title="Phone">P:</abbr>
        800.255.3700
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:support@totallynotgofundme.com">support@totallynotgofundme.com</a><br />
    </address>
</asp:Content>