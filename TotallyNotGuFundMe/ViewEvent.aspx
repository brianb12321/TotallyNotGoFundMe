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
            <h2>Donation Statistics</h2>
            <asp:Label runat="server" ID="donationAmount"></asp:Label>
            <div class="progress my-2">
                <div class="progress-bar" style="width: <%: progressAmount %>%"role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="<%: progressAmount %>"><%: progressAmount %>%</div>
            </div>
            <asp:Button runat="server" ID="makePledgeButton" CssClass="btn btn-success btn-lg btn-block" Text="Make Pledge!" OnClick="makePledgeButton_Click"/>
        </div>
    </div>
    <div class="row">
        <div class="col">
            <h2>Description</h2>
            <asp:Label runat="server" ID="descriptionLabel"></asp:Label>
        </div>
    </div>
</asp:Content>