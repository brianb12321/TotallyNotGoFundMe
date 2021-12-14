using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using TotallyNotGuFundMe.Data;
using TotallyNotGuFundMe.Email;
using TotallyNotGuFundMe.Models;

namespace TotallyNotGuFundMe.AuthPages
{
    public partial class PayPledge : System.Web.UI.Page
    {
        public IEventDataService EventEDataService { get; set; }
        public ITransactionDataService TransactionDataService { get; set; }
        public IEmailService EmailService { get; set; }
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
            List<PledgeTransaction> transactionLog = new List<PledgeTransaction>();
            foreach (RepeaterItem item in pledgeRepeater.Items)
            {
                TextBox text = (TextBox)item.FindControl("payAmountTextBox");
                HiddenField pledgeIdHiddenField = (HiddenField) item.FindControl("pledgeIdHiddenField");

                //Would do some real processing, but for now, add the amount paid to the pledge.

                if (!string.IsNullOrWhiteSpace(text.Text))
                {
                    decimal paidAmount = decimal.Parse(text.Text);
                    if(paidAmount < 0) continue;

                    PledgeTransaction transaction = new PledgeTransaction()
                        { PledgeId = int.Parse(pledgeIdHiddenField.Value), TransactionAmount = paidAmount };

                    transactionLog.Add(transaction);
                    TransactionDataService.AddTransaction(transaction);
                }
            }

            if (transactionLog.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($@"
<p>
    Dear {User.Identity.Name}:<br/>
</p>
<p>Thank you for paying your pledges! This is a receipt confirmation of your payment:</p>
<ul>
");
                foreach (PledgeTransaction transaction in transactionLog)
                {
                    sb.AppendLine($"<li>Amount Paid: {transaction.TransactionAmount}</li>");
                }

                sb.AppendLine("</ul><br/>");
                sb.AppendLine($"<p>Thank you, the NotGoFundMe Team</p>");
                Task.Run(async () => await EmailService.SendEmailAsync(
                    await Context.GetOwinContext().GetUserManager<ApplicationUserManager>().GetEmailAsync(User.Identity.GetUserId()),
                    "You Paid Your Pledges!", sb.ToString(), sb.ToString()))
                    .GetAwaiter()
                    .GetResult();
            }
            Response.Redirect("PayPledgeSuccessful.aspx");
        }
    }
}