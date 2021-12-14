<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PayPledge.aspx.cs" Inherits="TotallyNotGuFundMe.AuthPages.PayPledge" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row">
        <div class="col">
            <h1>
                <asp:Label runat="server" ID="headingLabel"></asp:Label>
            </h1>
        </div>
    </div>
    <div class="row">
        <div class="col">
            <asp:Repeater runat="server" ID="pledgeRepeater">
                <ItemTemplate>
                    <div class="card mb-3">
                        <div class="card-header">
                            Pledge
                        </div>
                        <div class="card-body">
                            <h5 class="card-title">Amount Unpaid: $<%# DataBinder.Eval(Container.DataItem, "AmountRemaining") %></h5>
                            <div class="form-group">
                                <asp:Label runat="server">Pay Amount</asp:Label>
                                <asp:TextBox runat="server" ID="payAmountTextBox" CssClass="form-control" TextMode="number" min="0" step="0.01"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="pledgeIdHiddenField" Value='<%# Eval("PledgeId") %>'/>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Button runat="server" ID="payPledgeButton" CssClass="btn btn-success" Text="Pay Pledges" OnClick="payPledgeButton_Click"/>
        </div>
        <div class="col">
            <div class="card text-right mb-3">
                <div class="card-header">Totals</div>
                <div class="card-body">
                    <ul>
                        <asp:Repeater runat="server" ID="totalsRepeater">
                            <ItemTemplate>
                                <li>
                                    <asp:Label runat="server">Pledge Amount: $<%# DataBinder.Eval(Container.DataItem, "PledgeAmount") %></asp:Label><br/>
                                    <asp:Label runat="server">Amount Remaining: $<%# DataBinder.Eval(Container.DataItem, "AmountRemaining") %></asp:Label>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <ul class="list-group list-group-flush">
                    <li class="list-group-item">
                        <h1>
                            <asp:Label runat="server" ID="grandTotalAmount"></asp:Label>
                        </h1>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
