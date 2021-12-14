<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewEvent.aspx.cs" Inherits="TotallyNotGuFundMe.AuthPages.NewEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label runat="server" ID="errorLabel" Visible="False" CssClass="text-danger"></asp:Label>
    <h1>Create a New Event</h1>
    <div class="form-group">
        <asp:Label runat="server">Event Name</asp:Label>
        <asp:TextBox runat="server" ID="nameTextBox" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="nameTextBox" CssClass="text-danger" ErrorMessage="Event Name is required"></asp:RequiredFieldValidator>
    </div>
    <div class="form-group">
        <asp:label runat="server">Description</asp:label>
        <asp:TextBox runat="server" ID="descriptionTextBox" CssClass="form-control" TextMode="multiline"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="descriptionTextBox" CssClass="text-danger" ErrorMessage="Descripiton is required"></asp:RequiredFieldValidator>
    </div>
    <div class="form-group">
        <asp:label runat="server">Image URL</asp:label>
        <asp:TextBox runat="server" ID="imageUrlTextBox" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="imageUrlTextBox" CssClass="text-danger" ErrorMessage="Image Url is required"></asp:RequiredFieldValidator>
    </div>
    <div class="form-group">
        <asp:Button runat="server" class="btn btn-primary" ID="submitForm" Text="Create Event" OnClick="submitForm_Click" />
    </div>
</asp:Content>