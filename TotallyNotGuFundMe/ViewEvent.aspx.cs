﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using TotallyNotGuFundMe.Models;

namespace TotallyNotGuFundMe
{
    public partial class ViewEvent : System.Web.UI.Page
    {
        public int ProgressAmount = 0;
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
                Event foundEvent = context.Events
                    .Where(evt => evt.EventId == eventId)
                    .Include(evt => evt.Pledges)
                    .First();

                ViewState["currentEvent"] = foundEvent;

                if (Context.User != null && Context.User.Identity.GetUserId() == foundEvent.EventOwnerId)
                {
                    adminDiv.Visible = true;
                }
                eventNameLabel.Text = foundEvent.Name;
                eventImage.ImageUrl = foundEvent.ImageUrl;
                decimal amountDonated = foundEvent.Pledges.Sum(pledge => pledge.PledgeAmount);
                ProgressAmount = (int)((amountDonated / foundEvent.ExpectedAmount) * 100);
                donationAmount.Text = $"{amountDonated:C} / {foundEvent.ExpectedAmount:C}";
                descriptionLabel.Text = foundEvent.Description;
            }
        }

        protected void makePledgeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect($"AuthPages/MakePledge.aspx?eventId={eventId}");
        }

        protected void beginEventLinkButton_Click(object sender, EventArgs e)
        {
            Event currentEvent = (Event) ViewState["currentEvent"];

        }
    }
}