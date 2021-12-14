<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="TotallyNotGuFundMe.Events" MasterPageFile="~/Site.Master"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Events</h1>
    <asp:Button class="btn btn-primary mb-2" runat="server" ID="newEventButton" Text="New Event" PostBackUrl="~/AuthPages/NewEvent.aspx"/>
    
    <asp:GridView runat="server"
                  ID="eventGrid"
                  ItemType="TotallyNotGuFundMe.Models.Event"
                  DataKeyNames="EventID"
                  AutoGenerateColumns="False"
                  CssClass="table"
                  >
        <HeaderStyle CssClass="thead-dark"/>
        <Columns>
            <asp:HyperLinkField DataTextField="Name" DataNavigateUrlFields="EventId" HeaderText="Name" 
                            InsertVisible="False" SortExpression="EventId"
                            DataNavigateUrlFormatString="viewEvent.aspx?eventId={0}"/>

            <asp:BoundField DataField="Description" HeaderText="Description" 
                            SortExpression="Description" />
            <asp:BoundField DataField="EventState" HeaderText="State" 
                            SortExpression="EventState" />
        </Columns>
    </asp:GridView>
</asp:Content>