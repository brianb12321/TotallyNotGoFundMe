<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="TotallyNotGuFundMe.Events" MasterPageFile="~/Site.Master"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Events</h1>
    <asp:Button class="btn btn-primary" runat="server" ID="newEventButton" Text="New Event"/>
    
    <asp:GridView runat="server"
                  ID="eventGrid"
                  ItemType="TotallyNotGuFundMe.Models.Event"
                  DataKeyNames="EventID"
                  >

    </asp:GridView>
</asp:Content>