<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewEvent.aspx.cs" Inherits="TotallyNotGuFundMe.ViewEvent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Modal -->
    <!-- https://getbootstrap.com/docs/4.0/components/modal/ -->
    <div class="modal fade" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="deleteModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Delete Event</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>Are you sure you want to delete this event? All associated pledges will be deleted and any payment will be refunded.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">No</button>
                    <asp:Button runat="server" ID="deleteEventButton" CssClass="btn btn-danger" Text="Delete" onClick="deleteEventButton_OnClick"/>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col">
            <h1>
                <asp:Label runat="server" ID="eventNameLabel"></asp:Label>
            </h1>
            <div runat="server" id="alertDiv" class="alert alert-<%: AlertDivType %>" role="alert" visible="false">
                <asp:Label runat="server" ID="alertMessageLabel"></asp:Label>
            </div>
            <div runat="server" ID="adminDiv" Visible="false">
                <% //https://getbootstrap.com/docs/4.0/components/dropdowns/ %>
                <div class="dropdown">
                    <button class="btn btn-secondary dropdown-toggle mb-2" type="button" id="administerEventButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        Administer Event
                    </button>
                    <div class="dropdown-menu" aria-labelledby="administerEventButton">
                        <asp:LinkButton runat="server" ID="editLinkButton" CssClass="dropdown-item" OnClick="editLinkButton_Click">Edit</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="beginEventLinkButton" Visible="false" CssClass="dropdown-item" OnClick="beginEventLinkButton_Click">Begin Event</asp:LinkButton>
                        <a class="dropdown-item" data-toggle="modal" data-target="#deleteModal">Delete Event</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col">
            <asp:Image runat="server" CssClass="img-fluid" ID="eventImage"/>
        </div>
        <div class="col">
            <h2>Donation Statistics</h2>
            <asp:Label runat="server" ID="donationAmount"></asp:Label>
            <div class="progress my-2">
                <div class="progress-bar" style="width: <%: ProgressAmount %>%"role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="<%: ProgressAmount %>"><%: ProgressAmount %>%</div>
            </div>
            <asp:Button runat="server" ID="makePledgeButton" CssClass="btn btn-success btn-lg btn-block" Text="Make Pledge!" OnClick="makePledgeButton_Click"/>
            <asp:Button runat="server" ID="payPledgeButton" Visible="false" CssClass="btn btn-primary btn-lg btn-block" Text="Pay Pledge!" OnClick="payPledgeButton_Click"/>
        </div>
    </div>
    <div class="row">
        <div class="col">
            <h2>Description</h2>
            <asp:Label runat="server" ID="descriptionLabel"></asp:Label>
        </div>
    </div>
    </div>
</asp:Content>