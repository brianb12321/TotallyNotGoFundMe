using System;
using Microsoft.AspNet.Identity;
using TotallyNotGuFundMe.Models;

namespace TotallyNotGuFundMe.AuthPages
{
    public partial class MakePledge : System.Web.UI.Page
    {
        private int eventId;
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
                context.Pledges.Add(new Pledge()
                {
                    EventId = eventId,
                    PledgeAmount = decimal.Parse(pledgeAmountTextBox.Text),
                    UserId = loggedOnUserId
                });
                context.SaveChanges();
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