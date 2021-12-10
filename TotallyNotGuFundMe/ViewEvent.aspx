<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewEvent.aspx.cs" Inherits="TotallyNotGuFundMe.ViewEvent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        <asp:Label runat="server" ID="eventNameLabel"></asp:Label>
    </h1>
    <div class="row">
        <div class="col">
            <asp:Image runat="server" CssClass="img-fluid" ID="eventImage"/>
        </div>
        <div class="col">
            <p>Test</p>
        </div>
    </div>
</asp:Content>