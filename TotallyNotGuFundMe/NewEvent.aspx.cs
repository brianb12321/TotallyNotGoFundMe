using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TotallyNotGuFundMe.Models;

namespace TotallyNotGuFundMe
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
                    EventState = EventState.Created
                };
                context.Events.Add(newEvent);
                try
                {
                    context.SaveChanges();
                    Response.Redirect($"ViewEvent.aspx?eventId={newEvent.EventId}");
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