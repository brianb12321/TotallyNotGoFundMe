using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using TotallyNotGuFundMe.Data;
using TotallyNotGuFundMe.Models;

namespace TotallyNotGuFundMe.AuthPages
{
    public partial class PayPledge : System.Web.UI.Page
    {
        public IEventDataService EventEDataService { get; set; }
        public ITransactionDataService TransactionDataService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string eventIdString = Request.QueryString["eventId"];
                if (eventIdString == null)
                {
                    headingLabel.Text = "An event ID must be specified.";
                    return;
                }
                int eventId = int.Parse(eventIdString);

                Event associatedEvent = EventEDataService.GetEventById(eventId);
                Pledge[] pledges =
                    TransactionDataService.GetUnfulfilledPledgesByUser(associatedEvent, Context.User.Identity.GetUserId())
                        .ToArray();

                headingLabel.Text = $"Pledges for \"{associatedEvent.Name}\"";
                grandTotalAmount.Text = $"Grand Total: {pledges.Sum(p => p.AmountRemaining):C}";
                pledgeRepeater.DataSource = pledges;
                pledgeRepeater.DataBind();

                totalsRepeater.DataSource = pledges;
                totalsRepeater.DataBind();

                if (pledges.Length == 0)
                {
                    payPledgeButton.Visible = false;
                }
            }
        }

        protected void payPledgeButton_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in pledgeRepeater.Items)
            {
                TextBox text = (TextBox)item.FindControl("payAmountTextBox");
                HiddenField pledgeIdHiddenField = (HiddenField) item.FindControl("pledgeIdHiddenField");

                //Would do some real processing, but for now, add the amount paid to the pledge.

                if (!string.IsNullOrWhiteSpace(text.Text))
                {
                    decimal paidAmount = decimal.Parse(text.Text);
                    PledgeTransaction transaction = new PledgeTransaction()
                        { PledgeId = int.Parse(pledgeIdHiddenField.Value), TransactionAmount = paidAmount };

                    TransactionDataService.AddTransaction(transaction);
                }
            }
            Response.Redirect("PayPledgeSuccessful.aspx");
        }
    }
}