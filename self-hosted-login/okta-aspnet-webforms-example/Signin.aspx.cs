using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace okta_aspnet_webforms_example
#pragma warning disable SA1300 // Element should begin with upper-case letter
{
    public partial class Signin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.RequestType == "POST" && !Request.IsAuthenticated)
            {
                var sessionToken = Request.Form["sessionToken"]?.ToString();
                var properties = new AuthenticationProperties();
                properties.Dictionary.Add("sessionToken", sessionToken);
                properties.RedirectUri = "/";

                HttpContext.Current.GetOwinContext().Authentication.Challenge(
                        properties,
                        OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }
    }
}