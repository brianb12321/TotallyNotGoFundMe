using System;
using Microsoft.AspNet.Identity;
using TotallyNotGuFundMe.Models;

namespace TotallyNotGuFundMe.AuthPages
{
    public partial class NewEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void submitForm_Click(object sender, EventArgs e)
        {
            Validate();
            if (IsValid)
            {
                submitForm.Enabled = false;
                ApplicationDbContext context = new ApplicationDbContext();
                Event newEvent = new Event()
                {
                    Name = nameTextBox.Text,
                    Description = descriptionTextBox.Text,
                    ImageUrl = imageUrlTextBox.Text,
                    ExpectedAmount = decimal.Parse(expectedAmountTextBox.Text),
                    EventOwnerId = Context.User.Identity.GetUserId(),
                    EventState = EventState.Created
                };
                context.Events.Add(newEvent);
                try
                {
                    context.SaveChanges();
                    Response.Redirect($"~/ViewEvent.aspx?eventId={newEvent.EventId}");
                }
                catch (Exception exception)
                {
                    errorLabel.Text = exception.Message;
                    errorLabel.Visible = true;
                    submitForm.Enabled = true;
                }
            }
        }
    }
}