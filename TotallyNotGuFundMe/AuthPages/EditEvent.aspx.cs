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
    public partial class EditEvent : System.Web.UI.Page
    {
        private int eventId;
        public IEventDataService EventDataService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string eventIdString = Request.QueryString["eventId"];
            eventId = int.Parse(eventIdString);
            if (!IsPostBack)
            {
                Event eventObj = EventDataService.GetEventById(eventId);
                if (eventObj.EventOwnerId != Context.User.Identity.GetUserId())
                {
                    submitForm.Visible = false;
                    nameTextBox.Visible = false;
                    descriptionTextBox.Visible = false;
                    imageUrlTextBox.Visible = false;
                    headerLabel.InnerText = "Access Denied: You do not own or have permission to view this event.";
                    return;
                }

                nameTextBox.Text = eventObj.Name;
                descriptionTextBox.Text = eventObj.Description;
                imageUrlTextBox.Text = eventObj.ImageUrl;
            }
        }

        protected void submitForm_Click(object sender, EventArgs e)
        {
            Validate();
            if (IsValid)
            {
                Event eventObj = EventDataService.GetEventById(eventId);
                eventObj.Name = nameTextBox.Text;
                eventObj.Description = descriptionTextBox.Text;
                eventObj.ImageUrl = imageUrlTextBox.Text;
                EventDataService.Update();
                Response.Redirect($"~/ViewEvent.aspx?eventId={eventId}");
            }
        }
    }
}