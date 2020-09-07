using System;
using System.Web;
using System.Web.UI;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace okta_aspnet_webforms_example
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton12.Visible = false;
            Response.Redirect("Login.aspx");
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            if (ImageButton12.Visible)
            {
                ImageButton12.Visible = false;
            }
            else
            {
                ImageButton12.Visible = true;
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton12.Visible = false;
            if (!Request.IsAuthenticated)
            {
                HttpContext.Current.GetOwinContext().Authentication.Challenge(
                  new AuthenticationProperties { RedirectUri = "/shoppertokens.aspx" },
                  OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton12.Visible = false;
            Response.Redirect("IkonPass.aspx");
        }

        protected void ImageButton12_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton12.Visible = false;
            Response.Redirect("AgentSignin.aspx");
        }
    }
}