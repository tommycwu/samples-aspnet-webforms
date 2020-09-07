﻿using Microsoft.Owin.Security.Cookies;
using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace okta_aspnet_webforms_example
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                GridViewClaims.DataSource = HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => new { Name = x.Type, Value = x.Value });
                GridViewClaims.DataBind();
            }
            else
            {
                var mainContent = (ContentPlaceHolder)Page.Form.FindControl("MainContent");
                mainContent.Controls.Add(new Label
                {
                    Text = "You are not authenticated!",
                });
            }
        }

        protected void GridViewClaims_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in GridViewClaims.Rows)
            {
                row.Cells[1].Attributes.Add("id", $"claim-{row.Cells[0].Text}");
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                HttpContext.Current.GetOwinContext().Authentication.SignOut(
                    CookieAuthenticationDefaults.AuthenticationType);
            }
        }
    }
}
