using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TotallyNotGuFundMe.Models;

namespace TotallyNotGuFundMe
{
    public partial class ViewEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string eventId = Request.QueryString["eventId"];
                if (eventId == null)
                {
                    eventNameLabel.Text = "The requested event no longer exists.";
                    return;
                }
                ApplicationDbContext context = new ApplicationDbContext();
                Event foundEvent = context.Events.Find(int.Parse(eventId));
                eventNameLabel.Text = foundEvent.Name;
            }
        }
    }
}