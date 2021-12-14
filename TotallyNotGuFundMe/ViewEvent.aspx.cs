using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;
using Microsoft.AspNet.Identity;
using TotallyNotGuFundMe.Data;
using TotallyNotGuFundMe.Email;
using TotallyNotGuFundMe.Models;

namespace TotallyNotGuFundMe
{

    public partial class ViewEvent : System.Web.UI.Page
    {
        [Serializable]
        private class EventViewState
        {
            public int EventId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string UserId { get; set; }
            public decimal AmountDonated { get; set; }
            public decimal ExpectedAmount { get; set; }
            public string ImageUrl { get; set; }
            public EventState EventState { get; set; }
        }
        public int ProgressAmount = 0;
        private int eventId;

        public IEventDataService EventDataService { get; set; }
        public IEmailService EmailService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            EventViewState foundEvent = null;
            string eventIdString = Request.QueryString["eventId"];
            if (eventIdString == null)
            {
                eventNameLabel.Text = "The requested event no longer exists.";
                return;
            }

            eventId = int.Parse(eventIdString);
            if (!IsPostBack)
            {
                Event foundDatabaseEvent = EventDataService.GetEventById(eventId);
                eventNameLabel.Text = foundDatabaseEvent.Name;
                eventImage.ImageUrl = foundDatabaseEvent.ImageUrl;
                decimal amountDonated = foundDatabaseEvent.Pledges.Sum(pledge => pledge.PledgeAmount);
                foundEvent = new EventViewState()
                {
                    EventId = foundDatabaseEvent.EventId,
                    Name = foundDatabaseEvent.Name,
                    ImageUrl = foundDatabaseEvent.ImageUrl,
                    Description = foundDatabaseEvent.Description,
                    UserId = foundDatabaseEvent.EventOwnerId,
                    AmountDonated = amountDonated,
                    ExpectedAmount = foundDatabaseEvent.ExpectedAmount,
                    EventState = foundDatabaseEvent.EventState
                };
                ViewState["currentEvent"] = foundEvent;
            }
            else
            {
                foundEvent = (EventViewState) ViewState["currentEvent"];
            }

            if (Context.User != null && Context.User.Identity.IsAuthenticated)
            {
                payPledgeButton.Visible = true;
            }

            if((Context.User != null && Context.User.Identity.IsAuthenticated) && Context.User.Identity.GetUserId() == foundEvent.UserId)
            {
                adminDiv.Visible = true;
                if (foundEvent.EventState == EventState.Created)
                {
                    beginEventLinkButton.Visible = true;
                }

                else if (foundEvent.EventState == EventState.InProgress)
                {
                    finishEventLinkButton.Visible = true;
                }
                else if (foundEvent.EventState == EventState.Finished)
                {
                    editLinkButton.Visible = false;
                }
            }
            eventNameLabel.Text = foundEvent.Name;
            eventImage.ImageUrl = foundEvent.ImageUrl;
            ProgressAmount = Math.Min((int)((foundEvent.AmountDonated / foundEvent.ExpectedAmount) * 100), 100);
            donationAmount.Text = $"{foundEvent.AmountDonated:C} / {foundEvent.ExpectedAmount:C}";
            descriptionLabel.Text = foundEvent.Description;
        }

        protected void makePledgeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect($"AuthPages/MakePledge.aspx?eventId={eventId}");
        }

        protected void beginEventLinkButton_Click(object sender, EventArgs e)
        {
            EventViewState currentEvent = (EventViewState) ViewState["currentEvent"];
            try
            {
                var pledges = EventDataService.BeginEvent(currentEvent.EventId);

                //Send Email
                foreach (Pledge pledge in pledges.GroupBy(p => p.UserId).Select(p => p.First()))
                {
                    DonationUser user = pledge.User;
                    string message = $@"
<p>
    Dear {user.UserName},<br/>
</p>
<p>
    The administrator for the event '{currentEvent.Name}' made this event live!<br/>
</p>
<p>
From the Totally Not GoFundMe Team!
</p>
";
                    Task.Run(async () => await EmailService.SendEmailAsync(user.Email, $"Event {currentEvent.Name} Went Live!", message, message))
                        .GetAwaiter()
                        .GetResult();

                    alertMessageLabel.Text = "Your event successfully went live!";
                    alertDiv.Attributes.Add("class", "alert alert-success");
                    alertDiv.Visible = true;
                }
            }
            catch (Exception ex)
            {
                alertMessageLabel.Text = "An unexpected error occurred while processing your request. Please try again";
                alertDiv.Attributes.Add("class", "alert alert-danger");
                alertDiv.Visible = true;
            }
        }

        protected void payPledgeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect($"AuthPages/PayPledge.aspx?eventId={eventId}");
        }

        protected void editLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect($"AuthPages/EditEvent.aspx?eventId={eventId}");
        }

        protected void deleteEventButton_OnClick(object sender, EventArgs e)
        {
            EventDataService.DeleteEventById(eventId);
            Response.Redirect("Events.aspx");
        }

        protected void finishEventLinkButton_OnClick(object sender, EventArgs e)
        {
            EventViewState currentEvent = (EventViewState)ViewState["currentEvent"];
            try
            {
                var pledges = EventDataService.FinishEvent(currentEvent.EventId);

                //Send Email
                foreach (Pledge pledge in pledges.GroupBy(p => p.UserId).Select(p => p.First()))
                {
                    DonationUser user = pledge.User;
                    Uri payPledgeUri = new Uri(Request.Url,
                        $"/AuthPages/PayPledge.aspx?eventId={currentEvent.EventId}");
                    string message = $@"
<p>
    Dear {user.UserName},<br/>
</p>
<p>
    The event '{currentEvent.Name}' is now finished.
</p>
<p>
    Please pay your pledge(s) <a href='{payPledgeUri}'>here</a>
</p>
<p>
From the Totally Not GoFundMe Team!
</p>
";
                    Task.Run(async () => await EmailService.SendEmailAsync(user.Email, $"Event {currentEvent.Name} Finished!", message, message))
                        .GetAwaiter()
                        .GetResult();

                    alertMessageLabel.Text = "Your event is now finished. You will not be able to edit this event anymore.";
                    alertDiv.Attributes.Add("class", "alert alert-success");
                    alertDiv.Visible = true;
                }
            }
            catch (Exception ex)
            {
                alertMessageLabel.Text = "An unexpected error occurred while processing your request. Please try again";
                alertDiv.Attributes.Add("class", "alert alert-danger");
                alertDiv.Visible = true;
            }
        }
    }
}