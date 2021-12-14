using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using TotallyNotGuFundMe.Email;
using TotallyNotGuFundMe.Models;

namespace TotallyNotGuFundMe.AuthPages
{
    public partial class MakePledge : System.Web.UI.Page
    {
        private int eventId;
        public IEmailService EmailService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            string eventIdString = Request.QueryString["eventId"];
            if (eventIdString == null)
            {
                eventNameLabel.Text = "The requested event no longer exists.";
                return;
            }

            eventId = int.Parse(eventIdString);
            if (!IsPostBack)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                Event foundEvent = context.Events.Find(eventId);
                eventNameLabel.Text = foundEvent.Name;
                eventDescriptionLabel.Text = foundEvent.Name;
                alertDiv.Visible = false;
            }
        }

        protected void makePledgeButton_Click(object sender, EventArgs e)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            try
            {
                string loggedOnUserId = Context.User.Identity.GetUserId();
                Pledge pledge = new Pledge()
                {
                    EventId = eventId,
                    PledgeAmount = decimal.Parse(pledgeAmountTextBox.Text),
                    UserId = loggedOnUserId
                };
                context.Pledges.Add(pledge);
                Task asyncTask = Task.Run(async () =>
                {
                    await context.SaveChangesAsync();

                    DonationUser user = await Context.GetOwinContext().GetUserManager<ApplicationUserManager>()
                        .FindByIdAsync(loggedOnUserId);

                    string emailMessage = $@"
<p>
    Dear {user.UserName}:
</p>
<p>
    This is a receipt of your pledge donation you made to the '{eventNameLabel.Text}' event. You will not be charged for this pledge until the event is finished.
    You will receive another email when pledges are due for payment.
</p>
<br/>
<p>
    Pledge Amount: ${pledge.PledgeAmount}
</p>

<p>
    Thank you,<br/>
    The Totally Not GoFundMe Team
</p>
";
                    await EmailService.SendEmailAsync(user.Email, $"Thanks for making a pledge!", emailMessage,
                        emailMessage);
                });
                asyncTask.GetAwaiter().GetResult();
                Response.Redirect("MakePledgeSuccessful.aspx");
            }
            catch (Exception ex)
            {
                alertDiv.Visible = true;
                errorMessageLabel.Text = ex.InnerException?.Message ?? ex.Message;
            }
            finally
            {
                context.Dispose();
            }
        }
    }
}