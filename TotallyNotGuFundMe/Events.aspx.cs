using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TotallyNotGuFundMe.Models;

namespace TotallyNotGuFundMe
{
    public partial class Events : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            context.Events.Load();
            eventGrid.DataSource = context.Events.Local;
            eventGrid.DataBind();
        }
    }
}