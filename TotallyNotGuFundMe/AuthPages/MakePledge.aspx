<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MakePledge.aspx.cs" Inherits="TotallyNotGuFundMe.AuthPages.MakePledge" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col">
            <h1>Make Pledge</h1>
            <div runat="server" id="alertDiv" class="alert alert-danger" role="alert">
                <asp:Label runat="server" ID="errorMessageLabel"></asp:Label>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col">
            <h2>Pledge Details</h2>
            <p>Thank you for making a pledge! Your contribution will makes a big difference!</p>
            <div class="form-group">
                <asp:Label runat="server">Pledge Amount</asp:Label>
                <asp:TextBox runat="server" ID="pledgeAmountTextBox" CssClass="form-control" TextMode="Number" min="0" step="0.01"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="pledgeAmountTextBox" CssClass="text-danger" ErrorMessage="You must specify a pledge amount"></asp:RequiredFieldValidator>
                <asp:RangeValidator runat="server" Display="Dynamic" ControlToValidate="pledgeAmountTextBox" MinimumValue="1" MaximumValue="100000" CssClass="text-danger" ErrorMessage="Pledge amount must be between $1 to $100,000"></asp:RangeValidator>
            </div>
            <asp:Button runat="server" CssClass="btn btn-success" ID="makePledgeButton" Text="Make Pledge" OnClick="makePledgeButton_Click"/>
        </div>
        <div class="col">
            <h2>
                <asp:Label runat="server" ID="eventNameLabel"></asp:Label>
            </h2>
            <asp:Label runat="server" ID="eventDescriptionLabel"></asp:Label>
        </div>
    </div>
</asp:Content>